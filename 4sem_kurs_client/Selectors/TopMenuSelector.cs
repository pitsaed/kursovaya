using System;
using System.Collections.Generic;

namespace _4sem_kurs_client
{
    public class TopMenuSelector : Selector
    {
        List<Point> points = new List<Point>() { new Point(0, "Авторизация"), new Point(1, "Выход") };
        public override void Select(ref Selector selector, Manager manager, ref bool is_continue)
        {
            Menu menu = new Menu(points);
            menu.Dispaly();
            Console.Write("Введите сообщение:");
            switch (menu.GetSelection())
            {
                case (int)TopMenuPoints.Authorization:
                    {
                        selector = manager.Authorization();
                        break;
                        
                    }
                
                case (int)TopMenuPoints.Exit:
                    {
                        //manager.Send();
                        is_continue = false;
                        break;
                    }
                
            }

        }
    }
}
