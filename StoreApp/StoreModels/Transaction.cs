namespace StoreModels
{
    public class Transaction
    {
        public int OrderNumber { get; set;}
        public string ISBN {get; set; }
        public int? Quantity {get; set;}
        public Transaction() {}
        public Transaction(int ON, string isbn, int qty): this(isbn, qty)
        {
            this.Quantity = qty;
        }
        public Transaction(string isbn, int qty)
        {
            this.ISBN = isbn;
            this.Quantity = qty;
        }
        public override string ToString()
        {
            return "Quantity: " + this.Quantity + " ISBN: " + this.ISBN + " OrderNumber: " + this.OrderNumber; 
        }

    }
}