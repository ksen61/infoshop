namespace Pract10
{
    public class Man : Crud
    {
        List<Employee> employees;
        private string name;
        public Man(List<Employee> employees, string name)
        {
            this.employees = employees;
            this.name = name;
        }

        private void DrawMenu()
        {
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль: Менеджер персонала");
            Console.WriteLine("F1 - создать запись, Enter - Перейти к записи");
            Console.WriteLine("------------------------");
            foreach (var employee in employees)
            {
                string fio = $"{employee.f} {employee.i} {employee.o}";
                Console.WriteLine($"  {employee.id} - {fio}, {employee.post}");
            }
        }

        private void DrawEmployee(int index)
        {
            Employee employee = employees[index];
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль: Менеджер персонала");
            Console.WriteLine("Esc - назад, Del - удалить, R - редактировать");
            Console.WriteLine("------------------------");
            Console.WriteLine($"  ID: {employee.id}");
            Console.WriteLine($"  Фамилия: {employee.f}");
            Console.WriteLine($"  Имя: {employee.i}");
            Console.WriteLine($"  Отчество: {employee.o}");
            string birthday = employee.birthday.ToString("dd.MM.yyyy");
            Console.WriteLine($"  День рождения: {birthday}");
            Console.WriteLine($"  Паспорт: {employee.passport}");
            Console.WriteLine($"  Должность: {employee.post}");
            Console.WriteLine($"  Зарплата: {employee.salary}");
            string user_id = employee.user_id == null ? "" : employee.user_id.ToString();
            Console.WriteLine($"  ID пользователя: {user_id}");
        }

        public void Start()
        {
            DrawMenu();
            Cursor cursor = new Cursor(4, 4 + employees.Count - 1);
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
                        cursor.SetMax(4 + employees.Count - 1);
                        cursor.Show(-1);
                        break;
                    case (ConsoleKey)HotKeys.Create:
                        Create();
                        DrawMenu();
                        cursor.SetMax(4 + employees.Count - 1);
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
            Console.WriteLine("Ваша роль: Менеджер персонала");
            Console.WriteLine("Esc - назад, S - сохранить");
            Console.WriteLine("------------------------");
            int id;
            if (employees.Count > 0)
            {
                id = employees.Max(s => s.id) + 1;
            }
            else
            {
                id = 0;
            }
            Console.WriteLine($"  ID: {id} (выдается автоматически)");
            Console.WriteLine("  Фамилия: ");
            Console.WriteLine("  Имя: ");
            Console.WriteLine("  Отчество: ");
            Console.WriteLine("  День рождения: ");
            Console.WriteLine("  Паспорт: ");
            Console.WriteLine("  Должность: ");
            Console.WriteLine("  Зарплата: ");
            Console.WriteLine("  ID пользователя: ");
            Employee? employee = null;
            Cursor cursor = new Cursor(5, 12);
            string f = "";
            string i = "";
            string o = "";
            string birthday = "";
            string passport = "";
            string post = "";
            string salary = "";
            string user_id = "";

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
                            Console.SetCursorPosition(11, 5);
                            f = Input.GetValue(f);
                        }
                        else if (index == 1)
                        {
                            Console.SetCursorPosition(7, 6);
                            i = Input.GetValue(i);
                        }
                        else if (index == 2)
                        {
                            Console.SetCursorPosition(12, 7);
                            o = Input.GetValue(o);
                        }
                        else if (index == 3)
                        {
                            Console.SetCursorPosition(17, 8);
                            birthday = Input.GetValue(birthday);
                        }
                        else if (index == 4)
                        {
                            Console.SetCursorPosition(11, 9);
                            passport = Input.GetValue(passport);
                        }
                        else if (index == 5)
                        {
                            Console.SetCursorPosition(13, 10);
                            post = Input.GetValue(post);
                        }
                        else if (index == 6)
                        {
                            Console.SetCursorPosition(12, 11);
                            salary = Input.GetValue(salary);
                        }
                        else if (index == 7)
                        {
                            Console.SetCursorPosition(19, 12);
                            user_id = Input.GetValue(user_id);
                        }
                        break;
                    case (ConsoleKey)HotKeys.Save:
                        if (f == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Фамилия не может быть пустой.");
                            break;
                        }

                        if (i == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Имя не может быть пустым.");
                            break;
                        }

                        if (birthday == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("День рождения не может быть пустым.");
                            break;
                        }
                        DateTime birthday_dt;
                        try
                        {
                            birthday_dt = DateTime.Parse(birthday);
                        }
                        catch
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Неверный формат даты.");
                            break;
                        }

                        if (passport == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Пасспорт не может быть пустым.");
                            break;
                        }

                        if (post == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Должность не может быть пустой.");
                            break;
                        }

                        if (salary == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Зарплата не может быть пустой.");
                            break;
                        }
                        float salary_f;
                        try
                        {
                            salary_f = float.Parse(salary);
                        }
                        catch
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Неверный формат зарплаты.");
                            break;
                        }

                        int? user_id_i;
                        if (user_id != "")
                        {
                            try
                            {
                                user_id_i = int.Parse(user_id);
                            }
                            catch
                            {
                                Console.SetCursorPosition(0, 13);
                                Console.WriteLine("Неверный формат ID пользователя.");
                                break;
                            }
                        }
                        else
                        {
                            user_id_i = null;
                        }

                        if (employee == null)
                        {
                            employee = new Employee(id, f, i, birthday_dt, passport, post, salary_f, user_id_i, o);
                            employees.Add(employee);
                        }
                        else
                        {
                            employee.f = f;
                            employee.i = i;
                            employee.o = o;
                            employee.birthday = birthday_dt;
                            employee.passport = passport;
                            employee.post = post;
                            employee.salary = salary_f;
                            employee.user_id = user_id_i;
                        }
                        Console.SetCursorPosition(0, 13);
                        Console.WriteLine("Сохранено.");

                        break;
                }

                key = Console.ReadKey(true).Key;
            }

            Converter.Save<List<Employee>>(employees, "employees.json");
        }

        public void Read(int index)
        {
            DrawEmployee(index);

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
                        DrawEmployee(index);
                        break;
                }

                key = Console.ReadKey(true).Key;
            }
        }

        public void Update(int index)
        {
            Employee employee = employees[index];
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль: Менеджер персонала");
            Console.WriteLine("Esc - назад, S - сохранить");
            Console.WriteLine("------------------------");
            int id = employee.id;
            string f = employee.f;
            string i = employee.i;
            string o = employee.o;
            string birthday = employee.birthday.ToString("dd.MM.yyyy");
            string passport = employee.passport;
            string post = employee.post;
            string salary = employee.salary.ToString();
            string user_id = employee.user_id != null ? employee.user_id.ToString() : "";
            Console.WriteLine($"  ID: {id} (выдается автоматически)");
            Console.WriteLine($"  Фамилия: {f}");
            Console.WriteLine($"  Имя: {i}");
            Console.WriteLine($"  Отчество: {o}");
            Console.WriteLine($"  День рождения: {birthday}");
            Console.WriteLine($"  Паспорт: {passport}");
            Console.WriteLine($"  Должность: {post}");
            Console.WriteLine($"  Зарплата: {salary}");
            Console.WriteLine($"  ID пользователя: {user_id}");
            Cursor cursor = new Cursor(5, 12);

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
                            Console.SetCursorPosition(11, 5);
                            f = Input.GetValue(f);
                        }
                        else if (s_index == 1)
                        {
                            Console.SetCursorPosition(7, 6);
                            i = Input.GetValue(i);
                        }
                        else if (s_index == 2)
                        {
                            Console.SetCursorPosition(12, 7);
                            o = Input.GetValue(o);
                        }
                        else if (s_index == 3)
                        {
                            Console.SetCursorPosition(17, 8);
                            birthday = Input.GetValue(birthday);
                        }
                        else if (s_index == 4)
                        {
                            Console.SetCursorPosition(11, 9);
                            passport = Input.GetValue(passport);
                        }
                        else if (s_index == 5)
                        {
                            Console.SetCursorPosition(13, 10);
                            post = Input.GetValue(post);
                        }
                        else if (s_index == 6)
                        {
                            Console.SetCursorPosition(12, 11);
                            salary = Input.GetValue(salary);
                        }
                        else if (s_index == 7)
                        {
                            Console.SetCursorPosition(19, 12);
                            user_id = Input.GetValue(user_id);
                        }
                        break;
                    case (ConsoleKey)HotKeys.Save:
                        if (f == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Фамилия не может быть пустой.");
                            break;
                        }

                        if (i == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Имя не может быть пустым.");
                            break;
                        }

                        if (birthday == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Имя не может быть пустым.");
                            break;
                        }
                        DateTime birthday_dt;
                        try
                        {
                            birthday_dt = DateTime.Parse(birthday);
                        }
                        catch
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Неверный формат даты.");
                            break;
                        }

                        if (passport == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Пасспорт не может быть пустым.");
                            break;
                        }

                        if (post == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Должность не может быть пустой.");
                            break;
                        }

                        if (salary == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Зарплата не может быть пустой.");
                            break;
                        }
                        float salary_f;
                        try
                        {
                            salary_f = float.Parse(salary);
                        }
                        catch
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Неверный формат зарплаты.");
                            break;
                        }

                        int? user_id_i;
                        if (user_id != "")
                        {
                            try
                            {
                                user_id_i = int.Parse(user_id);
                            }
                            catch
                            {
                                Console.SetCursorPosition(0, 13);
                                Console.WriteLine("Неверный формат ID пользователя.");
                                break;
                            }
                        }
                        else
                        {
                            user_id_i = null;
                        }

                        employee.f = f;
                        employee.i = i;
                        employee.o = o;
                        employee.birthday = birthday_dt;
                        employee.passport = passport;
                        employee.post = post;
                        employee.salary = salary_f;
                        employee.user_id = user_id_i;

                        Console.SetCursorPosition(0, 13);
                        Console.WriteLine("Сохранено.");
                        break;
                }

                key = Console.ReadKey(true).Key;
            }

            Converter.Save<List<Employee>>(employees, "employees.json");
        }

        public void Delete(int index)
        {
            employees.RemoveAt(index);
            Converter.Save(employees, "employees.json");
        }
    }
}
