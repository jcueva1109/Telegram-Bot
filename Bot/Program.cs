using System;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace Awesome {
  class Program {
    static ITelegramBotClient botClient;

    static void Main() {
        //Cambiar el token del bot aqui
      botClient = new TelegramBotClient("1073045363:AAGj36rMsGOP_DfyxMUC-ihNeEjCaA_ra3M");

      var me = botClient.GetMeAsync().Result;
      Console.WriteLine(
        $"Hola! Me llamo {me.FirstName}."
      );

      botClient.OnMessage += Bot_OnMessage;
      botClient.StartReceiving();

      Console.WriteLine("Press any key to exit");
      Console.ReadKey();

      botClient.StopReceiving();
    }

    static async void Bot_OnMessage(object sender, MessageEventArgs e) {
      if (e.Message.Text != null)
      {
        //Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");
        Console.WriteLine($"Received a text message from @{e.Message.Chat.Username}:" + e.Message.Text);

        if (e.Message.Text == "/start"){

            var BotonesHYD = new InlineKeyboardMarkup(new[]{
                new []{
                    InlineKeyboardButton.WithCallbackData(
                      text:"Sintomas",
                      callbackData: " "),
                    InlineKeyboardButton.WithCallbackData(
                        text:"Prevencion",
                        callbackData: " "),
                    InlineKeyboardButton.WithCallbackData(
                        text:"Tratamiento",
                        callbackData: " ")
                },
                new []{
                    InlineKeyboardButton.WithUrl(
                        text:"Ver estadisticas",
                        url: "https://www.google.com/search?q=coronavirus+statistics&oq=coronavirus+st&aqs=chrome.0.0i67j69i57j0l6.6211j0j4&sourceid=chrome&ie=UTF-8")
                }
            });

            await botClient.SendTextMessageAsync(e.Message.Chat.Id,"Bienvenid@ al BOT COVID19HN \n Selecciona el comando a ejecutar",replyMarkup: BotonesHYD);
        
        }

      }
    }
  }
}