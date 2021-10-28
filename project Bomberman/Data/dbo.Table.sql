CREATE TABLE [dbo].[HighScores]
(
	[Player1Name] NCHAR(10) NOT NULL PRIMARY KEY, 
    [Player1Score] INT NOT NULL, 
    [Player2Name] NCHAR(10) NOT NULL, 
    [Player2Score] INT NOT NULL
)
