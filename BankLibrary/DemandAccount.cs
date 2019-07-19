using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public class DemandAccount:Account
    {
        public DemandAccount(decimal _sum, int _percentage) : base(_sum, _percentage)
        {
        }

        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArgs("Открыт новый счёт до востребования! ID счёта: "+this.id,this.sum));
        }
    }
}
