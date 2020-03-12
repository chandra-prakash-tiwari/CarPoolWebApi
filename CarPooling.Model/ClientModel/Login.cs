
using System.ComponentModel.DataAnnotations;

namespace CarPoolingWebApi.Models.Client
{
    public class Login
    {
        [Required(ErrorMessage ="Please Enter UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Enter Password Password")]
        public string Password { get; set; }
    }
}
