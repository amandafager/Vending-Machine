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
            VendingMachineItems.Add(new Item("3", "Candy", 60, 11));
        }
        
        public void DisplayAllItems()
        {
            Console.WriteLine("Vending Machine items");
            Console.WriteLine("-----------------");
            
            foreach (var item in VendingMachineItems)
            {
                Console.WriteLine($"{item.Id}. {item.Product}, {item.Price} SEK, stock: {item.ItemsRemaining}");
            }
            
            Console.WriteLine();
        }
        
        
    }
}