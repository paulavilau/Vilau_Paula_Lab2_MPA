﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModel.Models
{
    public class City
    {
        public int Id { get; set; }

        public string CityName { get; set; }

        public ICollection<Customer>? Customers { get; set; }
    }
}
