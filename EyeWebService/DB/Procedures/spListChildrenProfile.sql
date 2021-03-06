USE EYE_Database
GO
/****** Object:  StoredProcedure [dbo].[spListChildrenProfile]    Script Date: 12/23/2015 1:23:44 PM ******/

--DECLARE @RC int
--DECLARE @userId int
--DECLARE @Debug bit
--DECLARE @Error_Message varchar(1024)

---- TODO: Set parameter values here.
--SET @userId = 1
--SET @Debug =0 

--EXECUTE @RC = [dbo].[spListChildrenProfile] 
--   @userId
--  ,@Debug
--  ,@Error_Message OUTPUT
--GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[spListChildrenProfile](
             @userId  INT
			,@Debug            BIT = 0
			,@Error_Message    VARCHAR (1024) = NULL OUTPUT)
    
AS

BEGIN

     
      DECLARE @Return_Code           INT
            , @Object_Name           VARCHAR (256)
			,@parentId     INT
      
      -- =============================================================================================================================================== --
      --                                                                                                                                                 --
      --                                                V A R I A B L E   I N I T I A L I Z A T I O N                                                    --
      --                                                                                                                                                 --
      -- =============================================================================================================================================== --
      
      SET @Return_Code                = 0
      SET @Object_Name                = 'List children profile-- : --'
      
      -- =============================================================================================================================================== --
      --                                                                                                                                                 --
      --                                                                                        V A L I D A T I O N S                                                   --
      --                                                                                                                                                 --
      -- =============================================================================================================================================== --
      

            BEGIN TRY              
                  IF (ISNULL(@userId,'')='')
				       RAISERROR('Invalid/empty Input.', 16, 1)               
            END TRY

            
            BEGIN CATCH
            
                  SET @Error_Message = ERROR_MESSAGE()
                  SET @Error_Message = @Object_Name + ISNULL(@Error_Message, '')
                  SET @Return_Code = 1
                  GOTO Procedure_Exit
                        
            END CATCH
            
      -- =============================================================================================================================================== --
      --                                                                                                                                                 --
      --                                                        U P D A T I O N S                                                   --
      --                                                                                                                                                 --
      -- =============================================================================================================================================== --
      BEGIN TRANSACTION
                              
            BEGIN TRY

				--select A.userId as patientId,B.providerId as providerId,B.parentId as parentId,A.firstName as childFN,
				--A.lastName as childLN,convert(varchar(10), cast(A.dateOfBirth as date), 101)  as childDOB,
				--G.Id as genderId,G.Name as genderName,C.firstName as providerFN,C.lastName as providerLN from dbo.tblUser A 
				--JOIN dbo.tblParentXREF B ON A.userId=B.patientId 
				--JOIN dbo.tblGender G ON A.gender = G.genderId
				--JOIN dbo.tblUser C ON B.providerId=C.userId where B.parentId=@userId;

				select A.userId as patientId,B.providerId as providerId,B.parentId as parentId,A.firstName as childFN,
				A.lastName as childLN,convert(varchar(10), cast(A.dateOfBirth as date), 101)  as childDOB,
				A.gender as gender,C.firstName as providerFN,C.lastName as providerLN from dbo.tblUser A 
				JOIN dbo.tblParentXREF B ON A.userId=B.patientId 
				JOIN dbo.tblUser C ON B.providerId=C.userId where B.parentId=@userId;
            
            END TRY

            BEGIN CATCH

                  SET @Error_Message = ERROR_MESSAGE()
                        SET @Error_Message = @Object_Name + ISNULL(@Error_Message, '')
                        SET @Return_Code = 1
                        GOTO Procedure_Exit
                        
            END CATCH
      
      COMMIT TRANSACTION      
      
                        
      Procedure_Exit:
      
      IF XACT_STATE() <> 0 
            ROLLBACK TRANSACTION

      RETURN @Return_Code

END

GRANT EXECUTE ON dbo.[spListChildrenProfile] TO dbExecutor