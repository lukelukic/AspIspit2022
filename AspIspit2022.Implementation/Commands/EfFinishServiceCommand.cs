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
    public class EfFinishServiceCommand : IFinishServiceCommand
    {
        public int Id => 2;

        public string Name => "Finish service using EF";

        public string Description => "";

        private AspIspitContext _context;
        private FinishServiceValidator validator;

        public EfFinishServiceCommand(AspIspitContext context, FinishServiceValidator validator)
        {
            _context = context;
            this.validator = validator;
        }

        public void Execute(FinishServiceDto request)
        {
            validator.ValidateAndThrow(request);

            var service = _context.ServiceSchedules.Find(request.ServiceId.Value);

            service.Price = request.Price.Value;
            service.FinishedAt = DateTime.UtcNow;
            _context.SaveChanges();
        }
    }
}
