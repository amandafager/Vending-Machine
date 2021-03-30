using System;
using System.Collections.Generic;

namespace VendingMachine
{
    public class VendingMachine
    {
        public List<Item> SelectedItems { get; set; } = new();
        public void Run()
        {
            var inventory = new Inventory();
            var bankAccount = new BankAccount();
            var person = new Person();

            Console.WriteLine("WELCOME");
            Console.WriteLine("Main Menu");
            Console.WriteLine("1] Display Vending Machine Items");
            Console.WriteLine("2] Purchase");
            Console.WriteLine("3] Virtual bank");
            Console.WriteLine("4] Check balance on your bank card");
            Console.WriteLine("Q] Quit");

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
                    
                    var total = DisplaySelectedItem();
                    if (total == 0) continue;
                   
                    Console.WriteLine("Enter OK to pay");
                    var input = Console.ReadLine();
                    input = input.ToUpper();

                    if (input == "OK")
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
                        Console.WriteLine($"Your payment was successfully made, thank you!");
                        Console.ResetColor();
                        Quit();
                        break;
                    }

                    continue;
                }
                if (option == "3")
                {
                    
                    var balance = bankAccount.Balance();
                    
                    while (true)
                    {
                        if (balance == 0)
                        {
                            Console.WriteLine($"You have {balance} SEK on your bank account and cannot transfer any money");
                            break;
                        }
                        Console.WriteLine($"You have {balance} SEK on your bank account");
                        Console.WriteLine("Do you want to transfer money to your card? enter YES or NO:");
                        var input = Console.ReadLine();
                        input = input.ToUpper();
                        
                        if (input == "YES")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("OK");
                            Console.ResetColor();
                            Console.WriteLine("Enter amount you would like to transfer:");
                            var amount = Console.ReadLine();
                            
                            if (int.TryParse(amount, out int number))
                            {
                                var moneyTransferToCard = bankAccount.Withdraw(number);
                                person.addMoneyOnCard(moneyTransferToCard);
                                var moneyOnCard = person.MoneyOnCard();
                                Console.WriteLine($"You have {moneyOnCard} SEK on your card");
                                break;
                            }
                            continue;
                            
                        }
                        if (input == "NO")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("OK");
                            Console.ResetColor();
                            break;
                        }
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{input} is not a valid option, enter YES or NO");
                        Console.ResetColor();
                        
                    }
                    continue;
                    
                }
                if (option == "4")
                {
                    var moneyOnCard = person.MoneyOnCard();
                    Console.WriteLine($"You have {moneyOnCard} SEK on your card");
                    continue;
                }
                if (option == "Q")
                {
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
                Console.Write("What option do you want to select? ");

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
                Console.Write("What item do you want to select? [Enter B to go back]: ");

                var input = Console.ReadLine();

                if (input.ToUpper() == "B")
                {
                    return null;
                }

                foreach (var item in vendingMachineItems)
                {
                    if (item.Id == input)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("OK");
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
                Console.WriteLine("No selected items");
                Console.WriteLine(".................");
                Console.WriteLine("You have nothing to pay. Enter [1] to display items to buy.");
                return 0;
            }

            var heading = SelectedItems.Count == 1 ? "Selected item" : "Selected items"; 
            Console.WriteLine(heading);
            Console.WriteLine(".................");

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

        
    }
}