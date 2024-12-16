using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class Employer
    {
        public int Id { get; set; }

        [Required]
        public int PersonId { get; set; }

        [Required]
        [MaxLength(50)]
        public string EmployerName { get; set; }
    }
}
