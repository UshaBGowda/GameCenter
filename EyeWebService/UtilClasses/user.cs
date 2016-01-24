using System;
using System.Collections.Generic;

namespace EyeWebService.UtilClasses
{
    public class user
    {
        public string loginId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int userTypeId { get; set; }

        public int userId { get; set; }
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
        public List<Address> addressList { get; set;}


    }
}