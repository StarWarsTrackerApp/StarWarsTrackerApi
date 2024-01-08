CREATE TABLE [dbo].[EventDate]
(
	[Id] INT NOT NULL CONSTRAINT [PK_EventDate] PRIMARY KEY IDENTITY(1, 1),
	[EventId] INT NOT NULL CONSTRAINT [FK_Event_EventDate] FOREIGN KEY REFERENCES [Event](Id),
	[EventDateTypeId] INT NOT NULL CONSTRAINT [FK_EventDateType_EventDate] FOREIGN KEY REFERENCES EventDateType(Id),
	[YearsSinceBattleOfYavin] INT NOT NULL,
	[Sequence] INT NOT NULL CONSTRAINT [DF_EventDate_Sequence] DEFAULT 0,	
)
