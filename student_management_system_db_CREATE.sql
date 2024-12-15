create database student_management_system_db;
use student_management_system_db;

create table users(
	id int primary key identity(0,1),
	first_name varchar(20),
	middle_name varchar(20) default null,
	last_name varchar(20) not null,
	username varchar(30) not null,
	password binary(64) not null
);

create table blocks(
	id int primary key identity(0,1),
	name varchar(50) not null
);

create table students(
	id int primary key identity(0,1),
	first_name varchar(20) not null,
	middle_name varchar(20) default null,
	last_name varchar(20) not null,
	age int not null,
	block_id int,
	constraint FK_BlocksStudents foreign key(block_id) references blocks(id) on delete cascade
);

create table courses(
	id int primary key identity(0,1),
	name varchar(50) not null
);

create table grades(
	id int primary key identity(0,1),
	course_id int,
	student_id int,
	grade decimal(2,2) not null
	constraint FK_CoursesGrades foreign key(course_id) references courses(id) on delete cascade,
	constraint FK_StudentsGrades foreign key(student_id) references students(id) on delete cascade
);