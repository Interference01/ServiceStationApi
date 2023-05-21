using System;
using System.Collections.Generic;

namespace ServiceStationApi.Models;

public partial class Car
{
    public int IdAuto { get; set; }

    public int IdUser { get; set; }

    public DateTime YearsOfManufacture { get; set; }

    public string NameAuto { get; set; } = null!;

    public string? VinCode { get; set; }

    public virtual Owner IdUserNavigation { get; set; } = null!;
}
