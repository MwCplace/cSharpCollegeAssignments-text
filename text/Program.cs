/******************************************************************************
3. Implement a console application for text analysis. The application must have the following functionality:

 a) calculate the number of characters, words, sentences in the entered text;

 b) calculate the number of occurrences of each character;

 c) calculate the average length of words in the text;

 d) determine the position of the symbol in the text;

 e) determine the occurrence of a sentence in the text;

 f) defines and outputs palindrome words in the text.

All operations must be implemented as methods.
*******************************************************************************/

using static System.Console;

class HelloWorld
{
    private static string text = ""; // текст пользователя будет храниться тут

    private static string tryAgainText = ". Попробуйте еще раз";
    private static string[] rules = {
        "Во время любого ввода вы можете ввести слово \"стоп\" и программа завершится",
        "Во время любого ввода вы можете ввести слово \"правила\" и будут показаны правила программы",
        "При завершении ввода данных нажимайте клавишу Enter",
        "При сообщении \"Введите: да/нет\" если вы согласны, введите слово \"да\", иначе введите любое другое слово или символ",
        "Программа не может хранить более 1-ого текста, имейте это ввиду"
    };

    // a)
    static int GetNumberOfChars(string str) // принимаем текст (строку)

    {
        return str.Length; // возвращаем число (длина строки = кол-во символов)
    }

    static int GetNumberOfCharsWithoutSpaces(string str) // принимаем текст (строку)

