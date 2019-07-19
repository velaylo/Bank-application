using System;
using BankLibrary;

namespace BankApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank < Account > bank = new Bank<Account>("ЮнитБанк");
            bool alive = true;
            while (alive)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGreen; // выводим список команд зеленым цветом
                Console.WriteLine("1. Открыть счет \t 2. Вывести средства  \t 3. Добавить на счет");
                Console.WriteLine("4. Закрыть счет \t 5. Пропустить день \t 6. Выйти из программы");
                Console.WriteLine("Введите номер пункта:");
                Console.ForegroundColor = color;
                try
                {
                    int command = Convert.ToInt32(Console.ReadLine());

                    switch (command)
                    {
                        case 1:
                            OpenAccount(bank);
                            break;
                        case 2:
                            Withdraw(bank);
                            break;
                        case 3:
                            Put(bank);
                            break;
                        case 4:
                            CloseAccount(bank);
                            break;
                        case 5:
                            break;
                        case 6:
                            alive = false;
                            continue;
                    }
                    bank.CalculatePercentage();
                }
                catch (Exception ex)
                {
                    color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = color;
                }
            }
        }
        private static void OpenAccount(Bank<Account> bank)
        {
            Console.WriteLine("Укажите сумму для создания сёта: ");
            decimal sum = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Выберите тип счета: 1. До востребования 2. Депозит");
            AccountType accountType;

            int type = Convert.ToInt32(Console.ReadLine());
            if (type == 2)
            {
                accountType = AccountType.Deposit;
            }
            else
            {
                accountType = AccountType.Ordinary;
            }
            bank.Open(accountType, sum, AddSumHandler, WithdrowSumHandler, (sender, accountEventArgs) => Console.WriteLine(accountEventArgs.Message), CloseAccountHandler, OpenAccountHandler);
        }

        private static void Withdraw(Bank<Account> bank)
        {
            Console.WriteLine("Укажите сумму для вывода из счёта: ");
            decimal sum = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Введите id счета: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Withdraw(sum, id);
        }

        private static void Put(Bank<Account> bank)
        {
            Console.WriteLine("Укажите сумму для зачисления на счёта: ");
            decimal sum = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Введите id счета: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Put(sum, id);
        }

        private static void CloseAccount(Bank<Account> bank)
        {
            Console.WriteLine("Введите ID счёта, которого хотите закрыть: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Close(id);
        }

        private static void OpenAccountHandler(object sender, AccountEventArgs accountEventArgs)
        {
            Console.WriteLine(accountEventArgs.Message);
        }

        private static void AddSumHandler(object sender, AccountEventArgs accountEventArgs)
        {
            Console.WriteLine(accountEventArgs.Message);
        }

        private static void WithdrowSumHandler(object sender, AccountEventArgs accountEventArgs)
        {
            Console.WriteLine(accountEventArgs.Message);
            if (accountEventArgs.Sum > 0)
            {
                Console.WriteLine("Идём тратить деньги!");
            }
        }

        private static void CloseAccountHandler(object sender, AccountEventArgs accountEventArgs)
        {
            Console.WriteLine(accountEventArgs.Message);
        }
    }
}
