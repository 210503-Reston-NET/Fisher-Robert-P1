using StoreBL;
using StoreDL;
using StoreModels;
using System;
using System.Collections.Generic;


namespace StoreUI
{
    public class EditProductMenu : StoreMenu
    {
        StoreBLInterface bussinessLayer;
        MyValidate validate = new StringValidator();
        public EditProductMenu(StoreBLInterface BL)
        {
            this.bussinessLayer = BL;

            
        }
        public override void Start()
        {
            bool repeat = true;
            string isbn_13;
            Product found;
            while (repeat){
            string output = "--------Edit Product--------" + "\n";
                output += "Please make a selection." + "\n";
                output += "[0] Add Product." + "\n";
                output += "[1] Remove Product." + "\n";
                output += "[2] Find Product." + "\n";
                output += "[3] Edit Product." + "\n";
                output += "[4] Exit." + "\n";
                string input = validate.ValidateString(output);

                switch(input)
                {
                    // Case: Add Product
                    case "0":
                        Product item = DefineProductModel();

                        bussinessLayer.AddProduct(item);
                        break;
                    
                    // Case: Remove Product
                    case "1":
                        output = "Enter product ISBN: " + "\n";
                        isbn_13 = validate.ValidateString(output);
                        Product ToBeDeleted= bussinessLayer.GetProduct(isbn_13);

                        if (bussinessLayer.RemoveProduct(ToBeDeleted)){
                            System.Console.WriteLine("Product Successfully Deleted!");
                        }
                        break;
                    // Case: View Product
                    case "2":
                        output = "Enter product ISBN: " + "\n";
                        isbn_13 = validate.ValidateString(output);

                        found = bussinessLayer.GetProduct(isbn_13);
                        System.Console.WriteLine("--------Selected Product--------\n" + found);
                        break;
                    // Case: Edit Product
                    case "3":
                        Product EditedProduct = DefineProductModel();
                        if (bussinessLayer.GetProduct(EditedProduct.ISBN) != null)
                            if(bussinessLayer.UpdateProduct(EditedProduct))
                                System.Console.WriteLine("Succesfully edited Product!");
                        break;
                    
                    // Case: Exit
                    case "4":
                        repeat = false;
                        break;
                    // Case Invalid Entry
                    default:
                        System.Console.WriteLine("Invalid entry! Please try again.");
                        break;
                }
                }
        }
    }
}