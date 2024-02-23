using System.ComponentModel.DataAnnotations;

namespace GroopWebApp.ViewModels
{
    public class HomeUserCreateViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        [Required]
        public string City { get; set; }
        public string State { get; set; }
    }
}