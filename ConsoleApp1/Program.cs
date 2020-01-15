using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp1
{
    public class Data  
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Property Prop { get; set; }
        public ICollection<SubData> SubData { get; set; }
        public int Count { get; set; }
        public bool Flag { get; set; }
    } //class Data

    public class Property
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    } // class Property

    public class SubData
    {
        public Guid Id { get; set; }
        public Data Parent { get; set; }
        public decimal Value { get; set; }
    } // class SubData



    class Program
    {
        Object TakeData() { return null; } // Без реализации
        void SortData() { } // Без реализации
        static void Main(string[] args)
        {
            var rnd = new Random(); // не указываем тип данных явно, чтобы показать, что знаю такую возможность

            using (UserContext db = new UserContext())
            {

                // Возможно циклы можно было бы сдалать как-то иначе и красивее, но "самая короткая дорога эта так, которую ты знаешь"
                int pMax = rnd.Next(2,3);

                for (int pCnt=0; pCnt< pMax; pCnt++)
                {
                    Property aProperty = new Property { Id = Guid.NewGuid(), Title = "Property #" + pCnt.ToString() };
                    
                    db.PropertyDS.Add(aProperty);
                    
                    int dMax = rnd.Next(2, 4);
                    for (int dCnt=0; dCnt < dMax; dCnt++)
                    {
                        Data aData = new Data { Id = Guid.NewGuid()
                                                , Name = "Data_" + pCnt.ToString() + "_" + dCnt.ToString()
                                                , Prop = aProperty
                                                , SubData = new List<SubData>()
                                                , Count = 0
                                                , Flag = false
                                            };

                        int sMax = rnd.Next(1, 2);
                        for (int sCnt=0; sCnt < sMax; sCnt++)
                        {
                            aData.SubData.Add(new SubData { Id = Guid.NewGuid(), Parent = aData, Value = (decimal)(rnd.NextDouble() * 100) });
                        } // for sCnt

                        db.DataDS.Add(aData);

                    } // for dCnt

                } // for pCnt

                db.SaveChanges();

                Console.WriteLine("Данные:");
                var propertys = db.PropertyDS;
                foreach (Property aProperty in propertys)
                {
                    Console.WriteLine("{0} - {1}", aProperty.Id, aProperty.Title);

                    foreach (var aData in db.DataDS)
                    {
                        Console.WriteLine("> > > > > {0} - {1}", aData.Id, aData.Name);

                        foreach (var aSubData in db.SubDataDS)
                        {
                            Console.WriteLine("          > > > > > {0} - {1}", aSubData.Id, aSubData.Value);
                        }
                    }
                }
            }
            Console.Read();
        }

    }
}

