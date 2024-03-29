﻿using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParkingApplication.BL.Models;
using ParkingApplication.BL.Services.Interfaces;
using ParkingApplication.DTOs;

namespace ParkingApplication.Controllers;

[ApiController]
public class ParkingController : ControllerBase
{
    private readonly IParkingTemplateService _parkingTemplateService;
    private readonly IParkingService _parkingService;
    private readonly IOwnerService _ownerService;
    private readonly ICarService _carService;
    private readonly IMapper _mapper;

    public ParkingController(IMapper mapper, IParkingTemplateService parkingTemplateService, IParkingService parkingService, IOwnerService ownerService, ICarService carService)
    {
        _mapper = mapper;
        _parkingTemplateService = parkingTemplateService;
        _parkingService = parkingService;
        _ownerService = ownerService;
        _carService = carService;
    }

    [HttpPost("addNewParking"), Authorize]
    public async Task<IActionResult> AddNewParking([FromBody]ParkingTemplateDto parking)
    {
        var mapped = _mapper.Map<ParkingTemplateModel>(parking);
        await _parkingTemplateService.AddParkingTemplate(mapped);
        
        return Ok();
    }

    [HttpGet("getAllParking"), Authorize]
    public IActionResult GetAllParking()
    {
        var parking = _parkingTemplateService.GetAllParking();
        return Ok(Results.Json(parking));
    }
    
    [HttpDelete("deleteParking"), Authorize]
    public async Task<IActionResult> DeleteParking([FromBody]int id)
    {
        await _parkingTemplateService.DeleteParkingTemplate(id);
        return Ok();
    }
    
    [HttpPost("getParkingSlotsData"), Authorize]
    public IActionResult GetParkingSlotsData([FromBody]RequestSlotsDto request)
    {
        var result = _parkingService.GetParkingSlotsData(request.ParkingId, request.Floor);
        return Ok(Results.Json(result));
    }
    
    [HttpPost("getPriceByDateTime"), Authorize]
    public IActionResult GetPriceByDateTime([FromBody]DateTimePriceDto request)
    {
        var result = _parkingTemplateService.GetPriceByDateTime(request.StandsUntil, request.ParkingId);
        return Ok(Results.Json(new
        {
            price = result
        }));
    }
    
    [HttpPost("reservationCarToParking"), Authorize]
    public async Task<IActionResult> ReservationCarToParking([FromBody]ReservationDataDto data)
    {
        var mappedOwner = _mapper.Map<OwnerModel>(data.OwnerData);
        var ownerResult = await _ownerService.AddOwner(mappedOwner);
        
        var mappedCar = _mapper.Map<CarModel>(data.CarData);
        mappedCar.OwnerId = ownerResult.Id;
        var carResult = await _carService.AddCar(mappedCar);
        
        var mappedParking = _mapper.Map<ParkingModel>(data.ParkingData);
        mappedParking.CarId = carResult.Id;
        await _parkingService.AddCarToParking(mappedParking);

        return Ok();
    }
    
    [HttpPost("getSlotData"), Authorize]
    public async Task<IActionResult> GetSlotData([FromBody]RequestSlotsDto request)
    {
        var result = _mapper.Map<ReservationDataDto>(await _parkingService.GetSlotData(request.ParkingId, request.Floor, request.Slot));
        return Ok(Results.Json(result));
    }
    
    [HttpPost("getHistoryForFloor"), Authorize]
    public async Task<IActionResult> GetHistoryForFloor([FromBody]RequestSlotsDto request)
    {
        var result = _mapper.Map<List<ReservationDataDto>>(await _parkingService.GetHistoryForFloor(request.ParkingId, request.Floor));
        return Ok(Results.Json(result));
    }
}