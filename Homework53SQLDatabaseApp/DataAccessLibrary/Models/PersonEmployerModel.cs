using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class PersonEmployerModel
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int EmployerId { get; set; }
    }
}
