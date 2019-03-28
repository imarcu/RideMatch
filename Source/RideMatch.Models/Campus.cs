using System;

namespace RideMatch.Models
{
	public class Campus	
	{
        public int Id { get; set; }
        public string Name { get; set; }

        public Location Location { get; set; }
    }

    public class Location
    {
        public int Id { get; set; }

        public string Latitude { get; set; }

        public string  Longitude { get; set; }
    }
}
