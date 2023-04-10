namespace e_commerce_server.src.Core.Modules.Auth.Service
{
    public static class AuthEnum
    {
        public const string LOGIN_SUCCESS = "Login successfully";
        public const string LOGIN_INCORRECT = "Email or password is incorrect";
        public const string DUPLICATE_EMAIL = "This email is already exists";
        public const string PASSWORDS_NOT_MATCH = "Password and confirmation password does not match";
        public const string DUPLICATE_PHONE_NUMBER = "This phone number is already exists";
        public const string REGISTER_SUCCESS = "Register successfully";
        public const string INVALID_REFRESH_TOKEN = "Invalid refresh token";
        public const string REQUEST_RESET_PASSWORD_SUCCESS = "Password recovery link has been sent to your email";
        public const string NOT_FOUND_EMAIL = "User not found with that email";
        public const string VALID_TOKEN = "Valid token";
        public const string INVALID_TOKEN = "Invalid token";
        public const string EXPIRED_TOKEN = "Expired token";
        public const string UPDATE_PASSWORD_SUCCESS = "Updated password successfully";
    }
}
