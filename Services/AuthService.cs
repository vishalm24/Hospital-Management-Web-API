using Hospital_Management.Data;
using Hospital_Management.Exceptions;
using Hospital_Management.Helper;
using Hospital_Management.Model;
using Hospital_Management.Model.DTO;
using Hospital_Management.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace Hospital_Management.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _db;
        private readonly JwtTokenHelper _jwtHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(ApplicationDbContext context, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _jwtHelper = new JwtTokenHelper(config);
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> Login(LoginRequest model)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == model.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                throw new UnauthorizedException("Login Error");

            var token = _jwtHelper.GenerateToken(user);
            return token;
        }

        //public async Task<string> RegisterFirstAdmin(RegisterRequest model)
        //{
        //    var user = new User
        //    {
        //        Name = model.Name,
        //        Username = model.Username,
        //        Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
        //        Role = "Admin",
        //        JoiningDate = DateOnly.FromDateTime(DateTime.Now),
        //        Email = model.Email,
        //        Phone = model.Phone,
        //        Address = model.Address,
        //        City = model.City,
        //        State = model.State,
        //        IsActive = true,
        //        AdminId = null
        //    };
        //    await _db.Users.AddAsync(user);
        //    await _db.SaveChangesAsync();
        //    return $"New Admin created. UserName : {user.Username}";
        //}

        public async Task<string> RegisterAdmin(RegisterRequest model)
        {
            var adminName = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            var admin = await _db.Users.FirstOrDefaultAsync(u => u.Username == adminName);
            if (admin == null)
                throw new ForbiddenException("You are not authorized to create an admin account.");
            var user = new User
            {
                Name = model.Name,
                Username = model.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Role = "Admin",
                JoiningDate = DateOnly.FromDateTime(DateTime.Now),
                Email = model.Email,
                Phone = model.Phone,
                Address = model.Address,
                City = model.City,
                State = model.State,
                IsActive = true,
                AdminId = admin.Id
            };
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return $"New Admin created. UserName : {user.Username}";
        }

        //public async Task<string> RegisterAdmin(RegisterRequest model)
        //{
        //    //var adminName = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
        //    //var admin = await _db.Users.FirstOrDefaultAsync(u => u.Username == adminName);
        //    var user = new User
        //    {
        //        Name = model.Name,
        //        Username = model.Username,
        //        Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
        //        Role = "Admin",
        //        JoiningDate = DateOnly.FromDateTime(DateTime.Now),
        //        Email = model.Email,
        //        Phone = model.Phone,
        //        Address = model.Address,
        //        City = model.City,
        //        State = model.State,
        //        IsActive = true,
        //        //AdminId = admin.Id
        //    };
        //    await _db.Users.AddAsync(user);
        //    await _db.SaveChangesAsync();
        //    return $"New Admin created. UserName : {user.Username}";
        //}

        public async Task<string> RegisterReceptionist(RegisterRequest model)
        {
            var adminName = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            var admin = await _db.Users.FirstOrDefaultAsync(u => u.Username == adminName);
            if (admin == null)
                throw new ForbiddenException("You are not authorized to create an admin account.");
            var user = new User
            {
                Name = model.Name,
                Username = model.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Role = "Receptionist",
                JoiningDate = DateOnly.FromDateTime(DateTime.Now),
                Email = model.Email,
                Phone = model.Phone,
                Address = model.Address,
                City = model.City,
                State = model.State,
                IsActive = true,
                AdminId = admin.Id
            };
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return $"New Receptionist created. UserName : {user.Username}";
        }
        public async Task<string> RegisterDoctor(RegisterDoctorRequest model)
        {
            var adminName = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            var admin = await _db.Users.FirstOrDefaultAsync(u => u.Username == adminName);
            if (admin == null)
                throw new ForbiddenException("You are not authorized to create an admin account.");
            var user = new User
            {
                Name = model.Name,
                Username = model.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Role = "Doctor",
                JoiningDate = DateOnly.FromDateTime(DateTime.Now),
                Email = model.Email,
                Phone = model.Phone,
                Address = model.Address,
                City = model.City,
                State = model.State,
                IsActive = true,
                AdminId = admin.Id
            };
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            var doctor = new Doctor
            {
                DepartmentId = model.DepartmentId,
                DoctorId = user.Id,
                AdminId = admin.Id,
                Specialization = model.Specialization
            };
            await _db.Doctors.AddAsync(doctor);
            await _db.SaveChangesAsync();
            return $"New Dcotor created. UserName : {user.Username}";
        }
        public async Task<ResponseModel<string>> PasswordChange(string password)
        {
            var result = new ResponseModel<string>();
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == userName);
            if (user == null)
                throw new NotFoundException("User not found.");
            user.Password = BCrypt.Net.BCrypt.HashPassword(password);
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            result.SetSeccess($"{user.Name} has changed password");
            return result;
        }
    }
}
