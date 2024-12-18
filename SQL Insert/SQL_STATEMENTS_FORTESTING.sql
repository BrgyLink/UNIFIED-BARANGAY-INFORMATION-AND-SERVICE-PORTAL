-- Insert Puroks
INSERT INTO Purok (Name, Description, NumberOfRegisteredPeople) VALUES
('Purok 1', 'Description for Purok 1', 0),
('Purok 2', 'Description for Purok 2', 0),
('Purok 3', 'Description for Purok 3', 0),
('Purok 4', 'Description for Purok 4', 0),
('Purok 5', 'Description for Purok 5', 0),
('Purok 6', 'Description for Purok 6', 0),
('Purok 7', 'Description for Purok 7', 0),
('Purok 8', 'Description for Purok 8', 0);

-- Insert Committees
INSERT INTO Committee (CommitteeName, CommitteeDescription) VALUES
('Committee on Health', 'Responsible for health-related issues.'),
('Committee on Education', 'Handles education programs and initiatives.'),
('Committee on Environment', 'Focuses on environmental protection.'),
('Committee on Safety', 'Ensures community safety and security.'),
('Committee on Infrastructure', 'Manages infrastructure development.'),
('Committee on Culture', 'Promotes cultural events and activities.'),
('Committee on Finance', 'Oversees barangay financial matters.'),
('Committee on Sports', 'Encourages sports activities and events.');

-- Insert Barangay Officials
INSERT INTO BarangayOfficial (FirstName, MiddleName, LastName, Birthdate, Gender, MaritalStatus, BarangayPosition, TermStart, TermEnd, RankNo, Status, Photo) VALUES
('Juan', 'Dela', 'Cruz', '1980-01-01', 'Male', 'Single', 'Barangay Captain', '2022-01-01', '2025-12-31', 1, 'Active', NULL),
('Maria', 'Santos', 'Reyes', '1985-05-10', 'Female', 'Married', 'Barangay Kagawad', '2022-01-01', '2025-12-31', 2, 'Active', NULL),
('Pedro', 'Pablo', 'Gomez', '1978-07-15', 'Male', 'Single', 'Barangay Kagawad', '2022-01-01', '2025-12-31', 3, 'Active', NULL),
('Luz', 'Bautista', 'Alonzo', '1990-03-22', 'Female', 'Single', 'Barangay Treasurer', '2022-01-01', '2025-12-31', 4, 'Active', NULL),
('Andres', 'Cruz', 'Alvarado', '1982-11-30', 'Male', 'Married', 'Barangay Secretary', '2022-01-01', '2025-12-31', 5, 'Active', NULL),
('Sofia', 'Mendoza', 'Castro', '1992-06-18', 'Female', 'Single', 'Barangay Kagawad', '2022-01-01', '2025-12-31', 6, 'Active', NULL),
('Miguel', 'Ortiz', 'Martinez', '1986-08-05', 'Male', 'Married', 'Barangay Kagawad', '2022-01-01', '2025-12-31', 7, 'Active', NULL),
('Rosa', 'Hernandez', 'Flores', '1995-02-14', 'Female', 'Single', 'Barangay Kagawad', '2022-01-01', '2025-12-31', 8, 'Active', NULL),
('Carlos', 'Ruiz', 'Nieves', '1975-12-25', 'Male', 'Married', 'Barangay Kagawad', '2022-01-01', '2025-12-31', 9, 'Active', NULL),
('Ana', 'Fernandez', 'Serrano', '1993-09-11', 'Female', 'Single', 'Barangay Kagawad', '2022-01-01', '2025-12-31', 10, 'Active', NULL);

