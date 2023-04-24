namespace e_commerce_server.src.Core.Modules.Review.Service
{
    public static class ReviewEnum
    {
        public const string REVIEW_ALREADY_EXIST = "Review already exist";
        public const string CREATE_REVIEW_SUCCESS = "Create review successfully";
        public const string UPDATE_REVIEW_SUCCESS = "Update review successfully";
        public const string CANNOT_REVIEW_OWN_PRODUCT = "You cannot review your own product";
        public const string REVIEW_NOT_FOUND = "Can't find review about this product";
        public const string DELETE_REVIEW_SUCCESS = "Delete review successfully";
    }
}
