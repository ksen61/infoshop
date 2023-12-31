﻿using System.Data;

namespace Pract10
{
    public class Admin : Crud
    {
        private List<User> users;
        private string name;

        public Admin(List<User> users, string name)
        {
            this.users = users;
            this.name = name;
        }

        private void DrawMenu()
        {
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль: Администратор");
            Console.WriteLine("F1 - создать запись, Enter - Перейти к записи");
            Console.WriteLine("------------------------");
            foreach (var user in users)
            {
                string role = GetRoleName(user.role);
                Console.WriteLine($"  {user.id} - {user.login}, {role}");
            }
        }

        private void DrawUser(int index)
        {
            User user = users[index];
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль: Администратор");
            Console.WriteLine("Esc - назад, Del - удалить, R - редактировать");
            Console.WriteLine("------------------------");
            Console.WriteLine($"  ID: {user.id}");
            Console.WriteLine($"  Логин: {user.login}");
            Console.WriteLine($"  Пароль: {user.password}");
            string role = GetRoleName(user.role);
            Console.WriteLine($"  Роль: {(int)user.role} - {role}");
        }

        public void Start()
        {
            DrawMenu();
            Cursor cursor = new Cursor(4, 4 + users.Count - 1);
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
                        cursor.SetMax(4 + users.Count - 1);
                        cursor.Show(-1);
                        break;
                    case (ConsoleKey)HotKeys.Create:
                        Create();
                        DrawMenu();
                        cursor.SetMax(4 + users.Count - 1);
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
            Console.WriteLine("Ваша роль: Администратор");
            Console.WriteLine("Esc - назад, S - сохранить | 0 - Администратор, 1 - Менеджер, 2 - Склад-менеджер, 3 - Кассир, 4 - Бухгалтер");
            Console.WriteLine("------------------------");
            int id = users.Max(u => u.id) + 1;
            Console.WriteLine($"  ID: {id} (выдается автоматически)");
            Console.WriteLine("  Логин: ");
            Console.WriteLine("  Пароль: ");
            Console.WriteLine("  Роль: ");
            User? user = null;
            Cursor cursor = new Cursor(5, 7);
            string login = "";
            string password = "";
            string role = "";

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
                        int i = cursor.GetIndex();

                        if (i == 0)
                        {
                            Console.SetCursorPosition(9, 5);
                            login = Input.GetValue(login);
                        }
                        else if (i == 1)
                        {
                            Console.SetCursorPosition(10, 6);
                            password = Input.GetValue(password);
                        }
                        else if (i == 2)
                        {
                            Console.SetCursorPosition(8, 7);
                            role = Input.GetValue(role);
                        }
                        break;
                    case (ConsoleKey)HotKeys.Save:
                        if (login == "")
                        {
                            Console.SetCursorPosition(0, 8);
                            Console.WriteLine("Логин не может быть пустым. ");
                            break;
                        }
                        if (password == "")
                        {
                            Console.SetCursorPosition(0, 8);
                            Console.WriteLine("Пароль не может быть пустым.");
                            break;
                        }
                        Role user_role;
                        try
                        {
                            int role_id = int.Parse(role);
                            user_role = (Role)role_id;
                        }
                        catch
                        {
                            Console.SetCursorPosition(0, 8);
                            Console.WriteLine("Роль должна быть числом.");
                            break;
                        }

                        if (user == null)
                        {
                            user = new User(id, login, password, user_role);
                            users.Add(user);

                        }
                        else
                        {
                            user.login = login;
                            user.password = password;
                            user.role = user_role;
                        }

                        Console.SetCursorPosition(0, 8);
                        Console.WriteLine("Сохранено.");
                        break;
                }

                key = Console.ReadKey(true).Key;
            }

            Converter.Save<List<User>>(users, "users.json");
        }

        public void Read(int index)
        {
            DrawUser(index);

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
                        DrawUser(index);
                        break;
                }

                key = Console.ReadKey(true).Key;
            }
        }

        public void Update(int index)
        {
            User user = users[index];
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль: Администратор");
            Console.WriteLine("Esc - назад, S - сохранить | 0 - Администратор, 1 - Менеджер, 2 - Склад-менеджер, 3 - Кассир, 4 - Бухгалтер");
            Console.WriteLine("------------------------");
            int id = user.id;
            string login = user.login;
            string password = user.password;
            string role = ((int)user.role).ToString();
            Console.WriteLine($"  ID: {id} (выдается автоматически)");
            Console.WriteLine($"  Логин: {login}");
            Console.WriteLine($"  Пароль: {password}");
            Console.WriteLine($"  Роль: {role}");
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
                        int i = cursor.GetIndex();

                        if (i == 0)
                        {
                            Console.SetCursorPosition(9, 5);
                            login = Input.GetValue(login);
                        }
                        else if (i == 1)
                        {
                            Console.SetCursorPosition(10, 6);
                            password = Input.GetValue(password);
                        }
                        else if (i == 2)
                        {
                            Console.SetCursorPosition(8, 7);
                            role = Input.GetValue(role);
                        }
                        break;
                    case (ConsoleKey)HotKeys.Save:
                        if (login == "")
                        {
                            Console.SetCursorPosition(0, 8);
                            Console.WriteLine("Логин не может быть пустым.");
                            break;
                        }
                        if (password == "")
                        {
                            Console.SetCursorPosition(0, 8);
                            Console.WriteLine("Пароль не может быть пустым.");
                            break;
                        }
                        Role user_role;
                        try
                        {
                            int role_id = int.Parse(role);
                            user_role = (Role)role_id;
                        }
                        catch
                        {
                            Console.SetCursorPosition(0, 8);
                            Console.WriteLine("Роль должна быть числом.");
                            break;
                        }

                        user.login = login;
                        user.password = password;
                        user.role = user_role;

                        Console.SetCursorPosition(0, 8);
                        Console.WriteLine("Сохранено.");
                        break;
                }

                key = Console.ReadKey(true).Key;
            }

            Converter.Save<List<User>>(users, "users.json");
        }

        public void Delete(int index)
        {
            users.RemoveAt(index);
            Converter.Save(users, "users.json");
        }

        private string GetRoleName(Role role)
        {
            string roleName = "";
            switch (role)
            {
                case Role.Admin:
                    roleName = "Администратор";
                    break;
                case Role.Man:
                    roleName = "Менеджер персонала";
                    break;
                case Role.StoreMan:
                    roleName = "Склад-менеджер";
                    break;
                case Role.Kas:
                    roleName = "Кассир";
                    break;
                case Role.Buh:
                    roleName = "Бухгалтер";
                    break;
            }

            return roleName;
        }
    }
}
