using System.Collections.Generic;

namespace MixTelematics
{
    public class Coordinates
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public static IEnumerable<Coordinates> ReturnCoordinatesData()
        {
            return new[]
            {
                new Coordinates { Id=1, Latitude = 34.544909, Longitude = -102.100843 },
                new Coordinates { Id=2, Latitude = 32.345544, Longitude = -99.123124 },
                new Coordinates { Id=3, Latitude = 33.234235, Longitude = -100.214124 },
                new Coordinates { Id=4, Latitude = 35.195739, Longitude = -95.348899 },
                new Coordinates { Id=5, Latitude = 31.895839, Longitude = -97.789573 },
                new Coordinates { Id=6, Latitude = 32.895839, Longitude = -101.789573 },
                new Coordinates { Id=7, Latitude = 34.115839, Longitude = -100.225732 },
                new Coordinates { Id=8, Latitude = 32.335839, Longitude = -99.992232 },
                new Coordinates { Id=9, Latitude = 33.535339, Longitude = -94.792232 },
                new Coordinates { Id=10, Latitude = 32.234235, Longitude = -100.222222 }
            };
        }
    }
}
