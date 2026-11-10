using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{
    class Program
    {
        static void Main(string[] args)
        {
            ScriptureLibrary library = new ScriptureLibrary();
            Scripture scripture = library.GetRandomScripture();

            while (true)
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine();

                if (scripture.IsCompletelyHidden())
                {
                    Console.WriteLine("Congratulations! You have memorized the scripture.");
                    break;
                }

                Console.Write("Press Enter to continue or type 'quit' to finish: ");
                string input = Console.ReadLine()?.Trim();

                if (input?.Equals("quit", StringComparison.OrdinalIgnoreCase) == true)
                {
                    break;
                }

                scripture.HideRandomWords(3);
            }
        }
    }

    public class Reference
    {
        private string _book;
        private int _chapter;
        private int _verse;
        private int? _endVerse;

        public Reference(string book, int chapter, int verse)
        {
            _book = book;
            _chapter = chapter;
            _verse = verse;
            _endVerse = null;
        }

        public Reference(string book, int chapter, int startVerse, int endVerse)
        {
            _book = book;
            _chapter = chapter;
            _verse = startVerse;
            _endVerse = endVerse;
        }

        public string GetDisplayText()
        {
            if (_endVerse.HasValue)
            {
                return $"{_book} {_chapter}:{_verse}-{_endVerse.Value}";
            }

            return $"{_book} {_chapter}:{_verse}";
        }
    }

    public class Word
    {
        private string _text;
        private bool _isHidden;

        public Word(string text)
        {
            _text = text;
            _isHidden = false;
        }

        public void Hide()
        {
            _isHidden = true;
        }

        public void Show()
        {
            _isHidden = false;
        }

        public bool IsHidden()
        {
            return _isHidden;
        }

        public string GetDisplayText()
        {
            if (_isHidden)
            {
                return new string('_', _text.Length);
            }

            return _text;
        }
    }

    public class Scripture
    {
        private Reference _reference;
        private List<Word> _words;

        public Scripture(Reference reference, string text)
        {
            _reference = reference;
            _words = new List<Word>();

            string[] splitText = text.Split(' ');
            foreach (string wordText in splitText)
            {
                _words.Add(new Word(wordText));
            }
        }

        public void HideRandomWords(int numberToHide)
        {
            Random random = new Random();
            List<Word> visibleWords = _words.Where(w => !w.IsHidden()).ToList();

            int countToHide = Math.Min(numberToHide, visibleWords.Count);

            for (int i = 0; i < countToHide; i++)
            {
                int index = random.Next(visibleWords.Count);
                visibleWords[index].Hide();
                visibleWords.RemoveAt(index);
            }
        }

        public bool IsCompletelyHidden()
        {
            return _words.All(w => w.IsHidden());
        }

        public string GetDisplayText()
        {
            string renderedText = string.Join(" ", _words.Select(w => w.GetDisplayText()));
            return $"{_reference.GetDisplayText()} {renderedText}";
        }
    }

    public class ScriptureLibrary
    {
        private List<Scripture> _scriptures;

        public ScriptureLibrary()
        {
            _scriptures = new List<Scripture>
            {
                new Scripture(new Reference("Proverbs", 3, 5, 6), "Trust in the Lord with all thine heart and lean not unto thine own understanding;"),
                new Scripture(new Reference("John", 3, 16), "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life."),
                new Scripture(new Reference("Philippians", 4, 13), "I can do all things through Christ which strengtheneth me.")
            };
        }

        public Scripture GetRandomScripture()
        {
            Random random = new Random();
            int index = random.Next(_scriptures.Count);
            return _scriptures[index];
        }
    }
}