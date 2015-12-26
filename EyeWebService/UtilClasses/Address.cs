using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeWebService.UtilClasses
{
    public class Address
    {
        public string addressType { get; set; }
        public string streetName { get; set; }
        public string city { get; set; }
        public string stateName { get; set; }
        public string country { get; set; }
        public int zipcode { get; set; }
        public int phoneNo { get; set; }
    }
}