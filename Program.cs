using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Notes
{

    public class Note
    {
        public string Text { get; set; }
    }
    class Program
    {
        private static List<Note> notes = new List<Note>();
        private const string filePath = "notes.json";

        static void Main(string[] args)
        {
            LoadNotes();
            if (notes.Count == 0)
            {
                notes.Add(new Note { Text = "Простейшая заметка." });
            }

            while (true)
            {
                Console.Clear();
                ShowNotes();
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Создать заметку");
                Console.WriteLine("2. Редактировать заметку");
                Console.WriteLine("3. Удалить заметку");
                Console.WriteLine("4. Выход");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateNote();
                        break;
                    case "2":
                        EditNote();
                        break;
                    case "3":
                        DeleteNote();
                        break;
                    case "4":
                        SaveNotes();
                        return;
                    default:
                        Console.WriteLine("Неправильный ввод, попробуйте снова.");
                        break;
                }
            }
        }

        private static void ShowNotes()
        {
            Console.WriteLine("Список заметок:");
            for (int i = 0; i < notes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {notes[i].Text}");
            }
        }

        private static void CreateNote()
        {
            Console.WriteLine("Введите текст заметки:");
            var text = Console.ReadLine();
            notes.Add(new Note { Text = text });
            SaveNotes();
        }

        private static void EditNote()
        {
            Console.WriteLine("Введите номер заметки для редактирования:");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= notes.Count)
            {
                Console.WriteLine("Введите новый текст заметки:");
                notes[index - 1].Text = Console.ReadLine();
                SaveNotes();
            }
            else
            {
                Console.WriteLine("Неверный номер заметки.");
            }
        }

        private static void DeleteNote()
        {
            Console.WriteLine("Введите номер заметки для удаления:");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= notes.Count)
            {
                notes.RemoveAt(index - 1);
                SaveNotes();
            }
            else
            {
                Console.WriteLine("Неверный номер заметки.");
            }
        }

        private static void LoadNotes()
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                notes = JsonConvert.DeserializeObject<List<Note>>(json) ?? new List<Note>();
            }
        }

        private static void SaveNotes()
        {
            var json = JsonConvert.SerializeObject(notes);
            File.WriteAllText(filePath, json);
        }
    }
}
