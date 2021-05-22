using System;
using StoreModels;

namespace StoreUI
{
    class Program
    {
        /// <summary>
        /// This is the main method, its the starting point of your application
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            MenuFactory.GetMenu("login", new User()).Start();
        }
    }
}
