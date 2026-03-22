using HW1_GameOfLife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace HW2_Expedition.NolanK
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game myGame = new Game();
            bool running = true;
            int townsToVisit = 4;

            //testing space
            ///*

            //*/

            while (running == true)
            {
                char[] mainMenu = { '1', '2', '3', '4' };
                char response = SmartConsole.Prompt("What would you like to do?\n\n1 - Begin new game\n2 - TEMP\n3 - TEMP\n4 - Quit\n\n", mainMenu);
                Console.WriteLine();

                switch (response)
                {
                    case '1':
                        //start in a town with some starting cash
                        myGame.setGame();
                        myGame.pressToCont();
                        StreamReader reader = new StreamReader("../../../Town&ShopData.txt");

                        //begin game, loop for however many towns to visit and go through 
                        for (int i = 1; i <= townsToVisit; i++)
                        {
                            //travel, chance of random event
                            myGame.randomEvent();

                            //make camp
                            myGame.partyStatus();
                            myGame.campAbilities();
                            myGame.pressToCont();

                            myGame.partyStatus();
                            myGame.campItems();
                            myGame.pressToCont();

                            //after party has gone to sleep, display current status and check if lost
                            Console.WriteLine("End of day " + i + ". Party Status:");
                            bool allDead = myGame.partyStatus();
                            myGame.displayInventory();

                            if (allDead == true)
                            {
                                SmartConsole.PrintError("GAME OVER");
                                running = false;
                                return;
                                break;
                            }

                            myGame.pressToCont();

                            //travel and arrive at town, load in name, description, and items for sale from file
                            string input = reader.ReadLine();
                            //split int name, description, and items
                            string[] split1 = input.Split(",");
                            //split by items
                            string[] split2 = split1[2].Split("!");

                            List<Item> itemsForSale = new List<Item>();

                            //add each item in split2 to itemsforsale
                            for(int j=0; j < split2.Count(); j++)
                            {
                                //split by item name, type, tier, value, and description
                                string[] split3 = split2[j].Split("?");

                                //enum code sam wrote to correctly referance the enum
                                Enum.TryParse(split3[1], out ItemTypes itemTypes);

                                //create and add each item to the list
                                Item item = new Item(split3[0], itemTypes, Convert.ToInt32(split3[2]), Convert.ToDouble(split3[3]), split3[4]);
                                itemsForSale.Add(item);
                            }

                            //call method for town arrival
                            myGame.townArrival(split1[0], split1[1], itemsForSale);

                            

                        }
                        reader.Close();

                        //code to handel winning the game here
                        Console.WriteLine("After setting out the next morning, the party makes a small journey over the next few hills, the town of Ashville comes into view! The party has reached their destination! You Win!");

                        break;

                    case '2':
                        //
                        break;

                    case '3':
                        //
                        break;

                    case '4':
                        //quit
                        Console.WriteLine("Thanks for playing!");
                        running = false;
                        Environment.ExitCode = 0;
                        break;

                }


            }

        }




    }
}
