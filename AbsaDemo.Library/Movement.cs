namespace AbsaDemo.Library
{
    public class Movement
    {
        public int Latitude { get; set; }
        public int Longitude { get; set; }
        public string GetCoordinates() => $"{this.Longitude},{this.Latitude}";
    }
}
