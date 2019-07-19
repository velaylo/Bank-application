using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public class DepositAccount:Account
    {
        public DepositAccount(decimal _sum,int _percentage) : base(_sum, _percentage)
        {
        }

        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArgs("Открыт новый депозитный счет!Id счета: "+this.id,this.sum));
        }

        public override void Put(decimal _sum)
        {
            if (days % 30 == 0)
            {
                base.Put(_sum);
            }
            else
            {
                base.OnAdded(new AccountEventArgs("На счет можно положить только после 30-ти дневного периода.", 0));
            }
        }

        public override decimal Withdrow(decimal _sum)
        {
            if (days % 30 == 0)
            {
                return base.Withdrow(_sum);
            }
            else
            {
                base.OnWithdrawed(new AccountEventArgs("Вывести средства можно только после 30-ти дневного периода.",0));
                return 0; //
            }
        }

        protected internal override void Calculate()
        {
            if (days % 30 == 0)
            {
                base.Calculate();
            }
        }
    }
}
