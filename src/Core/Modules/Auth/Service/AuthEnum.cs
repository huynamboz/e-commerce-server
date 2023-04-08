namespace e_commerce_server.Src.Core.Modules.Auth.Service
{
    public static class AuthEnum
    {
        public const string LOGIN_SUCCESS = "Login successfully";
        public const string LOGIN_INCORRECT = "Email or password is incorrect";
        public const string DUPLICATE_EMAIL = "This email is already exists";
        public const string DUPLICATE_PHONE_NUMBER = "This phone number is already exists";
        public const string REGISTER_SUCCESS = "Register successfully";
        public const string INVALID_REFRESH_TOKEN = "Invalid refresh token";
    }
}
