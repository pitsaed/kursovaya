using System;

namespace _4sem_kurs
{
    [Serializable]
    public class Request
    {
        public ElectricalStation Station { get; set; }
        public Place Place { get; set; }
        public PowerBlock Block { get; set; }
        public Request() { }
        public Request(ElectricalStation station, Place place, PowerBlock block)
        {
            Station = station;
            Place = place;
            Block = block;
        }
        public override string ToString() => $"Станция: \n{Station}\n\nМесто:\n{Place}\n\nЭнергоблок:\n{Block}\n";
        public override bool Equals(object obj)
        {
            Request request = null;
            if (obj is Request)
            {
                request = obj as Request;
            }
            return ToString() == request.ToString();
        }
    }
}
