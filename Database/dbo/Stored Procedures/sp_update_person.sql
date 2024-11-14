CREATE PROCEDURE sp_update_person (
	@id INT
	,@GuidId NVARCHAR(max)
	,@UserName NVARCHAR(100)
	,@FirstName NVARCHAR(100)
	,@LastName NVARCHAR(100)
	,@Gender NVARCHAR(100)
	,@Email NVARCHAR(100)
	,@MobileNo NVARCHAR(15)
	,@Address NVARCHAR(200)
	,@Password NVARCHAR(25)
	,@ConfirmPassword NVARCHAR(25)
	,@IsActive INT
	,@CreatedBy NVARCHAR(50)
	,@CreatedOn DATETIME
	)
AS
BEGIN
	UPDATE dbo.Person
	SET [UserName] = @UserName
		,[FirstName] = @FirstName
		,[LastName] = @LastName
		,[Gender] = @Gender
		,[Email] = @Email
		,[MobileNo] = @MobileNo
		,[Address] = @Address
		,[Password] = @Password
		,[ConfirmPassword] = @ConfirmPassword
		,[IsActive] = @IsActive
		,[CreatedBy] = @CreatedBy
		,[CreatedOn] = @CreatedOn
	WHERE Id = @id
		AND [GuidId] = @GuidId
END;
