using System;
using System.Collections.Generic;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            bool gameActive;
            bool endApp = false;
            //Game Code
            while (!endApp)
            {
                title();
                Console.Write("Choose a word or phrase: ");
                string word = Console.ReadLine().ToUpper();
                char[] letters = word.ToCharArray();
                var guessed = new List<char>();
                int falseGuesses = 0;
                int maxFalseGuesses = 0;
                title();
                Console.Write("Write the maximum wrong guesses allowed: ");
                string mfg = Console.ReadLine();
                while (!int.TryParse(mfg, out maxFalseGuesses))
                {
                    title();
                    Console.Write("Write the maximum wrong guesses allowed: ");
                    mfg = Console.ReadLine();
                }
                int lettersRevealed;
                guessed.Add(char.Parse(" "));
                writeWord();
                gameActive = true;
                //Game Logic
                while (gameActive)
                {
                    if (falseGuesses >= maxFalseGuesses) gameLost();
                    if (lettersRevealed == word.Length) gameWon();
                    if (gameActive == false) break;

                    Console.Write("Enter a letter or guess the word/phrase: ");
                    string guess = Console.ReadLine().ToUpper();
                    while (guess == "" || guess.Length > 1)
                    {
                        if (guess == word)
                        {
                            guessed.AddRange(letters);
                            gameWon();
                            break;
                        }
                        else if (guess.Length > 1 && guess != word) falseGuesses++;
                        writeWord();
                        Console.WriteLine("Your guess was incorrect or you entered nothing as your guess");
                        Console.Write("Enter another letter or guess the word/phrase: ");
                        guess = Console.ReadLine().ToUpper();
                    }
                    if (gameActive == false) break;

                    char letter = char.Parse(guess);
                    if (!guessed.Contains(letter))
                    {
                        if (word.Contains(letter))
                        {
                            guessed.Add(letter);
                            writeWord();
                            Console.WriteLine($"The letter \"{guess}\" is correct");
                        }
                        else if (!word.Contains(letter))
                        {
                            falseGuesses++;
                            writeWord();
                            Console.WriteLine($"The letter \"{guess}\" is incorrect");
                        }
                    }
                    else
                    {
                        writeWord();
                        Console.WriteLine($"You've already guessed \"{guess}\"");
                    }
                }
                void title()
                {
                    Console.Clear();
                    Console.WriteLine("__________________________ \n");
                    Console.WriteLine("BullEKorV's HANGMAN");
                    Console.WriteLine("__________________________ \n");
                }
                void writeWord()
                {
                    title();
                    Console.WriteLine($"Wrong guesses allowed left: {maxFalseGuesses - falseGuesses}\n\n");
                    Console.Write("    ");
                    lettersRevealed = 0;
                    foreach (var c in letters)
                    {
                        if (guessed.Contains(c))
                        {
                            Console.Write(c + " ");

                            lettersRevealed++;
                        }
                        else Console.Write("_ ");
                    }
                    Console.WriteLine("\n\n");
                }
                void gameWon()
                {
                    writeWord();
                    Console.WriteLine("Congratulations you won!");
                    Console.WriteLine("Type in any character if you want to play again or write \"n\" if you want to end game");
                    if (Console.ReadLine() == "n") endApp = true;
                    gameActive = false;
                }
                void gameLost()
                {
                    writeWord();
                    Console.WriteLine("You lost the game...");
                    Console.WriteLine($"The word was \"{word.ToLowerInvariant()}\"\n");
                    Console.WriteLine("Type in any character if you want to play again or write \"n\" if you want to end game");
                    if (Console.ReadLine() == "n") endApp = true;
                    gameActive = false;
                }
            }
        }
    }
}