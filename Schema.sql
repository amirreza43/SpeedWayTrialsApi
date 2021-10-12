CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);
CREATE TABLE IF NOT EXISTS "Drivers" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Drivers" PRIMARY KEY,
    "FirstName" TEXT NULL,
    "LastName" TEXT NULL,
    "Nickname" TEXT NULL,
    "Age" INTEGER NOT NULL,
    "Wins" INTEGER NOT NULL,
    "Losses" INTEGER NOT NULL,
    "BirthDate" TEXT NOT NULL
);
CREATE TABLE IF NOT EXISTS "Trials" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Trials" PRIMARY KEY,
    "Name" TEXT NULL,
    "Date" TEXT NOT NULL,
    "BestTime" TEXT NOT NULL,
    "WinnerName" TEXT NULL,
    "RaceCategory" INTEGER NOT NULL
);
CREATE TABLE IF NOT EXISTS "RaceCars" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_RaceCars" PRIMARY KEY,
    "Nickname" TEXT NULL,
    "Model" INTEGER NOT NULL,
    "Year" INTEGER NOT NULL,
    "TopSpeed" INTEGER NOT NULL,
    "Status" INTEGER NOT NULL,
    "CarType" INTEGER NOT NULL,
    "DriverId" TEXT NULL,
    CONSTRAINT "FK_RaceCars_Drivers_DriverId" FOREIGN KEY ("DriverId") REFERENCES "Drivers" ("Id") ON DELETE RESTRICT
);
CREATE TABLE IF NOT EXISTS "DriverRace" (
    "DriverId" TEXT NOT NULL,
    "RaceId" TEXT NOT NULL,
    CONSTRAINT "PK_DriverRace" PRIMARY KEY ("DriverId", "RaceId"),
    CONSTRAINT "FK_DriverRace_Drivers_DriverId" FOREIGN KEY ("DriverId") REFERENCES "Drivers" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_DriverRace_Trials_RaceId" FOREIGN KEY ("RaceId") REFERENCES "Trials" ("Id") ON DELETE CASCADE
);
CREATE INDEX "IX_DriverRace_RaceId" ON "DriverRace" ("RaceId");
CREATE INDEX "IX_RaceCars_DriverId" ON "RaceCars" ("DriverId");
