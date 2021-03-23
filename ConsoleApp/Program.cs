using DataAccessLayer;
using DataAccessLayer.Credentials;
using DataAccessLayer.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataRetriever = new DataRetriever();
            var dataService = new DataService(new MapApiKeyProvider(""), dataRetriever);

            var result = dataService.GetGeocodeResponseAsync("Киев Янгеля 20").Result;
            //var result = dataService.GetDirectionsResponseAsync(new List<string> {"Харьков Победы 66д", "Харьков Рост Алексеевский", "{Харьков метро Алексеевская", "{Харьков метро 23 августа" }).Result;

        }
    }
}
