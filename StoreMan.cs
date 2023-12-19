namespace Pract10
{
    public class StoreMan : Crud
    {
        List<Product> products;
        private string name;
        public StoreMan(List<Product> products, string name)
        {
            this.products = products;
            this.name = name;
        }

        private void DrawMenu()
        {
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль: Склад-менеджер");
            Console.WriteLine("F1 - создать запись, Enter - Перейти к записи");
            Console.WriteLine("------------------------");
            foreach (var product in products)
            {
                Console.WriteLine($"  {product.id} - {product.name}, {product.count}шт., {product.price}руб.");
            }
        }

        private void DrawProduct(int index)
        {
            Product product = products[index];
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль: Склад-менеджер");
            Console.WriteLine("Esc - назад, Del - удалить, R - редактировать");
            Console.WriteLine("------------------------");
            Console.WriteLine($"  ID: {product.id}");
            Console.WriteLine($"  Наименование: {product.name}");
            Console.WriteLine($"  Цена: {product.price}");
            Console.WriteLine($"  Количество: {product.count}");
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
                        Read(cursor.GetIndex());
                        DrawMenu();
                        cursor.SetMax(4 + products.Count - 1);
                        cursor.Show(-1);
                        break;
                    case (ConsoleKey)HotKeys.Create:
                        Create();
                        DrawMenu();
                        cursor.SetMax(4 + products.Count - 1);
                        cursor.Show(-1);
                        break;
                }

                key = Console.ReadKey(true).Key;
            }
        }

        public void Create()
        {
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль: Склад-менеджер");
            Console.WriteLine("Esc - назад, S - сохранить");
            Console.WriteLine("------------------------");
            int id;
            if (products.Count > 0)
            {
                id = products.Max(s => s.id) + 1;
            }
            else
            {
                id = 0;
            }
            Console.WriteLine($"  ID: {id} (выдается автоматически)");
            Console.WriteLine("  Наименование: ");
            Console.WriteLine("  Цена: ");
            Console.WriteLine("  Количество: ");
            Product? product = null;
            Cursor cursor = new Cursor(5, 7);
            string t_name = "";
            string price = "";
            string count = "";

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
                        int index = cursor.GetIndex();

                        if (index == 0)
                        {
                            Console.SetCursorPosition(16, 5);
                            t_name = Input.GetValue(t_name);
                        }
                        else if (index == 1)
                        {
                            Console.SetCursorPosition(8, 6);
                            price = Input.GetValue(price);
                        }
                        else if (index == 2)
                        {
                            Console.SetCursorPosition(14, 7);
                            count = Input.GetValue(count);
                        }
                        break;
                    case (ConsoleKey)HotKeys.Save:
                        if (t_name == "")
                        {
                            Console.SetCursorPosition(0, 8);
                            Console.WriteLine("Наименование не может быть пустым.");
                            break;
                        }

                        if (price == "")
                        {
                            Console.SetCursorPosition(0, 8);
                            Console.WriteLine("Цена не может быть пустой.");
                            break;
                        }
                        float price_f;
                        try
                        {
                            price_f = float.Parse(price);
                        }
                        catch
                        {
                            Console.SetCursorPosition(0, 8);
                            Console.WriteLine("Неверный формат цены.");
                            break;
                        }

                        if (count == "")
                        {
                            Console.SetCursorPosition(0, 8);
                            Console.WriteLine("Количество не может быть пустым.");
                            break;
                        }
                        int count_i;
                        try
                        {
                            count_i = int.Parse(count);
                        }
                        catch
                        {
                            Console.SetCursorPosition(0, 8);
                            Console.WriteLine("Неверный формат количества.");
                            break;
                        }


                        if (product == null)
                        {
                            product = new Product(id, t_name, price_f, count_i);
                            products.Add(product);
                        }
                        else
                        {
                            product.name = t_name;
                            product.price = price_f;
                            product.count = count_i;
                        }
                        Console.SetCursorPosition(0, 8);
                        Console.WriteLine("Сохранено.");
                        break;
                }

                key = Console.ReadKey(true).Key;
            }

            Converter.Save<List<Product>>(products, "products.json");
        }

        public void Read(int index)
        {
            DrawProduct(index);

            ConsoleKey key = Console.ReadKey(true).Key;
            while (key != (ConsoleKey)HotKeys.Exit)
            {
                switch (key)
                {
                    case (ConsoleKey)HotKeys.Delete:
                        Delete(index);
                        return;
                    case (ConsoleKey)HotKeys.Update:
                        Update(index);
                        DrawProduct(index);
                        break;
                }

                key = Console.ReadKey(true).Key;
            }
        }

        public void Update(int index)
        {
            Product product = products[index];
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль: Склад-менеджер");
            Console.WriteLine("Esc - назад, S - сохранить");
            Console.WriteLine("------------------------");
            int id = product.id;
            string t_name = product.name;
            string price = product.price.ToString();
            string count = product.count.ToString();
            Console.WriteLine($"  ID: {id} (выдается автоматически)");
            Console.WriteLine($"  Наименование: {t_name}");
            Console.WriteLine($"  Цена: {price}");
            Console.WriteLine($"  Количество: {count}");
            Cursor cursor = new Cursor(5, 7);


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
                        int s_index = cursor.GetIndex();

                        if (s_index == 0)
                        {
                            Console.SetCursorPosition(16, 5);
                            t_name = Input.GetValue(t_name);
                        }
                        else if (s_index == 1)
                        {
                            Console.SetCursorPosition(8, 6);
                            price = Input.GetValue(price);
                        }
                        else if (s_index == 2)
                        {
                            Console.SetCursorPosition(14, 7);
                            count = Input.GetValue(count);
                        }
                        break;
                    case (ConsoleKey)HotKeys.Save:
                        if (t_name == "")
                        {
                            Console.SetCursorPosition(0, 8);
                            Console.WriteLine("Наименование не может быть пустым.");
                            break;
                        }

                        if (price == "")
                        {
                            Console.SetCursorPosition(0, 8);
                            Console.WriteLine("Цена не может быть пустой.");
                            break;
                        }
                        float price_f;
                        try
                        {
                            price_f = float.Parse(price);
                        }
                        catch
                        {
                            Console.SetCursorPosition(0, 8);
                            Console.WriteLine("Неверный формат цены.");
                            break;
                        }

                        if (count == "")
                        {
                            Console.SetCursorPosition(0, 8);
                            Console.WriteLine("Количество не может быть пустым.");
                            break;
                        }
                        int count_i;
                        try
                        {
                            count_i = int.Parse(count);
                        }
                        catch
                        {
                            Console.SetCursorPosition(0, 8);
                            Console.WriteLine("Неверный формат количества.");
                            break;
                        }

                        product.name = t_name;
                        product.price = price_f;
                        product.count = count_i;

                        Console.SetCursorPosition(0, 8);
                        Console.WriteLine("Сохранено.");
                        break;
                }

                key = Console.ReadKey(true).Key;
            }

            Converter.Save<List<Product>>(products, "products.json");
        }

        public void Delete(int index)
        {
            products.RemoveAt(index);
            Converter.Save(products, "products.json");
        }
    }
}
