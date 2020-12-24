using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TelegramBot1
{
    class Program
    {

        private static ITelegramBotClient botClient;
        private static string weatherKey = "fd7b782958da8a7b5199332d283f87d3";
        private static string telegramKey = "1459058796:AAHdiTcRjRl8IZsexyr7RfB6FcvKsWwmx5E";

        static void Main(string[] args)
        {
            botClient = new TelegramBotClient(telegramKey) { Timeout = TimeSpan.FromSeconds(10) };

            var me = botClient.GetMeAsync().Result;
            if(me != null)
            {
                Console.WriteLine("Bot its connecting");
                botClient.OnMessage += Bot_OnMessage;
                botClient.StartReceiving();
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Bot its not connecting");
            }


        }

        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            string city = e?.Message?.Text;
            if (city != null)
            {
                string url = "http://api.openweathermap.org/data/2.5/weather?q=" + city + "&units=metric&appid=" + weatherKey;
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    string weather = streamReader.ReadToEnd();
                    WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(weather);

                    await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: $" В городе { weatherResponse.Name }: температура: { weatherResponse.Main.Temp }°C").ConfigureAwait(false);
                }

            } else 
            {
                return;
            }
        }

    }
}
