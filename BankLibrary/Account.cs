using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public abstract class Account:IAccount
    {
        protected internal event AccountStateHandler Withdrawеd;
        protected internal event AccountStateHandler Added;
        protected internal event AccountStateHandler Opened;
        protected internal event AccountStateHandler Closed;
        protected internal event AccountStateHandler Calculated;

        protected int id;
        static int counter = 0;

        protected decimal sum;
        protected int percentage;

        protected int days = 0;  

        public Account(decimal _sum, int _percentage)
        {
            sum = _sum;
            percentage = _percentage;
            id = ++counter;
        }

        public decimal CurrentSum
        {
            get
            {
                return sum;
            }
        }
        public int Percentage
        {
            get
            {
                return percentage;
            }
        }
        public int ID
        {
            get
            {
                return id;
            }
        }

        private void CallEvent(AccountEventArgs accountEventArgs, AccountStateHandler handler)
        {
            if(handler!=null && accountEventArgs != null)
            {
                handler(this, accountEventArgs);
            }
        }

        protected virtual void OnOpened(AccountEventArgs accountEventArgs)
        {
            CallEvent(accountEventArgs, Opened);
        }
        protected virtual void OnWithdrawed(AccountEventArgs accountEventArgs)
        {
            CallEvent(accountEventArgs, Withdrawеd); 
        }
        protected virtual void OnAdded(AccountEventArgs accountEventArgs)
        {
            CallEvent(accountEventArgs, Added);
        }
        protected virtual void OnClosed(AccountEventArgs accountEventArgs)
        {
            CallEvent(accountEventArgs, Closed);
        }
        protected virtual void OnCalculated(AccountEventArgs accountEventArgs)
        {
            CallEvent(accountEventArgs, Calculated);
        }

        public virtual void Put(decimal _sum)
        {
            sum += _sum;
            OnAdded(new AccountEventArgs("На счёт поступило " + _sum, _sum));
        }

        public virtual decimal Withdrow(decimal _sum)
        {
            decimal result = 0;
            if (_sum <= sum)
            {
                sum -= _sum;
                result = sum;
                OnWithdrawed(new AccountEventArgs("Сумма " + _sum + " снята со счёта " + id, _sum));
            }
            else
            {
                OnWithdrawed(new AccountEventArgs("Недостаточно средств на счёте " + id, 0));
            }
            return result;
        }

        protected internal virtual void Open()
        {
            OnOpened(new AccountEventArgs("Открытие нового счёта! ID сёта: " + this.id, this.sum));
        }

        protected internal virtual void Close()
        {
            OnClosed(new AccountEventArgs("Счёт " + id + " закрыт. Итоговая сумма: " + CurrentSum, CurrentSum));
        }

        protected internal void IncrementDays()
        {
            days++;
        }

        protected internal virtual void Calculate()
        {
            decimal increment = sum * percentage / 100;
            sum = sum + increment;
            OnCalculated(new AccountEventArgs("Начисленны проценты в размере: " + increment, increment));
        }
    }
}
