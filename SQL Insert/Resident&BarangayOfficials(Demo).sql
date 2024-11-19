-- Insert additional Barangay Officials into the BarangayOfficial table
INSERT INTO BarangayOfficials(
    FirstName, MiddleName, LastName, Birthdate, Gender, MaritalStatus,
    BarangayPosition, Committee, TermStart, TermEnd, RankNo, Status, Photo
)
VALUES
-- Punong Barangay
('Roberto', 'Castro', 'Dela Rosa', '1970-05-20', 'Male', 'Married', 'Punong Barangay', 'Leadership and Governance', '2023-01-01', '2025-12-31', 1, 'Active', NULL),

-- Sangguniang Barangay Members (Barangay Kagawads)
('Ernesto', 'Villanueva', 'Cruz', '1982-02-10', 'Male', 'Single', 'Sangguniang Barangay Member', 'Peace and Order', '2023-01-01', '2025-12-31', 2, 'Active', NULL),
('Lucia', 'Morales', 'Santos', '1988-09-15', 'Female', 'Married', 'Sangguniang Barangay Member', 'Health and Sanitation', '2023-01-01', '2025-12-31', 3, 'Active', NULL),
('Miguel', 'Dela', 'Torre', '1991-11-05', 'Male', 'Single', 'Sangguniang Barangay Member', 'Infrastructure', '2023-01-01', '2025-12-31', 4, 'Active', NULL),
('Cecilia', 'Luna', 'Garcia', '1985-07-19', 'Female', 'Married', 'Sangguniang Barangay Member', 'Livelihood', '2023-01-01', '2025-12-31', 5, 'Active', NULL),
('Alberto', 'Ramos', 'Torres', '1990-04-23', 'Male', 'Married', 'Sangguniang Barangay Member', 'Environment', '2023-01-01', '2025-12-31', 6, 'Active', NULL),
('Veronica', 'Rivera', 'Lopez', '1993-12-01', 'Female', 'Single', 'Sangguniang Barangay Member', 'Education', '2023-01-01', '2025-12-31', 7, 'Active', NULL),
('Rafael', 'Diaz', 'Martinez', '1987-06-14', 'Male', 'Single', 'Sangguniang Barangay Member', 'Sports and Recreation', '2023-01-01', '2025-12-31', 8, 'Active', NULL),

-- Sangguniang Kabataan Chairperson
('Angela', 'Rosario', 'Aquino', '2001-08-25', 'Female', 'Single', 'Sangguniang Kabataan Chairperson', 'Youth Affairs', '2023-01-01', '2025-12-31', 9, 'Active', NULL),

-- Barangay Secretary
('Beatrice', 'Reyes', 'Velasco', '1975-03-30', 'Female', 'Married', 'Barangay Secretary', 'Administrative', '2023-01-01', '2025-12-31', NULL, 'Active', NULL),

-- Barangay Treasurer
('Carlos', 'Santiago', 'Bautista', '1968-01-12', 'Male', 'Married', 'Barangay Treasurer', 'Finance and Budgeting', '2023-01-01', '2025-12-31', NULL, 'Active', NULL);
