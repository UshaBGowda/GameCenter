using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using EyeWebService.UtilClasses;

namespace EyeWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : EyeWebService
    {
        private readonly DBConnect _dbConnect = new DBConnect();

        /// <summary>
        /// Sets the profile.
        /// </summary>
        /// <param name="newUser">The new user.</param>
        /// <returns></returns>
        public bool SetProfile(user newUser)
        {
            var varParams = new SqlParameter[9];

            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParams[2] = new SqlParameter("@firstname", newUser.firstName);
            varParams[3] = new SqlParameter("@lastName", newUser.lastName);
            varParams[4] = new SqlParameter("@loginId", newUser.loginId);
            varParams[5] = new SqlParameter("@userTypeId", newUser.userTypeId);
            varParams[6] = new SqlParameter("@dateOfBirth", newUser.dob);
            varParams[7] = new SqlParameter("@gender", newUser.gender);
            varParams[8] = new SqlParameter("@userId", newUser.userId);

            var dt = _dbConnect.RunProcedureGetDataTable("dbo.spCreateUpdateUser", varParams);
            var error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
            var retVal = int.Parse(varParams[1].Value.ToString());

            if (retVal == 1)
                return false;

            foreach (var address in newUser.addressList)
            {
                var varParamsNew = new SqlParameter[10];

                varParamsNew[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
                varParamsNew[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
                varParamsNew[2] = new SqlParameter("@addressType", address.addressType);
                varParamsNew[3] = new SqlParameter("@loginId", newUser.loginId);
                varParamsNew[4] = new SqlParameter("@streetName", address.streetName);
                varParamsNew[5] = new SqlParameter("@city", address.city);
                varParamsNew[6] = new SqlParameter("@stateName", address.stateName);
                varParamsNew[7] = new SqlParameter("@country", address.country);
                varParamsNew[8] = new SqlParameter("@zipcode", address.zipcode);
                varParamsNew[9] = new SqlParameter("@phoneNo", address.phoneNo);

                var dtNew = _dbConnect.RunProcedureGetDataTable("dbo.spCreateUpdateAddress", varParamsNew);
                var errorNew = varParamsNew[0].Value == DBNull.Value ? "" : varParamsNew[1].Value.ToString();
                var retValNew = int.Parse(varParamsNew[1].Value.ToString());

                if (retValNew == 1)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <param name="loginId">The login identifier.</param>
        /// <returns></returns>
        public int GetUserId(string loginId)
        {
            var varParams = new SqlParameter[3];

            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParams[2] = new SqlParameter("@loginId", loginId);
            var dt = _dbConnect.RunProcedureGetDataTable("dbo.spGetUserId", varParams);
            var error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
            var retVal = int.Parse(varParams[1].Value.ToString());
            var userId = new int();

            foreach (DataRow dr in dt.Rows)
            {
                userId = int.Parse(dr["userId"].ToString());
            }

            if (retVal == 1)
                return -1;
            return userId;
        }


        /// <summary>
        /// Gets the user type identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public int GetUserTypeId(int userId)
        {
            var varParams = new SqlParameter[3];

            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParams[2] = new SqlParameter("@userId", userId);
            var dt = _dbConnect.RunProcedureGetDataTable("dbo.spGetUserTypeId", varParams);
            var error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
            var retVal = int.Parse(varParams[1].Value.ToString());
            var userTypeId = new int();

            foreach (DataRow dr in dt.Rows)
            {
                userTypeId = int.Parse(dr["userTypeId"].ToString());
            }

            if (retVal == 1)
                return -1;
            return userTypeId;
        }


        /// <summary>
        /// Lists the children profile.
        /// </summary>
        /// <param name="parentId">The parent identifier.</param>
        /// <returns></returns>
        public List<Patient> ListChildrenProfile(int parentId)
        {
            var childrenList = new List<Patient>();
            var errMsg = string.Empty;

            var varParams = new SqlParameter[3];
            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParams[2] = new SqlParameter("@userId", parentId);

            var dt = _dbConnect.RunProcedureGetDataTable("dbo.spListChildrenProfile", varParams);
            var error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
            var retVal = int.Parse(varParams[1].Value.ToString());
            foreach (DataRow dr in dt.Rows)
            {
                var newChild = new Patient
                {
                    patientId = int.Parse(dr["patientId"].ToString()),
                    providerId = int.Parse(dr["providerId"].ToString()),
                    parentId = int.Parse(dr["parentId"].ToString()),
                    firstName = dr["childFN"].ToString(),
                    lastName = dr["childLN"].ToString(),
                    dob = dr["childDOB"].ToString(),
                    //    gender = new Gender
                    //    {
                    //        id = dr["genderId"].ToString(),
                    //        name = dr["genderName"].ToString()
                    //    }
                    //};
                    gender = dr["gender"].ToString()
                };
                childrenList.Add(newChild);
            }

            if (retVal == 1)
                return null;
            return childrenList;
        }

        /// <summary>
        /// Gets the patient profile.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        public Patient GetPatientProfile(int patientId)
        {
            var errMsg = string.Empty;

            var varParams = new SqlParameter[3];
            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParams[2] = new SqlParameter("@patientId", patientId);

            var dt = _dbConnect.RunProcedureGetDataTable("dbo.spGetPatientProfile", varParams);
            var error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
            var retVal = int.Parse(varParams[1].Value.ToString());

            var dr = dt.Rows[0];
            var newPatient = new Patient
            {
                patientId = int.Parse(dr["patientId"].ToString()),
                providerId = int.Parse(dr["providerId"].ToString()),
                parentId = int.Parse(dr["parentId"].ToString()),
                firstName = dr["childFN"].ToString(),
                lastName = dr["childLN"].ToString(),
                dob = dr["childDOB"].ToString(),
                gender = dr["childGender"].ToString()
            };


            switch (retVal)
            {
                case 1:
                    return null;
                default:
                    return newPatient;
            }
        }

        /// <summary>
        /// Lists the provider profiles.
        /// </summary>
        /// <returns></returns>
        public List<user> ListAllProviderProfile()
        {
            var errMsg = string.Empty;
            var providerList = new List<user>();

            var varParams = new SqlParameter[2];
            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            var dt = _dbConnect.RunProcedureGetDataTable("dbo.spListProviderProfiles", varParams);
            var error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
            var retVal = int.Parse(varParams[1].Value.ToString());
            foreach (DataRow dr in dt.Rows)
            {
                var newProvider = new user
                {
                    firstName = dr["firstName"].ToString(),
                    lastName = dr["lastName"].ToString(),
                    dob = dr["dateOfBirth"].ToString(),
                    gender = dr["gender"].ToString(),
                    userId = int.Parse(dr["userId"].ToString())
                };
                providerList.Add(newProvider);
            }

            if (retVal == 1)
                return null;
            return providerList;
        }


        /// <summary>
        /// Sets the child profile.
        /// </summary>
        /// <param name="newPatient">The new patient.</param>
        /// <returns></returns>
        public Patient SetChildProfile(Patient newPatient)
        {
            var varParams = new SqlParameter[9];


            varParams[0] = new SqlParameter("@firstName", newPatient.firstName);
            varParams[1] = new SqlParameter("@lastName", newPatient.lastName);
            varParams[2] = new SqlParameter("@loginId", "");
            varParams[3] = new SqlParameter
            {
                ParameterName = "@userId",
                Direction = ParameterDirection.InputOutput,
                Value = newPatient.patientId
            };
            varParams[4] = new SqlParameter("@userTypeId", 3);
            varParams[5] = new SqlParameter("@dateOfBirth", newPatient.dob);
            varParams[6] = new SqlParameter("@gender", newPatient.gender);
            varParams[7] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[8] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);

            var dt = _dbConnect.RunProcedureGetDataTable("dbo.spCreateUpdateUser", varParams);
            var error = varParams[7].Value == DBNull.Value ? "" : varParams[7].Value.ToString();
            var retVal = int.Parse(varParams[8].Value.ToString());

            if (retVal == 1)
                return new Patient();
            newPatient.patientId = int.Parse(varParams[3].Value.ToString());
            var varParamsNew = new SqlParameter[5];

            varParamsNew[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParamsNew[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParamsNew[2] = new SqlParameter("@parentId", newPatient.parentId);
            varParamsNew[3] = new SqlParameter("@patientId", int.Parse(varParams[3].Value.ToString()));
            varParamsNew[4] = new SqlParameter("@providerId", newPatient.providerId);

            var dtNew = _dbConnect.RunProcedureGetDataTable("dbo.spSetParentXREF", varParamsNew);
            var errorNew = varParamsNew[0].Value == DBNull.Value ? "" : varParamsNew[1].Value.ToString();
            var retValNew = int.Parse(varParamsNew[1].Value.ToString());

            return retValNew == 1 ? new Patient() : GetPatientProfile(newPatient.patientId);
        }



        public List<Patient> ListPatientsProfile(int providerId)
        {
            List<Patient> patientList = new List<Patient>();
            string errMsg = string.Empty;

            SqlParameter[] varParams = new SqlParameter[3];
            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParams[2] = new SqlParameter("@providerId", providerId);

            DataTable dt = _dbConnect.RunProcedureGetDataTable("dbo.spListPatientsProfile", varParams);
            string error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
            int retVal = Int32.Parse(varParams[1].Value.ToString());
            foreach (DataRow dr in dt.Rows)
            {
                Patient newPatient = new Patient();
                newPatient.firstName = dr["childFN"].ToString();
                newPatient.lastName = dr["childLN"].ToString();
                newPatient.parentId = int.Parse(dr["parentId"].ToString());
                newPatient.providerId = int.Parse(dr["providerId"].ToString());
                newPatient.patientId = int.Parse(dr["patientId"].ToString());
                newPatient.dob = dr["childDOB"].ToString();
                newPatient.gender = dr["childGender"].ToString();
                patientList.Add(newPatient);
            }

            if (retVal == 1)
                return null;
            return patientList;
        }



        public bool DeleteProfile(int userId)
        {
            SqlParameter[] varParams = new SqlParameter[4];

            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParams[2] = new SqlParameter("@userId", userId);
            DataTable dt = _dbConnect.RunProcedureGetDataTable("dbo.spDeleteUser", varParams);
            string error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
            int retVal = Int32.Parse(varParams[1].Value.ToString());

            if (retVal == 1)
                return false;
            return true;
        }







        /// <summary>
        /// Gets the user profile.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public user GetUserProfile(int userId)
        {
            var newUser = new user();
            var errMsg = string.Empty;

            var varParams = new SqlParameter[3];
            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParams[2] = new SqlParameter("@userId", userId);

            var dt = _dbConnect.RunProcedureGetDataTable("dbo.spGetUserProfile", varParams);
            var error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
            var retVal = int.Parse(varParams[1].Value.ToString());
            foreach (DataRow dr in dt.Rows)
            {
                newUser.firstName = dr["firstName"].ToString();
                newUser.lastName = dr["lastName"].ToString();
                newUser.dob = dr["dateOfBirth"].ToString();
                newUser.gender = dr["gender"].ToString();
                newUser.userTypeId = int.Parse(dr["userTypeId"].ToString());
                newUser.userId = int.Parse(dr["userId"].ToString());
            }
            if (retVal == 1)
                return null;

            var varParamsNew = new SqlParameter[3];
            varParamsNew[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParamsNew[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParamsNew[2] = new SqlParameter("@userId", userId);

            var dtNew = _dbConnect.RunProcedureGetDataTable("dbo.spGetUserAddress", varParamsNew);
            var errorNew = varParamsNew[0].Value == DBNull.Value ? "" : varParamsNew[1].Value.ToString();
            var retValNew = int.Parse(varParamsNew[1].Value.ToString());
            newUser.addressList = new List<Address>();
            foreach (DataRow dr in dtNew.Rows)
            {
                var newAdd = new Address();
                newAdd.addressType = dr["addressType"].ToString();
                newAdd.streetName = dr["streetName"].ToString();
                newAdd.city = dr["city"].ToString();
                newAdd.stateName = dr["stateName"].ToString();
                newAdd.phoneNo = dr["phoneNo"].ToString();
                newAdd.zipcode = dr["zipcode"].ToString();
                newAdd.country = dr["country"].ToString();
                newUser.addressList.Add(newAdd);
            }


            ////using (var db = new EYE_DatabaseEntities())
            ////{
            ////    //db.tblUsers.Where(c=>userId=userId)
            ////    var users = db.tblUsers.FirstOrDefault(a => a.userId == userId);
            ////    db.tblUsers.Add(new tblUser
            ////    {
            ////        firstName = "G",dateOfBirth = DateTime.MaxValue,gender = "M",lastName = "P",loginId = "",userTypeId =2
            ////    })
            ////;
            ////    db.SaveChanges();
            ////}
            if (retValNew == 1)
                return null;
            return newUser;
        }


        public bool CreateUpdateTherapy(Therapy newTherapy)
        {
            SqlParameter[] varParams = new SqlParameter[5];

            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParams[2] = new SqlParameter("@therapyId", newTherapy.therapyId);
            varParams[3] = new SqlParameter("@therapyName", newTherapy.therapyName);
            varParams[4] = new SqlParameter("@therapyDescription", newTherapy.therapyDescription);

            DataTable dt = _dbConnect.RunProcedureGetDataTable("dbo.spCreateUpdateTherapy", varParams);
            string error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
            int retVal = Int32.Parse(varParams[1].Value.ToString());
            switch (retVal)
            {
                case 1:
                    return false;
                default:
                    return true;
            }
        }

        public Game CreateUpdateGame(Game newGame)
        {
            SqlParameter[] varParams = new SqlParameter[6];

            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParams[2] = _dbConnect.MakeParameter("@gameId", ParameterDirection.InputOutput, -1,newGame.gameId);
            varParams[3] = new SqlParameter("@gameName", newGame.gameName);
            varParams[4] = new SqlParameter("@gameDescription", newGame.gameDescription);
            varParams[5] = new SqlParameter("@therapyId", newGame.therapy.therapyId);

            DataTable dt = _dbConnect.RunProcedureGetDataTable("dbo.spCreateUpdateGame", varParams);
            string error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
            int retVal = Int32.Parse(varParams[1].Value.ToString());
            switch (retVal)
            {
                case 1:
                    return null;
                default:

                    if (dt == null) return null;
                    var dr = dt.Rows[0];
                    return new Game
                    {
                        gameId = int.Parse(dr["gameId"].ToString()),
                        gameName = dr["gameName"].ToString(),
                        gameDescription = dr["gameDescription"].ToString(),
                        therapy = new Therapy
                        {
                            therapyId = int.Parse(dr["therapyId"].ToString()),
                            therapyName = dr["therapyName"].ToString(),
                            therapyDescription = dr["therapyDescription"].ToString()
                        }
                    };
            }

        }

        public bool DeleteGame(int gameId)
        {
            SqlParameter[] varParams = new SqlParameter[3];

            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParams[2] = new SqlParameter("@gameId", gameId);

            DataTable dt = _dbConnect.RunProcedureGetDataTable("dbo.spDeleteGameForTherapy", varParams);
            string error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
            int retVal = Int32.Parse(varParams[1].Value.ToString());

            if (retVal == 1)
                return false;
            return true;
        }

        public bool DeleteTherapy(int therapyId)
        {
            SqlParameter[] varParams = new SqlParameter[3];

            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParams[2] = new SqlParameter("@therapyId", therapyId);

            DataTable dt = _dbConnect.RunProcedureGetDataTable("dbo.spDeleteTherapy", varParams);
            string error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
            int retVal = Int32.Parse(varParams[1].Value.ToString());

            if (retVal == 1)
                return false;
            return true;
        }


        public List<Therapy> ListAllTherapy()
        {
            List<Therapy> therapyList = new List<Therapy>();
            string errMsg = string.Empty;

            SqlParameter[] varParams = new SqlParameter[2];
            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);

            DataTable dt = _dbConnect.RunProcedureGetDataTable("dbo.spListTherapy", varParams);
            string error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
            int retVal = Int32.Parse(varParams[1].Value.ToString());
            foreach (DataRow dr in dt.Rows)
            {
                Therapy newTherapy = new Therapy
                {
                    therapyId = int.Parse(dr["therapyId"].ToString()),
                    therapyName = dr["therapyName"].ToString(),
                    therapyDescription = dr["therapyDescription"].ToString()
                };
                therapyList.Add(newTherapy);
            }

            if (retVal == 1)
                return null;
            return therapyList;
        }

        public List<Game> ListAllGamesForTherapy(int therapyId)
        {
            List<Game> gameList = new List<Game>();
            string errMsg = string.Empty;

            SqlParameter[] varParams = new SqlParameter[3];
            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParams[2] = new SqlParameter("@therapyId", therapyId);

            DataTable dt = _dbConnect.RunProcedureGetDataTable("dbo.spListGameForTherapy", varParams);
            string error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
            int retVal = Int32.Parse(varParams[1].Value.ToString());
            foreach (DataRow dr in dt.Rows)
            {
                Game newGame = new Game
                {     
                gameId = int.Parse(dr["gameId"].ToString()),
                gameName = dr["gameName"].ToString(),
                gameDescription = dr["gameDescription"].ToString()
                };
                gameList.Add(newGame);
            }

            if (retVal == 1)
                return null;
            return gameList;
        }


        public user GetParentProfile(int childId)
        {
            string errMsg = string.Empty;
            int parentId = 0;

            SqlParameter[] varParams = new SqlParameter[3];
            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParams[2] = new SqlParameter("@childId", childId);

            DataTable dt = _dbConnect.RunProcedureGetDataTable("dbo.spGetParentId", varParams);
            string error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
            int retVal = Int32.Parse(varParams[1].Value.ToString());
            foreach (DataRow dr in dt.Rows)
            {
                user parent = new user();
                parentId = int.Parse(dr["parentId"].ToString());
            }
            if (retVal == 1)
                return null;
            return GetUserProfile(parentId);
        }

        public user GetProviderProfile(int patientId)
        {
            string errMsg = string.Empty;
            int providerId = 0;

            SqlParameter[] varParams = new SqlParameter[3];
            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParams[2] = new SqlParameter("@patientId", patientId);

            DataTable dt = _dbConnect.RunProcedureGetDataTable("dbo.spGetProviderId", varParams);
            string error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
            int retVal = Int32.Parse(varParams[1].Value.ToString());
            foreach (DataRow dr in dt.Rows)
            {
                providerId = int.Parse(dr["providerId"].ToString());
            }
            if (retVal == 1)
                return null;
            return GetUserProfile(providerId);
        }

        public PatientGames ListGameAssignment(int patientId)
        {
            PatientGames patientGameAssign = new PatientGames();
            string errMsg = string.Empty;

            SqlParameter[] varParams = new SqlParameter[3];
            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParams[2] = new SqlParameter("@patientId", patientId);

            DataTable dt = _dbConnect.RunProcedureGetDataTable("dbo.spListGameAssignment", varParams);
            string error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
            int retVal = Int32.Parse(varParams[1].Value.ToString());
            patientGameAssign.patientId = patientId;
            patientGameAssign.gameAssignments = new List<GameAssignment>();

            foreach (DataRow dr in dt.Rows)
            {
                GameAssignment gameAssign = new GameAssignment
                {
                    game = new Game
                    { 
                           therapy = new Therapy
                           {
                           therapyId = int.Parse(dr["therapyId"].ToString()),
                           therapyName = dr["therapyName"].ToString(),
                           therapyDescription = dr["therapyDescription"].ToString()
                            },
                        gameId = int.Parse(dr["gameId"].ToString()),
                        gameName = dr["gameName"].ToString(),
                        gameDescription = dr["gameDescription"].ToString()
                   },
                    startDate = dr["startDate"].ToString(),
                    endDate = dr["endDate"].ToString(),
                    level = int.Parse(dr["level"].ToString())
                    
                };
                patientGameAssign.gameAssignments.Add(gameAssign);
            }

            if (retVal == 1)
                return null;
            return patientGameAssign;
        }

        public PatientGameScore ListGameScoresForPatient(PatientGameScore patientGameScore)
        {
            string errMsg = string.Empty;

            SqlParameter[] varParams = new SqlParameter[5];
            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParams[2] = new SqlParameter("@patientId", patientGameScore.patientId);
            varParams[3] = new SqlParameter("@level", patientGameScore.level);
            varParams[4] = new SqlParameter("@gameId", patientGameScore.game.gameId);

            DataTable dt = _dbConnect.RunProcedureGetDataTable("dbo.spListGameScores", varParams);
            string error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
            int retVal = Int32.Parse(varParams[1].Value.ToString());

            patientGameScore.gameScoreList = new List<GameScore>();

            foreach (DataRow dr in dt.Rows)
            {
                DateTime startTime = (DateTime) dr["startTime"];
                DateTime endTime = (DateTime)dr["endTime"];
              
                GameScore gameScore = new GameScore
                {
                score = int.Parse(dr["gameScore"].ToString()),
                datePlayed = startTime.Date.ToString(CultureInfo.InvariantCulture),
                durationInMin = (endTime - startTime)
                   
                };
                patientGameScore.gameScoreList.Add(gameScore);
            }

            if (retVal == 1)
                return null;
            return patientGameScore;
        }


        public bool SetGameAssignment(PatientGames patientGames)
        {
            string errMsg = string.Empty;

            SqlParameter[] varParams = new SqlParameter[7];
            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParams[2] = new SqlParameter("@patientId", patientGames.patientId);
            varParams[3] = new SqlParameter("@level", patientGames.gameAssignments[0].level);
            varParams[4] = new SqlParameter("@gameId", patientGames.gameAssignments[0].game.gameId);
            varParams[5] = new SqlParameter("@startDate", patientGames.gameAssignments[0].startDate);
            varParams[6] = new SqlParameter("@endDate", patientGames.gameAssignments[0].endDate);

            DataTable dt = _dbConnect.RunProcedureGetDataTable("dbo.spAddGameAssignment", varParams);
            string error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
            int retVal = Int32.Parse(varParams[1].Value.ToString());

            if (retVal == 1)
                return false;
            return true;
        }

        public bool DeleteGameAssignment(PatientGames patientGames)
        {
            string errMsg = string.Empty;

            SqlParameter[] varParams = new SqlParameter[7];
            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParams[2] = new SqlParameter("@patientId", patientGames.patientId);
            varParams[3] = new SqlParameter("@level", patientGames.gameAssignments[0].level);
            varParams[4] = new SqlParameter("@gameId", patientGames.gameAssignments[0].game.gameId);
            varParams[5] = new SqlParameter("@startDate", patientGames.gameAssignments[0].startDate);
            varParams[6] = new SqlParameter("@endDate", patientGames.gameAssignments[0].endDate);

            DataTable dt = _dbConnect.RunProcedureGetDataTable("dbo.spDeleteGameAssignment", varParams);
            string error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
            int retVal = Int32.Parse(varParams[1].Value.ToString());

            if (retVal == 1)
                return false;
            return true;
        }


    }
}