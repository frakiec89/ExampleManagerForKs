// See https://aka.ms/new-console-template for more information
using ExampleManagerForKs;

PrintYellow("Менеджер учета пищи:"); 
PrinHelp();   // выведем  список  команд 
HealthyEatingService service; // обьявим основной  сервер 
try
{
    service = new HealthyEatingService();   // создадим объект  !! тут  будет  подгрузка  контента 
}
catch (Exception ex ) // если ошибка 
{
    PrintRed(ex.Message); 
    Console.ReadKey();    // задержка 
    return;   //выйдем  из  программы 
}

while (true)  // повторять 
{
    PrintYellow("Введите команду:");   // ждем  команды 
     
    switch (Console.ReadLine())
    {
        case "1": AddConsole();  break;     // добавить 
        case "2": DeleteConsole(); break;   // улалить 
        case "3": PrintEatingsConsole(); break;   // вывести весь  список 
        case "4": PrintEatingsForDayConsole();  break; // вывести  список  по  дням 
        case "5": PrintCountCaloriesforDayConsole();  break;  // вывести  каллории  по  дням 
        case "6": PrintCountCaloriesConsole(); break;         // вывести  суммарные  колллрии  
        case "7": AllDellConsole(); break;                // очистить файл 
        case "8": Console.Clear();   break;               // очистить консоль 
        case "help":  PrinHelp(); break;                // вывести  подсказку 
        default:
            PrintRed("не верная команда!"); break;            // если не верная  комманда 
    }
}

void AddConsole()
{
    PrintYellow("Добавление  новой  записи:");

    PrintYellow("Введите описание:");
    string _description = Console.ReadLine();
    try
    {
        PrintYellow("Введите каллории:");
        int _calories = Convert.ToInt32(Console.ReadLine());

        PrintYellow("Введите день:");
        int _day = Convert.ToInt32(Console.ReadLine());
        PrintYellow("Введите месяц:");
        int _month = Convert.ToInt32(Console.ReadLine());

        // вызываем  метод  Add  у  сервиса  передаем 
                                            // описание
                                            // колории 
                                            // дата :    
                                                    // день 
                                                    // месяц
        service.Add(new Eating( _description, _calories, new MyDate(_day, _month)));

        PrintYellow("Запись добавлена:"); //  если  успешно  выведем  сообщение 
        PrintEatingsConsole(); // выведем полный список для  удобства  
    }
    catch (Exception ex)
    {
        PrintRed(ex.Message);
    }


}
void DeleteConsole()
{
    PrintYellow("Удаление записи:");
    PrintEatingsConsole();
    PrintYellow("Укажите ид записи которую хотите удалить:");
    try
    {
        int id = Convert.ToInt32(Console.ReadLine());
        if (service.Delete(id) == true)    //если истина  то  значит  удалилось
            PrintYellow("Запись удалена!");
        else
            PrintRed("Запись не удалена"!);
        PrintEatingsConsole();           // выведем полный список для  удобства  
    }
    catch (Exception ex)
    {
        PrintRed(ex.Message);
    }
}
void PrintEatingsConsole()
{
    PrintYellow("Список записей:");

    if (service.Eatings.Count == 0) // если пусто
    {
        PrintGreen("нет записей!");
        return;         // выйдем  из  метода 
    }

    foreach (Eating eat in service.Eatings)
    {
        PrintGreen(eat.Info());  // выведим  инфу 
    }
}
void PrintEatingsForDayConsole()
{
    try
    {
        PrintYellow("Список записей на конкретный день:");
        PrintYellow("Укажите день:");
        int day = Convert.ToInt32(Console.ReadLine());
        PrintYellow("Укажите месяц:");
        int month = Convert.ToInt32(Console.ReadLine());

        PrintYellow("Список записей:");

        List<Eating> eatings = service.GetEatingsForDay(day, month);

        if (eatings.Count == 0)
        {
            PrintGreen("нет записей!");
            return;
        }

        foreach (Eating eat in eatings)
        {
            PrintGreen(eat.Info());
        }
    }
    catch (Exception ex)
    {
        PrintRed(ex.Message);
    }
}
void PrintCountCaloriesforDayConsole()
{
    try
    {
        PrintYellow("Сумма колллорий  на конкретный день:");
        PrintYellow("Укажите день:");
        int day = Convert.ToInt32(Console.ReadLine());
        PrintYellow("Укажите месяц:");
        int month = Convert.ToInt32(Console.ReadLine());
        int summa = service.GetCountCalories(day, month);
        PrintGreen($"За день было съедиенно {summa} каллорий.");
    }
    catch (Exception ex)
    {
        PrintRed(ex.Message);
    }
}
void PrintCountCaloriesConsole()
{
    PrintYellow("Сумма колллорий за все время:");
    int summa = service.GetCountCalories();
    PrintGreen($"Всего было съедиенно {summa} каллорий.");
}
void AllDellConsole()
{
    Random rnd = new Random();   
    PrintYellow("Удаление всех записей:");
    PrintRed("Внимание - вернуть  записи назад  будет нельзя!!!");

    int  randomCapcha = rnd.Next(100 , 999);             // случайное  число 
    PrintYellow($"Введите код подтверждения \"{randomCapcha}\".");
    PrintYellow($"Если вы передумали введите \"n\":");

    string capcha = Console.ReadLine();
    if (capcha == "n")   // если  n  то  не  удалять 
        return;          // выйти 

    if(capcha == randomCapcha.ToString()) // если совпало с  капчей 
    {
        service.Clear();   // удалить 
        PrintYellow("Все записи удалены!"); // вывести сообщение 
    }
    else
    {
        PrintRed("не верный код, удаления не будет!"); // если не верная  капча 
    }
}

///Список  команд 
void PrinHelp ()
{
    PrintYellow("Список команд:");
    PrintGreen("1 - Добавить запись,");
    PrintGreen("2 - Удалить запись,");
    PrintGreen("3 - Получить список всех записей,");
    PrintGreen("4 - Получить список записей на конкретный день,");
    PrintGreen("5 - Получить сумму каллорий на конкретный день,");
    PrintGreen("6 - Получить сумму всех каллорий,");
    PrintGreen("7 - Очистить все записи,");
    PrintGreen("8 - Очистить консоль,");
    PrintGreen("help -помошь.");
}


// Цвета  для консоли

void PrintGreen(string message)
{
    ConsoleColor color = Console.ForegroundColor; // запомним цвет
    Console.ForegroundColor = ConsoleColor.Green; // поставим зеленый  
    Console.WriteLine(message);                   // выведем сообщение
    Console.ForegroundColor = color;              // вернем цвет
}
void PrintRed(string message)
{
    ConsoleColor color = Console.ForegroundColor; // запомним цвет
    Console.ForegroundColor = ConsoleColor.Red; // поставим красный 
    Console.WriteLine(message);                   // выведем сообщение
    Console.ForegroundColor = color;              // вернем цвет
}
void PrintYellow(string message)
{
    ConsoleColor color = Console.ForegroundColor; // запомним цвет
    Console.ForegroundColor = ConsoleColor.Yellow; // поставим жёлтый 
    Console.WriteLine(message);                   // выведем сообщение
    Console.ForegroundColor = color;              // вернем цвет
}