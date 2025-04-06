DROP DATABASE ClinicManagement;
CREATE DATABASE ClinicManagement;
GO
USE ClinicManagement;

CREATE TABLE Patients (
    Patient_ID INT IDENTITY(1,1) PRIMARY KEY,
    Patient_Full_Name NVARCHAR(100) NOT NULL,
    Patient_DOB DATE NOT NULL,
    Patient_Gender NVARCHAR(10) CHECK (Patient_Gender IN ('Male', 'Female')),
    Patient_Phone VARCHAR(15) UNIQUE NOT NULL,
    Patient_Address NVARCHAR(255) NOT NULL
);

CREATE TABLE Departments (
    Department_ID INT IDENTITY(1,1) PRIMARY KEY,
    Department_Name NVARCHAR(100) NOT NULL UNIQUE,
    Department_Specialization NVARCHAR(100) NOT NULL
);

CREATE TABLE Doctors (
    Doctor_ID INT IDENTITY(1,1) PRIMARY KEY,
    Doctor_Full_Name NVARCHAR(100) NOT NULL,
    Doctor_Phone VARCHAR(15) UNIQUE NOT NULL,
    Department_ID INT NOT NULL,
    FOREIGN KEY (Department_ID) REFERENCES Departments(Department_ID) ON DELETE CASCADE
);

CREATE TABLE DiagnosisTreatment (
    Diagnosis_ID INT IDENTITY(1,1) PRIMARY KEY,
    Diagnosis_Name NVARCHAR(255) NOT NULL UNIQUE,
    Treatment_Details NVARCHAR(255) NOT NULL
);

CREATE TABLE MedicalRecords (
    Record_ID INT IDENTITY(1,1) PRIMARY KEY,
    Patient_ID INT NOT NULL,
    Doctor_ID INT NOT NULL,
    Diagnosis_ID INT NOT NULL,
    Record_Date DATE NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (Patient_ID) REFERENCES Patients(Patient_ID) ON DELETE CASCADE,
    FOREIGN KEY (Doctor_ID) REFERENCES Doctors(Doctor_ID) ON DELETE CASCADE,
    FOREIGN KEY (Diagnosis_ID) REFERENCES DiagnosisTreatment(Diagnosis_ID) ON DELETE CASCADE
);

CREATE TABLE Medicines (
    Medicine_ID INT IDENTITY(1,1) PRIMARY KEY,
    Medicine_Name NVARCHAR(100) NOT NULL UNIQUE,
    Medicine_Unit NVARCHAR(50) NOT NULL,
    Medicine_Price DECIMAL(10,2) NOT NULL CHECK (Medicine_Price > 0)
);

CREATE TABLE Prescriptions (
    Prescription_ID INT IDENTITY(1,1) PRIMARY KEY,
    Record_ID INT NOT NULL,
    Prescription_Date_Issued DATE NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (Record_ID) REFERENCES MedicalRecords(Record_ID) ON DELETE CASCADE
);

CREATE TABLE PrescriptionDetails (
    Prescription_ID INT NOT NULL,
    Medicine_ID INT NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    Dosage NVARCHAR(255) NOT NULL,
    PRIMARY KEY (Prescription_ID, Medicine_ID),
    FOREIGN KEY (Prescription_ID) REFERENCES Prescriptions(Prescription_ID) ON DELETE CASCADE,
    FOREIGN KEY (Medicine_ID) REFERENCES Medicines(Medicine_ID) ON DELETE CASCADE
);

CREATE TABLE Bills (
    Bill_ID INT IDENTITY(1,1) PRIMARY KEY,
    Patient_ID INT NOT NULL,
    Total_Amount DECIMAL(10,2) NOT NULL CHECK (Total_Amount >= 0),
    Bill_Date DATE NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (Patient_ID) REFERENCES Patients(Patient_ID) ON DELETE CASCADE
);

CREATE TABLE Services (
    Service_ID INT IDENTITY(1,1) PRIMARY KEY,
    Service_Name NVARCHAR(255) NOT NULL UNIQUE,
    Service_Price DECIMAL(10,2) NOT NULL CHECK (Service_Price >= 0)
);

