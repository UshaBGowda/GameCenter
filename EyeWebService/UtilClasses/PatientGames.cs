using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeWebService.UtilClasses
{
    public class PatientGames
    {
        public int patientId { get; set; }
        public List<GameAssignment> gameAssignments { get; set; }
    }
}