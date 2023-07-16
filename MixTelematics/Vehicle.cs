namespace MixTelematics
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public string VehicleRegistration { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public ulong RecordedTimeUtc { get; set; }
    }
}
