﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HomeworkLibrary
{
    public class PersonModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<AddressModel> Addresses { get; set; }
    }
}