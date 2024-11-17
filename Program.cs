using Microsoft.EntityFrameworkCore;

namespace StadiumDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new Stadium(
            new DbContextOptionsBuilder<Stadium>()
                .UseSqlServer("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True;")
                .Options)
                )
                {
                    context.ExecuteSqlRaw(@"CREATE TABLE [Zone] (
  [ZoneID] int,
  [ZoneName] varchar(1),
  [NearestExit] int,
  PRIMARY KEY ([ZoneID])
);

CREATE INDEX [Key] ON  [Zone] ([ZoneName], [NearestExit]);

CREATE TABLE [CCTV] (
  [ID] int,
  [Zone] int,
  [IsLive] bit,
  PRIMARY KEY ([ID])
);

CREATE INDEX [Key] ON  [CCTV] ([IsLive]);

CREATE TABLE [Employee] (
  [ID] int,
  [FirstName] nvarchar(50),
  [LastName] nvarchar(50),
  [AccessLevel] int,
  [Vehicle] varchar(10),
  PRIMARY KEY ([ID])
);

CREATE INDEX [Key] ON  [Employee] ([FirstName], [LastName], [AccessLevel]);

CREATE TABLE [Event] (
  [ID] int,
  [StartDate] datetime,
  [EndDate] datetime,
  PRIMARY KEY ([ID])
);

CREATE INDEX [Key] ON  [Event] ([StartDate], [EndDate]);

CREATE TABLE [AccessGate] (
  [ID] int,
  [Location] varchar(50),
  [AccessType] int,
  [RequiredAccess] int,
  PRIMARY KEY ([ID])
);

CREATE INDEX [Key] ON  [AccessGate] ([Location], [AccessType], [RequiredAccess]);

CREATE TABLE [Attendee] (
  [ID] int,
  [FirstName] nvarchar(50),
  [LastName] nvarchar(50),
  [Vehicle] varchar(10),
  [Location] int,
  PRIMARY KEY ([ID])
);

CREATE INDEX [Key] ON  [Attendee] ([FirstName], [LastName]);

CREATE TABLE [EventAttendeeTable] (
  [EventAttendeeID] int idenity(1,1),
  [EventId] int,
  [AttendeeId] int,
  [Zone] int,
  [Seat] int,
  PRIMARY KEY ([EventAttendeeID]),
  CONSTRAINT [FK_EventAttendeeTable.AttendeeId]
    FOREIGN KEY ([AttendeeId])
      REFERENCES [Attendee]([ID])
);

CREATE INDEX [Key] ON  [EventAttendeeTable] ([Zone], [Seat]);

CREATE TABLE [Vehicle] (
  [VehicleRegistration] varchar(10),
  [AccessLevel] int,
  PRIMARY KEY ([VehicleRegistration])
);

CREATE INDEX [Key] ON  [Vehicle] ([AccessLevel]);
                    ");
                }
        }
    }
}