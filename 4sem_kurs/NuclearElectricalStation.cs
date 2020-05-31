using System;

namespace _4sem_kurs
{
    [Serializable]
    public class NuclearElectricalStation : ElectricalStation
    {
        public override string Name => "АЭС";

        public override int MinimalPower => 5000;

        public override int MaximalPower => 100000;

        public override decimal Price => 200000;

        public override int NoiseLevel => 130;
        public override string ToString() => $"Тип станции: {Name}\nМинимальная мощность: {MinimalPower}\nМаксимальная мощность: {MaximalPower}\nЦена: {Price}\nУровень шума: {NoiseLevel}\n";
        public override bool Equals(object obj)
        {
            NuclearElectricalStation place = null;
            if (obj is NuclearElectricalStation)
            {
                place = obj as NuclearElectricalStation;
            }
            return ToString() == place.ToString();
        }
        public override string XmlToString() => "NuclearElectricalStation";

    }
}
