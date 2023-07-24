-- Create the Users table
DROP TABLE IF EXISTS Users;
CREATE TABLE Users (
    userName VARCHAR(50) PRIMARY KEY,
    userType VARCHAR(20) NOT NULL CHECK (userType IN ('SIGNED', 'APPLICANT', 'ADMIN')),
    userPwd VARCHAR(50) NOT NULL,
    resName VARCHAR(100),
);

-- Insert data into the Users table
INSERT INTO Users (userName, userType, userPwd, resName)
VALUES
    ('john_doe', 'SIGNED', '1234', NULL),
    ('jane_smith', 'SIGNED', '1234', NULL),
    ('mike_jones', 'SIGNED', '1234', NULL),
    ('emma_lee', 'SIGNED', '1234', NULL),
    ('lily_white', 'SIGNED', '1234', NULL),
    ('chef_mario', 'APPLICANT', '1234', 'Bella Cucina'),
    ('mark_lee', 'SIGNED', '1234', NULL),
    ('anna_wu', 'SIGNED', '1234', NULL),
    ('owner_chris', 'APPLICANT', '1234', 'Tandoori Temptations'),
    ('foodie_gal', 'SIGNED', '1234', NULL),
    ('taste_explorer', 'SIGNED', '1234', NULL),
    ('culinary_maestro', 'APPLICANT', '1234', 'Gourmet Gateway'),
    ('flavor_adventurer', 'SIGNED', '1234', NULL),
    ('delicious_diner', 'SIGNED', '1234', NULL),
    ('taste_savant', 'SIGNED', '1234', NULL),
    ('food_fusionist', 'APPLICANT', '1234', 'Fusion Fusion'),
    ('taste_wanderer', 'SIGNED', '1234', NULL),
    ('savor_seeker', 'SIGNED', '1234', NULL),
    ('epicurean_explorer', 'APPLICANT', '1234', 'Taste Trails'),
    ('admin_user', 'ADMIN', '1234', '');  -- User with userType as ADMIN and an empty resName

-- Create the Restaurant table
DROP TABLE IF EXISTS Restaurant;
CREATE TABLE Restaurant (
    resName VARCHAR(100) PRIMARY KEY,
    resRating DECIMAL(3, 1),
    resImgName VARCHAR(100),
    resType VARCHAR(20) NOT NULL CHECK (resType IN ('GREEK', 'CONTEMPORARY', 'ASIAN', 'ITALIAN', 'MEXICAN')),
    resDescription VARCHAR(255),
    resOwner VARCHAR(50) REFERENCES Users(userName)
);

-- Insert data into the Restaurant table
INSERT INTO Restaurant (resName, resRating, resImgName, resType, resDescription, resOwner)
VALUES
    ('The Flavor Hub', 4.5, 'tasty_bite.jpg', 'CONTEMPORARY', 'A modern bistro serving global delicacies.', 'john_doe'),
    ('Fusion Delights', 4.8, 'fusion_feast.jpg', 'ASIAN', 'Experience the best fusion of Asian flavors.', 'jane_smith'),
    ('Epicurean Bites', 4.2, 'ephemeral_eats.jpg', 'GREEK', 'A taste of Greece in every bite.', 'mike_jones'),
    ('Savory Eats', 4.7, 'flavors_galore.jpg', 'CONTEMPORARY', 'An explosion of flavors to tantalize your taste.', 'emma_lee'),
    ('Admin Bistro', 4.6, 'spicebox.jpg', 'MEXICAN', 'Authentic Mexican dishes with love.', 'admin_user'),
    ('Spice Junction', 4.4, 'spice_junction.jpg', 'ASIAN', 'Spices from the heart of India.', 'lily_white'),
    ('Bella Cucina', 4.9, 'bella_napoli.jpg', 'ITALIAN', 'Authentic Italian dishes with love.', 'chef_mario'),
    ('Taste of Italia', 3.8, 'umami_garden.jpg', 'ITALIAN', 'Discover the authentic taste of Italy.', 'mark_lee'),
    ('Secret Suppers', 4.9, 'secret_supper.jpg', 'CONTEMPORARY', 'Discover the secrets behind every dish.', 'anna_wu'),
    ('Foodie Fantasy', 4.7, 'foodie_fantasy.jpg', 'CONTEMPORARY', 'Indulge in a delightful food journey.', 'foodie_gal'),
    ('Global Tastes', 3.9, 'global_tastes.jpg', 'CONTEMPORARY', 'Explore tastes from around the world.', 'taste_explorer'),
    ('Gourmet Gateway', 4.5, 'gourmet_gateway.jpg', 'CONTEMPORARY', 'Embark on a gourmet adventure.', 'culinary_maestro'),
    ('Spice Odyssey', 4.6, 'spice_odyssey.jpg', 'ASIAN', 'Discover the world of spices.', 'flavor_adventurer'),
    ('Flavors Galore', 4.7, 'flavors_galore.jpg', 'CONTEMPORARY', 'An explosion of flavors to tantalize your taste.', 'delicious_diner'),
    ('Tasty Travels', 4.1, 'tasty_travels.jpg', 'CONTEMPORARY', 'Travel through flavors and cuisines.', 'taste_savant'),
    ('Fusion Fusion', 4.3, 'fusion_fusion.jpg', 'ASIAN', 'A delightful fusion of Asian flavors.', 'food_fusionist'),
    ('Culinary Crossroads', 4.5, 'culinary_crossroads.jpg', 'CONTEMPORARY', 'Where flavors meet and entwine.', 'taste_wanderer'),
    ('Gourmet Getaway', 4.2, 'gourmet_getaway.jpg', 'CONTEMPORARY', 'Escape to a gourmet paradise.', 'savor_seeker'),
    ('Taste Trails', 4.6, 'taste_trails.jpg', 'CONTEMPORARY', 'Embark on trails of delightful tastes.', 'epicurean_explorer');

-- Create the Reviews table
DROP TABLE IF EXISTS Reviews;
CREATE TABLE Reviews (
    userName VARCHAR(50) REFERENCES Users(userName),
    resName VARCHAR(100) REFERENCES Restaurant(resName),
    revRating DECIMAL(3, 1),
    PRIMARY KEY (userName, resName)
);

-- Insert data into the Reviews table
INSERT INTO Reviews (userName, resName, revRating)
VALUES
    ('john_doe', 'The Flavor Hub', 4.5),
    ('jane_smith', 'Fusion Delights', 4.8),
    ('mike_jones', 'Epicurean Bites', 4.2),
    ('emma_lee', 'Savory Eats', 4.7),
    ('anna_wu', 'Secret Suppers', 4.9),
    ('foodie_gal', 'Foodie Fantasy', 4.7),
    ('culinary_maestro', 'Gourmet Gateway', 4.5),
    ('flavor_adventurer', 'Spice Odyssey', 4.6),
    ('delicious_diner', 'Flavors Galore', 4.7),
    ('taste_savant', 'Tasty Travels', 4.1);
