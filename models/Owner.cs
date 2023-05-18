﻿using System;
using System.Collections.Generic;

namespace ServiceStationApi.Models;

public partial class Owner
{
    public int IdUser { get; set; }

    public string NameOwner { get; set; } = null!;

    public DateTime RegistrationDate { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