CREATE TABLE BillDetails (
    Bill_ID INT NOT NULL,
    Service_ID INT NOT NULL,
    PRIMARY KEY (Bill_ID, Service_ID),
	FOREIGN KEY (Bill_ID) REFERENCES Bills(Bill_ID) ON DELETE CASCADE,
    FOREIGN KEY (Service_ID) REFERENCES Services(Service_ID) ON DELETE CASCADE
);

-- Insert data into Patients table
INSERT INTO Patients (Patient_Full_Name, Patient_DOB, Patient_Gender, Patient_Phone, Patient_Address)
VALUES 
('John Smith', '1990-05-12', 'Male', '0987123456', 'New York'),
('Emily Johnson', '1985-09-25', 'Female', '0918123457', 'Los Angeles'),
('Michael Brown', '2000-02-18', 'Male', '0923123458', 'Chicago'),
('Sophia Williams', '1995-07-30', 'Female', '0934123459', 'Houston'),
('David Miller', '1988-03-21', 'Male', '0945123460', 'Philadelphia'),
('Olivia Wilson', '1992-11-15', 'Female', '0956123461', 'Phoenix'),
('James Davis', '1997-06-07', 'Male', '0967123462', 'San Antonio'),
('Emma Garcia', '1993-04-10', 'Female', '0978123463', 'San Diego'),
('Benjamin Martinez', '1986-08-05', 'Male', '0989123464', 'Dallas'),
('Ava Rodriguez', '1991-12-20', 'Female', '0999123465', 'San Jose');

-- Insert data into Departments table
INSERT INTO Departments (Department_Name, Department_Specialization)
VALUES 
('General Dentistry', 'Examination and treatment of oral diseases'),
('Cosmetic Dentistry', 'Improving dental aesthetics'),
('Restorative Dentistry', 'Dental implants and crowns'),
('Pediatric Dentistry', 'Oral care for children'),
('Orthodontics', 'Braces and dental restoration'),
('Implant Dentistry', 'Dental implant procedures'),
('Preventive Dentistry', 'Preventing oral diseases'),
('Endodontics', 'Root canal treatments'),
('Emergency Dentistry', 'Handling urgent dental conditions'),
('Periodontics', 'Gum disease treatment');

-- Insert data into Doctors table
INSERT INTO Doctors (Doctor_Full_Name, Doctor_Phone, Department_ID)
VALUES 
('Dr. William Anderson', '0978123401', 1),
('Dr. Ava Thompson', '0989123402', 2),
('Dr. Daniel Harris', '0999123403', 3),
('Dr. Mia Young', '0967123404', 4),
('Dr. Ethan Scott', '0956123405', 5),
('Dr. Isabella Lewis', '0945123406', 6),
('Dr. Alexander Walker', '0934123407', 7),
('Dr. Charlotte Hall', '0923123408', 8),
('Dr. Matthew Allen', '0918123409', 9),
('Dr. Amelia Wright', '0907123410', 10);

-- Insert data into DiagnosisTreatment table
INSERT INTO DiagnosisTreatment (Diagnosis_Name, Treatment_Details)
VALUES 
('Tooth Decay', 'Dental fillings, root canal if necessary'),
('Gingivitis', 'Oral hygiene, antibiotics'),
('Wisdom Tooth Growth', 'Tooth extraction, pain management'),
('Orthodontic Treatment', 'Braces or aligners'),
('Dental Infection', 'Antibiotics and root canal treatment'),
('Periodontal Disease', 'Scaling and medication'),
('Tooth Fracture', 'Dental fillings or crowns'),
('Bad Breath', 'Oral hygiene and special mouthwash'),
('Misaligned Teeth', 'Orthodontic correction or extraction'),
('Unexplained Tooth Pain', 'Diagnosis and customized treatment');

-- Insert data into MedicalRecords table
INSERT INTO MedicalRecords (Patient_ID, Doctor_ID, Diagnosis_ID, Record_Date)
VALUES 
(1, 1, 1, '2024-03-10'),
(2, 2, 2, '2024-03-11'),
(3, 3, 3, '2024-03-12'),
(4, 4, 4, '2024-03-13'),
(5, 5, 5, '2024-03-14'),
(6, 6, 6, '2024-03-15'),
(7, 7, 7, '2024-03-16'),
(8, 8, 8, '2024-03-17'),
(9, 9, 9, '2024-03-18'),
(10, 10, 10, '2024-03-19');

