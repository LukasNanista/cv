using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class Address
    {
        public int Id { get; set; }
        [Required]
        public int PersonId { get; set; }

        [Required]
        [MaxLength(50)]
        //[Column(TypeName = "nvarchar(50)")]   //should be nvarchar by default
        public string City { get; set; }

        [Required]
        [MaxLength(50)]
        //[Column(TypeName = "nvarchar(50)")]
        public string State { get; set; }
    }
}
