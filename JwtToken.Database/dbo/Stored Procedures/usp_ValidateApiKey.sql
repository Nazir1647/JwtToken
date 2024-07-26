
CREATE PROCEDURE usp_ValidateApiKey  
    @Apikey NVARCHAR(100)  
AS  
BEGIN  
    SET NOCOUNT ON;  
  
    DECLARE @IsValid BIT;  
  
    SELECT @IsValid = IsValid FROM ApiKeys WHERE ApiKey = @Apikey;  
  
    IF @IsValid IS NULL  
    BEGIN  
        SET @IsValid = 0; -- If the API key is not found, set the result to false  
    END  
  
    SELECT @IsValid AS message;  
END;