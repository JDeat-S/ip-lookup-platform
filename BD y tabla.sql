-- Crear base de datos
CREATE DATABASE IpLookupDb;
GO

USE IpLookupDb;
GO

-- Crear tabla principal
CREATE TABLE IpQueries (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    
    IpAddress NVARCHAR(45) NOT NULL,
    
    Country NVARCHAR(100) NOT NULL,
    City NVARCHAR(100) NOT NULL,
    ISP NVARCHAR(150) NOT NULL,
    
    Latitude DECIMAL(9,6) NOT NULL,
    Longitude DECIMAL(9,6) NOT NULL,
    
    ThreatLevel NVARCHAR(50) NOT NULL,
    
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

-- Evitar IPs duplicadas
CREATE UNIQUE INDEX UX_IpQueries_IpAddress
ON IpQueries(IpAddress);
-- Crear login a nivel servidor
CREATE LOGIN IpLookupUser
WITH PASSWORD = 'IpLookup123!',
CHECK_POLICY = OFF;

GO

-- Crear usuario dentro de la base
CREATE USER IpLookupUser FOR LOGIN IpLookupUser;
GO

-- Dar permisos de lectura y escritura
ALTER ROLE db_datareader ADD MEMBER IpLookupUser;
ALTER ROLE db_datawriter ADD MEMBER IpLookupUser;

-- Permitir ejecutar procedimientos si luego agregas SPs
ALTER ROLE db_ddladmin ADD MEMBER IpLookupUser;
GO
