using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace _4sem_kurs_client
{
    using _4sem_kurs;
    using _4sem_kurs.Enums;
    public class Manager
    {
        static int port = 8005; // порт сервера
        static string address = "127.0.0.1"; // адрес сервера
        Socket socket;
        Database _database;
        public Manager()
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // подключаемся к удаленному хосту
            socket.Connect(ipPoint);
            byte[] length = new byte[256];
            socket.Receive(length, 0);
            byte[] data = new byte[(int)ObjectConverter.ByteArrayToObject(length)];
            socket.Receive(data, 0);
            _database = (Database)ObjectConverter.ByteArrayToObject(data);
        }
        public Selector Authorization()
        {
            string login;
            string password = "";
            Console.WriteLine("Логин: ");
            login = Console.ReadLine();
            Console.WriteLine("Пароль: ");
            ConsoleKeyInfo key;

            while (true)
            {
                key = Console.ReadKey(true);

                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    Console.Write("\b");
                    Console.Write(" ");
                    Console.Write("\b");
                }
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
            }
            User user = new User(login, password);
            foreach (var item in _database.Users)
            {
                if (item.Equals(user))
                {
                    if (item.UserRole == UserRole.Admin)
                    {
                        return new AdminMenuSelector();
                    }

                    if (item.UserRole == UserRole.Expert)
                    {
                        return new ExpertMenuSelector();
                    }
                }
            }
            Console.WriteLine("Не удалось найти пользователя");
            return new TopMenuSelector();
        }

        public Selector Add()
        {
            Console.Clear();
            ElectricalStation station = InputStation();
            Place place = InputPlace();
            PowerBlock block = InputBlock();
            Request request = new Request(station, place, block);
            Console.WriteLine(request);
            _database.Requests.Add(request);
            Console.Read();
            return new AdminMenuSelector();
        }

        public Selector Delete()
        {
            Console.Clear();
            Request request = ChooseRequest();
            _database.Requests.Remove(request);
            Show();
            Console.Read();
            return new AdminMenuSelector();
        }

        public void Show()
        {
            Console.Clear();
            foreach (var item in _database.Requests)
            {
                Console.WriteLine("------------------------------------------------------------------------------------");
                Console.Write(item);
                Console.WriteLine("------------------------------------------------------------------------------------");
            }
            Console.Read();
        }
        public Selector Edit()
        {
            Console.Clear();
            Console.WriteLine("Выберите станцию для изменения: ");
            Request request = ChooseRequest();
            Console.WriteLine("Выберите что вы хотите изменить: ");
            Menu menu = new Menu(new List<Point> { new Point(0, "Станция"), new Point(1, "Место"), new Point (2, "Энергоблок")});
            menu.Dispaly();
            Request result_request = new Request();
            switch (menu.GetSelection())
            {
                case 0:
                    {
                        result_request.Station = InputStation();
                        result_request.Place = request.Place;
                        result_request.Block = request.Block;
                        break;
                    }
                case 1:
                    {
                        result_request.Station = request.Station;
                        result_request.Place = InputPlace();
                        result_request.Block = request.Block;
                        break;
                    }
                case 2:
                    {
                        result_request.Station = request.Station;
                        result_request.Place = request.Place;
                        result_request.Block = InputBlock();
                        break;
                    }
            }
            List<Request> result_requests = new List<Request>();
            foreach (var item in _database.Requests)
            {
                if (item.Equals(request))
                {
                    result_requests.Add(result_request);
                }
                else
                {
                    result_requests.Add(item);
                }
            }
            _database.Requests = result_requests;
            Console.Read();
            return new AdminMenuSelector();
        }

        public Selector ExpertAnswer()
        {
            Console.Clear();
            Request request = ChooseRequest();
            Console.WriteLine("Одобрить или изменить запрос?");
            Menu menu = new Menu(new List<Point> { new Point(0, "Одобрить"), new Point(1, "Изменить") });
            menu.Dispaly();
            switch (menu.GetSelection())
            {
                case 0:
                    {
                        _database.Requests.Remove(request);
                        break;
                    }
                case 1:
                    {
                        break;
                    }
            }

            return new ExpertMenuSelector();
        }
        public void Sort()
        {
            Console.Clear();
            Menu menu = new Menu(new List<Point> { new Point(0, "Сортировать по цене"), new Point(1, "Сортировать по площади"), new Point(2, "Сортировать по улице") });
            menu.Dispaly();
            IComparer<Request> comparer = null;
            switch(menu.GetSelection())
            {
                case 0:
                    {
                        comparer = new PriceComparer();
                        break;
                    }
                case 1:
                    {
                        comparer = new SquareComparer();
                        break;
                    }
                case 2:
                    {
                        comparer = new StreetComparer();
                        break;
                    }
            }
            _database.Requests.Sort(comparer);
            Show();
            Console.Read();
        }

        public void Search()
        {
            Console.Clear();
            Menu menu = new Menu(new List<Point> { new Point(0, "Станция"), new Point(1, "Месту") });
            menu.Dispaly();
            switch (menu.GetSelection())
            {
                case 0:
                    {
                        ElectricalStation station = InputStation();
                        var result = _database.Requests.Where(x => x.Station.Name == station.Name);
                        if (result.Count() == 0)
                        {
                            Console.WriteLine("Не удалось найти запрос");
                        }
                        foreach (var item in result)
                        {
                            Console.WriteLine(item);
                        }
                        
                        break;
                    }
                case 1:
                    {
                        Console.Write("Введите улицу: ");
                        string street = Console.ReadLine();
                        var result = _database.Requests.Where(x => x.Place.Street == street);
                        if (result.Count() == 0)
                        {
                            Console.WriteLine("Не удалось найти запрос");
                        }
                        foreach(var item in result)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    }
            }
                    Console.Read();
        }

        

        ~Manager()
        {
            Send();
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

        private ElectricalStation InputStation()
        {
            List<Point> points_station = new List<Point> { new Point(0, "ТЭС"), new Point(1, "АЭС") };
            Menu menu = new Menu(points_station);

            Console.WriteLine("Выберите тип электростанции: ");
            menu.Dispaly();
            switch (menu.GetSelection())
            {
                case 0:
                    {
                        return new HeatElectricalStation();
                        
                    }
                case 1:
                    {
                        return new NuclearElectricalStation();
                        
                    }
            }
            throw new Exception();
        }
        private Place InputPlace()
        {
            Console.WriteLine("Выберите место: ");
            int i = 0;
            List<Place> reqPlaces = new List<Place>();

            foreach (var item in _database.Requests)
            {
                reqPlaces.Add(item.Place);
            }
            List<Place> places = new List<Place>();
            places = _database.Places.Except(reqPlaces).ToList();
            i = 0;
            foreach(var item in places)
            {
                Console.WriteLine($"{i++} \n{item}\n");
            }
            int result = Convert.ToInt32(Console.ReadLine());
            i = 0;
            foreach(var item in places)
            {
                if (i++ == result)
                {
                    return item;
                }
            }
            throw new Exception();

        }
        private PowerBlock InputBlock()
        {
            Console.WriteLine("Выберите блок: ");
            int i = 0;
            foreach (var item in _database.PowerBlocks)
            {
                Console.WriteLine($"{i++} \n{item}\n");
            }
            int result = Convert.ToInt32(Console.ReadLine());
            i = 0;
            foreach (var item in _database.PowerBlocks)
            {
                if (i++ == result)
                {
                    return item;
                }
            }
            throw new Exception();
        }
        private Request ChooseRequest()
        {
            List<Point> points = new List<Point>();
            int i = 0;
            foreach (var item in _database.Requests)
            {
                points.Add(new Point(i++, item.ToString()));
            }
            Menu menu = new Menu(points);
            menu.Dispaly();
            int result = menu.GetSelection();
            i = 0;
            Request request = new Request();
            foreach (var item in _database.Requests)
            {
                if (i++ == result)
                {
                    request = item;
                }
            }
            return request;
        }
        public void Send()
        {
            byte[] data = new byte[256];
            int bytes = 0; // количество полученных байтов
            data = ObjectConverter.ObjectToByteArray(_database); // буфер для получаемых данных
            bytes = data.Length;
            socket.Send(ObjectConverter.ObjectToByteArray(bytes), 0);
            socket.Send(data, 0);
        }
    }
}
