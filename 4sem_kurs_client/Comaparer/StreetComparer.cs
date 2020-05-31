using System.Collections.Generic;

namespace _4sem_kurs_client
{
    using _4sem_kurs;
    public class StreetComparer : IComparer<Request>
    {
        public int Compare(Request x, Request y) => x.Place.Street.CompareTo(y.Place.Street);

    }
}
