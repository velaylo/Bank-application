using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    interface IAccount
    {
        void Put(decimal sum);
        decimal Withdrow(decimal sum);
    }
}
