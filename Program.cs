using System;

namespace Lab_9
{
    public delegate void GameStateHandler(string mes);//объявление делегата
    class Game
    {   public event GameStateHandler Attack;//объявление события
        public event GameStateHandler Hill;

        public int _health;

        public Game(int health) => _health = health;

        public int CurrentHealth { get => _health; }

        public void UnchangedHealth(int damage, int hillpoint)
        {
            if (damage == 0)
            {
                if (_health <= 0)
                {   Hill?.Invoke($"ИЗЛЕЧИТЬСЯ НЕВОЗМОЖНО!\n \t-Вы мертвы.");    }
                else if (_health >= 100)
                {   Hill?.Invoke($"ИЗЛЕЧИТЬСЯ НЕВОЗМОЖНО!\n \t-Вы полностью здоровы.");  }
            }
            if (hillpoint == 0)
            {
                if (_health <= 0)
                {   Attack?.Invoke($"АТАКА НЕВОЗМОЖНА!\n \t-Вы мертвы.");    }
            }
        }

        public void UpHealth(int hillpoint)
        {
            if (_health <= 0)
            {   Hill?.Invoke($"-Вы мертвы\n ({ _health } hp).");   }
            else
            {
                _health += hillpoint;
                if (_health > 100)
                {   _health = 100;   }
                Hill?.Invoke($"Вы излечились на { hillpoint } ед.\n \t({ _health } hp).");
            }
        }

        public void DownHealth(int damage)
        {
            if (_health <= 0)
            {   Attack?.Invoke($"-Вы мертвы\n ({ _health }hp).");    }
            else
            {   _health -= damage;
                if (_health < 0)
                {   _health = 0;   }
                Attack?.Invoke($"Вам нанесли { damage } ед. урона. ({ _health } hp).");
            }
        }
    }

    public static class STRING
    {
        public static string Method(this string str, Func<string, string> func) => func.Invoke(str);
        public static string Method(this string str, Func<string, int, string> func, int pos) => func.Invoke(str, pos);
    }

    class Program
    {
        private static void Display(string mes) => Console.WriteLine(mes);
        static void Operation(string str_1, string str_2, Action<string, string> oper)
        {   if (str_1 != str_2)
                oper(str_1, str_2); 
        }
        static void CContact(string str_1, string str_2) => Console.WriteLine(str_1 + str_2);
        static void SSplit(string str_1, string str_2)
        {   string[] words = str_1.Split(str_2.ToCharArray());
            foreach (string s in words)
            {   Console.WriteLine(s);   }
        }

        static void Main(string[] args)
        {   Game Game_1 = new Game(50);
            Game Game_2 = new Game(100);
            Game Game_3 = new Game(0);

            Game_1.Attack += Display;//+= добавляет метод к списку
            Game_1.Hill += Display;

            Game_2.Attack += Display;
            Game_2.Hill += Display;

            Game_3.Attack += Display;
            Game_3.Hill += Display;

            Game_1.UpHealth(30);
            Game_1.DownHealth(80);
            Game_1.UnchangedHealth(40, 0);

            Console.WriteLine();

            Game_2.UpHealth(30);
            Game_2.DownHealth(1);
            Game_2.UnchangedHealth(0, 50);

            Console.WriteLine();

            Game_3.UpHealth(30);
            Game_3.DownHealth(80);
            Game_3.UnchangedHealth(0, 50);

            Console.WriteLine();

            Action<string, string> oper;
            oper = CContact;
            Operation("pine", "apple", oper);
            oper = SSplit;
            Operation("И моё сердце остановилось", " ", oper);

            Func<string, string> removeLowerCase = s => s.ToUpper();
            Func<string, string> removeUpperCase = s => s.ToLower();
            Func<string, int, string> removeIndex = (s, i) => s.Remove(i, 1);

            string str = "hoRRible";

            Console.WriteLine(str.Method(removeLowerCase));
            Console.WriteLine(str.Method(removeUpperCase));
            Console.WriteLine(str.Method(removeIndex, 2));
        }
    }
}
