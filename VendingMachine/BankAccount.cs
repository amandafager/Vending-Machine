using System;
using System.Reflection.Metadata.Ecma335;

namespace VendingMachine
{
    public class BankAccount
    {
        private int balance;
        
        public void Start(Person person)
        {
            while (true)
            
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Virtual bank");
                Console.WriteLine("´´´´´´´´´´´´");
                Console.ResetColor();
                Console.WriteLine($"You have {balance} SEK on your bank account"); 
                Console.Write("Do you want to transfer money to your card? enter YES or NO: ");
                
                var input = Console.ReadLine();
                input = input.ToUpper();
                
                if (input == "YES")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("OK");
                    Console.WriteLine();
                    Console.ResetColor();
                    
                    var money = GetAmountToWithdraw(balance);
                    var moneyToTransferToCard = Withdraw(money);
                    TransferMoney(moneyToTransferToCard, person);
                    continue;
                }
                if (input == "NO")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("OK");
                    Console.WriteLine();
                    Console.ResetColor();
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{input} is not a valid option, enter YES or NO");
                Console.ResetColor();
            }
        }


        private int GetAmountToWithdraw(int money)
        {
            while (true)
            {
                Console.Write("Enter amount you would like to transfer: ");
                var amount = Console.ReadLine();

                if (int.TryParse(amount, out int number))
                {
                    if (number > balance)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"You have only {money} SEK your account, please enter another amount.");
                        Console.ResetColor();
                        continue;
                    } 
                   
                    if (number <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You cannot do that. Try again..");
                        Console.ResetColor();
                        continue;
                    }
                    return number;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("It has to be numbers. Try again.");
                Console.ResetColor();
            }
        }
        private int Withdraw(int amount)
        {
            if (balance >= amount)
            {
                balance -= amount;
                return amount;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"You have {balance} SEK on your bank account and cannot transfer selected amount.");
            Console.ResetColor();
            return 0;
            
        }
        
        public void Deposit(int amount)
        {
            balance += amount;
        }
        private static void TransferMoney(int amount, Person person)
        {
            person.addMoneyOnCard(amount);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Your transaction has been completed.");
            var money = person.MoneyOnCard();
            Console.WriteLine($"You have now {money} SEK on your card.");
            Console.WriteLine();
            Console.ResetColor();
        }

        public int Balance()
        {
            return balance;
        } 
    }
}