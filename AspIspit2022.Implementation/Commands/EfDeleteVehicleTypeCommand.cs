using AspIspit2022.Application.Exceptions;
using AspIspit2022.Application.UseCases.Commands;
using AspIspit2022.DataAccess.Data;
using AspIspit2022.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspIspit2022.Implementation.Commands
{
    public class EfDeleteVehicleTypeCommand : IDeleteVehicleTypeCommand
    {
        public int Id => 6;

        public string Name => "Ef Delete Vehicle Type";

        public string Description => "";

        private AspIspitContext _context;

        public EfDeleteVehicleTypeCommand(AspIspitContext context)
        {
            _context = context;
        }

        public void Execute(int request)
        {
            var vehicleType = _context.VehicleTypes
                            .Include(x => x.ManufacturersVehicleTypes)
                            .Include(x => x.Models).FirstOrDefault(x => x.Id == request);

            if(vehicleType == null)
            {
                throw new EntityNotFoundException(nameof(VehicleType), request);
            }

            if(vehicleType.ManufacturersVehicleTypes.Any())
            {
                throw new UseCaseConflictException("Unable to delete vehicle type because it is linked to manufacturers.");
            }

            if(vehicleType.Models.Any())
            {
                throw new UseCaseConflictException("Unable to delete vehicle type because it is linked to models.");

            }

            _context.VehicleTypes.Remove(vehicleType);
            _context.SaveChanges();
        }
    }
}
