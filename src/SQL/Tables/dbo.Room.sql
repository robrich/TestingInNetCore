CREATE TABLE [dbo].[Room]
(
[RoomId] [int] NOT NULL IDENTITY(1, 1),
[RoomName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Room] ADD CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED  ([RoomId]) ON [PRIMARY]
GO
