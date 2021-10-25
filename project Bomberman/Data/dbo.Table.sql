CREATE TABLE [dbo].[HighScores]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY,
	[HighScore] INT NOT NULL,
	[Player] NCHAR(10) NOT NULL,
	[Date] DATE NOT NULL, 
    [Player2] NCHAR(10) NOT NULL, 
    [Score] INT NULL

)
