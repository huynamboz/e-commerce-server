namespace e_commerce_server.Model
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string message { get; set; }
        public object Data { get; set; }
    }
}
