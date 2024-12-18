CREATE PROCEDURE [dbo].[sp_InsertProduto]
	@nomeLivro varchar(50)
AS
BEGIN
	DECLARE @idProduto int = (SELECT MAX(Id) FROM Produto) + 1
	INSERT INTO Produto (id, NomeLivro) values (@idProduto, @nomeLivro)
END
