using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02._03hw
{


    class Poem
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Text { get; set; }
        public string Theme { get; set; }
    }

    class PoemCollection
    {
        private List<Poem> poems;

        public PoemCollection()
        {
            poems = new List<Poem>();
        }

        public void AddPoem(Poem poem)
        {
            poems.Add(poem);
        }

        public void RemovePoem(Poem poem)
        {
            poems.Remove(poem);
        }

        public void UpdatePoem(Poem poem, string title, string author, int year, string text, string theme)
        {
            poem.Title = title;
            poem.Author = author;
            poem.Year = year;
            poem.Text = text;
            poem.Theme = theme;
        }

        public List<Poem> SearchByTitle(string title)
        {
            return poems.FindAll(poem => poem.Title.Contains(title));
        }

        public List<Poem> SearchByAuthor(string author)
        {
            return poems.FindAll(poem => poem.Author.Contains(author));
        }

        public List<Poem> SearchByYear(int year)
        {
            return poems.FindAll(poem => poem.Year == year);
        }

        public List<Poem> SearchByTheme(string theme)
        {
            return poems.FindAll(poem => poem.Theme.Contains(theme));
        }

        public void SaveToFile(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (Poem poem in poems)
                {
                    writer.WriteLine($"{poem.Title}|{poem.Author}|{poem.Year}|{poem.Theme}|{poem.Text}");
                }
            }
        }

        public void LoadFromFile(string filename)
        {
            poems.Clear();
            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] fields = line.Split('|');
                    Poem poem = new Poem
                    {
                        Title = fields[0],
                        Author = fields[1],
                        Year = int.Parse(fields[2]),
                        Theme = fields[3],
                        Text = fields[4]
                    };
                    poems.Add(poem);
                }
            }
        }
    }
    class Program
    {
        static void Main()
        {
            PoemCollection collection = new PoemCollection();

            // Добавление стихов
            Poem poem1 = new Poem
            {
                Title = "Солнце",
                Author = "Александр Блок",
                Year = 1912,
                Theme = "Природа",
                Text = "Солнце взошло над городом..."
            };
            collection.AddPoem(poem1);

            Poem poem2 = new Poem
            {
                Title = "Есть в Божьем мире горы святые",
                Author = "Федор Тютчев",
                Year = 1867,
                Theme = "Религия",
                Text = "Есть в Божьем мире горы святые,\n\nОтдаленные Господу,\n\nКак в жизни те горы прекрасные,\n\nЧьи вершины навсегда в туманах."
            };
            collection.AddPoem(poem2);
            // Поиск стихов по заголовку
            List<Poem> poemsByTitle = collection.SearchByTitle("Солнце");
            Console.WriteLine($"Найдено стихов по заголовку 'Солнце': {poemsByTitle.Count}");

            // Поиск стихов по автору
            List<Poem> poemsByAuthor = collection.SearchByAuthor("Федор Тютчев");
            Console.WriteLine($"Найдено стихов по автору 'Федор Тютчев': {poemsByAuthor.Count}");

            // Сохранение коллекции стихов в файл
            collection.SaveToFile("poems.txt");

            // Очистка коллекции стихов
            collection = new PoemCollection();

            // Загрузка коллекции стихов из файла
            collection.LoadFromFile("poems.txt");
            Console.WriteLine($"Загружено стихов из файла: {collection.Poem.Count}");
        }

    }
}


