USE EYE_Database
GO
/****** Object:  StoredProcedure [dbo].[spCreateUpdateAddress]    Script Date: 12/23/2015 1:23:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[spCreateUpdateAddress](
			 @addressType  VARCHAR(20)
			,@loginId   nvarchar(128)
			,@streetName   VARCHAR(20)
			,@city        VARCHAR(20)
			,@stateName   VARCHAR(10)
			,@country    VARCHAR(15)
			,@zipcode VARCHAR(15)
			,@phoneNo VARCHAR(20)
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
      SET @Object_Name                = 'Create or set address-- : --'
      
      -- =============================================================================================================================================== --
      --                                                                                                                                                 --
      --                                                                                        V A L I D A T I O N S                                                   --
      --                                                                                                                                                 --
      -- =============================================================================================================================================== --
      

            BEGIN TRY              
                  IF (ISNULL(@addressType,'')='' or ISNULL(@loginId,'')='')
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
				IF EXISTS(select * FROM dbo.tblAddress where loginId=@loginId and addressType=@addressType)
				BEGIN
			     UPDATE dbo.tblAddress SET streetName=@streetName,city=@city,stateName=@stateName,country=@country,zipcode=@zipcode,@phoneNo=@phoneNo where loginId=@loginId and addressType=@addressType;
                END
			    ELSE
				BEGIN
				INSERT INTO dbo.tblAddress(addressType,loginId,streetName,city,stateName,country,zipcode,phoneNo) values(@addressType,@loginId,@streetName,@city,@stateName,@country,@zipcode,@phoneNo);
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

GRANT EXECUTE ON dbo.[spCreateUpdateAddress] TO dbExecutor
