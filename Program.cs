using System.Collections.Generic;
using System.Data;

namespace Pract10
{
    class Program
    {
        static List<User> users;
        static List<Employee> employees;
        static List<Product> products;
        static List<Note> notes;
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            LoadFiles();
            ConsoleKey key;
            while (true)
            {
                Auth auth = new Auth(users);
                User auth_user = auth.Login();
                Console.Clear();
                Employee? auth_employee = employees.Find(s => s.user_id == auth_user.id);
                string name = auth_employee == null ? auth_user.login : auth_employee.i;
                switch (auth_user.role)
                {
                    case Role.Admin:
                        Admin admin = new Admin(users, name);
                        admin.Start();
                        break;
                    case Role.Man:
                        Man man = new Man(employees, name);
                        man.Start();
                        break;
                    case Role.StoreMan:
                        StoreMan storeMan = new StoreMan(products, name);
                        storeMan.Start();
                        break;
                    case Role.Kas:
                        Kas kas = new Kas(notes, name);
                        kas.Start();
                        break;
                    case Role.Buh:
                        Buh buh = new Buh(notes, name);
                        buh.Start();
                        break;
                }
            }
        }

        static void LoadFiles()
        {
            List<User>? loaded_users = Converter.Load<List<User>>("users.json");
            if (loaded_users == null)
            {
                loaded_users = new List<User>();
                User admin = new User(0, "admin", "password", Role.Admin);
                loaded_users.Add(admin);
                Converter.Save<List<User>>(loaded_users, "users.json");
            }
            users = loaded_users;

            List<Employee>? loaded_employees = Converter.Load<List<Employee>>("employees.json");
            if (loaded_employees == null)
            {
                loaded_employees = new List<Employee>();
                Converter.Save<List<Employee>>(loaded_employees, "employees.json");
            }
            employees = loaded_employees;

            List<Product>? loaded_products = Converter.Load<List<Product>>("products.json");
            if (loaded_products == null)
            {
                loaded_products = new List<Product>();
                Converter.Save<List<Product>>(loaded_products, "products.json");
            }
            products = loaded_products;

            List<Note>? loaded_notes = Converter.Load<List<Note>>("notes.json");
            if (loaded_notes == null)
            {
                loaded_notes = new List<Note>();
                Converter.Save<List<Note>>(loaded_notes, "notes.json");
            }
            notes = loaded_notes;
        }
    }
}
