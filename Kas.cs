namespace Pract10
{
    public class Kas
    {
        List<SelectedProduct> products;
        List<Note> notes;
        private string name;

        public Kas(List<Note> notes, string name)
        {
            LoadProducts();
            this.notes = notes;
            this.name = name;
        }

        private void LoadProducts()
        {
            List<SelectedProduct>? products = Converter.Load<List<SelectedProduct>>("products.json");
            if (products == null)
            {
                products = new List<SelectedProduct>();
            }
            this.products = products;
        }

        private void DrawMenu()
        {
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль: Кассир");
            Console.WriteLine("Enter - Перейти к записи, S - подтвердить покупку");
            Console.WriteLine("------------------------");
            float total = 0;
            foreach (var product in products)
            {
                total += product.price * product.selectedCount;
                Console.WriteLine($"  {product.id} - {product.name}, {product.price}руб. | {product.selectedCount}");
            }
            Console.WriteLine("------------------------");
            Console.WriteLine($"Итого: {total}руб.");
        }

        private void DrawProduct(int index)
        {
            SelectedProduct product = products[index];
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль: Кассир");
            Console.WriteLine("Esc - назад");
            Console.WriteLine("------------------------");
            Console.WriteLine($"  ID: {product.id} (выдается автоматически)");
            Console.WriteLine($"  Наименование: {product.name}");
            Console.WriteLine($"  Цена: {product.price}");
            Console.WriteLine($"  Количество на складе: {product.count}");
            Console.WriteLine($"  Выбранное количество: {product.selectedCount}");
        }

        public void Start()
        {
            DrawMenu();
            Cursor cursor = new Cursor(4, 4 + products.Count - 1);
            ConsoleKey key = Console.ReadKey(true).Key;
            while (key != (ConsoleKey)HotKeys.Exit)
            {
                switch (key)
                {
                    case (ConsoleKey)HotKeys.Next:
                        cursor.Next();
                        break;
                    case (ConsoleKey)HotKeys.Prev:
                        cursor.Prev();
                        break;
                    case (ConsoleKey)HotKeys.Submit:
                        Select(cursor.GetIndex());
                        DrawMenu();
                        cursor.Show(-1);
                        break;
                    case (ConsoleKey)HotKeys.Save:
                        Submit();
                        LoadProducts();
                        DrawMenu();
                        cursor.Show(-1);
                        break;
                }

                key = Console.ReadKey(true).Key;
            }
        }

        private void Select(int index)
        {
            DrawProduct(index);
            SelectedProduct product = products[index];
            ConsoleKey key = Console.ReadKey(true).Key;
            while (key != (ConsoleKey)HotKeys.Exit)
            {
                switch (key)
                {
                    case (ConsoleKey)HotKeys.Add:
                        product.selectedCount += 1;
                        if (product.selectedCount > product.count)
                        {
                            product.selectedCount = product.count;
                        }
                        break;
                    case (ConsoleKey)HotKeys.Sub:
                        product.selectedCount -= 1;
                        if (product.selectedCount < 0)
                        {
                            product.selectedCount = 0;
                        }
                        break;
                }
                Console.SetCursorPosition(24, 8);
                Console.Write($"{product.selectedCount}                    ");
                key = Console.ReadKey(true).Key;
            }
        }

        private void Submit()
        {
            List<Product> products = new List<Product>();
            foreach (var product in this.products)
            {
                product.count -= product.selectedCount;
                products.Add(product);

                int note_id;
                if (notes.Count > 0)
                {
                    note_id = notes.Max(z => z.id) + 1;
                }
                else
                {
                    note_id = 0;
                }
                Note note = new Note(note_id, product.name, product.selectedCount * product.price, DateTime.Now, true);
                notes.Add(note);
                product.selectedCount = 0;
            }

            Converter.Save<List<Note>>(notes, "notes.json");
            Converter.Save<List<Product>>(products, "products.json");
        }
    }
}
