using System.Collections.Generic;

namespace _4sem_kurs_client
{
    using _4sem_kurs;
    public class PriceComparer : IComparer<Request>
    {
        public int Compare(Request x, Request y) => x.Station.Price.CompareTo(y.Station.Price);

    }
}
