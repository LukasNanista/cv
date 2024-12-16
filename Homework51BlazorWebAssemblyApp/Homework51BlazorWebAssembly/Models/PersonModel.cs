using System.ComponentModel.DataAnnotations;

namespace Homework51BlazorWebAssembly.Models
{
    public class PersonModel
    {
        [Required]
        public string FirstName { get; set; }

        //not required on purpose
        public string LastName { get; set; }
    }
}
