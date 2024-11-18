-- Step 1: Insert Residents first

-- Insert Resident Juan Dela Cruz
INSERT INTO Residents (FirstName, LastName, MiddleName, Address, ContactNumber, Email, BirthDate, Gender, ResidencyStatus, IsSeniorCitizen, IsPWD, Nationality, CivilStatus, Occupation, VoterStatus, DateRegistered, EmergencyContact, ImageData, HealthConditions)
VALUES 
('Juan', 'Dela Cruz', 'Santos', 'Cabadiangan, Compostela Cebu', '09123456789', 'juan@email.com', '1990-05-15', 'Male', 'Residents', 0, 0, 'Filipino', 'Single', 'Teacher', 'Registered', '2023-06-01', 'Maria Dela Cruz - 09987654321', NULL, 'None');

-- Insert Resident Maria Santos
INSERT INTO Residents (FirstName, LastName, MiddleName, Address, ContactNumber, Email, BirthDate, Gender, ResidencyStatus, IsSeniorCitizen, IsPWD, Nationality, CivilStatus, Occupation, VoterStatus, DateRegistered, EmergencyContact, ImageData, HealthConditions)
VALUES 
('Maria', 'Santos', 'Garcia', 'Cabadiangan, Compostela Cebu', '09234567890', 'maria@email.com', '1985-08-20', 'Female', 'Residents', 0, 0, 'Filipino', 'Married', 'Nurse', 'Registered', '2023-06-02', 'Pedro Santos - 09876543210', NULL, 'Asthma');

-- Insert Resident Pedro Reyes
INSERT INTO Residents (FirstName, LastName, MiddleName, Address, ContactNumber, Email, BirthDate, Gender, ResidencyStatus, IsSeniorCitizen, IsPWD, Nationality, CivilStatus, Occupation, VoterStatus, DateRegistered, EmergencyContact, ImageData, HealthConditions)
VALUES 
('Pedro', 'Reyes', 'Luna', 'Cabadiangan, Compostela Cebu', '09345678901', 'pedro@email.com', '1978-11-30', 'Male', 'Residents', 0, 1, 'Filipino', 'Widowed', 'Accountant', 'Registered', '2023-06-03', 'Ana Reyes - 09765432109', NULL, 'Diabetes');

-- Insert Resident Ana Lim
INSERT INTO Residents (FirstName, LastName, MiddleName, Address, ContactNumber, Email, BirthDate, Gender, ResidencyStatus, IsSeniorCitizen, IsPWD, Nationality, CivilStatus, Occupation, VoterStatus, DateRegistered, EmergencyContact, ImageData, HealthConditions)
VALUES 
('Ana', 'Lim', 'Tan', 'Cabadiangan, Compostela Cebu', '09456789012', 'ana@email.com', '1995-02-14', 'Female', 'Residents', 0, 0, 'Filipino', 'Single', 'Engineer', 'Registered', '2023-06-04', 'Carlos Lim - 09654321098', NULL, 'None');

-- Insert Resident Carlos Garcia
INSERT INTO Residents (FirstName, LastName, MiddleName, Address, ContactNumber, Email, BirthDate, Gender, ResidencyStatus, IsSeniorCitizen, IsPWD, Nationality, CivilStatus, Occupation, VoterStatus, DateRegistered, EmergencyContact, ImageData, HealthConditions)
VALUES 
('Carlos', 'Garcia', 'Santos', 'Cabadiangan, Compostela Cebu', '09567890123', 'carlos@email.com', '1980-07-07', 'Male', 'Residents', 0, 0, 'Filipino', 'Married', 'Lawyer', 'Registered', '2023-06-05', 'Isabel Garcia - 09543210987', NULL, 'Hypertension');

-- Insert Resident Isabel Torres
INSERT INTO Residents (FirstName, LastName, MiddleName, Address, ContactNumber, Email, BirthDate, Gender, ResidencyStatus, IsSeniorCitizen, IsPWD, Nationality, CivilStatus, Occupation, VoterStatus, DateRegistered, EmergencyContact, ImageData, HealthConditions)
VALUES 
('Isabel', 'Torres', 'Reyes', 'Cabadiangan, Compostela Cebu', '09678901234', 'isabel@email.com', '1992-09-25', 'Female', 'Residents', 0, 0, 'Filipino', 'Single', 'Architect', 'Registered', '2023-06-06', 'Ramon Torres - 09432109876', NULL, 'None');

