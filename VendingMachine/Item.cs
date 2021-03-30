namespace VendingMachine
{ 
    public class Item
    {
        public string Id { get; set; }
        public string Product { get; set; }
        public int Price { get; set; }
        public int ItemsRemaining { get; set; }
        
        public Item(string id, string product, int price, int itemsRemaining)
        {
            this.Id = id;
            this.Product = product;
            this.Price = price;
            this.ItemsRemaining = itemsRemaining;
        }
    }
}