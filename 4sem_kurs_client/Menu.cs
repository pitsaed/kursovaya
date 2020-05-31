using System;
using System.Collections.Generic;
using System.Linq;

namespace _4sem_kurs_client
{
    public class Menu
    {
        public List<Point> Points { get; set; }
        public Menu(List<Point> points)
        {
            if (points == null)
            {
                throw new ArgumentNullException();
            }
            Points = points;
        }
        public void Dispaly()
        {
            Points.OrderBy(x => x.Number);
            foreach (var item in Points)
            {
                Console.WriteLine($"{item.Number} {item.Name}");

            }
        }

        public int GetSelection()
        {
            var choose = Convert.ToInt32(Console.ReadLine());
            var item = Points.Where(x => x.Number == choose).First();
            return item.Number;
        }


    }
}
