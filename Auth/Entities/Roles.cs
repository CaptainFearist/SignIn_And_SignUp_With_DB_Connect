﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace users.Entities;

public partial class Roles
{
    public int Id { get; set; }

    public string RoleName { get; set; }

    public virtual ICollection<Users> Users { get; set; } = new List<Users>();
}