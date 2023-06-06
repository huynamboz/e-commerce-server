namespace e_commerce_server.src.Core.Utils
{
    public static class Regex
    {
        public const string PASSWORD = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
        public const string PHONE_NUMBER = @"(84|0[3|5|7|8|9])+([0-9]{8})\b";
        public const string EMAIL = @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$";
    }
}
