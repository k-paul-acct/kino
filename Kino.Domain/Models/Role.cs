﻿namespace Kino.Domain.Models;

public class Role
{
    public int Id { get; set; }
    public string? RoleName { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
}