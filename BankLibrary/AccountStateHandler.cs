using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public delegate void AccountStateHandler(object sender, AccountEventArgs accountEventArgs);
    public class AccountEventArgs
    {
        public string Message;
        public decimal Sum;

        public AccountEventArgs(string _message, decimal _sum)
        {
            Message = _message;
            Sum = _sum;
        }

    }
}
