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