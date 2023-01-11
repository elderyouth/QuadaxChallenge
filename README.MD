
# Quadax Programming Exercise
![image](https://user-images.githubusercontent.com/13540588/211927238-1b2ee321-38f5-4daf-a2e1-0ffb622da913.png)
## By Adrian Salazar

Greetings! This is a console application with a similar ruleset as **Mastermind**. The goal of the game is to guess the hidden number within 10 tries.
**Rules**: The program will generate a random answer with 4 digits, each digit ranging between 1 and 6.
After the player attempts to enter a combination, a red plus ([red bold]+[/]) sign will be printed for every digit that is both correct and in the correct position, and a minus ([violet bold]-[/]) sign for every digeit that is correct but in the wrong position.
The player has 10 attempts to guesss the number before getting a Game Over message.

This solution took approximately 4 hours to implement, including debugging and stylizing.

### Implementation of digit verificaiton
- Using a Dictionary, I was able to easily identify if there were correct digits within the guess
- After populating the dictionary with the 4 digits as keys and the number of times the digit appears in the answer, i could then count how many times a correct digit would appear in the guess
- To associate the + sign, i would simply compare if the current digit position in the guess is equal to the answer
- To associate the - sign, I would first assume the guess and answer digits are unequal, followed by verifying if the digit exists as a key in the dicitonary, and finally to verify there is still a count of digits

### Caveats
- Because it is a console application, I did not have a chance to set an appropiate XUnit Test project, however, I'd like to provide a few test cases for y'all to use
- Using `Spectre.Console`as a visual enhancement
- The `VerifyCorrectDigitAndPosition` method must use stringified versions of the guess and answer as I had difficulties working with them as ints (the modulo operation would not work properly)
- Forgot to wrap the game in a while loop to ensure `EndGame()` could proerly restart the game until the user's input would say N/n

### Test cases
- Attempt to guess using non-numeric characters, there should be a validation message
- Attempt to guess a number lower than 1111 and higher than 6666
- Attempt to guess a number with 7,8,9,0 on its digits
-  Uncomment line 20 from `Program.cs` to have areference of the correct digit
  - Ensure the secret number abides to the rules (4 digit between 1111 and 6666)
  - Include in your guess a digit in the correct position, feedback should return a + sign
  - Include in your guess a digit in the incorrect position, feedback should return a - sign after any + signs
  - Type the correct number, the game should end with a 'You won' message followed by a 'would you like to play again' prompt
- Use all 10 attempts to prompt a 'Game Over' message

### Troubleshooting
- If the project fails to load, ensure `Spectre.Console` is included within your NuGet packages
- After the message 'generating secret number', if your debugger hits a Thread Error, comment out lines 184 thru 199 in  `Program.cs` this is only flavor text running in sync
- For any issues running the program, don't hesitate to contact me
