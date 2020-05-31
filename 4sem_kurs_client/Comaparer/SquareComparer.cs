using System.Collections.Generic;

namespace _4sem_kurs_client
{
    using _4sem_kurs;
    public class SquareComparer : IComparer<Request>
    {
        public int Compare(Request x, Request y) => x.Place.Square.CompareTo(y.Place.Square);

    }
}
