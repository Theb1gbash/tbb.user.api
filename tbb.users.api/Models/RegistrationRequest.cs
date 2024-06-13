using System.ComponentModel.DataAnnotations;

namespace tbb.users.api.Models
{
    public class RegistrationRequest
    {
        [Required]
        [MinLength(2, ErrorMessage = "First Name must be at least 2 characters long.")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Last Name must be at least 2 characters long.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Password must be at least 8 characters long and include at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        public bool NewsletterSubscription { get; set; }

        [Required]
        public string UserType { get; set; } // "common" or "organizer"

        [OrganizerRequired("UserType", "organizer", ErrorMessage = "Organization Name is required for organizers.")]
        [MinLength(2, ErrorMessage = "Organization Name must be at least 2 characters long.")]
        public string OrganizationName { get; set; }

        [OrganizerRequired("UserType", "organizer", ErrorMessage = "Contact Number is required for organizers.")]
        [RegularExpression(@"^\d{10,}$", ErrorMessage = "Contact Number must be at least 10 digits.")]
        public string ContactNumber { get; set; }
    }
}
