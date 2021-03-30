using System;

namespace VendingMachine
{
    public class Person
    {
        private int Money { get; set; } = 0;
        
        public void addMoneyOnCard(int money)
        {
            if (money > 0)
            {
                Money += money;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Your transaction has been completed");
                Console.ResetColor();
            }
            
        }
        
        public int removeMoneyOnCard(int money)
        {
            if (Money >= money)
            {
                Money -= money;
                return money;
            }
            return 0;
        }
        
        public int MoneyOnCard()
        {
            return Money;
        }
    }
}