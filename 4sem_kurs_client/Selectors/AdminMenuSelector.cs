using System;
using System.Collections.Generic;

namespace _4sem_kurs_client
{
    public class AdminMenuSelector : Selector
    {
        List<Point> points = new List<Point>() { new Point(0, "Добавить план постройки"), new Point(1, "Удалить из плана постройки"), new Point(2, "Редактировать план постройки"), new Point(3, "Посмотреть спсиок застроек"), new Point(4, "Сортировка"), new Point(5, "Поиск"), new Point(6, "Выход") };
        public override void Select(ref Selector selector, Manager manager, ref bool is_continue)
        {
            Menu menu = new Menu(points);
            menu.Dispaly();
            Console.Write("Сделайте выбор:");
            switch ((AdminMenuPoints)menu.GetSelection())
            {
                case AdminMenuPoints.Add:
                    {
                        manager.Add();
                        break;

                    }
                case AdminMenuPoints.Delete:
                    {
                        manager.Delete();
                        break;
                    }
                case AdminMenuPoints.Edit:
                    {
                        manager.Edit();
                        break;
                    }
                case AdminMenuPoints.Watch:
                    {
                        manager.Show();
                        break;
                    }
                case AdminMenuPoints.Search:
                    {
                        manager.Search();
                        break;
                    }
                case AdminMenuPoints.Sort:
                    {
                        manager.Sort();
                        break;
                    }
                case AdminMenuPoints.Exit:
                    {
                        selector = new TopMenuSelector();
                        break;
                    }
            }
        }
    }
}
