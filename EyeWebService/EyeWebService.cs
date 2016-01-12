using System.Collections.Generic;
using System.ServiceModel;
using EyeWebService.UtilClasses;

/// <summary>
/// 
/// </summary>
namespace EyeWebService
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface EyeWebService
    {

        /// <summary>
        /// Sets the profile.
        /// </summary>
        /// <param name="newUser">The new user.</param>
        /// <returns></returns>
        [OperationContract]
        bool SetProfile(user newUser);

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <param name="loginId">The login identifier.</param>
        /// <returns></returns>
        [OperationContract]
        int GetUserId(string loginId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        int GetUserTypeId(int userId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Patient> ListChildrenProfile(int parentId);

         /// <summary>
         /// 
         /// </summary>
         /// <param name="patientId"></param>
         /// <returns></returns>
         [OperationContract]
         Patient GetPatientProfile(int patientId);

         /// <summary>
         /// 
         /// </summary>
         /// <returns></returns>
         [OperationContract]
         List<user> ListAllProviderProfile();

         /// <summary>
         /// 
         /// </summary>
         /// <param name="newPatient"></param>
         /// <returns></returns>
         [OperationContract]
         Patient SetChildProfile(Patient newPatient);

         [OperationContract]
        List<Patient> ListPatientsProfile(int providerId);

        
        [OperationContract]
        bool DeleteProfile(int userId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        user GetUserProfile(int userId);

        [OperationContract]
        user GetParentProfile(int childId);

        [OperationContract]
        user GetProviderProfile(int patientId);


        [OperationContract]
        int CreateUpdateTherapy(Therapy newTherapy);

        [OperationContract]
        int CreateUpdateGame(Game newGame);

        [OperationContract]
        List<Therapy> ListAllTherapy();

        [OperationContract]
        List<Game> ListAllGamesForTherapy(int therapyId);

        [OperationContract]
        bool DeleteGame(int gameId);

        [OperationContract]
        bool DeleteTherapy(int therapyId);

        // TODO: Add your service operations here
    }
}
