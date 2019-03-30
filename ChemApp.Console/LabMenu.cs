﻿namespace ChemApp.Console
{
    using ChemApp.Console.Infrastructure;
    using ChemApp.Console.Utilities;
    using ChemApp.Domain;
    using ChemApp.Domain.Abstract;
    using ChemApp.Infrastructure.Exceptions;
    using System;
    using System.Linq;

    internal class LabMenu
    {
        private readonly ILaboratory chemLaboratory;
        private readonly IStorage myStorage;

        public LabMenu(ILaboratory chemLaboratory, IStorage myStorage)
        {
            this.chemLaboratory = chemLaboratory;
            this.myStorage = myStorage;
        }

        public void Run()
        {
            while (true)
            {
                PrintLaboratory();
                int input = InputUtility.GetInputFromUser();

                switch (input)
                {
                    case 1:
                        Console.WriteLine("I'm attempting to perform a reaction");
                        var listOfProducts = myStorage.GetItems();
                        System.Threading.Thread.Sleep(500);
                        chemLaboratory.PerformReaction(listOfProducts);
                        Console.ReadKey();
                        break;

                    case 2:
                        Console.WriteLine("Type id of item to wash it");
                        int itemID = InputUtility.GetInputFromUser();
                        var foundItem = myStorage.GetProductById(itemID);
                        if (foundItem != null)
                        {
                            chemLaboratory.WashItem(foundItem);
                        }
                        else
                        {
                            var ex = new ItemNotFoundException(itemID.ToString());
                            DefaultLogger.Instance.Error("Item not found");
                            DefaultLogger.Instance.Error(ex.ToString());
                        }
                        Console.ReadKey();
                        break;

                    case 3:
                        Console.WriteLine("I'm starting to wash all dirty items in Your storage");
                        chemLaboratory.WashDirtyItems(myStorage.GetItems());
                        break;

                    case 4:
                        Console.WriteLine("Type id of item to polish:");
                        itemID = InputUtility.GetInputFromUser();
                        foundItem = myStorage.GetItems().FirstOrDefault(i => i.ItemID == itemID);
                        if (foundItem != null)
                        {
                            chemLaboratory.PolishItem(foundItem);
                        }
                        else
                        {
                            Console.WriteLine($"I didn't find item with ID {itemID}");
                        }
                        break;

                    case 5:
                        Console.WriteLine("I'm polishing all items in Your lab");
                        chemLaboratory.PolishAllItems(myStorage.GetItems());
                        break;

                    case 6:
                        //code
                        break;

                    default:
                        Console.WriteLine("Wrong choice");
                        break;
                }
                Console.Clear();
            }
        }

        private void PrintLaboratory()
        {
            Console.WriteLine("1. Perform a reaction");
            Console.WriteLine("2. Wash an item");
            Console.WriteLine("3. Wash all items");
            Console.WriteLine("4. Polish an item");
            Console.WriteLine("5. Polish all items");
            Console.WriteLine("6. Go back to storage");
        }
    }
}