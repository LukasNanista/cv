using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class FullPersonModel
    {
        public BasicPersonModel BasicInfo { get; set; }

        //can have more than one house
        public List<AddressModel> Addresses { get; set; } = new List<AddressModel>();

        //can have more than one job
        public List<EmployerModel> Employers { get; set; } = new List<EmployerModel>();
    }
}
