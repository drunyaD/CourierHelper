import requests
import json

location_url = "https://localhost:44349/api/location"
optimized_route_url = "https://localhost:44349/api/routeoptimization"

def record_location_info(search_array):
    location_info_array = []
    for search in search_array:
        PARAMS = {'searchString':search}
        response = requests.get(url = location_url, params = PARAMS, verify=False)
        data = response.json()
        location_info_array.append(data)
        
    with open('location_info_array.txt', 'w') as outfile:
        json.dump(location_info_array, outfile)

def get_recorded_location_infos():
    with open('location_info_array.txt') as json_file:
        data = json.load(json_file)
        return data

def record_optimized_route(locations):
    payload = {
        'startLocation': locations[0],
        'locationsToVisit': locations[1:]
    }
    response = requests.post(url = optimized_route_url, json = payload, verify=False)
    data = response.json()

    with open('optimized_route.txt', 'w') as outfile:
        json.dump(data, outfile)
    
def get_recorded_optimized_route():
    with open('optimized_route.txt') as json_file:
        data = json.load(json_file)
        return data