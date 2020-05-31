using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace _4sem_kurs
{
    using Enums;
    class Program
    {
        static int port = 8005; // порт для приема входящих запросов
        static void Main(string[] args)
        {

            Database database = new Database();
            database.LoadUsers();
            database.LoadPowerBlocks();
            database.LoadPlaces();
            database.LoadRequests();

            // получаем адреса для запуска сокета
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
             
            // создаем сокет
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // связываем сокет с локальной точкой, по которой будем принимать данные
                listenSocket.Bind(ipPoint);
 
                // начинаем прослушивание
                listenSocket.Listen(10);
 
                Console.WriteLine("Сервер запущен. Ожидание подключений...");
 
                Socket handler = listenSocket.Accept();
                Console.WriteLine("Пользватель вошел в систему");
                    // получаем сообщение

                    byte[] data = new byte[256];
                   
                    
                    int bytes = 0; // количество полученных байтов
                    data = ObjectToByteArray(database); // буфер для получаемых данных
                    bytes = data.Length;
                    handler.Send(ObjectToByteArray(bytes), 0);
                    
                    
                    handler.Send(data, 0);
                do
                {
                    byte[] length = new byte[256];
                    handler.Receive(length, 0);
                    data = new byte[(int)ByteArrayToObject(length)];
                    handler.Receive(data, 0);
                }
                while (handler.Available > 0);
                Database database1 = new Database();
                database1 = (Database)ByteArrayToObject(data);
                database1.Save();
                    // закрываем сокет
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
                    
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static Object ByteArrayToObject(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }
        public static byte[] ObjectToByteArray(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}
