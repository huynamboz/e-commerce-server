namespace e_commerce_server.src.Core.Env
{
    public class ENV
    {
        public static readonly string? JWT_SECRET;
        public static readonly string? EXPIRE_MINUTE;
        public static readonly string? EXPIRE_DAY;
        public static readonly string? CLIENT;
        public static readonly string? CONNECTION_STRING;
        public static readonly string? WORK_FACTOR;
        public static readonly string? CLOUDINARY_NAME;
        public static readonly string? CLOUDINARY_API_KEY;
        public static readonly string? CLOUDINARY_API_SECRET;
        public static readonly string? SENDGRID_API_KEY;
        public static readonly string? SENDGRID_EMAIL_ADDRESS;
        public static readonly string? ENVIRONMENT;
        public static readonly string? HOST;
        static ENV()
        {
            DotNetEnv.Env.Load();
            WORK_FACTOR = Environment.GetEnvironmentVariable("WORK_FACTOR") ?? "4";
            JWT_SECRET = Environment.GetEnvironmentVariable("JWT_SECRET") ?? "Ew8A?gWZj9!A5vnz!H?w5eBe=AG??{{";
            EXPIRE_MINUTE = Environment.GetEnvironmentVariable("EXPIRE_MINUTE") ?? "10";
            EXPIRE_DAY = Environment.GetEnvironmentVariable("EXPIRE_DAY") ?? "30";
            CLIENT = Environment.GetEnvironmentVariable("CLIENT") ?? "http://localhost:3000";
            CONNECTION_STRING = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            CLOUDINARY_NAME = Environment.GetEnvironmentVariable("CLOUDINARY_NAME");
            CLOUDINARY_API_KEY = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY");
            CLOUDINARY_API_SECRET = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET");
            SENDGRID_API_KEY = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            SENDGRID_EMAIL_ADDRESS = Environment.GetEnvironmentVariable("SENDGRID_EMAIL_ADDRESS");
            ENVIRONMENT = Environment.GetEnvironmentVariable("ENVIRONMENT") ?? "development";
            HOST = Environment.GetEnvironmentVariable("HOST") ?? "http://localhost:5000";
        }
    }
}
