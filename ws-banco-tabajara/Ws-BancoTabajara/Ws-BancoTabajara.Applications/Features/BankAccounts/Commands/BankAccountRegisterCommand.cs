using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ws_BancoTabajara.Applications.Features.BankAccounts.Commands
{
    public class BankAccountRegisterCommand
    {
        public int Number { get; set; }
        public int ClientId { get; set; }
        public double Balance { get; set; }
        public bool Activated { get; set; }
        public double Limit { get; set; }

        public virtual ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<BankAccountRegisterCommand>
        {
            public Validator()
            {
                RuleFor(ba => ba.Number).NotNull().NotEmpty().GreaterThan(0);
                RuleFor(ba => ba.ClientId).NotNull().GreaterThan(0);
                RuleFor(ba => ba.Balance).NotNull().NotEmpty();
                RuleFor(ba => ba.Limit).NotNull().NotEmpty();
            }
        }
    }
}
