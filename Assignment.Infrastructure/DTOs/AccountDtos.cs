using System.ComponentModel.DataAnnotations;

namespace Models.WebApiViewModels
{
    /// <summary> Login model to system</summary>
    public class LoginDto
    {
        /// <summary> Email of User</summary>
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage = "Email format not valid")]
        public string Email { get; set; }

        /// <summary> Password of User</summary>
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password, ErrorMessage = "Please enter a stronger password")]
        public string Password { get; set; }
    }

    /// <summary> Response model of Login API</summary>
    public class LoginResponse
    {
        /// <summary> Authorization Bearer Token</summary>
        public string Token { get; set; }

        /// <summary>  Full Name of User</summary>
        public string FullName { get; set; }

        /// <summary> Email of User</summary>
        public string Email { get; set; }
    }

    /// <summary> Register model to system</summary>
    public class RegisterDto
    {
        /// <summary>  Full Name of User</summary>
        [Required(ErrorMessage = "Name Required")]
        [MaxLength(100, ErrorMessage = "Register Name can't be more than 100 characters")]
        public string FullName { get; set; }

        /// <summary> Email of User</summary>
        [Required(ErrorMessage = "Mail is required")]
        [EmailAddress(ErrorMessage = "Mail is not valid")]
        public string Email { get; set; }

        /// <summary> Password of User</summary>
        [Required(ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
