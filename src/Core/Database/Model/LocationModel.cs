namespace e_commerce_server.src.Core.Database.Model
{
    public class LocationModel
    {
        public int id {get; set;}
        public string name {get; set;}
        public List<DistrictModel> districts {get; set;}
    }

    public class DistrictModel
    {
        public int id {get; set;}
        public string name {get; set;}
    }
}
