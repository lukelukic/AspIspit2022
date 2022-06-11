﻿using System;
using System.Collections.Generic;

#nullable disable

namespace AspIspit2022.DataAccess.Models
{
    public partial class ManufacturersVehicleType
    {
        public int VehicleTypeId { get; set; }
        public int ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
        public virtual VehicleType VehicleType { get; set; }
    }
}
