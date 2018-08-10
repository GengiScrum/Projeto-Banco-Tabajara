using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ws_BancoTabajara.Applications.Features.BankAccounts.Commands
{
    public class BankAccountOperationCommand
    {
        public int Id { get; set; }
        public double Value { get; set; }

        public virtual ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<BankAccountOperationCommand>
        {
            public Validator()
            {
                RuleFor(ba => ba.Id).NotNull().NotEmpty().GreaterThan(0);
                RuleFor(ba => ba.Value).NotNull().GreaterThan(0);
            }
        }
    }
}
