﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Applications.Features.BankAccounts.Commands;
using Ws_BancoTabajara.Applications.Features.Clients.Commands;
using Ws_BancoTabajara.Applications.Features.Clients.ViewModels;
using Ws_BancoTabajara.Domain.Features.BankAccounts;
using Ws_BancoTabajara.Domain.Features.BankStatements;
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
                Number = 1234
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

        public static BankAccount BankAccountWithClientWithAnotherId(Client client)
        {
            return new BankAccount
            {
                Id = 9,
                Client = client,
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
                BankAccountId = bankAccount.Id,
                Date = DateTime.Now,
                OperationType = OperationTypeEnum.Credit,
                Value = 500
            };
        }

        public static Transaction TransactionInvalidValue(BankAccount bankAccount)
        {
            return new Transaction
            {
                BankAccountId = bankAccount.Id,
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

        #region ClientCommands

        public static ClientRegisterCommand AddClient()
        {
            return new ClientRegisterCommand
            {
                Name = "jão",
                CPF = "123.234.345-13",
                RG = "2.123.123",
                BirthDate = "12/08/1996"
            };
        }

        public static ClientUpdateCommand UpdateClient()
        {
            return new ClientUpdateCommand
            {
                Name = "jão",
                CPF = "123.234.345-13",
                RG = "2.123.123",
                BirthDate = "12/08/1996",
                Id = 1
            };
        }

        public static ClientRemoveCommand RemoveClient()
        {
            return new ClientRemoveCommand
            {
                Id = 1
            };
        }

        #endregion

        #region BankAccountCommands
        public static BankAccountRegisterCommand BankAccountRegister()
        {
            return new BankAccountRegisterCommand
            {
                Activated = true,
                Balance = 100,
                ClientId = 1,
                Limit = 100,
                Number = 12345
            };
        }

        public static BankAccountUpdateCommand BankAccountUpdate()
        {
            return new BankAccountUpdateCommand
            {
                Id = 1,
                Activated = true,
                Balance = 100,
                ClientId = 1,
                Limit = 100,
                Number = 1234
            };
        }

        public static BankAccountRemoveCommand BankAccountRemove()
        {
            return new BankAccountRemoveCommand
            {
                Id = 1
            };
        }

        public static BankAccountTransferCommand BrankAccountTransferCommand()
        {
            return new BankAccountTransferCommand
            {
                OriginId = 1,
                DestinationId = 2,
                Value = 490
            };
        }

        public static BankAccountOperationCommand BankAccountOperationCommand()
        {
            return new BankAccountOperationCommand
            {
                Id = 1,
                Value = 700
            };
        }
        #endregion
    }
}
