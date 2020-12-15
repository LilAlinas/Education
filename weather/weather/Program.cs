using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace weather
{
    class Program
    {
        static void Main(string[] args)
        {

            string key = "fd7b782958da8a7b5199332d283f87d3";
            string city = "";

            Console.WriteLine("Vvedite nazvanie goroda v kotorom hotite yznat` temperatyry. \n");
            city = Console.ReadLine();
            Console.WriteLine();

            string url = "http://api.openweathermap.org/data/2.5/weather?q=" + city + "&units=metric&appid=" + key;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(result);

                Console.WriteLine("Temperature in " + weatherResponse.Name + ": " + weatherResponse.Main.Temp + "°C \n");

            }
            Console.ReadLine();
        }
    }
}
