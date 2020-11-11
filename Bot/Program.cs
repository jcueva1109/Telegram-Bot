using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Awesome {
  class Program {
    static ITelegramBotClient botClient;
    //\n
    static void Main() {
      
      botClient = new TelegramBotClient("1073045363:AAGj36rMsGOP_DfyxMUC-ihNeEjCaA_ra3M");

      var me = botClient.GetMeAsync().Result;
      Console.Title = me.Username;
      Console.WriteLine(
        $"botClient>> Hola! Me llamo {me.FirstName}."
      );

      botClient.OnMessage += Bot_OnMessage;
      botClient.OnCallbackQuery += BotOnCallbackQueryRecieved;
      botClient.OnReceiveError += BotOnReceiveError;
      botClient.StartReceiving();

      Console.WriteLine("Press any key to exit");
      Console.ReadKey();

      botClient.StopReceiving();
    }
    static void BotOnReceiveError(object sender, ReceiveErrorEventArgs e){
      Console.WriteLine($"botClient>> Error recibido: "+e.ApiRequestException.Message);
    }
    public static async void BotOnCallbackQueryRecieved(object sender, CallbackQueryEventArgs callbackQueryEventArgs){

      var callbackQuery = callbackQueryEventArgs.CallbackQuery;

        Console.WriteLine($"botCliente>> El usuario selecciono {callbackQuery.Data}");

        if(callbackQuery.Data == "Circulacion"){

          await DiaCirculacionAsync(callbackQuery);

        }else if(callbackQuery.Data == "AutoEvaluate"){

          CuestionarioSintomas(callbackQuery);

        }else if(callbackQuery.Data == "/help"){

        }
        else{

          await botClient.AnswerCallbackQueryAsync(
            callbackQueryId: callbackQuery.Id,
            text: $"..."
          );

        }

    }
    static async Task DiaCirculacionAsync(CallbackQuery callbackQuery){

      await botClient.SendTextMessageAsync(
        chatId:callbackQuery.Message.Chat.Id,
        text:"¡¡¡Aqui va la informacion de los dias de circulación!!!"
      );

    }
    static async void CuestionarioSintomas(CallbackQuery callbackQuery){

      var rkm = new ReplyKeyboardMarkup();

      rkm.Keyboard = new KeyboardButton[][]{
        new KeyboardButton[]{
          new KeyboardButton("Sí"),
          new KeyboardButton("No")
        }
      };

      await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Tienes fiebre?", replyMarkup: rkm);
      await SegundaPregunta(callbackQuery);

    }
    static async Task SegundaPregunta(CallbackQuery callbackQuery){

      var rkm = new ReplyKeyboardMarkup();

      rkm.Keyboard = new KeyboardButton[][]{
        new KeyboardButton[]{
          new KeyboardButton("Sí"),
          new KeyboardButton("No")
        }
      };

      await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Tienes tos seca?", replyMarkup: rkm);
      await TerceraPregunta(callbackQuery);
    }
    static async Task TerceraPregunta(CallbackQuery callbackQuery){

      var rkm = new ReplyKeyboardMarkup();

      rkm.Keyboard = new KeyboardButton[][]{
        new KeyboardButton[]{
          new KeyboardButton("Sí"),
          new KeyboardButton("No")
        }
      };

      await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Tienes cansancio?", replyMarkup: rkm);
      await CuartaPregunta(callbackQuery);

    }
    static async Task CuartaPregunta(CallbackQuery callbackQuery){

      var rkm = new ReplyKeyboardMarkup();

      rkm.Keyboard = new KeyboardButton[][]{
        new KeyboardButton[]{
          new KeyboardButton("Sí"),
          new KeyboardButton("No")
        }
      };

      await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Tienes dolores y/o molestias?", replyMarkup: rkm);
      
    }
    static async void Bot_OnMessage(object sender, MessageEventArgs e) {
      if (e.Message.Text != null){
        
        Console.WriteLine($"botClient>> Received a text message from @{e.Message.Chat.Username}:" + e.Message.Text);

        if (e.Message.Text == "/start"){

          var BotonesHYD = new InlineKeyboardMarkup(new[]{
              new []{   //Fila nueva
                InlineKeyboardButton.WithCallbackData(    //Columna nueva
                  text:"Días de Circulación",
                  callbackData: "Circulacion"),
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
              },
              new[]{
                InlineKeyboardButton.WithCallbackData(
                  text:"Help",
                  callbackData:"/help")
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

      }
    }
  
  }
}