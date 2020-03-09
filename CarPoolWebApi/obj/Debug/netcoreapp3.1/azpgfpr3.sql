IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Users] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(max) NULL,
    [Password] nvarchar(max) NULL,
    [Name] nvarchar(max) NULL,
    [Mobile] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [Address] nvarchar(max) NULL,
    [DrivingLicence] nvarchar(max) NULL,
    [Rating] real NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Cars] (
    [Id] nvarchar(450) NOT NULL,
    [Number] nvarchar(max) NULL,
    [Model] nvarchar(max) NULL,
    [NoofSeat] int NOT NULL,
    [OwnerId] nvarchar(450) NULL,
    CONSTRAINT [PK_Cars] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Cars_Users_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Rides] (
    [Id] nvarchar(450) NOT NULL,
    [From] nvarchar(max) NULL,
    [To] nvarchar(max) NULL,
    [TotalDistance] real NOT NULL,
    [TravelDate] datetime2 NOT NULL,
    [AvailableSeats] int NOT NULL,
    [RideDate] datetime2 NOT NULL,
    [RatePerKM] real NOT NULL,
    [OwnerId] nvarchar(450) NULL,
    [CarId] nvarchar(450) NULL,
    [ViaPoints] nvarchar(max) NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_Rides] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Rides_Cars_CarId] FOREIGN KEY ([CarId]) REFERENCES [Cars] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Rides_Users_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Bookings] (
    [Id] nvarchar(450) NOT NULL,
    [RideId] nvarchar(450) NULL,
    [BookerId] nvarchar(450) NULL,
    [From] nvarchar(max) NULL,
    [To] nvarchar(max) NULL,
    [TravellingDistance] real NOT NULL,
    [BookingDate] datetime2 NOT NULL,
    [TravelDate] datetime2 NOT NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_Bookings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Bookings_Users_BookerId] FOREIGN KEY ([BookerId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Bookings_Rides_RideId] FOREIGN KEY ([RideId]) REFERENCES [Rides] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Bookings_BookerId] ON [Bookings] ([BookerId]);

GO

CREATE INDEX [IX_Bookings_RideId] ON [Bookings] ([RideId]);

GO

CREATE INDEX [IX_Cars_OwnerId] ON [Cars] ([OwnerId]);

GO

CREATE INDEX [IX_Rides_CarId] ON [Rides] ([CarId]);

GO

CREATE INDEX [IX_Rides_OwnerId] ON [Rides] ([OwnerId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200306092929_createdb', N'3.1.2');

GO

