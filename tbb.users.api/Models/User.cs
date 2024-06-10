namespace tbb.users.api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public bool NewsletterSubscription { get; set; }

        public string UserType { get; set; } // "common" or "organizer"
        public string OrganizationName { get; set; }
        public string ContactNumber { get; set; }
    }
}
