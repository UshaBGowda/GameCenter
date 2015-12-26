using EyeWebService.UtilClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace EyeWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface EyeWebService
    {

        [OperationContract]
        bool CreateLogin(Login newLogin);

        [OperationContract]
        bool Authenticate(string username, string password);

        [OperationContract]
        bool setPassword(string username, string password);

        [OperationContract]
        bool setProfile(user newUser);

        [OperationContract]
        bool deleteProfile(string firstName, string lastName);

        [OperationContract]
        bool setChildProfile(patient newPatient);

       [OperationContract]
        List<patient> listChildrenProfile(string parentLogin);

        [OperationContract]
       List<patient> listPatientsProfile(string providerLogin);


        // TODO: Add your service operations here
    }
    }
