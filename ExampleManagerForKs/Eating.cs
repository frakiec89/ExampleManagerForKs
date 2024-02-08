// track
namespace ExampleManagerForKs
{
    public  class Eating
    {
        /// <summary>
        /// Уникальный ид
        /// </summary>
        public int Id { get;  set; } 
        /// <summary>
        /// Описание - например  тарелка  супа
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// колории 
        /// </summary>
        public int Calories { get; private set; }

        /// <summary>
        /// дата  - я придумал  свой класс  так  как  системный   мы пока не  проходили
        /// </summary>
        public MyDate Date { get; private set; }


        public Eating( string description, int calories, MyDate date)
        {
           
            Description = description;

            if(calories>0)  
                Calories = calories;

            Date = date;
        }


        /// <summary>
        /// Удобный вывод
        /// </summary>
        /// <returns></returns>
        public string Info ()
        {
            return  $"ID:{Id} | " +
                    $"Месяц: {Date.Month}, День {Date.Day}\n" +
                    $"{Description}\n" +
                    $"Каллории: {Calories}";
        }

    }

    public class MyDate
    {
        public int  Day { get; private set; }
        public int Month { get; private set; }

        public MyDate (int day, int month)
        {
           if (day > 31 || day<=0 ) 
                throw new Exception("не корректыный день"); 
           if(month>12 || month<=0)
                throw new Exception("не корректыный месяц");

           Month = month;
           Day = day;
        }   
    }
}
