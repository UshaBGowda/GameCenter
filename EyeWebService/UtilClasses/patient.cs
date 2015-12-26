using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeWebService.UtilClasses
{
    public class patient
    {
        public string parentLoginName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        private string dateOfBirth;
        public string dob
        {
            get
            {
                try
                {
                    if (dateOfBirth == null || DateTime.Parse(dateOfBirth) < DateTime.Parse("1/1/1753"))
                        return "1/1/1753";
                    return dateOfBirth;
                }
                catch
                {
                    return "1/1/1753";
                }
            }
            set
            {
                dateOfBirth = value;

            }
        }
        public string gender { get; set; }
        public string  providerFirstName { get; set; }
        public string providerLastName { get; set; }
    }
}