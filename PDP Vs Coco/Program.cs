using System;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System.Threading;
using System.Media;
using System.IO;
using System.Text;

namespace PDP_vs_Coco
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "PewDiePie Vs Cocomelon";
            Console.CursorVisible = false;
            Console.SetWindowSize(52, 10);

            string root = AppDomain.CurrentDomain.BaseDirectory;
            string txt = "";
            int toggle = 1;

            string musictoggle = root + $"Music.txt";
            if (File.Exists(musictoggle))
            {
                txt = File.ReadAllText(musictoggle);
                toggle = Convert.ToInt32(txt);
            }

            else if (!File.Exists(musictoggle))
            {
                using (FileStream fs = File.Create(musictoggle))
                {
                    // Add some text to file    
                    byte[] author = new UTF8Encoding(true).GetBytes("1");
                    fs.Write(author, 0, author.Length);
                }

                txt = File.ReadAllText(musictoggle);
                toggle = Convert.ToInt32(txt);

            }

            if (toggle == 1)
            {
                SoundPlayer player = new SoundPlayer();
                player.SoundLocation = root + $"\\Coco.wav";
                player.Load();
                player.PlayLooping();
            }

            while (true)
            {

                //Connect to YT API
                YouTubeService yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = "AIzaSyB8WBO0gOu2AofonrB0ySUHpfu2wJ6N51k" });
                string PDPChannelId = "UC-lHJZR3Gqxm24_Vd_AJ5Yw";
                string cmChannelId = "UCbCmjCuTUZos6Inko4u57UQ";
                string mrbChannelId = "UCX6OQ3DkcsbYNE6H8uQQuVA";

                //Get PDP statistics from YT API
                var pdpSubscriptionNumRequest = yt.Channels.List("statistics");
                pdpSubscriptionNumRequest.Id = PDPChannelId;
                var pdpGetSubCount = pdpSubscriptionNumRequest.Execute();

                //Get CM statistics from YT API
                var cmSubscriptionNumRequest = yt.Channels.List("statistics");
                cmSubscriptionNumRequest.Id = cmChannelId;
                var cmGetSubCount = cmSubscriptionNumRequest.Execute();

                //Get Mrb statistics from YT API
                var mrbSubscriptionNumRequest = yt.Channels.List("statistics");
                mrbSubscriptionNumRequest.Id = mrbChannelId;
                var mrbGetSubCount = mrbSubscriptionNumRequest.Execute();

                int pdpSubCountInt = 0;
                int cmSubCountInt = 0;
                int mrbSubCountInt = 0;

                Console.SetCursorPosition(0, 0);

                Console.Write("PewDiePie Subcount: ");

                foreach (var item in pdpGetSubCount.Items)
                {
                    var pdpVarSubCount = item.Statistics.SubscriberCount;
                    pdpSubCountInt = Convert.ToInt32(pdpVarSubCount);
                    int pdpSubCount = pdpSubCountInt / 1000000;
                    Console.WriteLine(pdpSubCount + "m");
                }

                Console.SetCursorPosition(0, 1);

                Console.Write("Cocomelon Subcount: ");

                foreach (var item in cmGetSubCount.Items)
                {
                    var cmVarSubCount = item.Statistics.SubscriberCount;
                    cmSubCountInt = Convert.ToInt32(cmVarSubCount);
                    int cmSubCount = cmSubCountInt / 1000000;
                    Console.WriteLine(cmSubCount + "m");
                }

                Console.SetCursorPosition(0, 2);

                Console.Write("MrBeast Subcount: ");

                foreach (var item in mrbGetSubCount.Items)
                {
                    var mrbVarSubCount = item.Statistics.SubscriberCount;
                    mrbSubCountInt = Convert.ToInt32(mrbVarSubCount);
                    int mrbSubCount = mrbSubCountInt / 1000000;
                    Console.WriteLine(mrbSubCount + "m");
                }

                if (pdpSubCountInt >= cmSubCountInt)
                {
                    Console.WriteLine("The war is still on!");
                }

                if (pdpSubCountInt < cmSubCountInt)
                {
                    Console.WriteLine("The war is lost for now ;-;");
                }

                Console.WriteLine();
                Console.WriteLine("If you would like to turn the music off,\nplease go into Music.txt and change the value to 0");

                Thread.Sleep(1000);
            }
        }
    }
}