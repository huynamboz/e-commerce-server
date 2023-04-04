namespace e_commerce_server.Src.Core.Env
{
    public class ENV
    {
        public static readonly string JWT_SECRET;
        public static readonly string EXPIRE_DAY;
        public static readonly string CLIENT;
        public static readonly string CONNECTION_STRING;
        static ENV() 
        {
            DotNetEnv.Env.Load();
            JWT_SECRET = Environment.GetEnvironmentVariable("JWT_SECRET") ?? "Ew8A?gWZj9!A5vnz!H?w5eBe=AG??{{";
            EXPIRE_DAY = Environment.GetEnvironmentVariable("EXPIRE_DAY") ?? "1";
            CLIENT = Environment.GetEnvironmentVariable("CLIENT") ?? "http://localhost:3000";
            CONNECTION_STRING = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? "";
        }


    }
}
