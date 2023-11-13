# Guide for Creating the Database
#### We must first create our database
```
CREATE DATABASE [DummyMedi];
```
#### Next, we must group our tables together in a schema
A schema is just a way to group tables into names. For example, ```doctor.credentials and doctor.schedule``` Where ```doctor``` is the schema.
```sql 
USE DummyMedi;
CREATE SCHEMA users;
CREATE SCHEMA patient;
CREATE SCHEMA record;
```

#### Now, we will be creating tables
The creation of the tables must be in order to avoid errors for CONSTRAINTS and FOREIGN KEYS.
<br/>

### users.credentials TABLE
```sql
USE DummyMedi;
CREATE TABLE users.credentials
(
	userId int PRIMARY KEY IDENTITY(1,1),
	username varchar(255) NOT NULL UNIQUE,
	password varchar(255) NOT NULL,
	fName varchar(255) NOT NULL,
	lname varchar(255) NOT NULL,
	email varchar(255) NOT NULL UNIQUE,
	contactNum char(11) NOT NULL,
	department char(50) NOT NULL,
	userRole char(11) NOT NULL,
);
```

USE DummyMedi;
INSERT INTO users.credentials(username,password,fName,lName,email,contactNum,department,userRole)
VALUES (
'nikolai',
'admin123',
'Nikolai',
'Oraya',
'orayanics@gmail.com',
'09567052826',
'Opthalmology',
'Staff'
);

### users.schedule TABLE
```sql
USE DummyMedi;
CREATE TABLE users.schedule
(
	scheduleId int PRIMARY KEY IDENTITY(1,1),
	scheduleDate varchar(20) NOT NULL,
	scheduleInfo varchar(255) NOT NULL,
	userId int,
	FOREIGN KEY (userId) REFERENCES users.credentials(userId)
);
```
#### To INSERT DUMMY DATA
```sql
INSERT INTO users.credentials(scheduleDate, scheduleInfo, userId)
VALUES (
'Monday',
'7:00 AM - 2:00 PM',
1
);
```
### patient.credentials TABLE
```sql
USE DummyMedi;
CREATE TABLE patient.credentials
(
patientId int PRIMARY KEY IDENTITY(1,1),
patientRef varchar(16) NOT NULL UNIQUE,
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
	pastMedHistory varchar(255),
	pastHospitalization varchar(255),
	pastSurgicalOperation varchar(255),
	medConcern varchar(255),
	foodAllergy varchar(255),
	drugAllergy varchar(255),
	attendingDoctor varchar(255) NOT NULL,
	visitDate datetime NOT NULL,
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
	attendingDoctor varchar(255) NOT NULL,
	visitDate date NOT NULL,
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
