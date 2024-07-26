using System;
using System.Collections.Generic;

namespace JwtToken.Models;

public partial class ApiKey
{
    public int Id { get; set; }

    public string ApiKey1 { get; set; } = null!;

    public bool IsValid { get; set; }
}
