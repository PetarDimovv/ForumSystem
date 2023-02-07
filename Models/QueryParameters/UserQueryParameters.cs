namespace StackOverflow.Models.QueryParameters
{
    public class UserQueryParameters
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public string SortBy { get; set; }
        public string SortOrder { get; set; }

        public string PhoneNumber { get; set; }

    }
}
