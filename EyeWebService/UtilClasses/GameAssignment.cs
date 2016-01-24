using System;
using System.Collections.Generic;

namespace EyeWebService.UtilClasses
{
    public class GameAssignment
    {
        public Game game { get; set; }
        public int level { get; set; }

        public string startDate { get; set; }
        public string endDate { get; set; }
    }
}