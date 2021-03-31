using System;
using System.Collections.Generic;

namespace VendingMachine
{
    public class VendingMachine
    {
        public List<Item> SelectedItems { get; } = new();
        
        public void Run()
        {
            var inventory = new Inventory();
            var person = new Person();
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("WELCOME TO AMANDA'S VENDING MACHINE");
            Console.ResetColor();

            var menuOptions = new List<String>
            {   
                "1", 
                "2",
                "3",
                "4",
                "Q"
            };
            
            while (true)
            {
                var option = GetSelectedMenuOption(menuOptions);

                if (option == "1")
                {
                    Console.Clear();
                    inventory.DisplayAllItems();
                    var input = GetSelectedItem(inventory.VendingMachineItems);
                    if (input != null) DisplaySelectedItem();
                    continue;
                }
                if (option == "2")
                {   
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Purchase");
                    Console.WriteLine("´´´´´´´´");
                    Console.ResetColor();
                    
                    var total = DisplaySelectedItem();
                    if (total == 0) continue;

                   var personWantToPay = CheckIfPersonWantToPay();

                   if (personWantToPay)
                   {
                       var moneyToPayWith = person.removeMoneyOnCard(total);
                       
                       if (moneyToPayWith == 0)
                       {
                           Console.ForegroundColor = ConsoleColor.Red;
                           Console.WriteLine($"You have {moneyToPayWith} SEK on your card, go to virtual bank to transfer more money.");
                           Console.ResetColor();
                           continue;
                       }
                       Console.ForegroundColor = ConsoleColor.Green;
                       Console.WriteLine("Your payment was successfully made, enjoy!");
                       Console.ResetColor();
                       Quit();
                       break;
                   }
                   continue;
                }
                if (option == "3")
                {
                    Console.Clear();
                    var bankAccount = new BankAccount();
                    bankAccount.Deposit(1000);
                    bankAccount.Start(person);
                    continue;
                }
                if (option == "4")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Balance on your card");
                    Console.WriteLine("´´´´´´´´´´´´´´´´´´´´");
                    Console.ResetColor();
                    var moneyOnCard = person.MoneyOnCard();
                    Console.WriteLine($"You have {moneyOnCard} SEK on your card.");
                    Console.WriteLine();
                    continue;
                }
                if (option == "Q")
                {
                    Console.Clear();
                    Quit();
                    break;
                }
            }
        }

        public void Quit()
        {
            Console.WriteLine("Goodbye!");
        }

        public string GetSelectedMenuOption(List<string> menuOptions)
        {
            while (true)
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("Main Menu");
                Console.WriteLine("1] Display Vending Machine items");
                Console.WriteLine("2] Purchase");
                Console.WriteLine("3] Virtual bank");
                Console.WriteLine("4] Check balance on your bank card");
                Console.WriteLine("Q] Quit");
                Console.WriteLine();
                Console.Write("Select a menu option, enter [1], [2], [3], [4] or [Q]: ");
                
                var input = Console.ReadLine();
                input = input.ToUpper();
                
                if (menuOptions.Contains(input))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("OK");
                    Console.ResetColor();
                    Console.WriteLine();
                    return input;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid menu option.");
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        public string GetSelectedItem(List<Item> vendingMachineItems)
       { 
            while (true)
            {
                Console.Write("Enter the ID on the item you want to select or [B] if you want to go back: ");

                var input = Console.ReadLine();

                if (input.ToUpper() == "B") return null;

                foreach (var item in vendingMachineItems)
                {
                    if (item.Id == input)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("OK");
                        Console.WriteLine();
                        Console.ResetColor();
                        item.ItemsRemaining--;
                        SelectedItems.Add(new Item(item.Id, item.Product, item.Price, item.ItemsRemaining));
                        return input;
                    }
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Not a valid option, try again.");
                Console.ResetColor();
            }
       }

       public int DisplaySelectedItem()
       {
           var total = 0;
           var countedItems = SelectedItems.Count;

           if (SelectedItems.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("No selected items");
                Console.WriteLine(".................");
                Console.ResetColor();
                Console.WriteLine("You have nothing to pay. Enter [1] to display items to buy.");
                Console.WriteLine();
                return 0;
            }

            var heading = SelectedItems.Count == 1 ? "Selected item" : "Selected items"; 
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(heading);
            Console.WriteLine(".................");
            Console.ResetColor();
            
            var index = 1; 
            foreach (var item in SelectedItems)
            {
                Console.WriteLine($"{index++}. {item.Product}, {item.Price} SEK");
                total += item.Price;
            }
            
            Console.WriteLine(".................");
            var text = SelectedItems.Count == 1 ? "Item" : "Items"; 
            Console.WriteLine($"{text}: ({countedItems})");
            Console.WriteLine($"Total: {total} SEK");
            Console.WriteLine();
            return total;
       }

       public bool CheckIfPersonWantToPay()
       {
           while (true)
           {
               Console.Write("Enter [OK] to pay or [B] to go back: ");
               var input = Console.ReadLine();
               input = input.ToUpper();

               if (input == "OK") return true;
               if (input == "B") return false;
               
               Console.ForegroundColor = ConsoleColor.Red;
               Console.WriteLine("Not valid option");
               Console.ResetColor();

           }
       }
    }
}