-- Creates CourseProjectDb as a physical .mdf/.ldf file pair that lives inside
-- the solution (src\CourseProject.UI\Data), instead of relying on the LocalDB
-- instance's own catalog. This way the data survives the LocalDB instance
-- being recreated, and the file is visible/portable inside the solution.
IF DB_ID('CourseProjectDb') IS NOT NULL
BEGIN
    ALTER DATABASE CourseProjectDb SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE CourseProjectDb;
END
GO

DECLARE @DataPath NVARCHAR(260) = N'C:\Work\Others\Курсач\src\CourseProject.UI\Data\';
DECLARE @Sql NVARCHAR(MAX) = N'
CREATE DATABASE CourseProjectDb
    ON PRIMARY (NAME = CourseProjectDb, FILENAME = ''' + @DataPath + N'CourseProjectDb.mdf'')
    LOG ON (NAME = CourseProjectDb_log, FILENAME = ''' + @DataPath + N'CourseProjectDb_log.ldf'');';
EXEC (@Sql);
GO

USE CourseProjectDb;
GO

CREATE TABLE dbo.Counter
(
    Id    INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Value INT NOT NULL
);

INSERT INTO dbo.Counter (Value) VALUES (0);
GO

-- Detach from the instance's catalog so the app can attach the .mdf itself
-- (via AttachDbFilename) without a name clash against this registration.
USE master;
GO
ALTER DATABASE CourseProjectDb SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
EXEC sp_detach_db 'CourseProjectDb';
GO
