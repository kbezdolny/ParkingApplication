﻿using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using ParkingApplication.BL.Models;
using ParkingApplication.BL.Services.Interfaces;
using ParkingApplication.DAL.Entities;
using ParkingApplication.DAL.Repositories.Interfaces;

namespace ParkingApplication.BL.Services.Classes;

public class ParkingTemplateService : IParkingTemplateService
{
    private readonly IParkingTemplateRepository _repository;
    private readonly IAdminService _adminService;
    private readonly IMapper _mapper;

    public ParkingTemplateService(IMapper mapper, IParkingTemplateRepository repository, IAdminService adminService)
    {
        _mapper = mapper;
        _repository = repository;
        _adminService = adminService;
    }

    public async Task AddParkingTemplate(ParkingTemplateModel parkingTemplate)
    {
        var entity = _mapper.Map<ParkingTemplate>(parkingTemplate);
        await _repository.AddAsync(entity);
    }

    public async Task DeleteParkingTemplate(int id)
    {
        await _repository.DeleteAsync(id);
    }

    public List<ParkingTemplate> GetAllParking()
    {
        var parking = _repository.GetAll();
        return parking.ToList();
    }

    public async Task<float> GetPriceByDateTime(DateTime dateTime, int parkingId)
    {
        var parking = await _repository.GetByIdAsync(parkingId);
        float totalPrice = (float)(dateTime - DateTime.Now).TotalHours * parking.Price;
        totalPrice = (float)Math.Round(totalPrice, 2);
        return totalPrice < 0 ? 0 : totalPrice;
    }
}