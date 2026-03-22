using HW1_GameOfLife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2_Expedition.NolanK
{
    enum Roles
    {
        Tank,
        Merchant,
        Doctor,
        Fighter,
    }
    enum ItemTypes
    {
        Food,
        Medicine,
        Valuable,
        Tool,
    }
    internal class Game
    {
        //feilds
        double partyMoney;
        List<PartyMember> members = new List<PartyMember>();
        List<Item> items = new List<Item>();
        Random rand = new Random();
        bool abilityUsedToday = false;
        
        //party member creation
        Tank Dalton = new Tank("Dalton", 500, Roles.Tank);
        Doctor Kureha = new Doctor("Kureha", 100, Roles.Doctor);
        Fighter Sanji = new Fighter("Sanji", 250, Roles.Fighter);
        Merchant Nami = new Merchant("Nami", 200, Roles.Merchant);

        //properties

        //constructor
        internal Game()  
        {
            
        
        }

        //methods
        internal void setGame()
        {
            //Introduction for game here
            Console.WriteLine("\nOur party starts in a town called Axel, prepearing for a cross country journey across magical lands with each party member trying to reach the town of Ashville " +
                "to start their new lives!");

            //introduce party members and add to list of members
            Console.WriteLine("\nThe first party member to join is Dalton! Class: Tank, with the ability to heal 200 of his own health on ability activation.");
            members.Add(Dalton);

            Console.WriteLine("\nThe second party member to join is Kureha! Class: Doctor, with the ability to grant another party member 100 health on ability usage, up to a max of 200.");
            members.Add(Kureha);

            Console.WriteLine("\nThe third party member to join is Sanji! Class: Fighter, with the ability to fight by himself at no harm to items or other party members on usage.");
            members.Add(Sanji);

            Console.WriteLine("\nThe forth and final party member to join is Nami! Class: Merchant, with the ability to double an items value on ability usage.");
            members.Add(Nami);

            Console.WriteLine("\nPooling together their collective money, the party has $500 and their wits and skills to make their journey ahead to Ashville!");
            partyMoney += 50;

            //begin game and allow party to purchase starting items

            partyStatus();

            

        }

        //give players the opportunity to eat food and use downtime items
        internal void campAbilities()
        {
            abilityUsedToday = false;
            Console.WriteLine("After a long day of travels, the party decides to stop and make camp for the night. The party has enough time before dark to perform one " +
                "extra role ability.");
            char[] mainMenu = { '1', '2', '3', '5'};
            bool selectionRunning = true;

            while (selectionRunning == true)
            {

                char response = SmartConsole.Prompt("Actions currently avaliable (enter \"1\", \"2\", ect):\n\n1 - Use Kureha's ability\n2 - Use Nami's ability\n3 - Use Dalton's ability\n4 - Use Sanji's ability (NOT AVALIABLE HERE)\n5 - No ability use at current time\n\n", mainMenu);

                switch (response)
                {
                    case '1':
                        //Doctor ability
                        int index = 0;
                        bool choose = true;
                        while (choose == true)
                        {
                            string mem = SmartConsole.Prompt("Which party member would you like to use your ability on? (Enter name)").ToLower();
                            switch (mem)
                            {
                                case "dalton":
                                    index = members.IndexOf(Dalton);
                                    choose = false;
                                    break;
                                case "nami":
                                    index = members.IndexOf(Nami);
                                    choose = false;
                                    break;
                                case "sanji":
                                    index = members.IndexOf(Sanji);
                                    choose = false;
                                    break;
                                default:
                                    SmartConsole.PrintWarning("No party member by that name exists");
                                    break;
                            }
                        }

                        if (members[index].Alive)
                        {
                            if (members[index].Health >= 100)
                            {
                                members[index].Health = 200;
                            }
                            else
                            {
                                members[index].Health += 100;
                            }
                            SmartConsole.PrintSuccess(members[index].Name + "'s health has been retored to " + members[index].Health);
                            abilityUsedToday = true;
                            return;
                        }
                        else
                        {
                            SmartConsole.PrintError("The party member is dead and connot be interected with.");
                            return;
                        }
                       
                    case '2':
                        //Merchant ability
                        bool ability = true;
                        while (ability = true)
                        {
                            displayInventory();
                            string itemName = SmartConsole.Prompt("Would you like to double the value of any of the following owned items? If so, enter the name of the item, or if not simply type \"n\"");
                            if (itemName.ToLower() == "n")
                            {
                                ability = false;
                                break;
                            }
                            else
                            {
                                //scan through list of items, see if any match the name of the response
                                bool itemFound = false;
                                for (int i = 0; i < items.Count(); i++)
                                //foreach(Item item in items)
                                {
                                    //exception handeling stuff here for when there are no items in list
                                    if (items.Count() <= 0) { break; }
                                    if (items[i].Name.ToLower() == itemName.ToLower())
                                    {
                                        //case if item with that name is found
                                        itemFound = true;
                                        char[] answers = { 'y', 'n' };
                                        char sellYN = SmartConsole.Prompt("Would you like to increase " + itemName + " item value to $" + items[i].Value*2 + "? (\"y\" or \"n\")", answers);
                                        switch (sellYN)
                                        {
                                            case 'y':
                                                //item value increase case
                                                SmartConsole.PrintSuccess("Successfully increased " + items[i].Name + " value to $" + items[i].Value*2 + "!");
                                                abilityUsedToday = true;
                                                items[i].Value = items[i].Value*2;
                                                ability = false;
                                                return;
                                                break;
                                            case 'n':
                                                break;
                                        }
                                    }
                                }
                                if (itemFound == false) { SmartConsole.PrintWarning("No items were found with that name."); }
                            }
                        }
                        
                        
                        break;
                    case '3':
                        //Tank ability
                        int tankIndex = members.IndexOf(Dalton);
                        if (members[tankIndex].Alive)
                        {
                            if (members[tankIndex].Health >= 300)
                            {
                                members[tankIndex].Health = 500;
                            }
                            else
                            {
                                members[tankIndex].Health += 200;
                            }
                            SmartConsole.PrintSuccess(members[tankIndex].Name + "'s health has been retored to " + members[tankIndex].Health);
                            abilityUsedToday = true;
                            return;
                        }
                        else
                        {
                            SmartConsole.PrintError("The party member is dead and connot be interected with.");
                            return;
                        }  
                    case '5':
                        //none case
                        Console.WriteLine("The party decides to take the extra rest and not use any abilities tonight.");
                        return;

                }
            }

        }

        internal void campItems()
        {
            //gives the party an opportunity to use items.
            //scan through items list and create list of only consumables
            List<Item> usable = new List<Item>();
            foreach (Item item in items)
            {
                if(item.Type == "Medicine" || item.Type == "Food")
                {
                    usable.Add(item);
                }
            }

            Console.WriteLine("Before heading to bed, the party gathers around to discuss using any of their items of eating food.");

            foreach (PartyMember member in members)
            {
                displayInventory(usable);
                if (!member.Alive)
                {
                    continue;
                }
                string response = SmartConsole.Prompt("Would you like to apply any items to "+member.Name+"? If so, enter the name of the item, or if not simply type \"n\"");
                if (response.ToLower() == "n")
                {
                    continue;
                }
                else
                {
                    //scan through list of items, see if any match the name of the response
                    bool itemFound = false;
                    for (int i = 0; i < usable.Count(); i++)
                    //foreach(Item item in items)
                    {
                        //exception handeling stuff here for when there are no items in list
                        if (usable.Count() <= 0) { break; }
                        if (usable[i].Name.ToLower() == response.ToLower())
                        {
                            //case if item with that name is found
                            itemFound = true;
                            char[] answers = { 'y', 'n' };
                            char sellYN = SmartConsole.Prompt("Confirm you would like to use " + response + " on " + member.Name + "? (\"y\" or \"n\")", answers);
                            switch (sellYN)
                            {
                                case 'y':
                                    //use item case
                                    int restore = 0;
                                    switch (usable[i].Tier)
                                    {
                                        case 1:
                                            restore = 50;
                                            break;
                                        case 2:
                                            restore = 40;
                                            break;
                                        case 3:
                                            restore = 30;
                                            break;
                                        case 4:
                                            restore = 20;
                                            break;
                                        case 5:
                                            restore = 10;
                                            break;

                                    }
                                    SmartConsole.PrintSuccess("Successfully used " + usable[i] + " To restore " + restore + " health.");
                                    items.Remove(items[i]);
                                    break;
                                case 'n':
                                    break;
                            }
                        }
                    }
                    if (itemFound == false) { SmartConsole.PrintWarning("No items were found with that name."); }
                }
            }
        }

        internal void townArrival(string townName, string description, List<Item> itemsForSale)
        {
            //text describing entry to town
            Console.WriteLine($"The party arrives at the town of {townName}, {description} The party makes its way to the town shop to gather supplies for the journey ahead");

            //first, see if player wants to sell any of their items
            Console.WriteLine("Before showing you their wares, the shopkeeper asks if you have any items you would like to sell, their eyes greedily glancing over your visible belongings.");
            bool selling = true;
            while (selling = true)
            {
                displayInventory();
                string response = SmartConsole.Prompt("Would you like to sell any of these items? If so, enter the name of the item, or if not simply type \"n\"");
                if (response.ToLower() == "n")
                {
                    selling = false;
                    break;
                }
                else
                {
                    //scan through list of items, see if any match the name of the response
                    bool itemFound = false;
                    for(int i = 0; i < items.Count(); i++)
                    //foreach(Item item in items)
                    {
                        //exception handeling stuff here for when there are no items in list
                        if (items.Count() <= 0) { break; }
                        if (items[i].Name.ToLower() == response.ToLower())
                        {
                            //case if item with that name is found
                            itemFound = true;
                            char[] answers = { 'y', 'n' };
                            char sellYN = SmartConsole.Prompt("Would you like to sell " + response + " for $" + items[i].Value + "? (\"y\" or \"n\")", answers);
                            switch (sellYN)
                            {
                                case 'y':
                                    //sell item case
                                    partyMoney += items[i].Value;
                                    SmartConsole.PrintSuccess("Successfully sold " + items[i].Name + " for $" + items[i].Value + "! Your new balance is $" + partyMoney + ".");
                                    items.Remove(items[i]);
                                    break;
                                case 'n':
                                    break;
                            }
                        }
                    }
                    if (itemFound == false) { SmartConsole.PrintWarning("No items were found with that name."); }
                }
            }

            pressToCont();

            //display shop and allow player to purchase items
            bool shopRunning = true;
            while (shopRunning == true)
            {
                //display all items in the shop
                Console.WriteLine("\nThe items for sale here at this shop are...");
                int itemCounter = itemsForSale.Count;
                int displayNum = 1;
                foreach (Item item in itemsForSale)
                {
                    Console.WriteLine(displayNum + ": " + item.Name + ", Price: " + item.Value);
                    displayNum++;
                }

                //instantiate char[] of item options for smartConsole to work correctly
                char[] shopChoices = { '1', '2', '3', '4', '5', 'f' };
                
                char response = SmartConsole.Prompt("Enter the number of an item you wish to inspect, or \"f\" when you are finished. You currently have $" + partyMoney + ".", shopChoices);
                try
                {
                    switch (response)
                    {
                        case '1':
                            //
                            if (itemsForSale[0] != null) { shopInspect(itemsForSale[0]); }
                            break;
                        case '2':
                            //
                            if (itemsForSale[1] != null) { shopInspect(itemsForSale[1]); }
                            break;
                        case '3':
                            //
                            if (itemsForSale[2] != null) { shopInspect(itemsForSale[2]); }
                            break;
                        case '4':
                            //
                            if (itemsForSale[3] != null) { shopInspect(itemsForSale[3]); }
                            break;
                        case '5':
                            //
                            if (itemsForSale[4] != null) { shopInspect(itemsForSale[4]); }
                            break;
                        case 'f':
                            //case if user is done shopping and ready to continue
                            shopRunning = false;
                            Console.WriteLine("\nFinished shopping, the adventures continue on their journey.");
                            pressToCont();
                            return;
                            break;

                    }
                }
                catch (Exception ex) { SmartConsole.PrintWarning("That item does not exist."); }


            }
            

        }

        internal void shopInspect(Item item)
        {
            Console.WriteLine("\n\t" + item.Name + ", type: " + item.Type + ". Price: " + item.Value + ". Power tier of " + item.Tier + ". Description: " + item.Description + "");
            char[] repsonses = { 'y', 'n' };
            char response = SmartConsole.Prompt("Would you like to purchase this item? You currently have $" + partyMoney + " Type \"y\" or \"n\"", repsonses);
            switch (response) 
            {
                case 'y':
                    //case if user does want to buy the item has enough money
                    if(partyMoney >= item.Value)
                    {
                        partyMoney -= item.Value;
                        items.Add(item);
                        SmartConsole.PrintSuccess("Purchase successful! Your new balance is $" + partyMoney);
                    }
                    //case if user does not have enough money
                    else
                    {
                        SmartConsole.PrintError("You do not have enough money to purchase this item.");
                    }
                    break;
                case 'n':
                    //case if user does not want to buy the item
                    return;
                    break;
            }

        }

        internal void pressToCont()
        {
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            Console.Clear();
        }

        internal void randomEvent()
        {
            //generate random event iteration (inclusive, exclusive)
            int scenario = (int)rand.NextInt64(0,3);
            //event testing here
            //scenario = 0;

            //switch statement or something for what to do for the event generated
            switch (scenario) 
            {
                case 0:
                    //attacked by bandits
                    Console.WriteLine("While traveling along the path, the party gets jumped by a group of bandits!");
                    if(abilityUsedToday == false)
                    {
                        //give fighter opportunity to fight
                        Console.WriteLine("Because no ability was used in the past 24 hours, press enter to use Sanji's fighting abilities!");
                        Console.ReadLine();
                        SmartConsole.PrintSuccess("Sanji fights off the bandits and prevents them from stealing or causing harm to the rest of the party, although sustaining some injuries in the process!");
                        Sanji.takeDamage(75);
                    }
                    else
                    {
                        SmartConsole.PrintError("Because the party stayed up late last night to use an ability, Sanji is too ehxhusted to fight by himself and the bandits attack! " +
                            "Dalton takes the brunt of the attack in the skirmish, and Sanji still being a skilled fighter hold his own!\n");
                        Sanji.takeDamage(20);
                        Dalton.takeDamage(350);
                        Nami.takeDamage(60);
                        Kureha.takeDamage(80);

                        if(items.Count > 0)
                        {
                            SmartConsole.PrintError("The bandits steal an item! ITEM: " + items[0].Name);
                            items.Remove(items[0]);
                        }
                    }
                    break;

                case 1:
                    //use shovel to dig up valuables
                    Console.WriteLine("While traveling, the party finds a filled in pit of heavy dirt.");

                    if (itemSearch("Shovel") == true)
                    {
                        Console.WriteLine("Since the party has a shovel, they think if its worth the effort to investigate the mysterious pile.");
                        char[] yesNo = { 'y', 'n' };
                        char response = SmartConsole.Prompt("Do you want to start digging to investigate, or no? (enter \"y\" or \"n\")", yesNo);
                        if (response == 'y')
                        {
                            SmartConsole.PrintSuccess("After digging for about 30 minutes, a clank sounds as the shovel strikes an object! As the party looks to see what they " +
                                "unearthered, they see a large wooden chest, and upon opening find it filled with gold! The only tradeoff was some lost value of the shovel and a little time!");

                            Item treasureChest = new Item("Treasure", ItemTypes.Valuable, 1, 70.00, "A chest filled to the brim with gold.");
                            items.Add(treasureChest);
                            SmartConsole.PrintSuccess("\n+1 Treasure chest added to inventory!");

                            int index = nameIndex("Shovel");
                            items[index].Value -= 5;
                            SmartConsole.PrintWarning("Shovel value decreased by $5\n");
                        }
                        else
                        {
                            Console.WriteLine("The party decides to continue moving, leaving the mysterious pile for someone else to investigate.\n");
                        }
                    }
                    else
                    {
                        SmartConsole.PrintWarning("The party, not having a shovel or device able to properly dig, decides to move on, leaving the mysterious pile for someone else to investigate.\n");
                    }
                    break;

                case 2:
                    //rough travels
                    SmartConsole.PrintWarning("As the party travels, pricker bushes cause minor injuries to everyone in the party!\n");
                    Dalton.takeDamage(25);
                    Nami.takeDamage(25);
                    Sanji.takeDamage(25);
                    Kureha.takeDamage(25);
                    break;

                case 3:
                    //
                    break;
            }

            pressToCont();

        }

        //item search
        internal bool itemSearch(string input)
        {
            bool result = false;
            foreach(Item item in items)
            {
                if (item.Name == input)
                {
                    result = true; break;
                }
            }
            return result;
        }

        //name search, return index or -1
        internal int nameIndex(string input)
        {
            int index = -1;
            foreach (Item item in items)
            {
                if (item.Name == input)
                {
                    index = items.IndexOf(item);
                }
            }
            return index;
        }

        //format and display the items the party currently has when called
        internal void displayInventory()
        {
            if (items.Count == 0)
            {
                SmartConsole.PrintWarning("No items in inventory");
            }
            else
            {
                Console.WriteLine("Your Items:");
                foreach (Item item in items)
                {
                    Console.WriteLine("\t" + item.Name + ", value of $" + item.Value);
                }
            }
            Console.WriteLine();

        }

        internal void displayInventory(List<Item> specitems)
        {
            if (specitems.Count == 0)
            {
                SmartConsole.PrintWarning("No items in inventory");
            }
            else
            {
                Console.WriteLine("Your Items:");
                foreach (Item item in specitems)
                {
                    Console.WriteLine("\t" + item.Name + ", value of $" + item.Value);
                }
            }
            Console.WriteLine();

        }

        //method to sell items
        internal void sell(Item itemSold)
        {
            //use smartconsole to confirm sell choice
            char[] choices = {'Y', 'N'};
            char response = SmartConsole.Prompt($"Would you like to sell {itemSold.Name} of tier {itemSold.Tier} for ${itemSold.Value}?", choices);

            switch (response)
            {
                case 'Y':
                    //add money to account, remove item from party's item list, and confirmation message
                    items.Remove(itemSold);
                    partyMoney += itemSold.Value;
                    SmartConsole.PrintSuccess($"{itemSold.Name} of tier {itemSold.Tier} has been sold for ${itemSold.Value}");
                    break;

                case 'N':
                    //message to cancel transaction
                    SmartConsole.PrintWarning("Transaction cancelled");
                    break;
            }

            return;

        }

        //format and display the status of each party member
        internal bool partyStatus()
        {
            Console.WriteLine("Party Status:");
            //counter to see if all party members are dead or not at the end of the method
            int deadCounter = 0;

            foreach (PartyMember member in members)
            {
                string status;

                /*
                 * ask about this line for how to get this to work
                 * Console.WriteLine(member.Test);
                */

                //case of displaying the party member if they are dead
                if (member.Alive == false)
                {
                    status = "DEAD";
                    deadCounter++;
                    SmartConsole.PrintError($"{member.Name} ({member.CharType}) - Health: {status}");
                }
                //case of displaying party member if they are alive
                else
                {
                    status = member.Health.ToString();
                    SmartConsole.PrintSuccess($"{member.Name} ({member.CharType}) - Health: {status}");
                }


            }

            Console.WriteLine("Money: $" + partyMoney);

            Console.WriteLine();

            //check to see if all party members are dead, return true if so
            if (deadCounter >= members.Count)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

    }
}
