using System;
using Serilog;
using System.Text.RegularExpressions;

namespace StoreUI
{
    public class StringValidator : MyValidate
    {
        public StringValidator()
        {
            // Initialize Serilogger
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("../logs/StoreApp.txt", rollingInterval : RollingInterval.Day)
            .CreateLogger();
        }
        /// <summary>
        /// Takes in a prompt and recieves a user's input. Validates input as a string
        /// </summary>
        /// <param name="prompt">How you want to address the User</param>
        /// <returns>User's input</returns>
        public string ValidateString(string prompt)
        {
            string response;
            bool repeat;
            do
            {
                Console.WriteLine(prompt);
                response = Regex.Replace(Console.ReadLine(), "[^a-zA-Z0-9_]+", " ");
                repeat = String.IsNullOrWhiteSpace(response);
                if (repeat) Console.WriteLine("Please input a non empty string");
            } while (repeat);
            return response;
        }

        /// <summary>
        /// Takes in a prompt and recieves a user's input. Validates input as a string
        /// </summary>
        /// <param name="prompt">How you want to address the User</param>
        /// <returns>User's input</returns>
        public int ValidateInteger(string prompt)
        {
            string response;
            int value;
            bool repeat;
            do
            {
                Console.WriteLine(prompt);
                response = Regex.Replace(Console.ReadLine(), "[^0-9_]+", " ");
                repeat = String.IsNullOrWhiteSpace(response);
                value = int.Parse(response);
                if (repeat) Console.WriteLine("Please input a non empty string");
            } while (repeat);
            return value;
        }

        /// <summary>
        /// Takes in a prompt and recieves a user's input. Validates input as a double
        /// </summary>
        /// <param name="prompt">How you want to address the User</param>
        /// <returns>User's input</returns>
        public double ValidateDouble(string prompt)
        {
            double response = 0.0;
            bool repeat = true;
            do
            {
                try 
                {
                    Log.Debug("Attempting to parse a Double from a string.");
                    Console.WriteLine(prompt);
                    response = double.Parse(Regex.Replace(Console.ReadLine(), "[^.0-9_]+", " "));

                    repeat = false;
                } catch(Exception e)
                {
                    Log.Error(e.Message, "Failed to Parse Double in MyValidate.ValidateDouble() Method");
                    System.Console.WriteLine(e.Message);
                    System.Console.WriteLine("Please input a vaild Price (Include Integers and Decimal Points");
                }
            } while (repeat);
            return response;
        }

        public decimal ValidateDecimal(string prompt)
        {
            decimal response = 0.00M;
            bool repeat = true;
            do
            {
                try 
                {
                    Log.Debug("Attempting to parse a Decimal from a string.");
                    Console.WriteLine(prompt);
                    response = decimal.Parse(Regex.Replace(Console.ReadLine(), "[^.0-9_]+", " "));

                    repeat = false;
                } catch(Exception e)
                {
                    Log.Error(e.Message, "Failed to Parse Decimal in MyValidate.ValidateDecimal() Method");
                    System.Console.WriteLine(e.Message);
                    System.Console.WriteLine("Please input a vaild Price (Include Integers and Decimal Points");
                }
            } while (repeat);
            return response;
        }
    }
}