using System;
using StoreModels;
using System.Collections.Generic;
using Serilog;

namespace StoreUI
{
    public class HomeMenu : StoreMenu
    {
        public HomeMenu(User PassedUser)
        {
            base.CurrentUser = PassedUser;

        }
        public override void Start()
        {
            List<string> Options = new List<String>();
            StringValidator validate = new StringValidator();
            bool repeat = true;
            do
            {
                // Current Menu selector using Console as an output
                int index = 0;
                string output = "Welcome to the main store page!" + "\n";
                output += "Please make a selection." + "\n";
                output += "["+ index++ +"] Order Product." + "\n";
                Options.Add("Order");

                output += "[" + index++ +"] View Orders." + "\n";
                Options.Add("CustomerOrders");

                // Only Displays Manager Menu if User has an employee ID
                if (base.CurrentUser.Code != null){
                    output += "["+ index++ +"] Manager Menu." + "\n";
                    Options.Add("Manager");
                }
                
                output += "["+ index +"+] Exit." + "\n";

                int input = validate.ValidateInteger(output);

                if(input >= index)
                    break;
                
                MenuFactory.GetMenu(Options[input], base.CurrentUser).Start();

            } while (repeat);

            System.Console.WriteLine("Thanks for stopping by. Have a great day!");
        }
    }
}