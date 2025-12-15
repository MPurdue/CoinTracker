-- CoinTracker Database Setup
-- This script creates the necessary tables for the CoinTracker application
-- ASP.NET Identity tables will be created automatically via migrations

-- =============================================
-- Coins Table
-- =============================================
CREATE TABLE Coins (
    CoinId INT IDENTITY(1,1) PRIMARY KEY,
    Denomination DECIMAL(5,2) NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    Mint NVARCHAR(5) NOT NULL,
    Year INT NULL,
    Grade NVARCHAR(100),
    Notes NVARCHAR(1000),
    ReferenceUrl NVARCHAR(500),
    Symbol NVARCHAR(50) NOT NULL DEFAULT '',
    Description NVARCHAR(1000) NOT NULL DEFAULT ''
);

-- =============================================
-- CoinPrices Table (for historical price tracking)
-- =============================================
CREATE TABLE CoinPrices (
    CoinPriceId INT IDENTITY(1,1) PRIMARY KEY,
    CoinId INT NOT NULL,
    EstimatedValue DECIMAL(10,2),
    LastUpdated DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_CoinPrices_Coins FOREIGN KEY (CoinId) 
        REFERENCES Coins(CoinId) ON DELETE CASCADE
);

-- =============================================
-- Create Index for Performance
-- =============================================
CREATE INDEX IX_CoinPrices_CoinId ON CoinPrices(CoinId);
CREATE INDEX IX_Coins_Year ON Coins(Year);
CREATE INDEX IX_Coins_Name ON Coins(Name);

-- =============================================
-- Sample Data (Optional)
-- =============================================
-- Uncomment to insert sample data
/*
INSERT INTO Coins (Denomination, Name, Mint, Year, Grade, Notes, ReferenceUrl, Symbol, Description)
VALUES 
    (0.01, 'Lincoln Cent', 'P', 2004, 'MS65', 'Uncirculated condition', 'https://www.usacoinbook.com/coins/1735/cents/lincoln-memorial/2004-P/', 'CENT', 'Modern penny'),
    (0.05, 'Jefferson Nickel', 'D', 1999, 'PR69', 'Proof strike', 'https://www.usacoinbook.com/coins/1659/nickels/jefferson/1999-D/', 'NICK', 'Five cent piece'),
    (0.25, 'Washington Quarter', 'S', 1976, 'MS67', 'Bicentennial', 'https://www.usacoinbook.com/coins/1528/quarters/washington/1976-S/', 'QUAR', 'State quarter');
*/

-- =============================================
-- Verification Queries
-- =============================================
-- Run these to verify the tables were created successfully
SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME IN ('Coins', 'CoinPrices');
SELECT COUNT(*) AS CoinCount FROM Coins;