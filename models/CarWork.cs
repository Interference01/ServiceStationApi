using System;
using System.Collections.Generic;

namespace ServiceStationApi.Models;

public partial class CarWork
{
    public int IdWork { get; set; }

    public int IdAuto { get; set; }

    public int? Mileage { get; set; }

    public string DescriptionWork { get; set; } = null!;

    public DateTime? Date { get; set; }

    public string? Note { get; set; }
}
