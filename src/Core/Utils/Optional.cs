namespace e_commerce_server.src.Core.Utils
{
    public class Optional
    {
        private dynamic _instance;

        public Optional(dynamic instance)
        {
            _instance = instance;
        }

        public static Optional Of(dynamic instance) 
        {
            return new Optional(instance);
        }

        public void ThrowIfPresent(Exception exception)
        {
            if ((this._instance is List<dynamic> && this._instance.Count > 0 )|| (!(this._instance is List<dynamic>) && this._instance != null))
            {
                throw exception;
            }
        }

        public void ThrowIfNotPresent(Exception exception)
        {
            if ((this._instance is List<dynamic> && this._instance.Count == 0 )|| (!(this._instance is List<dynamic>) && this._instance == null))
            {
                throw exception;
            }
        }
    }
}
