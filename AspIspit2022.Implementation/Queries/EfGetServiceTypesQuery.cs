using AspIspit2022.Application.DTO;
using AspIspit2022.Application.UseCases.Queries;
using AspIspit2022.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspIspit2022.Implementation.Queries
{
    public class EfGetServiceSchedulesQuery : IGetServiceSchedulesQuery
    {
        public int Id => 3;

        public string Name => "Search Services";

        public string Description => "";

        private AspIspitContext _context;

        public EfGetServiceSchedulesQuery(AspIspitContext context)
        {
            _context = context;
        }

        public PagedResponse<ServiceScheduleDto> Execute(ServiceSchedulesSearch search)
        {
            var query = _context.ServiceSchedules
                        .Include(x => x.ServiceType)
                        .Include(x => x.Vehicle).ThenInclude(x => x.Registrations).ThenInclude(x => x.RegistrationPlate)
                        .Include(x => x.Vehicle).ThenInclude(x => x.Model).ThenInclude(x => x.Manufacturer)
                        .Include(x => x.Vehicle).ThenInclude(x => x.Model).ThenInclude(x => x.VehicleType)
                        .AsQueryable();

            if(!string.IsNullOrEmpty(search.Keyword))
            {
                /*
                    a. Keyword čija vrednost će se porediti sa sledećim tabelama i njihovim kolonama:
                    Vehicles (Label), Model(Name), Manufacturers(Name), VehicleTypes(Name),
                    RegistrationPlates(RegistrationPlate), ServiceType (Name)   
                 */
                var kw = search.Keyword.ToLower();
                query = query.Where(x => x.Vehicle.Label.Contains(kw) ||
                                         x.Vehicle.Model.Name.Contains(kw) ||
                                         x.Vehicle.Model.Manufacturer.Name.Contains(kw) ||
                                         x.Vehicle.Model.VehicleType.Name.Contains(kw) ||
                                         x.ServiceType.Name.Contains(kw) ||
                                         x.Vehicle.Registrations.Any(r => r.RegistrationPlate.RegistrationPlate1.Contains(kw) && r.ValidUntil > DateTime.UtcNow));
            }
            
            if(search.IsFinished.HasValue)
            {
                var finished = search.IsFinished.Value;
                query = query.Where(x => finished ? x.FinishedAt != null : x.FinishedAt == null);
            }


            if(search.MinPrice.HasValue)
            {
                query = query.Where(x => x.Price >= search.MinPrice.Value);
            }

            if (search.MaxPrice.HasValue)
            {
                query = query.Where(x => x.Price <= search.MaxPrice.Value);
            }

            if (search.PerPage == 0 || search.PerPage < 1)
            {
                search.PerPage = 15;
            }

            if (search.Page == 0 || search.Page < 1)
            {
                search.Page = 1;
            }

            var toSkip = (search.Page - 1) * search.PerPage;

            var response = new PagedResponse<ServiceScheduleDto>();
            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip).Take(search.PerPage).Select(x => new ServiceScheduleDto
            {
                Id = x.Id,
                AdditionalInfo = x.AdditionalInfo == null ? "/" : x.AdditionalInfo,
                FinishedAt = x.FinishedAt,
                Manufacturer = x.Vehicle.Model.Manufacturer.Name,
                Model = x.Vehicle.Model.Name,
                Price = x.Price,
                RegistrationPlate = x.Vehicle.Registrations.OrderByDescending(x => x.ValidUntil).Any() ? 
                                    x.Vehicle.Registrations.OrderByDescending(x => x.ValidUntil).FirstOrDefault().RegistrationPlate.RegistrationPlate1 : 
                                    "/",
                ServiceTypeName = x.ServiceType.Name,
                ScheduleFor = x.ScheduledFor
            }).ToList();
            response.CurrentPage = search.Page;
            response.ItemsPerPage = search.PerPage;

            return response;
        }
    }
}
