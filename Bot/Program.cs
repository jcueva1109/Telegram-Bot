using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;

namespace Awesome {
  class Program {
    static ITelegramBotClient botClient;

    static void Main() {
      //Cambiar el token del bot aqui
      botClient = new TelegramBotClient("1073045363:AAGj36rMsGOP_DfyxMUC-ihNeEjCaA_ra3M");

      var me = botClient.GetMeAsync().Result;
      Console.Title = me.Username;
      Console.WriteLine(
        $"Hola! Me llamo {me.FirstName}."
      );

      botClient.OnMessage += Bot_OnMessage;
      botClient.OnCallbackQuery += BotOnCallbackQueryRecieved;
      botClient.OnReceiveError += BotOnReceiveError;
      botClient.StartReceiving();

      Console.WriteLine("Press any key to exit");
      Console.ReadKey();

      botClient.StopReceiving();
    }

    static async void BotOnReceiveError(object sender, ReceiveErrorEventArgs e){
      Console.WriteLine(e.ApiRequestException.Message);
    }
    static async void BotOnCallbackQueryRecieved(object sender, CallbackQueryEventArgs callbackQueryEventArgs){

      var callbackQuery = callbackQueryEventArgs.CallbackQuery;

        await botClient.AnswerCallbackQueryAsync(
          callbackQueryId: callbackQuery.Id,
          text: $"Received {callbackQuery.Data}"
        );
        Console.WriteLine($"Received {callbackQuery.Data}");

        //Si descomento esto, el bot envia un mensaje con Received callbackQuery.Data
        // await botClient.SendTextMessageAsync(
        //   chatId: callbackQuery.Message.Chat.Id,
        //   text: $"Received {callbackQuery.Data}"
        // );

    }
    static async void Bot_OnMessage(object sender, MessageEventArgs e) {
      if (e.Message.Text != null){
        //Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");
        Console.WriteLine($"Received a text message from @{e.Message.Chat.Username}:" + e.Message.Text);

        if (e.Message.Text == "/start"){

          var BotonesHYD = new InlineKeyboardMarkup(new[]{
              new []{   //Fila nueva
                InlineKeyboardButton.WithCallbackData(    //Columna nueva
                  text:"Días de Circulación",
                  callbackData: "circulacion"),
                  InlineKeyboardButton.WithCallbackData(    //Columna nueva
                    text:"Auto Evaluate",
                    callbackData: "AutoEvaluate")
              },
              new []{   //Fila nueva
                InlineKeyboardButton.WithUrl(             //Columna nueva
                  text:"Estadísticas",
                  url: "https://www.google.com/search?q=coronavirus+statistics&oq=coronavirus+st&aqs=chrome.0.0i67j69i57j0l6.6211j0j4&sourceid=chrome&ie=UTF-8"),
                InlineKeyboardButton.WithUrl(
                  text:"Como prevenir?",
                  url:"https://www.who.int/es/emergencies/diseases/novel-coronavirus-2019/advice-for-public")
              }
          });

            await botClient.SendTextMessageAsync(e.Message.Chat.Id,"Bienvenid@ al BOT COVID19HN \n Selecciona el comando a ejecutar",replyMarkup: BotonesHYD);
        }

        if(e.Message.Text == "/usage" || e.Message.Text == "/help" || e.Message.Text == "/"){

          await botClient.SendTextMessageAsync(
            chatId: e.Message.Chat,
            text: "Comandos:\n" + 
                  "/start - ejecuta los comandos COVID 19\n"+
                  "/circulacion - muestra los dias de circulación\n"+
                  "/stats - muestra las estadisticas de COVID 19\n"+
                  "/evaluate - muestra una serie de preguntas sobre los sintomas que padeces\n"+
                  "/recomendaciones - muestra una serie de recomendaciones para prevenir el COVID 19\n"
          );
        }

        //Siguiente pedazo de codigo

      }
    }
  
  }
}