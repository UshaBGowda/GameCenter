using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeWebService.UtilClasses
{
    public class PatientGameScore
    {
        public int patientId { get; set; }

        public Game game { get; set; }

        public int level { get; set; }

        public List<GameScore> gameScoreList { get; set; }
    }
}