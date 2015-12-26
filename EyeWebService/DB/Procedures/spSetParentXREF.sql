USE EYE_Database
GO
/****** Object:  StoredProcedure [dbo].[[[spSetParentXREF]]]    Script Date: 12/23/2015 1:23:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[spSetParentXREF](
             @parentLogin  VARCHAR(12)
			,@PatientFirstName        VARCHAR(30)
			,@patientLastName        VARCHAR(30)
			,@providerFirstName     VARCHAR(30)
			,@providerLastName     VARCHAR(30)
			,@Debug            BIT = 0
			,@Error_Message    VARCHAR (1024) = NULL OUTPUT)
    
AS

BEGIN

     
      DECLARE @Return_Code           INT
            , @Object_Name           VARCHAR (256)
			,@patientId INT
			,@providerId INT
			,@parentId INT
      
      -- =============================================================================================================================================== --
      --                                                                                                                                                 --
      --                                                V A R I A B L E   I N I T I A L I Z A T I O N                                                    --
      --                                                                                                                                                 --
      -- =============================================================================================================================================== --
      
      SET @Return_Code                = 0
      SET @Object_Name                = 'set parent XREF-- : --'
      
      -- =============================================================================================================================================== --
      --                                                                                                                                                 --
      --                                                                                        V A L I D A T I O N S                                                   --
      --                                                                                                                                                 --
      -- =============================================================================================================================================== --
      

            BEGIN TRY              
                  IF (ISNULL(@parentLogin,'')='' or ISNULL(@PatientFirstName,'')='' or ISNULL(@patientLastName,'')='')
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
				IF NOT EXISTS(select userId FROM dbo.tblUser where LoginName=@parentLogin)
				RAISERROR('Parent login does not exist', 16, 1) 
				IF NOT EXISTS(select userId FROM dbo.tblUser where firstName=@PatientFirstName AND lastName=@patientLastName)
				RAISERROR('Patient does not exist', 16, 1)
				IF NOT EXISTS(select userId FROM dbo.tblUser where firstName=@providerFirstName AND lastName=@providerLastName)
				RAISERROR('provider does not exist', 16, 1) 

				select @parentId=userId FROM dbo.tblUser where LoginName=@parentLogin
				select @patientId=userId FROM dbo.tblUser where firstName=@PatientFirstName AND lastName=@patientLastName
				select @providerId=userId FROM dbo.tblUser where firstName=@providerFirstName AND lastName=@providerLastName

				IF EXISTS(select * FROM dbo.tblParentXREF where parentId=@parentId AND patientId=@patientId)
				BEGIN
			     UPDATE dbo.tblParentXREF SET providerId=@providerId where parentId=@parentId AND patientId=@patientId;
                END
			    ELSE
				BEGIN
				INSERT INTO dbo.tblParentXREF values(@parentId,@patientId,@providerId);
				END	       
			
            
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

GRANT EXECUTE ON dbo.[spSetParentXREF] TO dbExecutor