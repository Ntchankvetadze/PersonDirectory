USE [PersonsDirectory]

INSERT INTO dbo.City
(
    Name,
    Country
)
VALUES
( N'Istanbul', N'Turkey' ), 
( N'Taipei', N'Taiwan' ), 
( N'Paris', N'France' ), 
( N'Chaozhou', N'China' ), 
( N'Jaipur', N'India' ), 
( N'Omsk', N'Russia' ), 
( N'London', N'UK' ), 
( N'Warsaw', N'Poland' ), 
( N'Los Angeles', N'US' ), 
( N'Sachkhere', N'Georgia' ), 
( N'Tbilisi', N'Georgia' ), 
( N'Gotham', N'DC' )

INSERT INTO dbo.Persons
(
    FirstName,
    LastName,
    Gender,
    PersonalNo,
    BirthDate,
    CityId,
    ImageUrl
)
VALUES
( N'Jon', N'Doe', 'Male', '12345678910', N'1970-01-01T00:00:00', 6, 'NoPhoto.jpg' ), 
( N'Rick', N'Sanchez', 'Male', '01241215666', N'1951-06-06T00:00:00', 9, '8954cbc8-eec3-494d-97fc-99abcb4ff9fa_Rick3.jpg' ), 
( N'Morty', N'Smith', 'Male', '44556677889', N'2001-10-01T00:00:00', 7, '34c77bbf-5c29-4c5e-ac39-b87cf466514c_Morty4.jpeg' ), 
( N'Summer', N'Smith', 'Female', '01241563463', N'1993-08-23T00:00:00', 7, '66da4a17-be8b-422c-81b7-0dbe40ab8219_SummerSmith3.jpg' ), 
( N'Beth', N'Smith', 'Female', '91295251252', N'1973-12-09T00:00:00', 3, '22f69865-d19f-4132-b8e5-7dd920185031_BethSmith.jpeg' ), 
( N'Jessica', N'Smith', 'Female', '77758944478', N'1999-03-31T00:00:00', 8, '2ed8f18a-ea76-416f-9ae9-8ea4cabbe02f_JessicaVoice.jpg' ), 
( N'აკაკი', N'წერეთელი', 'Male', '10101010101', N'1840-06-09T00:00:00', 10, 'c7655f0d-0d22-4072-b417-56be45f5d6fa_Akaki.jpg' ), 
( N'ილია', N'ჭავჭავაძე', 'Male', '13109151582', N'1837-11-08T00:00:00', 11, 'aa081aa5-87ce-4bc0-9a26-f7120d63fd5f_ilia2.png' ), 
( N'ლუკა', N'რაზიკაშვილი', 'Male', '73463762357', N'1861-07-26T00:00:00', 11, '6a3adc0b-f054-4b04-98c4-901ecae53c96_vaja.jpg' ), 
( N'Angelina', N'Jolie', 'Female', '12121212122', N'1975-07-04T00:00:00', 9, 'b87795d3-00d7-4aa7-b3fd-3d62fdd5a046_angelinajolie.jpg' ), 
( N'Peter', N'Parker', 'Male', '88356858539', N'1987-07-16T00:00:00', 5, 'cbe61aec-c1f2-4398-88e4-c3fda7ecb902_pp.jpg' ), 
( N'Bruce', N'Wayne', 'Male', '73373734232', N'1984-09-08T00:00:00', 12, '4e516cde-34dd-44f5-9bab-fcaf15a534c3_bat.jpg' ), 
( N'Nugzar', N'Tchankvetadz', 'Male', '54001058007', N'1993-10-12T00:00:00', 11, 'NoPhoto.jpg' )

INSERT INTO dbo.TelephoneNumbers
(
    PersonId,
    TelephoneType,
    Number
)
VALUES
( 1, 'Home', '001002003' ), 
( 12, 'Office', '55555555555' ), 
( 7, 'Mobile', '22222222222' ), 
( 2, 'Mobile', '0101010101' ), 
( 12, 'Mobile', '78999997999' ), 
( 11, 'Mobile', '433463772352' ), 
( 11, 'Office', '032353467373' )

INSERT INTO dbo.PersonRelationMap
(
    PersonOneId,
    PersonTwoId,
    RelationType
)
VALUES
( 12, 11, 'Colleague' ), 
( 7, 8, 'Colleague' ), 
( 7, 9, 'Colleague' ), 
( 7, 10, 'Relative' ), 
( 2, 4, 'Relative' ), 
( 2, 6, 'Relative' ), 
( 9, 10, 'Other' ), 
( 9, 8, 'Colleague' ), 
( 11, 13, 'Acquaintance' ), 
( 12, 13, 'Other' )
