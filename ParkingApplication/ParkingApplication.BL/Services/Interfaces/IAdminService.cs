﻿using ParkingApplication.BL.Models;
using ParkingApplication.DAL.Entities;

namespace ParkingApplication.BL.Services.Interfaces;

public interface IAdminService
{
    Task<Admin> AddAdmin(AdminModel admin);
    Task<AdminModel?> GetAdminById(int id);
    Task<AdminModel?> GetAdminByEmail(string email);
    Task DeleteAdmin(int id);
    Task<bool> CheckAdmin(string token);
    Task AddParkingToAdmin(int adminId, int parkingTemplateId);
}