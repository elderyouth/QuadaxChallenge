using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Threading;

namespace QuadaxChallenge
{
    class Program
    {
        public static void Main(string[] args)
        {
            var challenge = new Program();
            challenge.IntroText();

            bool gameWon = false;
            int attempts = 10;
            int answer = GenerateGameNumber();
            
            //Feel free to comment out to have areference of the correct answer when testing
            //AnsiConsole.Markup($"Debug Mode - Current Answer is [khaki1]{answer}[/]\n");

            PlayMastermindGame(answer, gameWon, attempts);

            if (EndGame()) {
                AnsiConsole.Write(
                    new FigletText("New Game")
                    .Color(Color.Cyan1));

                PlayMastermindGame(GenerateGameNumber());
            }

        }

        /// <summary>
        /// Helper Method to run the game
        /// </summary>
        /// <param name="gameWon">Flag for whether the player has won or not</param>
        /// <param name="attempts"> Number of attempts set</param>
        /// <param name="answer">Generated secret number the player must guess</param>
        private static void PlayMastermindGame(int answer, bool gameWon=false, int attempts=10 )
        {
            while (attempts >= 1 && !gameWon)
            {
                AnsiConsole.Markup($"You have [yellow]{attempts}[/] attempt(s) left\n");
                
                int guess = ValidateGuessFormat();
                
                if (guess == answer)
                {
                    gameWon = true;
                    break;
                }
                else
                {
                    //stringify guess and answer to make the code easier
                    string feedback = VerifyCorrectDigitAndPositon(guess.ToString(), answer.ToString());
                    AnsiConsole.Markup($"[underline orange1]Feedback[/] {feedback}\n\n\n");
                    attempts--;
                }
            }

            //Check if the player won the game or did not guess correctly
            if (gameWon)
            {
                AnsiConsole.Write(
                    new FigletText("You won!")
                    .Color(Color.Green));
            }
            else
            {
                AnsiConsole.Write(
                    new FigletText("Game Over!")
                    .Color(Color.Blue));
                AnsiConsole.Markup($"\nSorry, you lose. The Secret Number was [yellow]{answer}[/]\n");
            }
        }

        /// <summary>
        /// Verify the times there is a correct digit in both the correct and incorrect position
        /// </summary>
        /// <param name="guess"></param>
        /// <param name="answer"></param>
        /// <returns>A feedback composed of + and/or - signs</returns>
        private static string VerifyCorrectDigitAndPositon(string guess, string answer)
        {
            string feedback = "";
            Dictionary<char, int> answerDigits = new Dictionary<char, int>();

            //Populate answerDigits dictionary
            for(int i = 0; i< 4; i++)
            {
                if (!answerDigits.ContainsKey(answer[i]))
                    answerDigits.Add(answer[i], 1);
                else
                    answerDigits[answer[i]]++;
            }

            //Count instances of correct digits in the correct positions
            for(int i = 0; i < 4; i++)
            {
                if(guess[i] == answer[i])
                {
                    feedback += "[red]+[/]";
                    answerDigits[answer[i]]--;
                }
            }

            // Count instances of correct digits in incorrect positions
            for (int i = 0; i < 4; i++)
            {
             
                //Mark only if the player's guess digit exists and we haven't exhausted the dictionary search
                if (guess[i] != answer[i] && answerDigits.ContainsKey(guess[i]) && answerDigits[guess[i]] > 0)
                {
                    feedback += "[violet]-[/]";
                    answerDigits[guess[i]]--;
                }

            }

            return feedback;

        }


        /// <summary>
        /// Creates a valid input number for the Mastermind game
        /// </summary>
        /// <returns>Player's input guess</returns>
        private static int ValidateGuessFormat()
        {
            return AnsiConsole.Prompt(
                new TextPrompt<int>("Guess the secret [khaki1]number[/]")
                    .PromptStyle("green")
                    .ValidationErrorMessage("[red]Invalid input, Make sure your input is a number between 1111 and 6666, with each digit ranging betwen 1 and 6[/]\n")
                    .Validate(guess =>
                    {
                        //Validate each digit ranges between 1 and 6
                        int currDigit = 0;
                        int tempDigt = guess;

                        for(int i=0; i<4; i++)
                        {
                            currDigit = tempDigt % 10;
                            if (currDigit % 10 < 1 || currDigit % 10 > 6)
                            {
                                return ValidationResult.Error("[red]Invalid Number! Make sure your input is between 1111 and 6666, with each digit ranging betwen 1 and 6[/]\n");
                            }
                            tempDigt /= 10;
                        }

                        switch (guess)
                        {
                            case int a when a < 1111:
                                return ValidationResult.Error("[red]Number is too Low! Make sure your input is between 1111 and 6666, with each digit ranging betwen 1 and 6[/]\n");
                            case int a when a > 6666:
                                return ValidationResult.Error("[red]Too high! Make sure your input is between 1111 and 6666, with each digit ranging betwen 1 and 6[/]\n");
                            default:
                                return ValidationResult.Success();
                        }
                        
                    }));
        }

        /// <summary>
        /// Introductory title with rules
        /// </summary>
        public void IntroText()
        {
            AnsiConsole.Write(
                new FigletText("Quadax Challenge")
                .LeftJustified()
                .Color(Color.Blue));
            AnsiConsole.Write(new Rule("[lightcyan1]By Adrian Salazar (@elderyouth)[/]"));
            AnsiConsole.Markup(@"Greetings! This application is a light version of [underline yellow]Mastermind[/]. The goal of the game is to guess the hidden number within 10 tries.
[underline bold]Rules[/]: The program will generate a random answer with 4 digits, each digit ranging between [khaki3]1[/] and [khaki3]6[/].
After the player attempts to enter a combination, a red plus ([red bold]+[/]) sign will be printed for every digit that is both correct and in the correct position, and a minus ([violet bold]-[/]) sign for every digeit that is correct but in the wrong position.
The player has 10 attempts to guesss the number before getting a Game Over message.

That being said let's start the game!

");

            AnsiConsole.Status()
                .Start("Starting...", ctx =>
                {
                    // Simulate some work
                    AnsiConsole.MarkupLine("Generating secret number....");
                    Thread.Sleep(1000);

                    // Update the status and spinner
                    ctx.Status("Almost there");
                    ctx.Spinner(Spinner.Known.Star);
                    ctx.SpinnerStyle(Style.Parse("blue"));

                    // Simulate some work
                    AnsiConsole.MarkupLine("[green]Done![/]");
                    Thread.Sleep(2000);
                });
        }

        /// <summary>
        /// Generates a valid number between 1111 and 6666, each digit ranging between 1 and 6
        /// </summary>
        /// <returns>Returns a valid secret number for the game to roll</returns>
        private static int GenerateGameNumber()
        {
            Random rand = new Random();
            int secretNumber = 0;
            for(int i =0; i < 4; i++)
            {
                //Per iteration, we will a digit between 1 and 6 in the (i+1)th space
                //Eg, if i=2 and digit is 4, it will be secretNumer += 40
                secretNumber += rand.Next(1, 6) * (int)Math.Pow(10, i);
            }
            return secretNumber;
        }

        /// <summary>
        /// Ask player whether they'd like to play once again
        /// </summary>
        /// <returns>True if user wants to play again, False if player says no</returns>
        private static bool EndGame()
        {
            if (!AnsiConsole.Confirm("\nWould you like to play again?"))
            {
                AnsiConsole.MarkupLine("\nOk... Thank you for playing! Goodbye");
                return false;
            }

            return true;
        }
    }
}
