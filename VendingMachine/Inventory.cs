using System;
using System.Collections.Generic;

namespace VendingMachine
{
    public class Inventory
    {
        public List<Item> VendingMachineItems { get; } = new();

        public Inventory()
        {
            VendingMachineItems.Add(new Item("1", "Banana", 40, 10));
            VendingMachineItems.Add(new Item("2", "Apple", 20, 19));
            VendingMachineItems.Add(new Item("3", "Candy", 40, 11));
            VendingMachineItems.Add(new Item("4", "Soda", 60, 2));
            VendingMachineItems.Add(new Item("5", "Chips", 45, 5));
        }
        
        public void DisplayAllItems()
        {
            RemoveItemNotInStock();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Vending Machine items");
            Console.WriteLine("´´´´´´´´´´´´´´´´´´´´´");
            Console.ResetColor();
            
            foreach (var item in VendingMachineItems)
            {
                Console.WriteLine($"{item.Id}. {item.Product}, {item.Price} SEK, stock: {item.ItemsRemaining}");
            }
            Console.WriteLine();
        }
        
        private void RemoveItemNotInStock()
        {
            var a = new Item("", "", 0, 0);
            
            foreach (var item in VendingMachineItems)
            {
                if (item.ItemsRemaining == 0)  a =  item;
            }
            VendingMachineItems.Remove(a);
        }
    }
}