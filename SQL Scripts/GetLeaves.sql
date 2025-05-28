CREATE PROCEDURE GetLeave
AS
BEGIN
	SELECT l.Id, u.Name AS DoctorName, l.StartDate, l.EndDate, l.Reason, u1.Name AS AdminName
	FROM Leaves l
	INNER JOIN Doctors dc ON dc.Id = l.DoctorId
	INNER JOIN Users u ON u.Id = dc.DoctorId AND u.IsActive = 1
	LEFT JOIN Users u1 ON u1.Id = l.AdminId;
END

