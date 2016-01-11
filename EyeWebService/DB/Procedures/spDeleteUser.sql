USE EYE_Database
GO
/****** Object:  StoredProcedure [dbo].[spDeleteUser]    Script Date: 12/23/2015 1:23:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[spDeleteUser](

			  @userId        INT
			, @Debug            BIT = 0
			, @Error_Message    VARCHAR (1024) = NULL OUTPUT)
    
AS

BEGIN

     
      DECLARE @Return_Code INT
			 ,@userTypeId INT
			 ,@loginId nVARCHAR(128)
            , @Object_Name VARCHAR (256)
      
      -- =============================================================================================================================================== --
      --                                                                                                                                                 --
      --                                                V A R I A B L E   I N I T I A L I Z A T I O N                                                    --
      --                                                                                                                                                 --
      -- =============================================================================================================================================== --
      
      SET @Return_Code                = 0
      SET @Object_Name                = 'Delete user-- : --'
      
      -- =============================================================================================================================================== --
      --                                                                                                                                                 --
      --                                                                                        V A L I D A T I O N S                                                   --
      --                                                                                                                                                 --
      -- =============================================================================================================================================== --
      

            BEGIN TRY              
                  IF (ISNULL(@userId,'')='')
				       RAISERROR('Invalid/empty Input.', 16, 1) 
				  IF NOT EXISTS(select * FROM dbo.tblUser where userId=@userId)  
					      RAISERROR('user does not exist', 16, 1)                 
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


				SELECT @userTypeId=userTypeId,@loginId=loginId FROM dbo.tblUser where userId=@userId;

				IF(@userTypeId=2)
				BEGIN
				DELETE FROM tblParentXREF WHERE parentId=@userId;
			    DELETE FROM AspNetUsers WHERE Id=@loginId;
				DELETE FROM tblAddress WHERE loginId=@loginId;
				END
				ELSE IF(@userTypeId=1)
				BEGIN
				DELETE FROM tblParentXREF WHERE providerId=@userId;
				DELETE FROM AspNetUsers WHERE Id=@loginId;
				DELETE FROM tblAddress WHERE loginId=@loginId;
				END
				ELSE
				BEGIN
				DELETE FROM tblParentXREF WHERE patientId=@userId;
				END

				DELETE FROM tblUser WHERE userId=@userId;
            
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

GRANT EXECUTE ON dbo.[spDeleteUser] TO dbExecutor