using System;

namespace Pract10
{
    public class Buh
    {
        List<Note> notes;
        private string name;
        public Buh(List<Note> notes, string name)
        {
            this.notes = notes;
            this.name = name;
        }

        private void DrawMenu()
        {
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль: Бухгалтер");
            Console.WriteLine("F1 - создать запись, Enter - Перейти к записи");
            Console.WriteLine("------------------------");
            float total = 0;
            foreach (var note in notes)
            {
                if (note.prihod)
                {
                    total += note.sum;
                }
                else
                {
                    total -= note.sum;
                }

                string date = note.date.ToString("dd.MM.yyyy");
                string type = note.prihod ? "Прибавка" : "Вычет";
                Console.WriteLine($"  {note.id} - {note.name}, {date}, {note.sum}руб., {type}");
            }
            Console.WriteLine("------------------------");
            Console.WriteLine($"Итого: {total}руб.");
        }

        private void DrawNote(int index)
        {
            Note note = notes[index];
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль: Бухгалтер");
            Console.WriteLine("Esc - назад, Del - удалить, R - редактировать");
            Console.WriteLine("------------------------");
            Console.WriteLine($"  ID: {note.id}");
            Console.WriteLine($"  Название: {note.name}");
            Console.WriteLine($"  Сумма: {note.sum}");
            string date = note.date.ToString("dd.MM.yyyy");
            Console.WriteLine($"  Дата: {date}");
            Console.WriteLine($"  Прибавка?: {note.prihod}");
        }

        public void Start()
        {
            DrawMenu();
            Cursor cursor = new Cursor(4, 4 + notes.Count - 1);
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
                        cursor.SetMax(4 + notes.Count - 1);
                        cursor.Show(-1);
                        break;
                    case (ConsoleKey)HotKeys.Create:
                        Create();
                        DrawMenu();
                        cursor.SetMax(4 + notes.Count - 1);
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
            Console.WriteLine("Ваша роль: Бухгалтер");
            Console.WriteLine("Esc - назад, S - сохранить");
            Console.WriteLine("------------------------");
            int id;
            if (notes.Count > 0)
            {
                id = notes.Max(s => s.id) + 1;
            }
            else
            {
                id = 0;
            }
            Console.WriteLine($"  ID: {id} (выдается автоматически)");
            Console.WriteLine("  Название: ");
            Console.WriteLine("  Сумма: ");
            Console.WriteLine("  Дата: ");
            Console.WriteLine("  Прибавка?: ");
            Note? note = null;
            Cursor cursor = new Cursor(5, 8);
            string z_name = "";
            string sum = "";
            string date = "";
            string prihod = "";

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
                            Console.SetCursorPosition(12, 5);
                            z_name = Input.GetValue(z_name);
                        }
                        else if (index == 1)
                        {
                            Console.SetCursorPosition(9, 6);
                            sum = Input.GetValue(sum);
                        }
                        else if (index == 2)
                        {
                            Console.SetCursorPosition(8, 7);
                            date = Input.GetValue(date);
                        }
                        else if (index == 3)
                        {
                            Console.SetCursorPosition(13, 8);
                            prihod = Input.GetValue(prihod);
                        }
                        break;
                    case (ConsoleKey)HotKeys.Save:
                        if (z_name == "")
                        {
                            Console.SetCursorPosition(0, 9);
                            Console.WriteLine("Название не может быть пустым.");
                            break;
                        }

                        if (sum == "")
                        {
                            Console.SetCursorPosition(0, 9);
                            Console.WriteLine("Сумма не может быть пустой.");
                            break;
                        }
                        float sum_f;
                        try
                        {
                            sum_f = float.Parse(sum);
                        }
                        catch
                        {
                            Console.SetCursorPosition(0, 9);
                            Console.WriteLine("Неверный формат суммы.");
                            break;
                        }

                        DateTime date_dt;
                        if (date == "")
                        {
                            date_dt = DateTime.Now;
                        }
                        else
                        {
                            try
                            {
                                date_dt = DateTime.Parse(date);
                            }
                            catch
                            {
                                Console.SetCursorPosition(0, 9);
                                Console.WriteLine("Неверный формат даты.");
                                break;
                            }
                        }

