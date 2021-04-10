using BusinessLogic.Calculation;
using BusinessLogic.DataModels;
using BusinessLogic.Services;
using DataAccessLayer;
using DataAccessLayer.Credentials;
using DataAccessLayer.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataRetriever = new DataRetriever();
            var dataService = new DataService(new MapApiKeyProvider(""), dataRetriever);
            var locationService = new LocationService(dataService);
            var routeService = new RouteService(dataService, new AlgorithmConfigProvider());

            var loc1 = locationService.GetLocationInfoAsync("Харьков Победы 66д").Result;
            var loc2 = locationService.GetLocationInfoAsync("Харьков Рост Алексеевский").Result;
            var loc3 = locationService.GetLocationInfoAsync("Харьков метро Победа").Result;
            var loc4 = locationService.GetLocationInfoAsync("Харьков метро Алексеевская").Result;

            var list = new List<LocationInfo>
            {
                loc1,
                loc2,
                loc3,
                loc4
            };

            var result = routeService.GetOptimizedRouteAsync(list).Result;
            //var result = locationService.GetLocationInfoAsync("Киев 8 общежитие КПИ").Result;

            //var result = dataService.GetDirectionsResponseByAddressesAsync(new List<string> {"Харьков Победы 66д", "Харьков Рост Алексеевский", "{Харьков метро Алексеевская", "{Харьков метро 23 августа" }).Result;
            //var result = dataService.GetDirectionsResponseByPlacesIdAsync(new List<string> { "ChIJg7HNnhikJ0ERTA_FJ4xPuUw", "ChIJM75PxhmkJ0ER6fcQY6ttasA", "ChIJB83RDqimJ0ER-EvBP4_7cHk", "ChIJM-mN7LSmJ0EReLdWQ8UNMPg" }).Result;
        }
    }
}
