using System;

namespace VendingMachine
{
    public class BankAccount
    {
        private int balance = 1000;
        public int Withdraw(int amount)
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

        public int Balance()
        {
            return balance;
        } 
    }
}