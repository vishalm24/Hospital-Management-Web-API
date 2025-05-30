﻿namespace Hospital_Management.Model.DTO
{
    public class DoctorUpdateDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateOnly JoiningDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int DepartmentId { get; set; }
        public string Specialization { get; set; }
        public int AdminId { get; set; }
    }
}
