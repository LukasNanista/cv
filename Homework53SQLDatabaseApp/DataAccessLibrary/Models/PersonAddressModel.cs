using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class PersonAddressModel
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int AddressId { get; set; }
    }
}
