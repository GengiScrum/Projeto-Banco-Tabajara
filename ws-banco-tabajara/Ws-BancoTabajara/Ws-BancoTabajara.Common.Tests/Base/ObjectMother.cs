using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Domain.Features.BankAccounts;
using Ws_BancoTabajara.Domain.Features.Clients;
using Ws_BancoTabajara.Domain.Features.Transactions;

namespace Ws_BancoTabajara.Common.Tests.Base
{
    public static class ObjectMother
    {

        #region BankAccount
        public static BankAccount ValidActivatedBankAccountWithoutId(Client client)
        {
            return new BankAccount
            {
                Client = client,
                Balance = 300,
                Limit = 500,
                Activated = true,
                Number = 123456
            };
        }

        public static BankAccount ValidDeactivatedBankAccountWithoutId(Client client)
        {
            return new BankAccount
            {
                Client = client,
                Balance = 300,
                Limit = 500,
                Activated = false,
                Number = 123456
            };
        }

        public static BankAccount InvalidBankAccountNumberWithoutId(Client client)
        {
            return new BankAccount
            {
                Client = client,
                Balance = 300,
                Limit = 500,
                Activated = true,
                Number = 0
            };
        }

        public static BankAccount BankAccountWithoutClientWithoutId()
        {
            return new BankAccount
            {
                Balance = 300,
                Limit = 500,
                Activated = true,
                Number = 123456
            };
        }

        public static BankAccount BankAccountWithClientWithoutId()
        {
            return new BankAccount
            {
                Client = ValidClientWithoutId(),
                Balance = 300,
                Limit = 500,
                Activated = true,
                Number = 123456
            };
        }

        public static BankAccount BankAccountWithClientWithId()
        {
            return new BankAccount
            {
                Id = 1,
                Client = ValidClientWithId(),
                Balance = 300,
                Limit = 500,
                Activated = true,
                Number = 123456
            };
        }

        public static BankAccount BankAccountWithClientWithAnotherId()
        {
            return new BankAccount
            {
                Id = 9,
                Client = ValidClientWithoutId(),
                Balance = 300,
                Limit = 500,
                Activated = true,
                Number = 123456
            };
        }

        public static BankAccount BankAccountWithClientWithId(Client client)
        {
            return new BankAccount
            {
                Id = 1,
                Client = client,
                Balance = 300,
                Limit = 500,
                Activated = true,
                Number = 123456
            };
        }
        #endregion

        #region Client
        public static Client ValidClientWithoutId()
        {
            return new Client
            {
                Name = "Jão",
                BirthDate = DateTime.Now.AddYears(-20),
                RG = "3.234.234",
                CPF = "567.123.345-76"
            };
        }

        public static Client ValidClientWithId()
        {
            return new Client
            {
                Id = 1,
                Name = "Jão",
                BirthDate = DateTime.Now.AddYears(-20),
                RG = "3.234.234",
                CPF = "567.123.345-76"
            };
        }

        public static Client ClientEmptyName()
        {
            return new Client
            {
                BirthDate = DateTime.Now.AddYears(-20),
                RG = "3.234.234",
                CPF = "567.123.345-76"
            };
        }

        public static Client ClientNameOverflow()
        {
            return new Client
            {
                Name = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                BirthDate = DateTime.Now.AddYears(-20),
                RG = "3.234.234",
                CPF = "567.123.345-76"
            };
        }

        public static Client ClientEmptyCPF()
        {
            return new Client
            {
                Name = "Jão",
                BirthDate = DateTime.Now.AddYears(-20),
                RG = "3.234.234"
            };
        }

        public static Client ClientCPFOverflow()
        {
            return new Client
            {
                Name = "Jão",
                BirthDate = DateTime.Now.AddYears(-20),
                RG = "3.234.234",
                CPF = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };
        }

        public static Client ClientEmptyRG()
        {
            return new Client
            {
                Name = "Jão",
                BirthDate = DateTime.Now.AddYears(-20),
                CPF = "567.123.345-76"
            };
        }

        public static Client ClientRGOverflow()
        {
            return new Client
            {
                Name = "Jão",
                BirthDate = DateTime.Now.AddYears(-20),
                RG = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                CPF = "567.123.345-76"
            };
        }
        #endregion

        #region Transaction

        public static Transaction ValidCreditTransaction(BankAccount bankAccount)
        {
            return new Transaction
            {
                BankAccount = bankAccount,
                Date = DateTime.Now,
                OperationType = OperationTypeEnum.Credit,
                Value = 500
            };
        }

        public static Transaction TransactionInvalidValue(BankAccount bankAccount)
        {
            return new Transaction
            {
                BankAccount = bankAccount,
                Date = DateTime.Now,
                OperationType = OperationTypeEnum.Credit,
                Value = 0
            };
        }

        public static Transaction TransactionWithoutBankAccount()
        {
            return new Transaction
            {
                Date = DateTime.Now,
                OperationType = OperationTypeEnum.Credit,
                Value = 500
            };
        }

        #endregion
    }
}
