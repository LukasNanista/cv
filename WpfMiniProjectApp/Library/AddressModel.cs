using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class AddressModel
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

        //might not be necessary when we should pull more things into box with WPF
        //but I like it better in one tidy package
        //might play with trying to send those bits directly or maybe will see in the demo app
        public string FullAddress => $"{StreetAddress}, {City}, {ZipCode}, {Country}";
    }
}
