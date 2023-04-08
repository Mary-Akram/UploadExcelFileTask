CREATE TABLE [dbo].[AAISDetail](
	[WO_Number] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Location] [varchar](200) NULL,
	[Status] [varchar](50) NULL,
	[Type] [varchar](100) NULL,
	[Priority] [varchar](100) NULL,
	[Scheduled_FinishDate] [datetime] NULL,
	[Completion_Date] [datetime] NULL,
	[StartDate] [datetime] NULL,
	[State] [varchar](100) NULL,
	[Reason] [nvarchar](max) NULL,
 CONSTRAINT [PK__Details__9CD6C177AA041CEB] PRIMARY KEY CLUSTERED 
(
	[WO_Number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
