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
using ReddTok.Services;
using ReddTok.Factories;

namespace ReddTok
{
    public class MainProgram
    {

        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public static void Main(string[] args)
        {

            RedditService redditService = new();

            string url = "https://www.reddit.com/r/AskReddit/comments/w997iz/whats_a_massive_scandal_controversy_that_people/";

            try
            {
                var post = redditService.GetPostFromReddit(url, 2);

                Console.WriteLine("Subreddit : " + post.Subreddit);
                Console.WriteLine("Author : " + post.Author.Pseudo);
                Console.WriteLine("Question : " + post.Text);

                if (post.Comments.Count != 0) Console.WriteLine("One comment : " + post.Comments[0].Author.Pseudo +
                     " - " + post.Comments[0].Text);

                VoiceService service = new();
                var voice = service.SelectFemaleUsEnVoice();

                AudioFactory factory = new();
                factory.GenerateAudioFromText(post.Text + post.Comments[0].Text, voice, @"Speech/generated.wav");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            // Speak a string.  
            

            

            
            
        }

    }
}
