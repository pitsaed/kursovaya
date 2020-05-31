using System;

namespace _4sem_kurs
{
        [Serializable]
    public class PowerBlock
    {
        public string Name { get; set; }
        public int Heat { get; set; }
        public int InAir { get; set; }
        public int InWater { get; set; }
        public int LimitPower { get; set; }
        public int MinSpace { get; set; }
        public double Price { get; set; }
        public PowerBlock() { }
        public override string ToString() => $"Энергоблок {Name}\nВыделение тепла на единицу энергии: {Heat}\nВыбросы в воздух на единицу энергии: {InAir}\nВыбросы в воду на единицу энергии: {InWater}\nПредельная мощность энергоблока: {LimitPower}\nМинимальная требуемая площадь: {MinSpace}\nЦена постройки: {Price}\n";
    }
}
