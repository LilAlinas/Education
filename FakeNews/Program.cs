
          using ConsoleApp444;
          using Newtonsoft.Json;
          using System;
          using System.IO;
          using System.Net;
            

namespace ConsoleApp444
{
    class Program
    {
        static void Main(string[] args)
        {
            string key = "4b945e4bcbc74a0a9410cffcf453c62b";
            string response = "";

            Console.WriteLine("Хотите увидеть Российские новости? Да/Нет \n");
            response = Console.ReadLine();
            Console.WriteLine();

            if (response.ToLower().Equals("да"))
            {
                string url = "http://newsapi.org/v2/top-headlines?country=ru&apiKey=" + key;

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();


                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    string result = streamReader.ReadToEnd();
                    Root root = JsonConvert.DeserializeObject<Root>(result);
                    Console.WriteLine("Актуальные новости в России:\n");

                    foreach (Article article in root.Articles)
                    {
                        Console.Write("Источник: " + article.Source.Name + "\nЗаголовок: " + article.Title + "\nОписание: " + article.Description + "\nСсылка на новость в источнике: ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(article.Url);
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                }
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Всего доброго!");
            }
        }  
    }
}

    

