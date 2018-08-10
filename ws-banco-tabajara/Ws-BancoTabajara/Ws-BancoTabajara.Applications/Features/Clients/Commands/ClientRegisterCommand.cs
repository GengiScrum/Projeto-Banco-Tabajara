using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ws_BancoTabajara.Applications.Features.Clients.Commands
{
    public class ClientRegisterCommand
    {
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }

        public ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<ClientRegisterCommand>
        {
            public Validator()
            {
                RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(50);
                RuleFor(c => c.CPF).NotNull().NotEmpty().MaximumLength(14);
                RuleFor(c => c.RG).NotNull().NotEmpty().MaximumLength(12);
            }
        }
    }
}
