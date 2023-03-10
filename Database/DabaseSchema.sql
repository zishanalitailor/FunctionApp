USE [WalletFunctionApp]
GO
/****** Object:  Table [dbo].[TransactionDetails]    Script Date: 09/03/2023 10:59:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionDetails](
	[TransactionID] [int] IDENTITY(1,1) NOT NULL,
	[Id] [int] NOT NULL,
	[Amount] [decimal](18, 0) NOT NULL,
	[Direction] [varchar](50) NOT NULL,
	[AccountID] [int] NOT NULL,
	[TimeStamp] [datetime] NOT NULL,
 CONSTRAINT [PK_TransactionDetails] PRIMARY KEY CLUSTERED 
(
	[TransactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wallet]    Script Date: 09/03/2023 10:59:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wallet](
	[AccountID] [int] IDENTITY(1,1) NOT NULL,
	[AccountBalance] [decimal](18, 0) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,	
 CONSTRAINT [PK_Wallet] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[TransactionDetails] ON 

INSERT [dbo].[TransactionDetails] ([TransactionID], [Id], [Amount], [Direction], [AccountID], [TimeStamp]) VALUES (3, 10002, CAST(200 AS Decimal(18, 0)), N'Debit', 1, CAST(N'2023-03-08T16:44:32.813' AS DateTime))
INSERT [dbo].[TransactionDetails] ([TransactionID], [Id], [Amount], [Direction], [AccountID], [TimeStamp]) VALUES (4, 10002, CAST(200 AS Decimal(18, 0)), N'Debit', 1, CAST(N'2023-03-08T17:00:55.260' AS DateTime))
SET IDENTITY_INSERT [dbo].[TransactionDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Wallet] ON 

INSERT [dbo].[Wallet] ([AccountID], [AccountBalance], [FirstName], [LastName]) VALUES (1, CAST(600 AS Decimal(18, 0)), N'Ali', N'Tailor')
INSERT [dbo].[Wallet] ([AccountID], [AccountBalance], [FirstName], [LastName]) VALUES (2, CAST(2500 AS Decimal(18, 0)), N'Sara', N'Tailor')
SET IDENTITY_INSERT [dbo].[Wallet] OFF
GO
ALTER TABLE [dbo].[TransactionDetails]  WITH CHECK ADD  CONSTRAINT [FK_TransactionDetails_Wallet] FOREIGN KEY([AccountID])
REFERENCES [dbo].[Wallet] ([AccountID])
GO
ALTER TABLE [dbo].[TransactionDetails] CHECK CONSTRAINT [FK_TransactionDetails_Wallet]
GO
/****** Object:  StoredProcedure [dbo].[AddTransactionUpdateWalletgivenAccount_sp]    Script Date: 09/03/2023 10:59:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddTransactionUpdateWalletgivenAccount_sp]
	@Id as int,
	@Amount as decimal(18,0),
	@Direction as varchar(50),
	@AccountID as int	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRY

	-- Insert data to Transaction Details
	INSERT INTO TransactionDetails (Id, Amount, Direction, AccountID, TimeStamp)
	VALUES (@Id, @Amount, @Direction, @AccountID, GETDATE());

	-- Deduct the amount from Wallet
	UPDATE Wallet SET AccountBalance = (AccountBalance - @Amount) WHERE AccountID = @AccountID;
   
	END TRY
	BEGIN CATCH
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT 
			@ErrorMessage = ERROR_MESSAGE(),
			@ErrorSeverity = ERROR_SEVERITY(),
			@ErrorState = ERROR_STATE();

	 
		RAISERROR (@ErrorMessage,
				   @ErrorSeverity,
				   @ErrorState);
	END CATCH;

	

END
GO
