using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ws_BancoTabajara.Applications.Features.BankAccounts.Commands
{
    public class BankAccountUpdateCommand
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int ClientId { get; set; }
        public double Balance { get; set; }
        public bool Activated { get; set; }
        public double Limit { get; set; }

        public virtual ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }
        class Validator : AbstractValidator<BankAccountUpdateCommand>
        {
            public Validator()
            {
                RuleFor(ba => ba.Id).NotNull().GreaterThan(0);
                RuleFor(ba => ba.Balance).NotNull().NotEmpty();
                RuleFor(ba => ba.Limit).NotNull().NotEmpty();
            }
        }
    }
}
