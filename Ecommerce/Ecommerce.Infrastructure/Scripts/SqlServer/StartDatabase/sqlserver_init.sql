IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '{{DB_NAME}}')
BEGIN
    CREATE DATABASE [{{DB_NAME}}]
    COLLATE Latin1_General_CI_AS;
END
GO

USE [{{DB_NAME}}]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Departments]') AND type = 'U')
BEGIN
    CREATE TABLE Departments (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Codigo VARCHAR(3) NOT NULL UNIQUE,
        Descricao VARCHAR(255) NOT NULL,
        DataCriacao DATETIME2 DEFAULT SYSDATETIME(),
        DataAtualizacao DATETIME2 DEFAULT SYSDATETIME()
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND type = 'U')
BEGIN
    CREATE TABLE Products (
        Id CHAR(36) PRIMARY KEY,
        Codigo VARCHAR(50) NOT NULL UNIQUE,
        Descricao VARCHAR(255) NOT NULL,
        Departamento VARCHAR(3) NOT NULL,
        Preco DECIMAL(10,2) NOT NULL CHECK (Preco >= 0),
        Status BIT NOT NULL DEFAULT 1,
        DataCriacao DATETIME2 DEFAULT SYSDATETIME(),
        DataAtualizacao DATETIME2 DEFAULT SYSDATETIME(),
        CONSTRAINT fk_products_departamento FOREIGN KEY (Departamento) REFERENCES Departments(Codigo)
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM Departments)
BEGIN
    INSERT INTO Departments (Codigo, Descricao) VALUES 
    ('010', 'BEBIDAS'), 
    ('020', 'CONGELADOS'), 
    ('030', 'LATICINIOS'), 
    ('040', 'VEGETAIS');
END
GO