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

INSERT INTO business (Auth0ID, BusinessName, PrimaryEmail, AddrLocation, TelephoneNumber, BusinessLogo, IsTrading)
VALUES
('auth0|606198aac96e2800685cabff','Pete The Meat','Pete@petethemeat.co.uk','Birmingham','01217007000','http://petethemeat.co.uk/all-images/00-header-logo-930-200-02.jpg','TRUE');

INSERT INTO business (Auth0ID, BusinessName, PrimaryEmail, AddrLocation, TelephoneNumber, BusinessLogo, IsTrading)
VALUES
('auth0|606198aac96e2800686cabff','Macrame World','Roberts@macrameworld.co.uk','Solihull','01217117111','https://upload.wikimedia.org/wikipedia/commons/thumb/a/a6/Macrame_.jpg/220px-Macrame_.jpg','TRUE');

INSERT INTO business (Auth0ID, BusinessName, PrimaryEmail, AddrLocation, TelephoneNumber, BusinessLogo, IsTrading)
VALUES
('auth0|606198aac96e2800687cabff','Margo the flo','Margaret@margotheflo.co.uk','Leamington-spa','01534711700','https://www.startupdonut.co.uk/sites/default/files/production%20image/florist.jpg','TRUE');

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


INSERT INTO product (BusinessId, ProductName, ProductDescription, ProductImage, ProductPrice, Quantity)
VALUES
('1','Steak','per kg, prime rump','https://cdn.shopify.com/s/files/1/0407/1850/0004/products/Beef_Steak_Sirloin_IMG_0124-1-e1581325055154_600x.jpg?v=1593595952','150','10');

INSERT INTO product (BusinessId, ProductName, ProductDescription, ProductImage, ProductPrice, Quantity)
VALUES
('1','chicken','per kg, free range','https://www.campbellsmeat.com/images/products/large_vertical/1584634373ChickenFresh5kg.png','7','15');

INSERT INTO product (BusinessId, ProductName, ProductDescription, ProductImage, ProductPrice, Quantity)
VALUES
('2','Macrame Poodle','per item, 100cm wide, 100cm length','http://www.free-macrame-patterns.com/image-files/mutt-medium.jpg','20','3');

INSERT INTO product (BusinessId, ProductName, ProductDescription, ProductImage, ProductPrice, Quantity)
VALUES
('3','Sunflower','per item','https://www.proflowers.com/blog/wp-content/uploads/2012/08/6-11_Meaning-of-Sunflowers_Images.jpg','5','100');