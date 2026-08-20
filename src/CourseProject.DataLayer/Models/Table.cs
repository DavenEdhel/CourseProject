using System;
using System.Collections.Generic;

namespace CourseProject.DataLayer.Models;

public partial class Table
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;
}
