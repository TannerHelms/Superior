using System;
using System.Collections.Generic;

namespace Superior_Cloud_Accounting.Models;

public partial class Form
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Message { get; set; } = null!;
}
