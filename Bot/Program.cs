using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Awesome {
  class Program {
    static ITelegramBotClient botClient;

    static void Main() {
      botClient = new TelegramBotClient("1073045363:AAGj36rMsGOP_DfyxMUC-ihNeEjCaA_ra3M");

      var me = botClient.GetMeAsync().Result;
      Console.WriteLine(
        $"Hola Mundo! Soy el usuario {me.Id} y me llamo {me.FirstName}."
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
        Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");

        if (e.Message.Text == "Hola!" || e.Message.Text == "Hola" || e.Message.Text == "Hey"){

            //General Response
            await botClient.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text:   "Hasta Pronto!"
            );

        }

        if (e.Message.Text == "Adios!" || e.Message.Text == "Adios"){

            //General Response
            await botClient.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text:   "Hasta Pronto!"
            );

        }

        if (e.Message.Text == "Muestrame una imagen"){

            //Send a sticker
            await botClient.SendPhotoAsync(
                chatId:  e.Message.Chat,
                photo: "https://github.com/TelegramBots/book/raw/master/src/docs/photo-ara.jpg",
                caption: "<b>Ara bird</b>. <i>Source</i>: <a href=\"https://pixabay.com\">Pixabay</a>"
            );

        }

        if (e.Message.Text == "Muestrame un sticker"){

            //Send a sticker
            await botClient.SendStickerAsync(
                chatId:  e.Message.Chat,
                sticker: "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-dali.webp"
            );

        }

        if (e.Message.Text == "Cool Giraffe"){

            //Send a sticker
            await botClient.SendStickerAsync(
                chatId:  e.Message.Chat,
                sticker: "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-fred.webp"
            );

        }

        if (e.Message.Text == "Muestrame un video"){

            //Send a video
            await botClient.SendVideoAsync(
                chatId:  e.Message.Chat,
                video: "https://github.com/TelegramBots/book/raw/master/src/docs/video-bulb.mp4"
            );

        }

        if (e.Message.Text == "Muestrame un audio"){ 

            await botClient.SendAudioAsync(
                e.Message.Chat,
                "https://github.com/TelegramBots/book/raw/master/src/docs/audio-guitar.mp3"
                /* ,
                performer: "Joel Thomas Hunger",
                title: "Fun Guitar and Ukulele",
                duration: 91 // in seconds
                */
            );

        }

      }
    }
  }
}