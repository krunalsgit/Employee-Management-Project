use RB_Training_2023

create Table Department351(
	Id int identity primary key,
	DepartmentName varchar(250) unique
)

INSERT INTO Department351 (DepartmentName) 
VALUES('Accounting'),
		('Administration'),
		('Asset Management'),
		('Brand Management'),
		('Business Analysis'),
		('Business Development'),
		('Compliance'),
		('Consulting'),
		('Content Writing'),
		('Corporate Communications'),
		('Customer Service'),
		('Customer Success'),
		('Data Analysis'),
		('Design'),
		('Digital Marketing'),
		('Engineering'),
		('Event Planning'),
		('Facilities Management'),
		('Finance'),
		('Graphic Design'),
		('Health and Safety'),
		('Human Resources'),
		('Information Technology'),
		('Internal Audit'),
		('Investor Relations'),
		('Legal'),
		('Logistics'),
		('Manufacturing'),
		('Market Research'),
		('Marketing'),
		('Media Relations'),
		('Network Administration'),
		('Operations'),
		('Operations Research'),
		('Product Management'),
		('Procurement'),
		('Project Management'),
		('Public Policy'),
		('Public Relations'),
		('Purchasing'),
		('Quality Assurance'),
		('Research and Development'),
		('Risk Management'),
		('Sales'),
		('Sales Operations'),
		('Software Development'),
		('Strategic Planning'),
		('Supply Chain'),
		('Training and Development'),
		('Training Coordination')


create Table Employee351(
	Id int primary key identity,
	Name varchar(250) not null,
	Email varchar(250) not null unique,
	Gender varchar(15) not null,
	Contact varchar(15) not null check(len(contact)>5),
	Salary decimal not null,
	Department int references Department351(Id),
	Skills varchar(250) not null,
	ProfilePic varchar(250)
)

ALTER TABLE Employee351
ADD Password varchar(200);

ALTER TABLE Employee351
Alter Column Password varchar(200) Not Null;


INSERT INTO Employee351 (Name, Email, Gender, Contact, Salary, Department, Skills, ProfilePic)
VALUES ('Alex Johnson', 'alexjohnson@example.com', 'Male', '5555555555', 7000, 1, 'Management, Leadership', 'alexpic.jpg'),
	('Jane Smith', 'janesmith@example.com', 'Female', '9876543210', 4000, 2, 'Design, Communication', 'Janepic.jpg'),
 ('John Doe', 'johndoe@example.com', 'Male', '1234567890', 5000.00, 1, 'Programming, Problem Solving', 'profile_pic.jpg')

create Table Address351(
	AddressId int primary key identity,
	Address varchar(555) not null,
	EmployeeId int references Employee351(Id)
)

Create Table Register351(
	RegistrationId int identity primary key,
	Name Varchar(250) not null,
	Email Varchar(200) not null unique,
	Password varchar(250) not null,
	EmployeeId Int References Employee351(Id)
)

alter table Register351
add IsVerified bit default 0

alter table EMployee351
drop column Password

--------------------------------------------------------------------------
Alter Proc sp_AddOrUpdateEmployee
@Id int=null,
@Name varchar(250),
@Email varchar(250),
@Gender varchar(25),
@Contact varchar(15),
@Salary decimal,
@DepartmentId int,
@Skills varchar(555),
@ProfilePic varchar(250)
as
begin
	if(@Id is null)
		begin
			Insert Into Employee351(Name,Email,Gender,Contact,Salary,Department,Skills,ProfilePic,RegistrationId)
				Values(@Name,@Email,@Gender,@Contact,@Salary,@DepartmentId,@Skills,@ProfilePic,(select RegistrationId From Register351 Where Email=@Email))
			Select * From Employee351 Where Id=@@IDENTITY;
		end
	else
		begin tran
		Update Employee351 Set Name=@Name,Email=@Email,Gender=@Gender,Contact=@Contact,
								Salary=@Salary,Department=@DepartmentId,Skills=@Skills,ProfilePic=@ProfilePic
									Where Id=@Id
		commit tran
		select @Id
end		

--------------------------------------------------------------------------
alter proc sp_AddUpdateAddress null,'fgdfgd',5
@Id int=null,
@Address varchar(500),
@EmpId int
as
begin
	if(@Id is null)
		insert into Address351(Address,EmployeeId) Values(@Address,@EmpId);
	else
		Update Address351 Set Address=@Address,EmployeeId=@EmpId Where AddressId=@Id
	select 1
end

truncate table Address351
--------------------------------------------------------------------------
alter proc sp_RegisterEmployee
@Email varchar(250),
@Password varchar(20),
@Name varchar(500)
as
begin
	insert into Register351(Name,Email,Password) Values(@Name,@Email,@Password)
	select 1
end

--------------------------------------------------------------------------
alter proc sp_LoginEmployee351
@Email varchar(250),
@Password varchar(250)
as
begin
	select RegistrationId from Register351 Where Email=@Email and Password=@Password
end


truncate table Register351

delete from Register351 where Email='srbhgjbh@gmail.com'

