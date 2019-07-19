using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public class Bank<T> where T : Account
    {
        T[] accounts;

        public string name;

        public Bank(string name)
        {
            this.name = name;
        }

        public void Open(AccountType accountType, decimal sum, AccountStateHandler addSumHandler, AccountStateHandler withdrawSumHandler,
        AccountStateHandler calculationHandler, AccountStateHandler closeAccountHandler,
        AccountStateHandler openAccountHandler)
        {
            T newAccount = null;

            switch (accountType)
            {
                case AccountType.Ordinary:
                    newAccount = new DemandAccount(sum, 1) as T;
                    break;
                case AccountType.Deposit:
                    newAccount = new DepositAccount(sum, 40) as T;
                    break;
            }
            if (newAccount == null)
            {
                throw new Exception("Ошибка создания счёта.");
            }
            if (accounts == null)
            {
                accounts = new T[] { newAccount };
            }
            else
            {
                T[] tempAccounts = new T[accounts.Length + 1];
                for(int i = 0; i < accounts.Length; i++)
                {
                    tempAccounts[i] = accounts[i];
                    tempAccounts[tempAccounts.Length - 1] = newAccount;
                    accounts = tempAccounts;
                }
            }

            newAccount.Added += addSumHandler;
            newAccount.Withdrawеd += withdrawSumHandler;
            newAccount.Closed += closeAccountHandler;
            newAccount.Opened += openAccountHandler;
            newAccount.Calculated += calculationHandler;

            newAccount.Open();
        }

        public void Put(decimal _sum, int _id)
        {
            T account = FindAccount(_id);
            if (account == null)
            {
                throw new Exception("Счёт не найден!");
            }
            account.Put(_sum);
        }

        public void Withdraw(decimal _sum, int _id)
        {
            T account = FindAccount(_id);
            if (account == null)
            {
                throw new Exception("Счёт не найден!");
            }
            account.Withdrow(_sum);
        }
        
        public void Close(int _id)
        {
            int index;
            T account = FindAccount(_id, out index);
            if (account == null)
            {
                throw new Exception("Счёт не найден");
            }

            account.Close();

            if (accounts.Length <= 1)
            {
                accounts = null;
            }
            else
            {
                T[] tempAccounts = new T[accounts.Length - 1];
                for(int i = 0, j = 0; i < accounts.Length; i++)
                {
                    if (i != index)
                    {
                        tempAccounts[j++] = accounts[i];
                    }
                }
                accounts = tempAccounts;
            }
        }

        public void CalculatePercentage()
        {
            if (accounts == null)
            {
                return;
            }
            for(int i=0;i<accounts.Length;i++)
            {
                T account = accounts[i];
                account.IncrementDays();
                account.Calculate();
            }
        }

        public T FindAccount(int _id)
        {
            for(int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i].ID == _id)
                {
                    return accounts[i];
                }
            }
            return null;
        }

        public T FindAccount(int _id, out int _index)
        {
            for(int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i].ID == _id)
                {
                    _index = i;
                    return accounts[i];
                }
            }
            _index = -1;
            return null;
        }
    }
    public enum AccountType
    {
        Ordinary,
        Deposit
    }
}
