// See https://aka.ms/new-console-template for more information
using ExampleManagerForKs;

PrintYellow("Менеджер учета пищи");
PrinHelp();
HealthyEatingService service;
try
{
    service = new HealthyEatingService();  
}
catch (Exception ex)
{
    PrintRed(ex.Message);
    return; 
}

while (true)
{
    PrintYellow("Введите команду:");

    switch (Console.ReadLine())
    {
        case "1": AddConsole();  break; 
        case "2": DeleteConsole(); break;
        case "3": PrintEatingsConsole(); break; 
        case "4": PrintEatingsForDayConsole();  break;
        case "5": PrintCountCaloriesforDayConsole();  break;
        case "6": PrintCountCaloriesConsole(); break;
        case "7": AllDellConsole(); break;
        case "8": Console.Clear();   break;
        case "help":  PrinHelp(); break;
        default:
            PrintRed("не верная команда"); break;
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
        service.Add(new Eating(0, _description, _calories, new MyDate(_day, _month)));
        PrintYellow("Запись добавллена:");
        PrintEatingsConsole();
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
    PrintYellow("Укажите ид записи которую хотите удалить");
    try
    {
        int id = Convert.ToInt32(Console.ReadLine());
        if (service.Delete(id) == true)
            PrintYellow("Запись  удалена");
        else
            PrintRed("Запись не удалена");
        PrintEatingsConsole();
    }
    catch (Exception ex)
    {
        PrintRed(ex.Message);
    }
}
void PrintEatingsConsole()
{
    PrintYellow("Список записей:");

    if (service.Eatings.Count == 0)
    {
        PrintGreen("нет записей");
        return;
    }


    foreach (Eating eat in service.Eatings)
    {
        PrintGreen(eat.Info());
    }
}
void PrintEatingsForDayConsole()
{
    try
    {
        PrintYellow("Список записей на конкретный день");
        PrintYellow("Укадите день");
        int day = Convert.ToInt32(Console.ReadLine());
        PrintYellow("Укадите месяц");
        int month = Convert.ToInt32(Console.ReadLine());

        PrintYellow("Список записей:");

        List<Eating> eatings = service.GetEatingsForDay(day, month);

        if (eatings.Count == 0)
        {
            PrintGreen("нет записей");
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
        PrintYellow("Сумма колллорий  на конкретный день");
        PrintYellow("Укадите день");
        int day = Convert.ToInt32(Console.ReadLine());
        PrintYellow("Укадите месяц");
        int month = Convert.ToInt32(Console.ReadLine());
        int summa = service.GetCountCalories(day, month);
        PrintGreen($"За день было съедиенно {summa} каллорий");


    }
    catch (Exception ex)
    {
        PrintRed(ex.Message);
    }
}
void PrintCountCaloriesConsole()
{
    PrintYellow("Сумма колллорий за все время");
    int summa = service.GetCountCalories();
    PrintGreen($"Всего было съедиенно {summa} каллорий");
}
void AllDellConsole()
{
    Random rnd = new Random();
    PrintYellow("Удаление всех записей ");
    PrintRed("Внимание - вернуть  записи назад  будет нельзя");

    int  randomCapcha = rnd.Next(100 , 999);
    PrintYellow($"Введите  код подтверждения \"{randomCapcha}\"");
    PrintYellow($"если вы передумали введите \"n\"");

    string capcha = Console.ReadLine();
    if (capcha == "n")
        return;

    if(capcha == randomCapcha.ToString())
    {
        service.Clear();
        PrintYellow("Все записи удалены");
    }
    else
    {
        PrintRed("не верный код, Удаления не будет");
    }

}

void PrinHelp ()
{
    PrintYellow("Список команд:");
    PrintGreen("1 - Добавить запись");
    PrintGreen("2 - Удалить запись");
    PrintGreen("3 - Получить список всех записей");
    PrintGreen("4 - Получить список записей на конкретный день");
    PrintGreen("5 - Получить сумму каллорий конкретный день");
    PrintGreen("6 - Получить сумму всех каллорий");
    PrintGreen("7 - Очистить  все записи");
    PrintGreen("8 - Очистить консоль");
    PrintGreen("help -помошь");
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