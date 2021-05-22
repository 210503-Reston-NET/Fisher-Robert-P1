namespace StoreModels
{

    public class Inventory
    {
        public int StoreID { get; set; }
        public string ISBN { get; set; } 
        public int? Quantity { get; set; }
        public Inventory(){}
        public Inventory(int ID, string isbn, int? Quantity)
        {
            this.StoreID = ID;
            this.ISBN = isbn;
            this.Quantity = Quantity;
        }
        public override string ToString()
        {
            return "StoreID: " + this.StoreID + " ISBN: " + this.ISBN + " Quantity: " + this.Quantity;
        }
    }
}