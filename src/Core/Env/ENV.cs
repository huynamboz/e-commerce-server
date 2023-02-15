namespace e_commerce_server.src.Core.Env
{
    public class ENV
    {
        public static readonly string JWT_SECRET;
        public static readonly string EXPIRE_DAY;
        static ENV() 
        {
            DotNetEnv.Env.Load();
            JWT_SECRET = Environment.GetEnvironmentVariable("JWT_SECRET") ?? "Ew8A?gWZj9!A5vnz!H?w5eBe=AG??{{";
            EXPIRE_DAY = Environment.GetEnvironmentVariable("EXPIRE_DAY") ?? "1";
        }


    }
}
