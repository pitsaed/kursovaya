using System;

namespace _4sem_kurs
{
    [Serializable]
    abstract public class ElectricalStation
    {
        public abstract string Name { get; }
        public abstract int MinimalPower { get; }
        public abstract int MaximalPower { get; }
        public abstract decimal Price { get; }
        public abstract int NoiseLevel { get; }
        public abstract string XmlToString();
    }
}
