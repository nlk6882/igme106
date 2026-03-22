// *************************************************************************
// HW 3 - Deck of Cards
// Full write-up: https://docs.google.com/document/d/1J-n1XZO8c2r-hWmJoYLBIL0hJj0CNlehBev9MreHhLk/edit?usp=sharing
//
//
// DO NOT MODIFY ANY CODE IN THIS FILE!!!
//
// NOTES:
//
// The Card and Deck classes leverage auto-properties.
// This lets us avoid extra code for properties that would only be
// implemented with the most basic get/set functionality.
// - https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/auto-implemented-properties
//
// The ? on some types (e.g., Card? Previous) in Card.cs & Deck.cs
// defines it as "nullable". The type is always actually nullable. The point
// is that we know that, for that specific usage, we expect it to sometimes
// store/return a null reference and we don't want to get warned about it.
// - https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/nullable-warnings
// - https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-value-types
//
// Similarly, sometimes an assignment or return will result in a warning about
// a "possibly null" reference even if WE know that our logic means that will
// never happen OR we don't care. An ! with the assignment/return tells the compiler
// to be quiet.
//
// For example: "current = current.Next!;" is helpful when we either know Next will
// never be null or we don't care b/c we have logic in place to not dereference
// current later if it is null.
// - https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-forgiving
//
// ALL of those warnings are harmless. But if they are bothering you, using the ? and !
// carefully can make them go away (+ the process of analyzing when ? or ! are
// appropriate helps you analyze your code to ensure you're using the references
// correctly).
// *************************************************************************
namespace HW3_DeckOfCards
{
    class Program
    {
        // Creates a test list and runs a user input loop to allow the list to be
        // modified
        static void Main(string[] args)
        {
            // Setup
            Deck myDeck = new Deck();
            Random rand = new Random();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Welcome to the Doubly Linked Deck of Cards homework!\n");

            // Main user loop
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("\n[A]dd, [P]rint, [B]ackwards, [C]lear, [D]eal, [M]ove last, [S]huffle, [Q]uit");
                string input = SmartConsole.Prompt("What would you like to do?");

                Console.WriteLine();

                try
                {
                    switch (input)
                    {
                        // Adds one card of each possible suit & rank to the deck.
                        case "a":
                        case "add":
                            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
                            {
                                foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
                                {
                                    myDeck.Add(suit, rank);
                                }
                            }
                            Console.WriteLine("The deck now has {0} cards.", myDeck.Count);
                            break;

                        // Tells the deck to print itself in order.
                        case "p":
                        case "print":
                            myDeck.Print();
                            break;

                        //Tells the deck to print itself in reverse order.
                        case "b":
                        case "backwards":
                            myDeck.PrintReversed();
                            break;

                        // Tells the deck to reset itself to empty.
                        case "c":
                        case "clear":
                            myDeck.Clear();
                            break;

                        // Tells the deck to copy itself into a user - specified # of sub-decks and then prints out the returned List of decks.
                        case "d":
                        case "deal":
                            Console.WriteLine();
                            int playerCount = SmartConsole.Prompt("How many players are there?", 1, 100);
                            List<Deck> playerHands = myDeck.DealPlayerHands(playerCount);

                            for (int i = 0; i < playerHands.Count; ++i)
                            {
                                Console.WriteLine("\nPlayer {0} hand:", i + 1);

                                playerHands[i].Print();
                            }
                            break;

                        // Tests removal of a user - specified # of cards from the end of the deck and insertion elsewhere.
                        // We can move at most Deck Size - 2 cards because the move happens twice and we want at least 
                        // the top card (head) to stay put so there's something to move the others before
                        case "m":
                        case "move last":
                            int cards = SmartConsole.Prompt("How many cards do you want to move?", 0, myDeck.Count-2);
                            Console.WriteLine("Moving the last {0} cards to the front", cards);
                            myDeck.Move(cards, 0);
                            myDeck.Print();

                            Console.WriteLine("Moving the last {0} cards to index 1", cards);
                            myDeck.Move(cards, 1);
                            myDeck.Print();
                            break;

                        // Shuffles the deck by repeatedly moving a random number of cards from the end of the deck to somewhere earlier in the deck.
                        // Don’t bother trying this until you are confident that[M]ove last is working!
                        case "s":
                        case "shuffle":
                            int overhandTimes = rand.Next(100, 1001);
                            Console.WriteLine("Shuffle {0} times", overhandTimes);

                            for (int i = 0; i < overhandTimes; ++i)
                            {
                                // Decide how many cards to move from the end of the deck into the middle somewhere
                                // Don't move more than half the deck at once
                                int cardsToMove = rand.Next(1, myDeck.Count / 2);

                                // We can't move cards into themselves so compute the last possible index we can move before
                                // For example, if the count were 8 and we decide to move 1 card, the new tail card is currently
                                // at index 6.
                                int indexLimt = myDeck.Count - (cardsToMove + 1);

                                if (indexLimt < 2)
                                {
                                    // This should never happen. If it does, give up. :(
                                    throw new IndexOutOfRangeException("Invalid index while shuffling: " + indexLimt+". This should never happen.... :(");
                                }

                                // Get a target index between 0 and the limit
                                int targetIndex = rand.Next(0
                                    , indexLimt + 1);

                                myDeck.Move(cardsToMove, targetIndex);
                            }
                            myDeck.Print();
                            break;

                        // End the user input loop
                        case "q":
                        case "quit":
                            Console.WriteLine("Goodbye");
                            isRunning = false;
                            break;

                        // Any other input is ignored.
                        default:
                            Console.WriteLine("Sorry that was an invalid command.");
                            break;
                    }
                }

                // Main() ONLY handles an IndexOutOfRangeException exception (which your custom
                // linked list must throw when appropriate). Any other exception type will
                // still cause the program to crash.
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine("Error handling command \"" + input + "\" - " + e.Message);
                }
            }
        }
    }
}
