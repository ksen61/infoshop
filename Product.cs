namespace Pract10
{
    public class Product
    {
        public int id;
        public string name;
        public float price;
        public int count;

        public Product() { }
        public Product(int id, string name, float price, int count)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.count = count;
        }
    }
}
