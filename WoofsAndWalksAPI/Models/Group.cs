﻿using System.ComponentModel.DataAnnotations;

namespace WoofsAndWalksAPI.Models;

public class Group
{

    public Group()
    {
        
    }

    public Group(string name)
    {
        Name = name;
    }
    
    [Key]
    public string Name { get; set; }

    public ICollection<Connection> Connections { get; set; } = new List<Connection>();
}