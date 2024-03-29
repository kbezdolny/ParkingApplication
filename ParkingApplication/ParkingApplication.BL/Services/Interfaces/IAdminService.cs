﻿using ParkingApplication.BL.Models;
using ParkingApplication.DAL.Entities;

namespace ParkingApplication.BL.Services.Interfaces;

public interface IAdminService
{
    Task<Admin> AddAdmin(AdminModel admin);
    Task<AdminModel?> GetAdminById(int id);
    Task<AdminModel?> GetAdminByEmail(string email);
    Task DeleteAdmin(int id);
    List<Admin> GetAllAdmins();
    Task<bool> CheckAdmin(string token);
    Task AddParkingToAdmin(AdminModel admin);
    Task<ParkingTemplateModel?> GetGetCurrentParkingForAdmin(string token);
}