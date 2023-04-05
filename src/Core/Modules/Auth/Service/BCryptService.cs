using e_commerce_server.Src.Core.Env;

namespace e_commerce_server.Src.Core.Modules.Auth.Service
{
    public class BCryptService
    {
        private readonly int _workFactor;
        public BCryptService() 
        {
            this._workFactor = Convert.ToInt32(ENV.WORK_FACTOR);
        }
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, this._workFactor);
        }
        public bool Verify(string password, string harsedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, harsedPassword);
        }
    }
}
