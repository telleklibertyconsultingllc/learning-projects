CREATE PROCEDURE [dbo].[udpCustomerView]
	@rankId int,
	@value VARCHAR(5) OUTPUT
AS
	DECLARE @NewId INT;
	SET @NewId = 1;
	PRINT @NewId;
	/* test */
	PRINT 'TEST';
	SET @value = 'PRINTED';

	SELECT
		VWCR.Id,
		VWCR.RankName
	FROM DBO.vwCustomerRanking AS VWCR
	WHERE VWCR.Id = @rankId
GO

