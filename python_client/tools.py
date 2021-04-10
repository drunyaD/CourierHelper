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

def get_optimized_route(locations):
    response = requests.post(url = optimized_route_url, json = locations, verify=False)
    data = response.json()
    return data
    