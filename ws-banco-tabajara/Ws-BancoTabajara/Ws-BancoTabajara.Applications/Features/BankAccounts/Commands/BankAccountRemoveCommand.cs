using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ws_BancoTabajara.Applications.Features.BankAccounts.Commands
{
    public class BankAccountRemoveCommand
    {
        public int Id { get; set; }

        public virtual ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }
        
        class Validator : AbstractValidator<BankAccountRemoveCommand>
        {
            public Validator()
            {
                RuleFor(ba => ba.Id).NotNull().GreaterThan(0);
            }
        }
    }
}
