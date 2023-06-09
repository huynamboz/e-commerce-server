namespace e_commerce_server.src.Core.Modules.User.Service
{
    public static class UserEnum
    {
        public const string USER_NOT_FOUND = "User not found";
        public const string UPDATE_USER_SUCCESS = "Updated user successfully";
        public const string ADD_TO_FAVORITE_SUCCESS = "Added to favorite successfully";
        public const string REMOVE_FROM_FAVORITE_SUCCESS = "Remove from favorite successfully";
        public const string DUPLICATE_FAVORITE_PRODUCT = "Product is already in your favorite";
        public const string FAVORITE_PRODUCT_NOT_FOUND = "Product is not in your favorite";
        public const string DELETE_USER_SUCCESS = "Deleted user successfully";
        public const string GET_ALL_USERS_DENIED = "You are not allowed to get all users";
        public const string DELETE_USER_DENIED = "You are not allowed to delete user";
        public const string USER_ALREADY_DELETED = "User is already deleted";
        public const string USER_NOT_DELETED = "User is not deleted";
        public const string USER_UNBAN = "User is unbaned";
    }
}