    {
        int count = str.Length; // узнаем длину строки

        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == 32) // каждый элемент проверяется на то, является ли он пробелом (пробел в Юникоде под номером 32)
            {
                count--; // убираем каждый подсчитанный пробел из счетчика
            }

        }
        return count; // возвращаем число (кол-во символов)
    }

    static int GetNumberOfWords(string str)

    {
        int count = 0; // создаем счетчик, задаем ему значение 0
        if (str.Length > 0)
        {
            count = 1; // задаем счетчику значение 1, если длина текста/строки больше 0 (т.е. если есть хотя бы 1 символ, значит есть 1 слово)
        }

        for (int i = 0; i < str.Length; i++) // задействуем все элементы/символы строки
        {
            if (str[i] == 32)
            {
                count++;  // увеличиваем показание счетчика на один при каждом нахождении пробела
            }

        }
        return count;
    }

    static int GetNumberOfSentences(string str)

    {
        int count = 0; // создаем счетчик, задаем ему значение 0

        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == 33 || str[i] == 46 || str[i] == 63 || str[i] == 59)
            {
                count++; // увеличиваем показание счетчика на один при каждом нахождении символов '.' '!' '?' ';'
            }

        }
        return count;
    }

    // b)
    static Dictionary<char, int> GetCharactersOccurences(string str)
    {
        Dictionary<char, int> charCount = new Dictionary<char, int>(); // создаем экземпляр класса Словарь. Здесь будут записаны пары [символ, счетчик]

        foreach (char c in str) // проход по каждому символу текста/строки
        {
            if (charCount.ContainsKey(c)) // если символ есть в словаре, счетчик в словаре +1
            {
                charCount[c]++;
            }
            else // иначе (если такого символа нет) добавляем символ и счетчик в словаре = 1
            {
                charCount.Add(c, 1);
            }
        }

        return charCount; // возвращаем словарь
    }

    // c)

    static bool IsLetter(int uNum) // принимает число(порядковый номер символа юникода)
    {
        // создаем булеан (принадлежит ли символ A-Z, a-z, А-Я а-я)
        bool isLetter = (uNum > 64 && uNum < 91 || uNum > 96 && uNum < 121 || uNum > 1039 && uNum < 1102);

        return isLetter;
    }

    static int GetNumberOfLetters(string str) // кол-во символов, которые явл. буквой

    {
        int count = 0;

        for (int i = 0; i < str.Length; i++)
        {
            if (IsLetter(str[i])) // если буква
            {
                count++; // добавляем
            }

        }
        return count; // возвращаем число (кол-во символов)
    }

    static int GetAverageWordlength(string str)
    {
        // разделить кол-во символов, которые явл. буквой на кол-во слов
        return GetNumberOfLetters(str) / GetNumberOfWords(str);

    }

    // d)
    static List<int> GetPositionsOfSymbol(string str, string c)
    {
        List<int> positions = new List<int>(str.Length); // str.Length - будет максимальным числом (для экономии ресурсов)

        // get unicode number of char c# - c[0]
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == c[0]) // get unicode number of char c# - c[0] // сравнение символа строки и выбранного символа
            {
                positions.Add(i); // если совпадают - добавляем позицию (индекс)
            }
        }

        return positions;
    }

    // e)
    static int GetNumberOfPartOccurrences(string whole, string part)
    {
        int occurrences = 0;

        for (int i = 0; i <= whole.Length - part.Length; i++)
        {
            bool found = true;

            for (int j = 0; j < part.Length; j++)
            {
                if (whole[i + j] != part[j])
                {
                    found = false;
                    break;
                }
            }

            if (found)
            {
                occurrences++;
                i += part.Length - 1;
            }
        }

        return occurrences;
    }

    // f)
    static List<string> GetPalindromeWords(string text)
    {
        List<string> palindromes = new List<string>();
        int l, count = 0;

        // GetArrayOfWords
        foreach (string word in text.Split(' ', '!', '.', ',', ';', ':')) // для каждого слова которое отделено пробелом и другими знаками
        {
            if (word.Length > 0) // убираем слова с нулем символов, которые образовались от убирания знаков
            {
                l = word.Length / 2; // расчитываем сколько нужно символов, чтобы пройти до конца левого и правого края
                while (l > 0) // длина уменьшается с каждой итерацией пока больше 0
                {
                    if (word[l - 1] == word[word.Length - l]) // берем индекс с центра к леву
                                                              // (половина длины слова минус один, потому что счет в массивах начинается с 0, а не 1)
                                                              // и с центра в право (длина слова минус половина длины слова, которая постоянно уменьшается - а значит двигается вправо)
                    {
                        count++;

                        if (count == word.Length / 2)
                        {
                            //// проверка того что происходит и почему код не работает
                            //Write("word " + word + ", ");
                            //Write("l " + l + ", ");
                            //Write("count " + count);
                            //WriteLine();
                            palindromes.Add(word);
                            break;
                        }
                    }

                    //// проверка того что происходит и почему код не работает
                    //Write("word " + word + ", ");
                    //Write("l " + l + ", ");
                    //Write("count " + count);
                    //WriteLine();

                    l--;
                }
                count = 0; // reset счетчик после каждого слова
            }
        }

        return palindromes;
    }

    // other
    public static void DisplayRules()
    {
        Write("Практикум по программированию. Практическая 1, задание 3. Работа с текстом");
        TitleMsg("\nПРАВИЛА\n");

        foreach (string i in rules) // вывести все правила в консоль
        {
            WriteLine("\t" + i);
        }
    }

    public static void ErrorMsg(string err)
    {
        ForegroundColor = ConsoleColor.Red;
        Write(err);
        ForegroundColor = ConsoleColor.Gray;
        Beep(3084, 200);
    }

    public static void ResultMsg(string text)
    {
        ForegroundColor = ConsoleColor.Green;
        Write(text);
        ForegroundColor = ConsoleColor.Gray;
    }

    public static void TitleMsg(string text)
    {
        ForegroundColor = ConsoleColor.Blue;
        Write(text);
        ForegroundColor = ConsoleColor.Gray;
    }

    private static int GetOption()
    {
        string str = ReadLine();

        if (str == "правила")
        {
            DisplayRules();
        }

        int num = 0;
        try
        {
            num = Convert.ToInt32(str);
        }
        catch (Exception ex) // проверка ввода (если не число, то вывести ошибку и попросить ввести данные снова
        {
            if (ex is FormatException)
            {
                ErrorMsg("Введено не число" + tryAgainText + "\n");
                Menu();
            }
        }

        if (num < 0) // проверка ввода (если число не положительное, то вывести ошибку и попросить ввести данные снова
        {
            ErrorMsg("Число должно быть положительным" + tryAgainText + "\n");
            Menu();
        }

        return num;
    }

    static void Menu()
    {
        string[] exercises =
        {
            "Добавить/Сменить текст", // 1
            "Подсчитать кол-во символов в тексте", // a 2
            "Подсчитать кол-во слов в тексте", // a 3
            "Подсчитать кол-во предложений в тексте", // a 4
            "Подсчитать частоту символов в тексте", // б 5
            "Подсчитать среднюю длину слова в тексте", // в 6
            "Определить позицию символа в тексте", // г 7
            "Определить вхождение предложения в текст", // д 8
            "Вывести палиндромы" // е 9
        };


        int option = 1; // выбор пользователя

        while (option != 0)
        {
            TitleMsg("\n\nГЛАВНОЕ МЕНЮ");
            WriteLine("\nВыберите опцию:");
            WriteLine("\t0 - Завершить программу");
            for (int i = 0; i < exercises.Length; i++)
            {
                Write("\t");
                WriteLine(i + 1 + " - " + exercises[i]);
            }
            Write("Ваш выбор: ");
            option = GetOption(); // выбор пользователя меняется и теперь от него зависят дальнейшие действия программы

            if (option != 0)
            { // если выбрана опция 0, то код просто заканчивается
                if (option > 1 && option < 10 && text.Length == 0) // если выбрана опция 2-9 (т.е. опция для работы с текстом) и текста нет, то выводится предупреждение и происходит возврат в меню
                {
                    ErrorMsg("Нет текста для работы. Возможно вы хотели добавить новый текст?\n");
                    Menu();
                }
                else
                {
                    switch (option)
                    {
                        case 1:
                            WriteLine("Предоставьте текст");
                            text = ReadLine();
                            if (text.Length == 0)
                            {
                                ErrorMsg("Текст не может быть пустым");
                                Menu();
                            }
                            else
                            {
                                ResultMsg("Текст принят!");
                            }
                            break;

                        case 2:
                            ResultMsg("Кол-во символов (c пробелами): ");
                            WriteLine(GetNumberOfChars(text));
                            ResultMsg("Кол-во символов (без пробелов): ");
                            Write(GetNumberOfCharsWithoutSpaces(text));
                            break;

                        case 3:
                            ResultMsg("Кол-во слов: ");
                            Write(GetNumberOfWords(text));
                            break;

                        case 4:
                            ResultMsg("Кол-во предложений: ");
                            Write(GetNumberOfSentences(text));
                            break;

                        case 5:
                            ResultMsg("Список символов и частота их появления: \n");
                            Dictionary<char, int> characters = GetCharactersOccurences(text);
                            foreach (var i in characters)
                            {
                                WriteLine($"Символ: {i.Key} Кол-во: {i.Value}");
                            }
                            break;

                        case 6:
                            ResultMsg("Средняя длина слова: ");
                            Write(GetAverageWordlength(text) + "\n");
                            break;

                        case 7:
                            Write("Введите символ для поиска: ");
                            string ch = ReadLine();

                            if (ch.Length == 0)
                            {
                                ErrorMsg("Символ не может быть пустым");
                            }
                            else if (ch.Length > 1)
                            {
                                ErrorMsg("Принимается лишь 1 символ");
                            }
                            else
                            {
                                ResultMsg("Позиции символа в тексте: ");
                                foreach (int c in GetPositionsOfSymbol(text, ch))
                                {
                                    Write(c + 1 + " ");
                                }
                            }
                            break;

                        case 8:
                            Write("Введите предложение для поиска: ");
                            string input = ReadLine();

                            if (input.Length == 0)
                            {
                                ErrorMsg("Символ не может быть пустым");
                            }
                            else
                            {
                                ResultMsg("\"" + input + "\" встречается в тексте " + GetNumberOfPartOccurrences(text, input) + " раз");
                            }
                            break;

                        case 9:
                            ResultMsg("Текст имеет следующие палиндромы: ");
                            if (GetPalindromeWords(text) == null)
                            {
                                ResultMsg("В тексте нет палиндромов");
                            }
                            else
                            {
                                foreach (string c in GetPalindromeWords(text))
                                {
                                    Write(c + " ");
                                }
                            }
                            break;

                        default:
                            ErrorMsg("Такой опции нет" + tryAgainText + "\n");
                            break;
                    }
                }
            }
        }
    }

    static void Main()
    {
        DisplayRules();
        Menu();


        // // // это тестирование функций без ввода пользователя
        //    string str = "Hello! I'm here. How are you? Now, we're going;";
        //    string str2 = "Hello! I'm here. How are you? Now, we're going; Hello! How ara you? How ara you?";

        //    WriteLine("Тексты в наличии");
        //    WriteLine("__________________");
        //    WriteLine("1-ый текст: \n" + str + "\n");
        //    WriteLine("2-ой текст: \n" + str2 + "\n");
        //    WriteLine("__________________");
        //    WriteLine("\n\n");

        //    // a)
        //    WriteLine(exercises[0]);
        //    WriteLine("__________________");
        //    WriteLine(str);
        //    WriteLine(GetNumberOfChars(str));
        //    WriteLine(GetNumberOfCharsWithoutSpaces(str));
        //    WriteLine(GetNumberOfWords(str));
        //    WriteLine(GetNumberOfSentences(str));
        //    WriteLine("__________________");
        //    WriteLine("\n\n");

        //    // b)
        //    WriteLine("__________________");
        //    Dictionary<char, int> charCount = GetNumberOfCharacterOccurences(str);
        //    foreach (var i in charCount)
        //    {
        //        WriteLine($"Символ: {i.Key} Кол-во: {i.Value}");
        //    }
        //    WriteLine("__________________");
        //    WriteLine("\n\n");

        //    // c)
        //    WriteLine("__________________");
        //    WriteLine(GetAverageWordlength(str));
        //    WriteLine("__________________");
        //    WriteLine("\n\n");

        //    // d)
        //    WriteLine("__________________");
        //    List<int> positions = GetPositionsOfSymbol(str, " ");
        //    foreach (var i in positions)
        //    {
        //        Write(i + " ");
        //    }
        //    WriteLine("__________________");
        //    WriteLine("\n\n");

        //    // e)
        //    WriteLine("__________________");
        //    WriteLine("Предложение \"" + "Hello!" + "\" встречается в 1-ом тексте " + GetNumberOfPartOccurrences(str, "Hello!") + " раз");
        //    WriteLine("Предложение \"" + "How ara you?" + "\" встречается во 2-ом тексте " + GetNumberOfPartOccurrences(str2, "How ara you?") + " раз");
        //    WriteLine("__________________");
        //    WriteLine("\n\n");

        //    // f)
        //    WriteLine("__________________");
        //    List<string> palindromes = GetPalindromeWords(str);
        //    if (palindromes == null)
        //    {
        //        WriteLine("Палиндромов нет");
        //    }
        //    else
        //    {
        //        foreach (string i in palindromes)
        //        {
        //            WriteLine(i);
        //        }
        //    }

        //    List<string> palindromes2 = GetPalindromeWords(str2);
        //    if (palindromes == null)
        //    {
        //        WriteLine("Палиндромов нет");
        //    }
        //    else
        //    {
        //        WriteLine("Палиндромы:");
        //        foreach (string i in palindromes2)
        //        {
        //            WriteLine(i);
        //        }
        //    }
        //    WriteLine("__________________");
        //    WriteLine("\n\n");
    }
}