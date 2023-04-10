using e_commerce_server.src.Core.Database.Data;
using Newtonsoft.Json;

namespace e_commerce_server.src.Core.Database.Model
{
    public class LocationModel
    {
        public int code { get; set; }
        public string status { get; set; }
        public List<CityData> data { get; set; }

        public async Task<List<CityData>> GetApi(string url)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LocationModel>(jsonString).data;
        }
    }
}
