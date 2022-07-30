/*using System;
using System.Speech.Synthesis;
using System.Speech.AudioFormat;

namespace SampleSynthesis
{
    class Program
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        static void Main(string[] args)
        {

            // Initialize a new instance of the SpeechSynthesizer.  
            using (SpeechSynthesizer synth = new SpeechSynthesizer())
            {

                // Output information about all of the installed voices.   
                Console.WriteLine("Installed voices -");
                foreach (InstalledVoice voice in synth.GetInstalledVoices())
                {
                    VoiceInfo info = voice.VoiceInfo;
                    string AudioFormats = "";
                    foreach (SpeechAudioFormatInfo fmt in info.SupportedAudioFormats)
                    {
                        AudioFormats += String.Format("{0}\n",
                        fmt.EncodingFormat.ToString());
                    }

                    Console.WriteLine(" Name:          " + info.Name);
                    Console.WriteLine(" Culture:       " + info.Culture);
                    Console.WriteLine(" Age:           " + info.Age);
                    Console.WriteLine(" Gender:        " + info.Gender);
                    Console.WriteLine(" Description:   " + info.Description);
                    Console.WriteLine(" ID:            " + info.Id);
                    Console.WriteLine(" Enabled:       " + voice.Enabled);
                    if (info.SupportedAudioFormats.Count != 0)
                    {
                        Console.WriteLine(" Audio formats: " + AudioFormats);
                    }
                    else
                    {
                        Console.WriteLine(" No supported audio formats found");
                    }

                    string AdditionalInfo = "";
                    foreach (string key in info.AdditionalInfo.Keys)
                    {
                        AdditionalInfo += String.Format("  {0}: {1}\n", key, info.AdditionalInfo[key]);
                    }

                    Console.WriteLine(" Additional Info - " + AdditionalInfo);
                    Console.WriteLine();
                }
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}

*/























































using System;
using ReddTok.Factories;
using ReddTok.Services;

namespace ReddTok
{
    public class MainProgram
    {

        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public static void Main(string[] args)
        {
            /*
            RedditService serv = new();
            var post = serv.GetPostFromReddit("https://www.reddit.com/r/AskReddit/comments/w9xx21/men_of_reddit_what_is_one_piece_of_advice_you/", 2);
            var com1 = post.Comments[0].Text;
            Console.WriteLine(com1);
            var com2 = post.Comments[1].Text;
            Console.WriteLine(com2);*/

            if (!Directory.Exists("Output")) Directory.CreateDirectory("Output");

            string url = "https://www.reddit.com/r/AskReddit/comments/w9xx21/men_of_reddit_what_is_one_piece_of_advice_you/";
            string outDir = "Output";
            string outFile = "genewvideo.mp4";

            new VideoFactory().GenerateVideo(url, 2, null, null, null, outDir, outFile);

            Console.WriteLine("Finished ---------------------- ");
            

            // new VideoFactory().GenerateBackgroundVideo(null, null, null);
        }

    }
}

