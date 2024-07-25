using System;
using System.Collections.Generic;

namespace JwtToken.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Category { get; set; }

    public decimal? Price { get; set; }

    public int? Quantity { get; set; }

    public string? ImageFile { get; set; }
}
