using AspIspit2022.Application.DTO;
using AspIspit2022.DataAccess.Data;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspIspit2022.Implementation.Validators
{
    public class FinishServiceValidator : AbstractValidator<FinishServiceDto>
    {
        public FinishServiceValidator(AspIspitContext context)
        {
            RuleFor(x => x.ServiceId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Servis je obavezan podatak.")
                .Must(x => context.ServiceSchedules.Any(ss => ss.Id == x))
                    .WithMessage("Traženi servis ne postoji.")
                .Must(x => context.ServiceSchedules.Any(ss => ss.Id == x && ss.FinishedAt == null))
                    .WithMessage("Traženi servis je već završen.");

            RuleFor(x => x.Price)
                  .Cascade(CascadeMode.Stop)
                  .NotEmpty().WithMessage("Cena je obavezan podatak.")
                  .GreaterThan(0).WithMessage("Cena mora biti pozitivan broj.");
        }
    }

    public class AddRegistrationValidator : AbstractValidator<RegistrationDto>
    {
        public AddRegistrationValidator(AspIspitContext context)
        {
            RuleFor(x => x.VehicleId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Vozilo je obavezan podatak.")
                .Must(x => context.Vehicles.Any(v => v.Id == x))
                    .WithMessage("Traženo vozilo ne postoji.")
                .Must(x => !context.Vehicles.Any(v => v.Id == x && v.Registrations.Any(r => r.ValidUntil > DateTime.UtcNow)))
                    .WithMessage("Nije moguće izvršiti registraciju jer je vozilo već registrovano.");

            RuleFor(x => x.RegistrationPlateId)
                  .Cascade(CascadeMode.Stop)
                  .NotEmpty().WithMessage("Tablica je obavezan podatak.")
                  .Must(x => context.RegistrationPlates.Any(rp => rp.Id == x)).WithMessage("Tablica ne postoji.")
                  .Must(x => !context.Registrations.Any(r => r.RegistrationPlateId == x && r.ValidUntil > DateTime.UtcNow))
                  .WithMessage("Tablica je već u upotrebi za drugo vozilo.");
        }
    }
}
