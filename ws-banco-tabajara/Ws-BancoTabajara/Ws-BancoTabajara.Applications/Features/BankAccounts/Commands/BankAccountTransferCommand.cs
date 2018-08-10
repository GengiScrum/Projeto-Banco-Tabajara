using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ws_BancoTabajara.Applications.Features.BankAccounts.Commands
{
    public class BankAccountTransferCommand
    {
        public int OriginId { get; set; }
        public int DestinationId { get; set; }
        public double Value { get; set; }
        public virtual ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<BankAccountTransferCommand>
        {
            public Validator()
            {
                RuleFor(ba => ba.OriginId).NotNull().NotEmpty().GreaterThan(0);
                RuleFor(ba => ba.DestinationId).NotNull().NotEmpty().GreaterThan(0).NotEqual(ba => ba.OriginId);
                RuleFor(ba => ba.Value).NotNull().GreaterThan(0);
            }
        }
    }
}