--------------------------------------------------------------------------
alter proc sp_isVariables
@Email varchar(250)
as
begin
	Update Register351 set IsVerified=1 where Email=@Email
end

insert into Address351 values('bs, ffdf, c',2)

delete from Address351 where AddressId=1
--------------------------------------------------------------------------
exec sp_GetEmployeeDetails

alter PROCEDURE sp_GetEmployeeDetails NULL,nULL,nULL,'@gmail','Name','Desc'
@Id INT = NULL,
@pageSize INT=Null,
@pageIndex INT=Null,
@search varchar(200)=null,
@sortColumn varchar(250)='Name',
@sortOrder varchar(250)='ASC'
as
begin
	if(@Id is null)
	begin
	  IF @pageSize IS NULL
		begin
			SET @pageSize = (SELECT COUNT(*) FROM Employee351)
		end
	  IF @pageIndex IS NULL
		begin
			SET @pageIndex = 1
		end
	  Select E.Id,E.Name,E.Email,E.Gender,E.Contact,E.Salary,D.DepartmentName as Department,
		STUFF((SELECT ', ' + Skill FROM Skills351 WHERE ',' + E.Skills + ',' LIKE '%,' + 
		CAST(SkillId AS VARCHAR(255)) + ',%'FOR XML PATH('')),1,1,'') AS Skills,
		STRING_AGG(Replace(a.Address,',',''), ', ') AS Address,E.ProfilePic From Employee351 E 
		left join Address351 A on A.EmployeeId=E.Id 
		join Department351 D on D.Id=E.Department 
		Where IsDelete=0 and (@search is null or 
								E.Name Like '%' + @search + '%' or 
								E.Email Like '%' + @search + '%' or 
								E.Gender Like '%' + @search + '%' or 
								E.Contact Like '%' + @search + '%' or 
								E.Salary Like '%' + @search + '%' or 
								D.DepartmentName Like '%' + @search + '%' or 
								E.Skills Like '%' + @search + '%' or 
								A.Address Like '%' + @search + '%'
								)
		Group By E.Id,E.Name,E.Gender,E.Salary,E.Skills,D.DepartmentName,E.Contact,E.Id,E.Email,E.ProfilePic
		ORDER BY
        CASE WHEN (@SortColumn = 'Name' AND @SortOrder='ASC') THEN E.Name END ASC,
        CASE WHEN (@SortColumn = 'Name' AND @SortOrder='DESC') THEN E.Name END DESC,
        CASE WHEN (@SortColumn = 'Salary' AND @SortOrder='ASC') THEN E.Salary END ASC,
        CASE WHEN (@SortColumn = 'Salary' AND @SortOrder='DESC') THEN E.Salary END DESC
		Offset @pageSize*(@pageIndex - 1) rows fetch next @pageSize rows only
	end
	else
		Select E.Id,E.Name,E.Email,E.Gender,E.Contact,E.Salary,D.DepartmentName  as Department,
			STUFF((SELECT ', ' + Skill FROM Skills351 WHERE ',' + E.Skills + ',' LIKE '%,' + 
			CAST(SkillId AS VARCHAR(255)) + ',%'FOR XML PATH('')),1,1,'') AS Skills,
			STRING_AGG(Replace(a.Address,',',''), ', ') AS Address,E.ProfilePic From Employee351 E 
			left join Address351 A on A.EmployeeId=E.Id 
			join Department351 D on D.Id=E.Department 
			Where E.Id=@Id and IsDelete=0
			Group By E.Id,E.Name,E.Gender,E.Salary,E.Skills,D.DepartmentName,E.Contact,E.Id,E.Email,E.ProfilePic
end


Create Table Skills351(
	SkillId int not null primary key identity,
	Skill varchar(250) not null unique,
)

INSERT INTO Skills351 
VALUES
  ('Programming'),
  ('Database management'),
  ('Network administration'),
  ('Cybersecurity'),
  ('Data analysis'),
  ('Web development'),
  ('Software engineering'),
  ('Cloud computing'),
  ('IT project management'),
  ('System administration'),
  ('Technical support'),
  ('Information systems management'),
  ('Mobile app development'),
  ('Artificial intelligence'),
  ('Machine learning'),
  ('UI/UX design'),
  ('DevOps'),
  ('Agile methodologies'),
  ('Troubleshooting'),
  ('IT infrastructure');

  update Employee351 set Skills='8,9' where Id=3

alter proc sp_DeleteEmployee 2
@Id int
as
begin
	Declare @RegistrationId int
	Select @RegistrationId=RegistrationId From Employee351 Where Id=@Id
	Begin Tran
		Update Employee351 Set IsDelete=1,RegistrationId=NULL Where Id=@Id
	Commit Tran
	Delete From Register351 Where RegistrationId=@RegistrationId
	select 1
end

insert into Register351 Values('sdsd','alexjohnson@example.com','sssssss',1)
update Employee351 set RegistrationId=2 where Id=2

Alter table Register351
drop column IsDelete

Alter table Employee351
Add RegistrationId int references Register351(RegistrationId) 

Alter table Register351
Add IsDelete bit default 0


------------------------------------------------------------------------------------
