CREATE TABLE [dbo].[Lock]
(
[LockId] [int] NOT NULL IDENTITY(1, 1),
[DoorId] [int] NOT NULL,
[IsUnlocked] [bit] NOT NULL,
[LockTypeId] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Lock] ADD CONSTRAINT [PK_Lock] PRIMARY KEY CLUSTERED  ([LockId]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Lock_DoorId] ON [dbo].[Lock] ([DoorId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Lock] ADD CONSTRAINT [FK_Lock_Door_DoorId] FOREIGN KEY ([DoorId]) REFERENCES [dbo].[Door] ([DoorId]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Lock] ADD CONSTRAINT [FK_Lock_LockType] FOREIGN KEY ([LockTypeId]) REFERENCES [dbo].[LockType] ([LockTypeId])
GO
