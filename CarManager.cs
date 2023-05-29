using Newtonsoft.Json;
using static Carseer.Models.CarMake;
using System.Net.Http;
using Carseer.Models;

namespace Carseer.Mangers
{
    public class CarManager
    {

        public async Task<List<Make>> GetAllMakes()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync("https://vpic.nhtsa.dot.gov/api/vehicles/getallmakes?format=json");
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(result))
                {
                    MakeApiResponse apiResponse = JsonConvert.DeserializeObject<MakeApiResponse>(result);
                    if (apiResponse != null && apiResponse.Makes != null)
                    {
                        return apiResponse.Makes;
                    }
                }

                return new List<Make>(); // Return an empty list if data is not available
            }
        }

        public async Task<List<VehicleType>> GetVehicleTypesForMaker(string makerId)
        {
            var nullResult = new List<VehicleType>();
            if (!string.IsNullOrEmpty(makerId))
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync($"https://vpic.nhtsa.dot.gov/api/vehicles/GetVehicleTypesForMakeId/{makerId}?format=json");
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(result))
                    {
                        VehicleTypeApiResponse apiResponse = JsonConvert.DeserializeObject<VehicleTypeApiResponse>(result);
                        if (apiResponse != null && apiResponse.VehicleTypes != null)
                        {
                            return apiResponse.VehicleTypes;
                        }
                    }

                    return nullResult; // Return an empty list if data is not available
                }
            }
            return nullResult;
        }

        public async Task<List<Model>> GetModelsForMakeIdYear(string makerId,string yearId)
        {
            var nullResult = new List<Model>();
            if (!string.IsNullOrEmpty(makerId) && !string.IsNullOrEmpty(yearId))
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync($"https://vpic.nhtsa.dot.gov/api/vehicles/GetModelsForMakeIdYear/makeId/{makerId}/modelyear/{yearId}?format=json");
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(result))
                    {
                        ModelApiResponse apiResponse = JsonConvert.DeserializeObject<ModelApiResponse>(result);
                        if (apiResponse != null && apiResponse.Models != null)
                        {
                            return apiResponse.Models;
                        }
                    }

                    return nullResult; // Return an empty list if data is not available
                }
            }
            return nullResult;
        }

    }
}
