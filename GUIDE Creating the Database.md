# Guide for Creating the Database
#### We must first create our database
```
CREATE DATABASE [DummyMedi];
```
#### Next, we must group our tables together in a schema
A schema is just a way to group tables into names. For example, ```doctor.credentials and doctor.schedule``` Where ```doctor``` is the schema.
```sql 
USE DummyMedi;
CREATE SCHEMA admin;
CREATE SCHEMA doctor;
CREATE SCHEMA patient;
CREATE SCHEMA record;
```

#### Now, we will be creating tables
The creation of the tables must be in order to avoid errors for CONSTRAINTS and FOREIGN KEYS.
<br/>
### admin.credentials TABLE
This table will contain the information of the admin of the system.
```sql
USE [dbNAME];
CREATE TABLE admin.credentials
(
	adminId int PRIMARY KEY IDENTITY(1,1),
	username varchar(255) NOT NULL UNIQUE,
	password varchar(255) NOT NULL,
	fName varchar(255) NOT NULL,
	lname varchar(255) NOT NULL,
	contactNum char(11) NOT NULL,
	email varchar(255) NOT NULL UNIQUE,
);
```
#### To INSERT DUMMY DATA 
```sql
USE DummyMedi;
INSERT INTO admin.credentials(username, password, fName, lname, contactNum, email)
VALUES (
	'orayanics',
	'admin123',
	'Nicole',
	'Oraya',
	'09567052824',
	'oraya@gmail.com'
);
```
### doctor.credentials TABLE
This table will contain the information of the doctors of the system.
```sql
USE DummyMedi;
CREATE TABLE doctor.credentials
(
	doctorId int PRIMARY KEY IDENTITY(1,1),
	email varchar(255) NOT NULL UNIQUE,
	password varchar(255) NOT NULL,
	fName varchar(255) NOT NULL,
	lname varchar(255) NOT NULL,
	contactNum char(11) NOT NULL,
	department varchar(255) NOT NULL,
	medLicense varchar (255) NOT NULL,
);
```
#### To INSERT DUMMY DATA 
```sql
INSERT INTO [doctor].[credentials]
           ([email]
           ,[password]
           ,[fName]
           ,[lname]
           ,[contactNum]
           ,[department]
           ,[medLicense])
     VALUES
           (
	'oraya@gmail.com',
	'admin123',
	'Nicole',
	'Oraya',
	'09567052824',
	'Opthalmology',
	'MD-123123'
);
```
### doctor.schedule TABLE
```sql
USE DummyMedi;
CREATE TABLE doctor.schedule
(
	scheduleId int PRIMARY KEY IDENTITY(1,1),
	scheduleDate varchar(20),
	scheduleInfo varchar(255),
	doctorId int,
	FOREIGN KEY (doctorId) REFERENCES doctor.credentials(doctorId)
);
```
#### To INSERT DUMMY DATA
```sql
INSERT INTO doctor.schedule(
	scheduleDate,
	scheduleInfo,
	doctorId
)
VALUES(
	'Monday',
	'5:00 AM - 7:00 AM',
	1
);
```
### patient.credentials TABLE
```sql
USE DummyMedi;
CREATE TABLE patient.credentials
(
	patientId int PRIMARY KEY IDENTITY(1,1),
    patientRef varchar(16) UNIQUE,
    fName varchar(255) NOT NULL,
    lName varchar(255) NOT NULL,
	address varchar(255) NOT NULL,
    region varchar(100) NOT NULL,
    city varchar(100) NOT NULL,
	gender varchar(255) NOT NULL,
	birthdate date NOT NULL,
	birthplace varchar(255) NOT NULL,
	contactNum varchar (255) NOT NULL,
	occupation varchar(255) NOT NULL,
	religion varchar(255) NOT NULL,
	emergencyName varchar(255) NOT NULL,
	emergencyNum varchar(255) NOT NULL
);
```
#### To INSERT DUMMY DATA
```sql
USE DummyMedi;
	INSERT INTO patient.credentials(patientRef, fName, lName, address, region, city, gender, birthdate, birthplace, contactNum, occupation, religion, emergencyName, emergencyNum)
	VALUES('P-0000000000001',
	'Makoy',
	'Lee',
	'37A Juan Luna',
	'Metro Manila',
	'Quezon City',
	'Male',
	'06-02-1999',
	'Seoul Korea',
	'09567052829',
	'Idol',
	'Catholic',
	'Nicole Oraya',
	'09567052824'
);
```

### RECORD.type TABLE
```sql
USE DummyMedi;
CREATE TABLE record.type
(
	rtypeId int PRIMARY KEY IDENTITY(1,1),
	recordType varchar(255) NOT NULL
);
```
#### To INSERT DUMMY DATA 
```sql
USE DummyMedi;
INSERT INTO record.type(recordType)
VALUES('Medical History Form');
```

### record.medhistory TABLE
```sql
USE DummyMedi;
CREATE TABLE record.medhistory(
	medhistoryId int PRIMARY KEY IDENTITY(1,1),
	pastMedHistory varchar(255) NOT NULL,
	pastHospitalization varchar(255),
	pastSurgicalOperation varchar(255),
	medConcern varchar(255),
	foodAllergy varchar(255),
	drugAllergy varchar(255),
	attendingDoctor varchar(255),
	visitDate datetime,
	rtypeId int,
	FOREIGN KEY (rtypeId) REFERENCES record.type(rtypeId),
	patientId int,
	FOREIGN KEY (patientId) REFERENCES patient.credentials(patientId),
);
```

### record.diagnosis TABLE
```sql
	USE DummyMedi;
	CREATE TABLE record.diagnosis(
	diagnosisId int PRIMARY KEY IDENTITY(1,1),
	diagnosisText varchar(500),
	additionalNote varchar(500),
	attendingDoctor varchar(255),
	visitDate date,
	rtypeId int,
	FOREIGN KEY (rtypeId) REFERENCES record.type(rtypeId),
	patientId int,
	FOREIGN KEY (patientId) REFERENCES patient.credentials(patientId),
);
```
<details>
RECORD.MEDHISTORY
- Past Medical History
	- GENERAL CONDITION
	- in form of checkbox and will be formatted into string with comma delimiter
- Past History of Hospitalization or Surgical Operation
- Special Medical Concerns
- Any FOOD Allergies?
- Any DRUG Allergies?

General condition
https://su.edu.ph/wp-content/uploads/2022/08/Medical-History-Form-and-Physicians-Report-for-SHS-and-College-Students.pdf

Check the conditions that apply to you
https://www.nhsinform.scot/illnesses-and-conditions/a-to-z/


// MVC Model Validation for C# .NET
https://www.c-sharpcorner.com/UploadFile/13048b/model-validation-in-Asp-Net-mvc909/
https://www.tutorialsteacher.com/mvc/implement-validation-in-asp.net-mvc

// SELECT AND JOIN STATEMENT
SELECT * FROM doctor.credentials c
INNER JOIN doctor.schedule s
ON c.doctorId = s.doctorId;
</details>
