CREATE DATABASE municipalites;
GO

USE municipalites;
GO

CREATE TABLE municipalites 
(
    mcode INT PRIMARY KEY,
    munnom NVARCHAR(255) NOT NULL,
    mcourriel NVARCHAR(255),
    mweb NVARCHAR(255),
    mdatcons DATE,
    msuperf FLOAT,
    mpopul INT,
    Actif BIT NOT NULL DEFAULT 1
);
GO

SELECT * FROM municipalites;
GO