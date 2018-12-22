CREATE TABLE [dbo].[BankCard]
(
	[Id] UNIQUEIDENTIFIER CONSTRAINT [PK_dbo_BankCard] PRIMARY KEY,
	[UserId] UNIQUEIDENTIFIER CONSTRAINT [FK_dbo_BankCard_User_Id] FOREIGN KEY REFERENCES [dbo].[User] ([Id]),
	[BankAccountId] UNIQUEIDENTIFIER CONSTRAINT [FK_dbo_BankCard_BankAccount_Id] FOREIGN KEY REFERENCES [dbo].[BankAccount] ([Id]),
	[Number] BIGINT NOT NULL CONSTRAINT [UK_dbo_BankCard_Number] UNIQUE,
	[VerificationCode] NVARCHAR(36) NOT NULL, -- md5
	[PinCode] NVARCHAR(36) NOT NULL, -- md5
	[Validity] DATE NOT NULL,
	[Active] BIT NOT NULL,
	CONSTRAINT CHK_BankCard_Number_LEN CHECK (LEN(Number) = 16)
)
