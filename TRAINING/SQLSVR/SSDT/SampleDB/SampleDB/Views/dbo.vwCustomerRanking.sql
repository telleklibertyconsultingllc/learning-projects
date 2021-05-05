CREATE VIEW [dbo].[vwCustomerRanking]
AS 
	SELECT 
		Id,
		RankName
	FROM [Ranking]
	WHERE Id = 1
GO;
