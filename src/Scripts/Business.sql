-- Address - how should it be entered
-- ADDING SOCIAL MEDIA LINK? though its a stretch goal
-- ADDING IMAGE TO DATABASE?
-- Adding opening hours as stetch goal.



-- CREATE TABLE business(
--     Id SERIAL PRIMARY KEY NOT NULL,
--     BusinessName VARCHAR(20),
--     PrimaryConatct VARCHAR(50), 
--     AddrBuildingName VARCHAR(20), 
--     AddrBuildingNumber INTEGER, 
--     AddrStreet VARCHAR (25), 
--     AddrCity VARCHAR(25), 
--     AddrCounty VARCHAR(25),
--     AddrPostcode VARCHAR(7),
--     TelephoneNumber INTEGER,
--     TwitterHandle TEXT,
--     SocialmediaLink TEXT, 
--     BusinessImage TEXT,
--     IsTrading BOOLEAN
-- );

CREATE TABLE business(
    Id SERIAL PRIMARY KEY NOT NULL,
    Auth0Id VARCHAR(40),
    BusinessName VARCHAR(20),
    PrimaryEmail VARCHAR(50),
    AddrLocation VARCHAR(50),
    TelephoneNumber INTEGER,
    BusinessLogo TEXT,
    IsTrading BOOLEAN
);

-- CREATE TABLE product(
--     BusinessId INTEGER,
--     ProductId SERIAL PRIMARY KEY NOT NULL, 
--     ProductName VARCHAR(25),
--     ProductType VARCHAR(25),
--     ProductDescription VARCHAR(50),
--     ProductImage TEXT, 
--     ProductPrice MONEY, 
--     UnitSize TEXT, 
--     Quantity INTEGER,     
--     CONSTRAINT fk_business
--         FOREIGN KEY(BusinessId)
--             REFERENCES business(Id)
-- );

CREATE TABLE product(
    BusinessId INTEGER,
    ProductId SERIAL PRIMARY KEY NOT NULL, 
    ProductName TEXT,
    ProductDescription TEXT,
    ProductImage TEXT,
    ProductPrice MONEY,
    Quantity INTEGER,     
    CONSTRAINT fk_business
        FOREIGN KEY(BusinessId)
            REFERENCES business(Id)
);