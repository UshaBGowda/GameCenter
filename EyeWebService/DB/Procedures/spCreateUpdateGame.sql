USE EYE_Database
GO
/****** Object:  StoredProcedure [dbo].[[spCreateUpdateGame]]    Script Date: 12/23/2015 1:23:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[spCreateUpdateGame](

			  @gameName        VARCHAR(30)
			 ,@therapyName        VARCHAR(30)
			, @gameDescription VARCHAR(50)
			, @Debug            BIT = 0
			, @Error_Message    VARCHAR (1024) = NULL OUTPUT)
    
AS

BEGIN

     
      DECLARE @Return_Code           INT
	         ,@therapyId INT
            , @Object_Name           VARCHAR (256)
      
      -- =============================================================================================================================================== --
      --                                                                                                                                                 --
      --                                                V A R I A B L E   I N I T I A L I Z A T I O N                                                    --
      --                                                                                                                                                 --
      -- =============================================================================================================================================== --
      
      SET @Return_Code                = 0
      SET @Object_Name                = 'Create or update game-- : --'
      
      -- =============================================================================================================================================== --
      --                                                                                                                                                 --
      --                                                                                        V A L I D A T I O N S                                                   --
      --                                                                                                                                                 --
      -- =============================================================================================================================================== --
      

            BEGIN TRY              
                  IF (ISNULL(@therapyName,'')='' or ISNULL(@gameName,'')='')
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
			 
			    SELECT @therapyId=therapyId FROM dbo.tblTherapy where therapyName=@therapyName;

				IF EXISTS(select * FROM dbo.tblGame where gameName=@gameName)
				BEGIN
			    UPDATE dbo.tblGame SET gameName=@gameName,gameDescription=@gameDescription,therapyId=@therapyId where gameName=@gameName;
                END
			    ELSE
				BEGIN
				INSERT INTO dbo.tblGame(gameName,gameDescription,therapyId) values(@gameName,@gameDescription,@therapyId);
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

GRANT EXECUTE ON dbo.[spCreateUpdateGame] TO dbExecutor