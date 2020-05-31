using System;
using System.Collections.Generic;

namespace _4sem_kurs_client
{
    public class ExpertMenuSelector : Selector
    {
        List<Point> points = new List<Point>() { new Point(0, "Подтвердить постройку"), new Point(1, "Посмотреть спсиок застроек"), new Point(2, "Сортировка"), new Point(3, "Поиск"), new Point(4, "Выход") };
        public override void Select(ref Selector selector, Manager manager, ref bool is_continue)
        {
            Menu menu = new Menu(points);
            menu.Dispaly();
            Console.Write("Сделайте выбор:");
            switch ((ExpertMenuPoints)menu.GetSelection())
            {
                case ExpertMenuPoints.Answer:
                    {
                        manager.ExpertAnswer();
                        break;

                    }
                case ExpertMenuPoints.Watch:
                    {
                        manager.Show();
                        break;
                    }
                
                case ExpertMenuPoints.Sort:
                    {
                        manager.Sort();
                        break;
                    }
                case ExpertMenuPoints.Search:
                    {
                        manager.Search();
                        break;
                    }
                case ExpertMenuPoints.Exit:
                    {
                        
                        selector = new TopMenuSelector();
                        break;
                    }
            }
        }
    }
}