-- Insert data into Medicines table
INSERT INTO Medicines (Medicine_Name, Medicine_Unit, Medicine_Price)
VALUES 
('Paracetamol', 'Tablet', 5000),
('Ibuprofen', 'Tablet', 7000),
('Amoxicillin', 'Tablet', 8000),
('Chlorhexidine', 'Solution', 30000),
('Aspirin', 'Tablet', 6000),
('Metronidazole', 'Tablet', 7500),
('Naproxen', 'Tablet', 8500),
('Oralgel', 'Gel', 25000),
('Lidocaine', 'Solution', 45000),
('Methylprednisolone', 'Tablet', 12000);

-- Insert data into Prescriptions table
INSERT INTO Prescriptions (Record_ID, Prescription_Date_Issued)
VALUES 
(1, '2024-03-10'),
(2, '2024-03-11'),
(3, '2024-03-12'),
(4, '2024-03-13'),
(5, '2024-03-14'),
(6, '2024-03-15'),
(7, '2024-03-16'),
(8, '2024-03-17'),
(9, '2024-03-18'),
(10, '2024-03-19');

-- Insert data into Bills table
INSERT INTO Bills (Patient_ID, Total_Amount, Bill_Date)
VALUES 
(1, 200000, '2024-03-10'),
(2, 300000, '2024-03-11'),
(3, 400000, '2024-03-12'),
(4, 250000, '2024-03-13'),
(5, 500000, '2024-03-14'),
(6, 150000, '2024-03-15'),
(7, 220000, '2024-03-16'),
(8, 320000, '2024-03-17'),
(9, 280000, '2024-03-18'),
(10, 310000, '2024-03-19');

-- Insert data into Services table
INSERT INTO Services (Service_Name, Service_Price)
VALUES 
('General Dental Checkup', 150000),
('Scaling and Polishing', 200000),
('Dental Filling', 500000),
('Tooth Extraction', 800000),
('Teeth Whitening', 600000),
('Dental Crowns', 3000000),
('Braces', 20000000),
('Dental Implant', 15000000),
('Orthodontics', 1000000),
('Root Canal Treatment', 1200000);

INSERT INTO PrescriptionDetails (Prescription_ID, Medicine_ID, Quantity, Dosage)
VALUES 
(1, 1, 2, 'Take 1 tablet every 6 hours'),  -- Paracetamol
(1, 3, 1, 'Take 1 tablet after meals'),  -- Amoxicillin
(2, 2, 3, 'Take 1 tablet every 8 hours'),  -- Ibuprofen
(2, 4, 1, 'Use mouthwash twice daily'),  -- Chlorhexidine
(3, 5, 2, 'Take 1 tablet every 12 hours'),  -- Aspirin
(3, 6, 1, 'Take 1 tablet before bed'),  -- Metronidazole
(4, 7, 1, 'Take 1 tablet after meals'),  -- Naproxen
(5, 8, 1, 'Apply gel to affected area twice daily'),  -- Oralgel
(6, 9, 1, 'Use solution for pain relief'),  -- Lidocaine
(7, 10, 1, 'Take 1 tablet in the morning');  -- Methylprednisolone


INSERT INTO BillDetails (Bill_ID, Service_ID)
VALUES 
(1, 1),  -- General Dental Checkup
(1, 2),  -- Scaling and Polishing
(2, 3),  -- Dental Filling
(3, 4),  -- Tooth Extraction
(3, 5),  -- Teeth Whitening
(4, 6),  -- Dental Crowns
(5, 7),  -- Braces
(6, 8),  -- Dental Implant
(7, 9),  -- Orthodontics
(8, 10); -- Root Canal Treatment


SELECT * FROM Patients;
SELECT * FROM Departments;
SELECT * FROM Doctors;
SELECT * FROM DiagnosisTreatment;
SELECT * FROM MedicalRecords;
SELECT * FROM Medicines;
SELECT * FROM Prescriptions;
SELECT * FROM PrescriptionDetails;
SELECT * FROM Services;
SELECT * FROM Bills;
SELECT * FROM BillDetails;

