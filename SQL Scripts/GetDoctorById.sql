CREATE PROCEDURE GetDoctorById
	@Id INT
AS
BEGIN
	SELECT u.Name, dc.Specialization, u.JoiningDate, u.Email, u.Phone, u.Address, u.City, u.State, u1.Name
	FROM Users u
	INNER JOIN Users u1 ON u.AdminId = u1.Id
	INNER JOIN Doctors dc ON dc.DoctorId = u.Id AND dc.Id = @Id
	INNER JOIN Departments d ON d.Id = dc.DepartmentId AND d.IsActive = 1
	WHERE u.IsActive = 0;
END