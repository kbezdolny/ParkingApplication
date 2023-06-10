﻿namespace ParkingApplication.DAL.Entities;

public class ParkingTemplate : BaseEntity
{
    public string Name { get; set; }
    public int FloorsCount { get; set; }
    public int SlotsCount { get; set; }
    public virtual ICollection<Admin>? Admins { get; set; }
    public virtual ICollection<Parking>? Parkings { get; set; }
}