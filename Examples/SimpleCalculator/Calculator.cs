// This file will contain your calculator code
using System;
using System.Collections.Generic;

namespace SimpleCalculator
{
    public class Program
    {
        // Make this method public and static so it can be called from the main program
        public static void RunCalculator()
        {
            // Welcome message
            Console.WriteLine("===== Simple C# Calculator =====");
            Console.WriteLine("Learn C# fundamentals through a practical example");
            
            // Boolean variable to control the main program loop
            bool continueCalculating = true;
            
            // List to store calculation history
            List<string> calculationHistory = new List<string>();
            
            // Main program loop - keeps running until user chooses to exit
            while (continueCalculating)
            {
                try
                {
                    // Step 1: Get the first number from user
                    Console.WriteLine("\nEnter the first number:");
                    // Convert string input to double (decimal number)
                    double firstNumber = Convert.ToDouble(Console.ReadLine());
                    
                    // Step 2: Get the operation from user
                    Console.WriteLine("\nChoose an operation:");
                    Console.WriteLine("1. Addition (+)");
                    Console.WriteLine("2. Subtraction (-)");
                    Console.WriteLine("3. Multiplication (*)");
                    Console.WriteLine("4. Division (/)");
                    Console.WriteLine("5. Modulus (%)");
                    Console.WriteLine("6. Power (^)");
                    
                    // Read the operation choice
                    int operationChoice = Convert.ToInt32(Console.ReadLine());
                    
                    // Step 3: Get the second number from user
                    Console.WriteLine("\nEnter the second number:");
                    double secondNumber = Convert.ToDouble(Console.ReadLine());
                    
                    // Step 4: Perform the calculation based on the chosen operation
                    double result = 0;
                    string operationSymbol = "";
                    
                    // Using switch statement to handle different operations
                    switch (operationChoice)
                    {
                        case 1: // Addition
                            result = Add(firstNumber, secondNumber);
                            operationSymbol = "+";
                            break;
                        case 2: // Subtraction
                            result = Subtract(firstNumber, secondNumber);
                            operationSymbol = "-";
                            break;
                        case 3: // Multiplication
                            result = Multiply(firstNumber, secondNumber);
                            operationSymbol = "*";
                            break;
                        case 4: // Division
                            // Check for division by zero
                            if (secondNumber == 0)
                            {
                                throw new DivideByZeroException("Cannot divide by zero!");
                            }
                            result = Divide(firstNumber, secondNumber);
                            operationSymbol = "/";
                            break;
                        case 5: // Modulus
                            if (secondNumber == 0)
                            {
                                throw new DivideByZeroException("Cannot calculate modulus with zero!");
                            }
                            result = Modulus(firstNumber, secondNumber);
                            operationSymbol = "%";
                            break;
                        case 6: // Power
                            result = Power(firstNumber, secondNumber);
                            operationSymbol = "^";
                            break;
                        default:
                            throw new ArgumentException("Invalid operation choice!");
                    }
                    
                    // Step 5: Display the result
                    string calculationString = $"{firstNumber} {operationSymbol} {secondNumber} = {result}";
                    Console.WriteLine($"\nResult: {calculationString}");
                    
                    // Add the calculation to history
                    calculationHistory.Add(calculationString);
                    
                    // Step 6: Ask if the user wants to see calculation history
                    Console.WriteLine("\nDo you want to see calculation history? (y/n)");
                    string? historyChoice = Console.ReadLine()?.ToLower();
                    
                    // Using if-else to handle user's choice
                    if (historyChoice == "y" || historyChoice == "yes")
                    {
                        DisplayCalculationHistory(calculationHistory);
                    }
                }
                // Exception handling for different types of errors
                catch (FormatException)
                {
                    Console.WriteLine("\nERROR: Please enter valid numbers!");
                }
                catch (DivideByZeroException ex)
                {
                    Console.WriteLine($"\nERROR: {ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"\nERROR: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Generic exception handler for any other unexpected errors
                    Console.WriteLine($"\nAn unexpected error occurred: {ex.Message}");
                }
                
                // Step 7: Ask if the user wants to continue
                Console.WriteLine("\nDo you want to perform another calculation? (y/n)");
                string? continueChoice = Console.ReadLine()?.ToLower();
                
                // Update the loop control variable based on user's choice
                continueCalculating = (continueChoice == "y" || continueChoice == "yes");
            }
            
            Console.WriteLine("\nThank you for using the Simple C# Calculator!");
        }
        
        // Method for addition operation
        // Static methods belong to the class rather than an instance of the class
        static double Add(double a, double b)
        {
            return a + b;
        }
        
        // Method for subtraction operation
        static double Subtract(double a, double b)
        {
            return a - b;
        }
        
        // Method for multiplication operation
        static double Multiply(double a, double b)
        {
            return a * b;
        }
        
        // Method for division operation
        static double Divide(double a, double b)
        {
            return a / b;
        }
        
        // Method for modulus operation
        static double Modulus(double a, double b)
        {
            return a % b;
        }
        
        // Method for power operation
        static double Power(double baseNumber, double exponent)
        {
            return Math.Pow(baseNumber, exponent);
        }
        
        // Method to display calculation history
        static void DisplayCalculationHistory(List<string> history)
        {
            Console.WriteLine("\n===== Calculation History =====");
            
            // Using if-else to check if history is empty
            if (history.Count == 0)
            {
                Console.WriteLine("No calculations performed yet.");
            }
            else
            {
                // Using for loop to iterate through history with index
                for (int i = 0; i < history.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {history[i]}");
                }
            }
            
            Console.WriteLine("=============================");
        }
    }
} 