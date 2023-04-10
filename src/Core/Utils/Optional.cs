namespace e_commerce_server.src.Core.Utils
{
    public class Optional
    {
        private object _instance;

        public Optional(object instance)
        {
            _instance = instance;
        }

        public static Optional Of(object instance) 
        {
            return new Optional(instance);
        }

        public void ThrowIfPresent(Exception exception)
        {
            if (this._instance == null)
            {
                throw exception;
            }
        }

        public void ThrowIfNotPresent(Exception exception)
        {
            if (this._instance != null)
            {
                throw exception;
            }
        }
    }
}
