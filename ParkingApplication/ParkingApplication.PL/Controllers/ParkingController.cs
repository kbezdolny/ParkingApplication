﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingApplication.BL.Models;
using ParkingApplication.BL.Services.Interfaces;
using ParkingApplication.DTOs;

namespace ParkingApplication.Controllers;

public class ParkingController : Controller
{
    private readonly ILogger<ParkingController> _logger;
    private readonly IParkingTemplateService _parkingTemplateService;
    private readonly IMapper _mapper;

    public ParkingController(ILogger<ParkingController> logger, IMapper mapper, IParkingTemplateService parkingTemplateService)
    {
        _logger = logger;
        _mapper = mapper;
        _parkingTemplateService = parkingTemplateService;
    }

    [HttpPost("addNewParking"), Authorize]
    public async Task<IActionResult> AddNewParking([FromBody]ParkingTemplateDto parking)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var mapped = _mapper.Map<ParkingTemplateModel>(parking);
        await _parkingTemplateService.AddParkingTemplate(mapped, token);
        
        return Ok();
    }
}