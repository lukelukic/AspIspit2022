using System;
using System.Collections.Generic;

#nullable disable

namespace AspIspit2022.DataAccess.Models
{
    public partial class ServiceType
    {
        public ServiceType()
        {
            ServiceSchedules = new HashSet<ServiceSchedule>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ServiceSchedule> ServiceSchedules { get; set; }
    }
}
