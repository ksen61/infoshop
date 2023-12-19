namespace Pract10
{
    public class Auth
    {
        private List<User> users;

        public Auth(List<User> users)
        {
            this.users = users;
        }

        public User Login()
        {
            Console.Clear();
            User? user = null;
            string login = "";
            string password = "";
            Console.WriteLine("Добро пожаловать в магазин!");
            Console.WriteLine("-----------");

            Console.WriteLine("  Логин: ");
            Console.WriteLine("  Пароль: ");
            Console.WriteLine("  Авторизоваться");

            Cursor cursor = new Cursor(2, 4);

            while (user == null)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
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
                        if (index == 2)
                        {
                            user = users.Find(u => u.login == login && u.password == password);
                            if (user == null)
                            {
                                Console.SetCursorPosition(0, 5);
                                Console.WriteLine("Неправильный логин или пароль");
                            }
                        }
                        else if (index == 0)
                        {
                            Console.SetCursorPosition(9, 2);
                            login = Input.GetValue(login);
                        }
                        else if (index == 1)
                        {
                            Console.SetCursorPosition(10, 3);
                            password = Input.GetValue(password, true);
                        }
                        break;
                }
            }

            return user;
        }
    }
}