
/****** Object:  Table [dbo].[sys_equipment_code]    Script Date: 03/27/2014 09:50:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[sys_equipment_code](
	[DataID] [nvarchar](50) NOT NULL,
	[SysCode] [nvarchar](50) NULL,
	[InfoCenterCode] [varbinary](50) NULL,
	[ETCCode] [nvarchar](50) NULL,
 CONSTRAINT [PK_sys_equipment_code] PRIMARY KEY CLUSTERED 
(
	[DataID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


