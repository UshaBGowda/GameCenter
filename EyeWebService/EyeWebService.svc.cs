using EyeWebService.UtilClasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace EyeWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : EyeWebService
    {
        DBConnect _dbConnect = new DBConnect();
        public bool CreateLogin(Login newLogin)
        {
            SqlParameter[] varParams = new SqlParameter[5];

            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParams[2] = new SqlParameter("@LoginName", newLogin.LoginName);
            varParams[3] = new SqlParameter("@Password", newLogin.Password);
            varParams[4] = new SqlParameter("@Email", newLogin.emailID);

            DataTable dt = _dbConnect.RunProcedureGetDataTable("dbo.spCreateLogin", varParams);
            string error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
            int retVal = Int32.Parse(varParams[1].Value.ToString());

                if (retVal == 1)
                    return false;
                else
                    return true;

        }

        public bool Authenticate(string username, string password)
        {
            SqlParameter[] varParams = new SqlParameter[4];

            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParams[2] = new SqlParameter("@LoginName", username);
            varParams[3] = new SqlParameter("@Password", password);

            DataTable dt = _dbConnect.RunProcedureGetDataTable("dbo.spAuthenticate", varParams);
            string error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
            int retVal = Int32.Parse(varParams[1].Value.ToString());

            if (retVal == 1)
                return false;
            else
                return true;
        }

        public bool setPassword(string username, string password)
        {
            SqlParameter[] varParams = new SqlParameter[4];

            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParams[2] = new SqlParameter("@LoginName", username);
            varParams[3] = new SqlParameter("@Password", password);

            DataTable dt = _dbConnect.RunProcedureGetDataTable("dbo.spSetPassword", varParams);
            string error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
            int retVal = Int32.Parse(varParams[1].Value.ToString());

            if (retVal == 1)
                return false;
            else
                return true;
        }

        public bool setProfile(user newUser)
        {
            SqlParameter[] varParams = new SqlParameter[8];

            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParams[2] = new SqlParameter("@firstname", newUser.firstName);
            varParams[3] = new SqlParameter("@lastName", newUser.lastName);
            varParams[4] = new SqlParameter("@loginName", newUser.loginName);
            varParams[5] = new SqlParameter("@userType", newUser.userType);
            varParams[6] = new SqlParameter("@dateOfBirth", newUser.dob);
            varParams[7] = new SqlParameter("@gender", newUser.gender);

            DataTable dt = _dbConnect.RunProcedureGetDataTable("dbo.spCreateUpdateUser", varParams);
            string error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
            int retVal = Int32.Parse(varParams[1].Value.ToString());

            if (retVal == 1)
                return false;

            foreach (Address address in newUser.addressList)
            {
                SqlParameter[] varParamsNew = new SqlParameter[10];

                varParamsNew[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
                varParamsNew[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
                varParamsNew[2] = new SqlParameter("@addressType", address.addressType);
                varParamsNew[3] = new SqlParameter("@loginName", newUser.loginName);
                varParamsNew[4] = new SqlParameter("@streetName", address.streetName);
                varParamsNew[5] = new SqlParameter("@city", address.city);
                varParamsNew[6] = new SqlParameter("@stateName", address.stateName);
                varParamsNew[7] = new SqlParameter("@country", address.country);
                varParamsNew[8] = new SqlParameter("@zipcode", address.zipcode);
                varParamsNew[9] = new SqlParameter("@phoneNo", address.phoneNo);

                DataTable dtNew = _dbConnect.RunProcedureGetDataTable("dbo.spCreateUpdateAddress", varParamsNew);
                string errorNew = varParamsNew[0].Value == DBNull.Value ? "" : varParamsNew[1].Value.ToString();
                int retValNew = Int32.Parse(varParamsNew[1].Value.ToString());

                if (retValNew == 1)
                    return false;

                }
            return true;

        }



        public bool setChildProfile(patient newPatient)
        {
            SqlParameter[] varParams = new SqlParameter[8];

            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParams[2] = new SqlParameter("@firstname", newPatient.firstName);
            varParams[3] = new SqlParameter("@lastName", newPatient.lastName);
            varParams[4] = new SqlParameter("@loginName", newPatient.firstName + newPatient.lastName);
            varParams[5] = new SqlParameter("@userType", "Patient");
            varParams[6] = new SqlParameter("@dateOfBirth", newPatient.dob);
            varParams[7] = new SqlParameter("@gender", newPatient.gender);

            DataTable dt = _dbConnect.RunProcedureGetDataTable("dbo.spCreateUpdateUser", varParams);
            string error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
            int retVal = Int32.Parse(varParams[1].Value.ToString());

            if (retVal == 1)
                return false;

                SqlParameter[] varParamsNew = new SqlParameter[7];

                varParamsNew[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
                varParamsNew[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
                varParamsNew[2] = new SqlParameter("@parentLogin", newPatient.parentLoginName);
                varParamsNew[3] = new SqlParameter("@PatientFirstName",newPatient.firstName);
                varParamsNew[4] = new SqlParameter("@patientLastName",newPatient.lastName);
                varParamsNew[5] = new SqlParameter("@providerFirstName",newPatient.providerFirstName);
                varParamsNew[6] = new SqlParameter("@providerLastName",newPatient.providerLastName);

                DataTable dtNew = _dbConnect.RunProcedureGetDataTable("dbo.spSetParentXREF", varParamsNew);
                string errorNew = varParamsNew[0].Value == DBNull.Value ? "" : varParamsNew[1].Value.ToString();
                int retValNew = Int32.Parse(varParamsNew[1].Value.ToString());

                if (retValNew == 1)
                    return false;
                else
                   return true;

        }

        public bool deleteProfile(string firstName, string lastName)
        {
            SqlParameter[] varParams = new SqlParameter[4];

            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParams[2] = new SqlParameter("@firstname", firstName);
            varParams[3] = new SqlParameter("@lastName", lastName);
            DataTable dt = _dbConnect.RunProcedureGetDataTable("dbo.spDeleteUser", varParams);
            string error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
            int retVal = Int32.Parse(varParams[1].Value.ToString());

            if (retVal == 1)
                return false;
            else
                return true;
        }


        public List<patient> listChildrenProfile(string parentLogin)
        {
            List<patient> childrenList= new List<patient>();
            string errMsg = string.Empty;

            SqlParameter[] varParams = new SqlParameter[3];
            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParams[2] = new SqlParameter("@parentLogin", parentLogin);
            
                DataTable dt = _dbConnect.RunProcedureGetDataTable("dbo.spListChildrenProfile", varParams);
                string error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
                int retVal = Int32.Parse(varParams[1].Value.ToString());
                foreach(DataRow dr in dt.Rows)
                {
                    patient newChild = new patient();
                    newChild.firstName = dr["childFN"].ToString();
                    newChild.lastName = dr["childLN"].ToString();
                    newChild.dob = dr["childDOB"].ToString();
                    newChild.gender = dr["childGender"].ToString();
                    newChild.providerFirstName = dr["providerFN"].ToString();
                    newChild.providerLastName = dr["providerLN"].ToString();
                    childrenList.Add(newChild);
                }

                if (retVal == 1)
                    return null;
                else
                    return childrenList;
            }

        


        public List<patient> listPatientsProfile(string providerLogin)
        {
            List<patient> patientList = new List<patient>();
            string errMsg = string.Empty;

            SqlParameter[] varParams = new SqlParameter[3];
            varParams[0] = _dbConnect.MakeParameter("Error_Message", ParameterDirection.Output, -1, string.Empty);
            varParams[1] = _dbConnect.MakeParameter("Return_Code", ParameterDirection.ReturnValue, -1, 0);
            varParams[2] = new SqlParameter("@providerLogin", providerLogin);

            DataTable dt = _dbConnect.RunProcedureGetDataTable("dbo.spListPatientsProfile", varParams);
            string error = varParams[0].Value == DBNull.Value ? "" : varParams[1].Value.ToString();
            int retVal = Int32.Parse(varParams[1].Value.ToString());
            foreach (DataRow dr in dt.Rows)
            {
                patient newPatient = new patient();
                newPatient.firstName = dr["childFN"].ToString();
                newPatient.lastName = dr["childLN"].ToString();
                newPatient.dob = dr["childDOB"].ToString();
                newPatient.gender = dr["childGender"].ToString();
                patientList.Add(newPatient);
            }

            if (retVal == 1)
                return null;
            else
                return patientList;
            }

        }
    }
