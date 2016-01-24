using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeWebService.UtilClasses
{
    public class GameScore
    {
        public string datePlayed { get; set; }
        public TimeSpan durationInMin { get; set; }
        public int score { get; set; }

    }
}
