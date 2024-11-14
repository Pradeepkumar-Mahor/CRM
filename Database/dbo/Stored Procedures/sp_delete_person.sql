/* DELETE OPERATION */
CREATE PROCEDURE sp_delete_person(@id INT)
AS
BEGIN
    DELETE FROM dbo.Person WHERE Id = @id
END;