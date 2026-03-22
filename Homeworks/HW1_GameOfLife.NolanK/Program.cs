/**
 * YOU_NAME_HERE
 * 
 * HW 1 - Game of Life: https://docs.google.com/document/d/13zQjG7a25GUA1Oh6pwQXUtF6lJTJscWuuL-wwnMBSIo/edit?usp=sharing
 * 
 * Release Notes:
 *  - 
 * 
 */
namespace HW1_GameOfLife
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game myGame = new Game();
            bool running = true;

            while (running == true)
            {
                char[] choices = { '1', '2', '3', '4' };
                char response = SmartConsole.Prompt("What would you like to do?\n\n1 - Generate random board\n2 - Display board\n3 - Load initial board from file\n4 - Quit\n\n", choices);

                switch (response)
                {
                    case '1':
                        //generate board
                        myGame.generateBoard();
                        break;

                    case '2':
                        //display board
                        myGame.diaplayBoard();

                        //chck to see if following conditions can be applued yet
                        if(myGame.HasBoard == false)
                        {
                            break;
                        }

                        //use for later
                        bool boardInPlay = true;

                        //give user options to advance, save board, or back to main menu
                        char[] choices2 = { '1', '2', '3'};
                        char response2 = SmartConsole.Prompt("What would you like to do?\n\n1 - Advance simulation\n2 - Save current board\n3 - Return to menu\n\n", choices);
                        switch (response2)
                        {
                            case '1':
                                //advance board
                                myGame.advance();
                                break;
                            case '2':
                                //save board
                                myGame.save();
                                break;
                            case '3':
                                //return to menu
                                break;
                        }


                        break;

                    case '3':
                        //load initial board from file
                        myGame.load();
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
