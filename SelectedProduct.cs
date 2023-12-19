namespace Pract10
{
    public class SelectedProduct : Product
    {
        public int selectedCount = 0;

        public SelectedProduct(int id, string name, float price, int count, int selectedCount = 0)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.count = count;
            this.selectedCount = selectedCount;
        }
    }
}
