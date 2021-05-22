using StoreModels;

namespace StoreUI
{
    public class StoreMenu : Runnable
    {
        public User CurrentUser { get; set; }

        public virtual void Start()
        {
        }

        public Product DefineProductModel()
        {
            string output = "";
            StringValidator validate = new StringValidator();
            Product item = new Product();
                
            output = "Enter product name: " + "\n";
            item.Name = validate.ValidateString(output);

            output = "Enter product ISBN: " + "\n";
            item.ISBN = validate.ValidateString(output);

            output = "Enter product Price: " + "\n";
            item.Price = validate.ValidateDecimal(output);

            return item;
        }
    }
}