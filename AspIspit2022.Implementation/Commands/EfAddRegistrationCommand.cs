using AspIspit2022.Application.DTO;
using AspIspit2022.Application.UseCases.Commands;
using AspIspit2022.DataAccess.Data;
using AspIspit2022.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspIspit2022.Implementation.Commands
{
    public class EfAddRegistrationCommand : IAddRegistrationCommand
    {
        public int Id => 4;

        public string Name => "Add new registration";

        public string Description => "";

        private readonly AspIspitContext _context;
        private readonly AddRegistrationValidator _validator;

        public EfAddRegistrationCommand(AspIspitContext context, AddRegistrationValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public void Execute(RegistrationDto request)
        {
            _validator.ValidateAndThrow(request);

            var registration = new DataAccess.Models.Registration
            {
                VehicleId = request.VehicleId.Value,
                RegistrationPlateId = request.RegistrationPlateId.Value,
                IssuedAt = DateTime.UtcNow,
                ValidUntil = DateTime.UtcNow.AddYears(1)
            };

            _context.Registrations.Add(registration);
            _context.SaveChanges();
        }
    }
}
