using Newtonsoft.Json;

namespace ExampleManagerForKs
{
    internal class HealthyEatingService
    {
        private string _path = "Eatings.json";
        public List<Eating> Eatings { get; set; } 
        public HealthyEatingService ()
        {
            if (File.Exists(_path) == false)
                File.Create(_path).Close();

            try
            {
                string jsonContent = File.ReadAllText(_path);
                Eatings = JsonConvert.DeserializeObject<List<Eating>>(jsonContent);
                if (Eatings == null)
                {
                    Eatings = new  List<Eating> ();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка чтения из файла"); 
            }
        }

        public void Add  (Eating eating )
        {
            try
            {
                eating.Id = GetId();
                Eatings.Add(eating);
                Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete (int id )
        {
            foreach (Eating eating in Eatings)
            { 
                if (eating.Id == id)
                {
                    Eatings.Remove(eating);
                    Save();
                    return true;
                }
            }
            return false;
        }


        public List<Eating> GetEatingsForDay (int day , int month)
        {
            List<Eating> eatings = new List<Eating>();
            foreach (Eating eat in Eatings)
            {
                if(eat.Date.Day == day && eat.Date.Month == month)
                    eatings.Add(eat);
            }
            return eatings;
        }

        private int GetId()
        {
               if (Eatings.Count == 0)
                    return 1;
               else 
                    return Eatings.Last().Id + 1 ;
        }

        private void Save()
        {
            try
            {
                string contentJson = JsonConvert.SerializeObject(Eatings);
                File.WriteAllText(_path, contentJson);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка записи ФАЙЛА");
            }
        }

        public int GetCountCalories(int day, int month)
        {

            int  summa = 0;
            foreach (Eating eat in Eatings)
            {
                if (eat.Date.Day == day && eat.Date.Month == month)
                    summa += eat.Calories;
            }
            return summa;
        }

        public int GetCountCalories()
        {

            int summa = 0;
            foreach (Eating eat in Eatings)
            {
               summa += eat.Calories;
            }
            return summa;
        }

        internal void Clear()
        {
            try
            {
                Eatings.Clear();
                Save();
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }
    }
}
