CREATE PROCEDURE GetAllDoctors    
AS    
BEGIN    
 SELECT u.Id, dc.Id AS DoctorId, u.Name, dc.Specialization, u.JoiningDate, u.Email, u.Phone, u.Address, u.City, u.State, u1.Name AS AdminName    
 FROM Users u    
 INNER JOIN Users u1 ON u.AdminId = u1.Id    
 INNER JOIN Doctors dc ON dc.DoctorId = u.Id    
 INNER JOIN Departments d ON d.Id = dc.DepartmentId AND d.IsActive = 1    
 WHERE u.IsActive = 1;    
END