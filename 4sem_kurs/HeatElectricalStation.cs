using System;

namespace _4sem_kurs
{
    [Serializable]
    public class HeatElectricalStation : ElectricalStation
    {
        public override string Name => "ТЭС";

        public override int MinimalPower => 15000;

        public override int MaximalPower => 3000;

        public override decimal Price => 130000;

        public override int NoiseLevel => 11;
        public override string ToString() => $"Тип станции: {Name}\nМинимальная мощность: {MinimalPower}\nМаксимальная мощность: {MaximalPower}\nЦена: {Price}\nУровень шума: {NoiseLevel}\n";
        public override bool Equals(object obj)
        {
            HeatElectricalStation place = null;
            if (obj is HeatElectricalStation)
            {
                place = obj as HeatElectricalStation;
            }
            return ToString() == place.ToString();
        }
        public override string XmlToString() => "HeatElectricalStation";
    }
}
