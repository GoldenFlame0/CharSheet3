using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharSheet3.Models;

public class Class5e
{
    public string Name { get; set; } = "New Class";
    public int HitDie { get; set; } = 8; // Default to D8
    public Dictionary<ClassFeature5e, int> Features { get; set; } = [];
    public List<Subclass5e> Subclasses { get; set; } = [];
}

public class Subclass5e
{
    public string Name { get; set; } = "New Subclass";
    public Dictionary<ClassFeature5e, int> Features { get; set; } = [];
}

public class  ClassFeature5e
{
    public string Name { get; set; } = "New Feature";
    public string Description { get; set; } = "Feature description goes here.";
}

