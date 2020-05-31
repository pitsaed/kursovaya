using System;
using System.Net;

namespace _4sem_kurs_client
{
    class Program
    {
        // адрес и порт сервера, к которому будем подключаться
        static int port = 8005; // порт сервера
        static string address = "127.0.0.1"; // адрес сервера
        static void Main(string[] args)
        {
            Manager manager = new Manager();
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
                Selector selector = new TopMenuSelector(); 
                bool is_continue = true;

                while (is_continue)
                {
                    Console.Clear();
                    try 
                    {

                        selector.Select(ref selector, manager, ref is_continue);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
        
    }
}

