USE EYE_Database
GO
/****** Object:  StoredProcedure [dbo].[spCreateUpdateUser]    Script Date: 12/23/2015 1:23:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[spCreateUpdateUser](

			 @firstName        VARCHAR(30)
			,@lastName        VARCHAR(30)
			,@loginId        nvarchar(128) = NULL
			,@userId        int = 0 OUTPUT
			,@userTypeId     INT
			,@dateOfBirth DATE
	        ,@gender char
			,@Debug            BIT = 0
			,@Error_Message    VARCHAR (1024) = NULL OUTPUT)
    
AS

BEGIN

     
      DECLARE @Return_Code           INT
            , @Object_Name           VARCHAR (256)
      
      -- =============================================================================================================================================== --
      --                                                                                                                                                 --
      --                                                V A R I A B L E   I N I T I A L I Z A T I O N                                                    --
      --                                                                                                                                                 --
      -- =============================================================================================================================================== --
      
      SET @Return_Code                = 0
      SET @Object_Name                = 'Create user-- : --'
      
      -- =============================================================================================================================================== --
      --                                                                                                                                                 --
      --                                                                                        V A L I D A T I O N S                                                   --
      --                                                                                                                                                 --
      -- =============================================================================================================================================== --
      

            BEGIN TRY              
                  IF (ISNULL(@firstName,'')='' or ISNULL(@lastName,'')='')
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
				IF EXISTS(select * FROM dbo.tblUser where userId=@userId)
				BEGIN
					UPDATE dbo.tblUser SET firstName=@firstName,lastName=@lastName,userTypeId=@userTypeId,dateOfBirth=@dateOfBirth,gender=@gender where userId=@userId;
                END
			    ELSE
				BEGIN
					INSERT INTO dbo.tblUser(firstName,lastName,loginId,userTypeId,dateOfBirth,gender) values(@firstName,@lastName,@loginId,@userTypeId,@dateOfBirth,@gender);
					SET @userId = @@IDENTITY;
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

GRANT EXECUTE ON dbo.[spCreateUpdateUser] TO dbExecutor
