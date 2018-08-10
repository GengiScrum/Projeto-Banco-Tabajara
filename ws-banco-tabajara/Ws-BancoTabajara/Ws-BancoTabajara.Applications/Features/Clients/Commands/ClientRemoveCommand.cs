using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ws_BancoTabajara.Applications.Features.Clients.Commands
{
    public class ClientRemoveCommand
    {
        public int Id { get; set; }
        public ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }
        class Validator : AbstractValidator<ClientRemoveCommand>
        {
            public Validator()
            {
                RuleFor(c => c.Id).NotNull().GreaterThan(0);
            }
        }
    }
}
