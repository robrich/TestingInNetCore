CREATE TABLE [dbo].[LockType]
(
[LockTypeId] [int] NOT NULL,
[LockTypeName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LockType] ADD CONSTRAINT [PK_LockType] PRIMARY KEY CLUSTERED  ([LockTypeId]) ON [PRIMARY]
GO
