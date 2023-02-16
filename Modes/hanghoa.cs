namespace e_commerce_server.Modes
{
    public class hanghoa
    {
        public string name { get; set; }
        public double cost { get; set; }
    }
    public class hanghoaVM : hanghoa { 
        public Guid id { get; set; }
    }
}