-- Insert Resident Ramon Cruz
INSERT INTO Residents (FirstName, LastName, MiddleName, Address, ContactNumber, Email, BirthDate, Gender, ResidencyStatus, IsSeniorCitizen, IsPWD, Nationality, CivilStatus, Occupation, VoterStatus, DateRegistered, EmergencyContact, ImageData, HealthConditions)
VALUES 
('Ramon', 'Cruz', 'Lim', 'Cabadiangan, Compostela Cebu', '09789012345', 'ramon@email.com', '1975-12-01', 'Male', 'Residents', 0, 0, 'Filipino', 'Divorced', 'Business Owner', 'Registered', '2023-06-07', 'Sofia Cruz - 09321098765', NULL, 'Arthritis');

-- Step 2: Insert Barangay Officials using ResidentID from above

-- Barangay Captain
INSERT INTO BarangayOfficials 
    (FirstName, LastName, MiddleName, BarangayPosition, TermStart, TermEnd, ResidentID, Birthdate, Gender, MaritalStatus, Committee, Status, Photo)
VALUES 
    ('Juan', 'Dela Cruz', 'Santos', 'Barangay Captain', '2023-01-01', '2026-01-01', 
     (SELECT ResidentID FROM Residents WHERE FirstName = 'Juan' AND LastName = 'Dela Cruz'), '1990-05-15', 'Male', 'Single', 'N/A', 'Active', NULL);

-- Barangay Secretary
INSERT INTO BarangayOfficials 
    (FirstName, LastName, MiddleName, BarangayPosition, TermStart, TermEnd, ResidentID, Birthdate, Gender, MaritalStatus, Committee, Status, Photo)
VALUES 
    ('Maria', 'Santos', 'Garcia', 'Barangay Secretary', '2023-01-01', '2026-01-01', 
     (SELECT ResidentID FROM Residents WHERE FirstName = 'Maria' AND LastName = 'Santos'), '1985-08-20', 'Female', 'Married', 'N/A', 'Active', NULL);

-- Barangay Treasurer
INSERT INTO BarangayOfficials 
    (FirstName, LastName, MiddleName, BarangayPosition, TermStart, TermEnd, ResidentID, Birthdate, Gender, MaritalStatus, Committee, Status, Photo)
VALUES 
    ('Pedro', 'Reyes', 'Luna', 'Barangay Treasurer', '2023-01-01', '2026-01-01', 
     (SELECT ResidentID FROM Residents WHERE FirstName = 'Pedro' AND LastName = 'Reyes'), '1978-11-30', 'Male', 'Widowed', 'N/A', 'Active', NULL);

-- Barangay Councilor
INSERT INTO BarangayOfficials 
    (FirstName, LastName, MiddleName, BarangayPosition, TermStart, TermEnd, ResidentID, Birthdate, Gender, MaritalStatus, Committee, Status, Photo)
VALUES 
    ('Ana', 'Lim', 'Tan', 'Barangay Councilor', '2023-01-01', '2026-01-01', 
     (SELECT ResidentID FROM Residents WHERE FirstName = 'Ana' AND LastName = 'Lim'), '1995-02-14', 'Female', 'Single', 'N/A', 'Active', NULL);

-- Barangay Councilor
INSERT INTO BarangayOfficials 
    (FirstName, LastName, MiddleName, BarangayPosition, TermStart, TermEnd, ResidentID, Birthdate, Gender, MaritalStatus, Committee, Status, Photo)
VALUES 
    ('Carlos', 'Garcia', 'Santos', 'Barangay Councilor', '2023-01-01', '2026-01-01', 
     (SELECT ResidentID FROM Residents WHERE FirstName = 'Carlos' AND LastName = 'Garcia'), '1980-07-07', 'Male', 'Married', 'N/A', 'Active', NULL);

-- Barangay Councilor
INSERT INTO BarangayOfficials 
    (FirstName, LastName, MiddleName, BarangayPosition, TermStart, TermEnd, ResidentID, Birthdate, Gender, MaritalStatus, Committee, Status, Photo)
VALUES 
    ('Isabel', 'Torres', 'Reyes', 'Barangay Councilor', '2023-01-01', '2026-01-01', 
     (SELECT ResidentID FROM Residents WHERE FirstName = 'Isabel' AND LastName = 'Torres'), '1992-09-25', 'Female', 'Single', 'N/A', 'Active', NULL);

-- Barangay Councilor
INSERT INTO BarangayOfficials 
    (FirstName, LastName, MiddleName, BarangayPosition, TermStart, TermEnd, ResidentID, Birthdate, Gender, MaritalStatus, Committee, Status, Photo)
VALUES 
    ('Ramon', 'Cruz', 'Lim', 'Barangay Councilor', '2023-01-01', '2026-01-01', 
     (SELECT ResidentID FROM Residents WHERE FirstName = 'Ramon' AND LastName = 'Cruz'), '1975-12-01', 'Male', 'Divorced', 'N/A', 'Active', NULL);