                        bool prihod_b;
                        if (prihod.ToLower() == "false")
                        {
                            prihod_b = false;
                        }
                        else if (prihod.ToLower() == "true")
                        {
                            prihod_b = true;
                        }
                        else
                        {
                            Console.SetCursorPosition(0, 9);
                            Console.WriteLine("Неверный формат прибавки.");
                            break;
                        }


                        if (note == null)
                        {
                            note = new Note(id, z_name, sum_f, date_dt, prihod_b);
                            notes.Add(note);
                        }
                        else
                        {
                            note.name = z_name;
                            note.sum = sum_f;
                            note.date = date_dt;
                            note.prihod = prihod_b;
                        }
                        Console.SetCursorPosition(0, 9);
                        Console.WriteLine("Сохранено.");
                        break;
                }

                key = Console.ReadKey(true).Key;
            }

            Converter.Save<List<Note>>(notes, "notes.json");
        }

        public void Read(int index)
        {
            DrawNote(index);

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
                        DrawNote(index);
                        break;
                }

                key = Console.ReadKey(true).Key;
            }
        }

        public void Update(int index)
        {
            Note note = notes[index];
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль: Бухгалтер");
            Console.WriteLine("Esc - назад, S - сохранить");
            Console.WriteLine("------------------------");
            int id = note.id;
            string z_name = note.name;
            string sum = note.sum.ToString();
            string date = note.date.ToString("dd.MM.yyyy");
            string prihod = note.prihod.ToString();
            Console.WriteLine($"  ID: {id} (выдается автоматически)");
            Console.WriteLine($"  Название: {z_name}");
            Console.WriteLine($"  Сумма: {sum}");
            Console.WriteLine($"  Дата: {date}");
            Console.WriteLine($"  Прибавка?: {prihod}");
            Cursor cursor = new Cursor(5, 8);

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
                            Console.SetCursorPosition(12, 5);
                            z_name = Input.GetValue(z_name);
                        }
                        else if (s_index == 1)
                        {
                            Console.SetCursorPosition(9, 6);
                            sum = Input.GetValue(sum);
                        }
                        else if (s_index == 2)
                        {
                            Console.SetCursorPosition(8, 7);
                            date = Input.GetValue(date);
                        }
                        else if (s_index == 3)
                        {
                            Console.SetCursorPosition(13, 8);
                            prihod = Input.GetValue(prihod);
                        }
                        break;
                    case (ConsoleKey)HotKeys.Save:
                        if (z_name == "")
                        {
                            Console.SetCursorPosition(0, 9);
                            Console.WriteLine("Название не может быть пустым.");
                            break;
                        }

                        if (sum == "")
                        {
                            Console.SetCursorPosition(0, 9);
                            Console.WriteLine("Сумма не может быть пустой.");
                            break;
                        }
                        float sum_f;
                        try
                        {
                            sum_f = float.Parse(sum);
                        }
                        catch
                        {
                            Console.SetCursorPosition(0, 9);
                            Console.WriteLine("Неверный формат суммы.");
                            break;
                        }

                        DateTime date_dt;
                        if (date == "")
                        {
                            date_dt = DateTime.Now;
                        }
                        else
                        {
                            try
                            {
                                date_dt = DateTime.Parse(date);
                            }
                            catch
                            {
                                Console.SetCursorPosition(0, 9);
                                Console.WriteLine("Неверный формат даты.");
                                break;
                            }
                        }

                        bool prihod_b;
                        if (prihod.ToLower() == "false")
                        {
                            prihod_b = false;
                        }
                        else if (prihod.ToLower() == "true")
                        {
                            prihod_b = true;
                        }
                        else
                        {
                            Console.SetCursorPosition(0, 9);
                            Console.WriteLine("Неверный формат прибавки.");
                            break;
                        }


                        note.name = z_name;
                        note.sum = sum_f;
                        note.date = date_dt;
                        note.prihod = prihod_b;
                        Console.SetCursorPosition(0, 8);
                        Console.WriteLine("Сохранено.");
                        break;
                }

                key = Console.ReadKey(true).Key;
            }

            Converter.Save<List<Note>>(notes, "notes.json");
        }

        public void Delete(int index)
        {
            notes.RemoveAt(index);
            Converter.Save(notes, "notes.json");
        }
    }
}
