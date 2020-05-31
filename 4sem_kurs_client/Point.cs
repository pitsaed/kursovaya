using System;

namespace _4sem_kurs_client
{
    public class Point
    {
        public string Name { get; set; }
        public int Number { get; set; }

        public Point(int number, string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException();
            }
            Name = name;
            Number = number;
        }
        public override bool Equals(object obj)
        {
            if (obj is Point)
            {
                Point point = obj as Point;
                return Name == point.Name && Number == point.Number;
            }
            return base.Equals(obj);
        }
    }
}
