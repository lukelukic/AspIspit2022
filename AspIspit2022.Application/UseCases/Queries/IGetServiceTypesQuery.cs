using AspIspit2022.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspIspit2022.Application.UseCases.Queries
{
    public interface IGetServiceSchedulesQuery : IQuery<ServiceSchedulesSearch, PagedResponse<ServiceScheduleDto>>
    {
    }
}
