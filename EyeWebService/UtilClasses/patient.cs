using System;

namespace EyeWebService.UtilClasses
{
    /// <summary>
    /// 
    /// </summary>
    public class Patient
    {
        public int patientId { get; set; }
        public int parentId { get; set; }
        public int providerId { get; set; }
        
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
    }
}