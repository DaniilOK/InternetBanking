CREATE TABLE [dbo].[BankAccount]
(
	[Id] UNIQUEIDENTIFIER CONSTRAINT [PK_dbo_BankAccount] PRIMARY KEY,
	[UserId] UNIQUEIDENTIFIER CONSTRAINT [FK_dbo_BankAccount_dbo_User] FOREIGN KEY REFERENCES [dbo].[User] ([Id]),
	[Number] BIGINT NOT NULL CONSTRAINT [UK_dbo_BankAccount_Number] UNIQUE,
	[Money] MONEY NOT NULL,
	[EndDate] DATE NOT NULL,
	[Active] BIT NOT NULL,
	CONSTRAINT CHK_BankAccount_Number_Len CHECK (LEN(Number) = 18)
)