-- Insert Residents
INSERT INTO Resident (FirstName, LastName, MiddleName, PurokId, ContactNumber, Email, BirthDate, Gender, ResidencyStatus, IsSeniorCitizen, IsPWD, Nationality, CivilStatus, Occupation, VoterStatus, DateRegistered, EmergencyContact, ImageData, HealthConditions) VALUES
('John', 'Doe', 'A.', 1, '09123456789', 'johndoe@example.com', '2000-01-01', 'Male', 'Resident', 0, 0, 'Filipino', 'Single', 'Student', 'Non-voter', '2023-01-01', 'Jane Doe', NULL, NULL),
('Jane', 'Smith', 'B.', 2, '09123456788', 'janesmith@example.com', '1995-02-02', 'Female', 'Resident', 0, 0, 'Filipino', 'Single', 'Teacher', 'Non-voter', '2023-01-01', 'John Smith', NULL, NULL),
('Emily', 'Johnson', 'C.', 3, '09123456787', 'emilyjohnson@example.com', '1988-03-03', 'Female', 'Resident', 0, 0, 'Filipino', 'Single', 'Engineer', 'Non-voter', '2023-01-01', 'Mark Johnson', NULL, NULL),
('Michael', 'Williams', 'D.', 4, '09123456786', 'michaelwilliams@example.com', '1992-04-04', 'Male', 'Resident', 0, 0, 'Filipino', 'Single', 'Doctor', 'Non-voter', '2023-01-01', 'Lisa Williams', NULL, NULL),
('Chris', 'Brown', 'E.', 5, '09123456785', 'chrisbrown@example.com', '1990-05-05', 'Male', 'Resident', 0, 0, 'Filipino', 'Single', 'Nurse', 'Non-voter', '2023-01-01', 'Pat Brown', NULL, NULL),
('Jessica', 'Jones', 'F.', 6, '09123456784', 'jessicajones@example.com', '1985-06-06', 'Female', 'Resident', 0, 0, 'Filipino', 'Single', 'Artist', 'Non-voter', '2023-01-01', 'Tom Jones', NULL, NULL),
('Daniel', 'Garcia', 'G.', 7, '09123456783', 'danielgarcia@example.com', '1991-07-07', 'Male', 'Resident', 0, 0, 'Filipino', 'Single', 'Chef', 'Non-voter', '2023-01-01', 'Nina Garcia', NULL, NULL),
('Sarah', 'Martinez', 'H.', 8, '09123456782', 'sarahmartinez@example.com', '1983-08-08', 'Female', 'Resident', 0, 0, 'Filipino', 'Single', 'Scientist', 'Non-voter', '2023-01-01', 'Mark Martinez', NULL, NULL),
('David', 'Hernandez', 'I.', 1, '09123456781', 'davidhernandez@example.com', '1975-09-09', 'Male', 'Resident', 0, 0, 'Filipino', 'Single', 'Lawyer', 'Non-voter', '2023-01-01', 'Maria Hernandez', NULL, NULL),
('Linda', 'Lopez', 'J.', 2, '09123456780', 'lindalopez@example.com', '1980-10-10', 'Female', 'Resident', 0, 0, 'Filipino', 'Single', 'Manager', 'Non-voter', '2023-01-01', 'John Lopez', NULL, NULL),
('George', 'Lee', 'K.', 3, '09123456779', 'georgelee@example.com', '1994-11-11', 'Male', 'Resident', 0, 0, 'Filipino', 'Single', 'Developer', 'Non-voter', '2023-01-01', 'Mia Lee', NULL, NULL),
('Emily', 'Garcia', 'L.', 4, '09123456778', 'emilygarcia@example.com', '1996-12-12', 'Female', 'Resident', 0, 0, 'Filipino', 'Single', 'Data Analyst', 'Non-voter', '2023-01-01', 'Liam Garcia', NULL, NULL),
('Henry', 'Walker', 'M.', 5, '09123456777', 'henrywalker@example.com', '1993-01-13', 'Male', 'Resident', 0, 0, 'Filipino', 'Single', 'Pilot', 'Non-voter', '2023-01-01', 'Sophia Walker', NULL, NULL),
('Amanda', 'Hall', 'N.', 6, '09123456776', 'amandahall@example.com', '1997-02-14', 'Female', 'Resident', 0, 0, 'Filipino', 'Single', 'Photographer', 'Non-voter', '2023-01-01', 'Jason Hall', NULL, NULL),
('Nicholas', 'Young', 'O.', 7, '09123456775', 'nicholasyoung@example.com', '1989-03-15', 'Male', 'Resident', 0, 0, 'Filipino', 'Single', 'Veterinarian', 'Non-voter', '2023-01-01', 'Isabella Young', NULL, NULL),
('Madison', 'King', 'P.', 8, '09123456774', 'madisonking@example.com', '1987-04-16', 'Female', 'Resident', 0, 0, 'Filipino', 'Single', 'Musician', 'Non-voter', '2023-01-01', 'Ethan King', NULL, NULL),
('Joshua', 'Scott', 'Q.', 1, '09123456773', 'joshuascott@example.com', '1992-05-17', 'Male', 'Resident', 0, 0, 'Filipino', 'Single', 'Journalist', 'Non-voter', '2023-01-01', 'Ella Scott', NULL, NULL),
('Sophia', 'Adams', 'R.', 2, '09123456772', 'sophiaadams@example.com', '1984-06-18', 'Female', 'Resident', 0, 0, 'Filipino', 'Single', 'Social Worker', 'Non-voter', '2023-01-01', 'James Adams', NULL, NULL),
('Ryan', 'Nelson', 'S.', 3, '09123456771', 'ryannelson@example.com', '1998-07-19', 'Male', 'Resident', 0, 0, 'Filipino', 'Single', 'Web Designer', 'Non-voter', '2023-01-01', 'Zoe Nelson', NULL, NULL),
('Olivia', 'Carter', 'T.', 4, '09123456770', 'oliviacarter@example.com', '1990-08-20', 'Female', 'Resident', 0, 0, 'Filipino', 'Single', 'Fashion Designer', 'Non-voter', '2023-01-01', 'Henry Carter', NULL, NULL);
