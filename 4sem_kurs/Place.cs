using System;

namespace _4sem_kurs
{
    [Serializable]
    public class Place
    {
        public string Street { get; set; }
        public int Length { get; set; }
        public int Heat { get; set; }
        public int Air { get; set; }
        public int Water { get; set; }
        public int NoiceLevel { get; set; }
        public double Square { get; set; }
        public Place() { }
        public Place(string street, int number, double square)
        {
            Street = street;
            Length = number;
            Square = square;
        }
        public override string ToString() => $"Местоположение: {Street}\nРасстояние: {Length}\nПВД тепла {Heat}\nПДВ в воздух{Air}\nПДВ в воду{Water}\nПлощадь: {Square}\nУровень шума: {NoiceLevel}";
        public override bool Equals(object obj)
        {
            Place place = null;
            if (obj is Place)
            {
                place = obj as Place;
            }
            return ToString() == place.ToString();
        }
    }
}
