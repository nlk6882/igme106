namespace PE_DictionariesVsLists
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creates a new file reader, which loads a file of words
            // into both a list and a dictionary
            WordLoader reader = new WordLoader();

            // Get the two data structures needed for the exercise
            List<String> wordList = reader.WordList;
            Dictionary<String, bool> wordDictionary = reader.WordDictionary;

            // *********************
            // TODO: Put your code between here...
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

            while (true)
            {
                Console.WriteLine("Would you like to search dictionary or list? Enter \"D\", \"L\", or \"Q\" to quit,  or enter a doubleword to see if it exists.");
                string response = Console.ReadLine().ToUpper().Trim();

                if (response == "D")
                {
                    watch.Start();
                    foreach (KeyValuePair<string, bool> entry in wordDictionary)
                    {
                        string doubleWord = entry.Key + entry.Key;

                        if (wordList.Contains(doubleWord))
                        {
                            Console.WriteLine(doubleWord);
                            wordDictionary["wordword"] = true;
                        }
                    }
                    Console.WriteLine("\nFinished Search in " + watch.Elapsed.TotalMilliseconds + " m.s.\n");

                }

                else if (response == "L")
                {
                    watch.Start();
                    foreach (String currentWord in wordList)
                    {
                        string doubleWord = currentWord + currentWord;

                        if (wordList.Contains(doubleWord))
                        {
                            Console.WriteLine(doubleWord);
                        }
                    }
                    Console.WriteLine("\nFinished Search in " + watch.Elapsed.TotalMilliseconds + " m.s.\n");
                }

                else if (response == "Q")
                {   
                    Environment.Exit(0);
                }

                else
                {
                    response = response.ToLower();
                    Console.WriteLine("Searching for "+response+"...");
                    if (wordDictionary.ContainsKey(response))
                    {
                        //this should work, but wontbecause I used a foreach loop above, therefore I cant change the values of wordDictionary
                        if (wordDictionary[response] == true)
                        {
                            Console.WriteLine("That is a double word!");
                        }
                        else
                        {
                            Console.WriteLine("That is not a double word");
                        }
                    }
                    else
                    {
                        Console.WriteLine("The word does not exists");
                    }
                }

            }

            // ...and here.
            // *********************
        }
    }
}
