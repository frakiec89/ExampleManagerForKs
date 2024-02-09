using Newtonsoft.Json;
// track
namespace ExampleManagerForKs
{
    internal class HealthyEatingService
    {
        private string _path = "Eatings.json"; // путь  к  файлу 

        /// <summary>
        /// Список записей 
        /// </summary>
        public List<Eating> Eatings { get; set; } 

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <exception cref="Exception"></exception>
        public HealthyEatingService ()
        {
            if (File.Exists(_path) == false) // проверить файл - если  нет  создать 
                File.Create(_path).Close(); // создали файл  если его нет 

            try
            {
                string jsonContent = File.ReadAllText(_path); // читаем  файл
                Eatings = JsonConvert.DeserializeObject<List<Eating>>(jsonContent); // де сериализуем 
                if (Eatings == null) // если  не  получилось - значит  там пусто
                {
                    Eatings = new  List<Eating> (); // создадим  новый лист 
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка чтения из файла");  // иначе  ошибка  при  работе  с файлом 
            }
        }

        /// <summary>
        /// Добавить  запись
        /// </summary>
        /// <param name="eating"></param>
        public void Add  (Eating eating )  
        {
            try
            {
                eating.Id = GetId(); // тут  надо  получить  уникальный ид 
                Eatings.Add(eating); // добавим в  лист 
                Save(); // сохраним в  файл 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// удалить  по  ид 
        /// </summary>
        /// <param name="id"> ид  записис</param>
        /// <returns>да если успешно</returns>
        public bool Delete (int id )
        {
            foreach (Eating eating in Eatings) // перебираем 
            { 
                if (eating.Id == id) // проверяем 
                {
                    Eatings.Remove(eating); // удаляем 
                    Save(); // сохраняем 
                    return true; // выходим 
                }
            }
            return false; // если  никого  не  нашли 
        }




        /// <summary>
        /// получаем  лист  записей на конкретную дату 
        /// </summary>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <returns>новый  список  на конкретную дату </returns>
        public List<Eating> GetEatingsForDay (int day , int month)
        {
            List<Eating> eatings = new List<Eating>(); // выделем  помять 
            foreach (Eating eat in Eatings) // пройдемся  по  листу 
            {
                if(eat.Date.Day == day && eat.Date.Month == month)
                    eatings.Add(eat); // если да  - положем  в  память 
            }
            return eatings; // вернем  все что  нашли 
        }

        /// <summary>
        ///  Сохраням  в  файл 
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void Save()
        {
            try
            {
                string contentJson = JsonConvert.SerializeObject(Eatings); // сериализуем  
                File.WriteAllText(_path, contentJson); // полноситью  перезаписываем  п.с. не очень эфективно по  произволительности 
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка записи ФАЙЛА");
            }
        }

        /// <summary>
        /// получаем  сумму  калорий  за  день 
        /// </summary>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public int GetCountCalories(int day, int month)
        {

            int  summa = 0; // объявим  переменную 
            foreach (Eating eat in Eatings)
            {
                if (eat.Date.Day == day && eat.Date.Month == month)
                    summa += eat.Calories;  // суммируем  если да 
            }
            return summa;
        }

        public int GetCountCalories()
        {

            int summa = 0;
            foreach (Eating eat in Eatings)
            {
               summa += eat.Calories; // суммируем  все 
            }
            return summa;
        }

        public void Clear()
        {
            try
            {
                Eatings.Clear(); // очистим стисок 
                Save();          // перезапишем  файл
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// получает  уникальный ид 
        /// </summary>
        /// <returns></returns>
        private int GetId()
        {
            if (Eatings.Count == 0) // если  список  пустой 
                return 1; // то  начнем  с 1 
            else
                return Eatings.Last().Id + 1; // иначе - последний  элемент  списка  + 1 
        }
    }
}
