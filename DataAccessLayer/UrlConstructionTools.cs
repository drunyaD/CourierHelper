﻿using DataAccessLayer.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer
{
    public static class UrlConstructionTools
    {
        private static readonly string _geocodeEndpoint = "https://maps.googleapis.com/maps/api/geocode/json";
        private static readonly string _directionEndpoint = "https://maps.googleapis.com/maps/api/directions/json";

        public static string GetGeocodeUrl(string address, string key)
        {
            return $"{_geocodeEndpoint}?address={address}&key={key}";
        }

        public static string GetDirectionsUrl(List<string> addresses, TravelMode travelMode, string key)
        {
            var origin = addresses.First();
            var destination = addresses.Last();

            var url = $"{_directionEndpoint}?mode={travelMode}&origin={origin}&destination={destination}&key={key}";

            if (addresses.Count > 2)
            {
                var waypoints = string.Join("|", addresses.Skip(1).Take(addresses.Count - 2));
                url += $"&waypoints={waypoints}";
            }

            return url;
        }
    }
}
