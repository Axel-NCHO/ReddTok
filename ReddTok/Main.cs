using System;
using ReddTok.Factories;

namespace ReddTok
{
    public class MainProgram
    {
        static string url = "";
        static int commentsCount = 1;
        static string? background;
        static string? start;
        static string? maxSeqDuration = "";
        static string? gender;
        static string? language;
        static string outputDir = "";
        static string outputFile = "";

        // Command exemples
        // All customized : reddtok -url some_url -c commentsCount -bg back.mp4 -start 00:00:00 -msd 00:00:30 -g MALE -l EN -od od -of of.mp4
        // With default background : reddtok -url some_url -c commentsCount -od od -of of.mp4

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public static void Main(string[] args)
        {
            EvaluateCommand(args);

            if ((url.Equals("")) || (outputDir.Equals("")) || (outputFile.Equals(""))) {
                Console.WriteLine("Necessary argument missing");
                Environment.Exit(1);
            }

            if (!Directory.Exists(outputDir)) Directory.CreateDirectory(outputDir);

            new VideoFactory().GenerateVideo(url, commentsCount, background, start, maxSeqDuration, gender, language, outputDir, outputFile);
        }


        public static void EvaluateCommand(string[] args)
        {
            int index;
            if (args.Contains<string>("-url"))
            {
                index = Array.IndexOf<string>(args, "-url");
                url = args[index + 1];
            }

            if (args.Contains<string>("-c"))
            {
                index = Array.IndexOf<string>(args, "-c");
                try { commentsCount = int.Parse(args[index + 1]); }
                catch (FormatException e) { Console.WriteLine(e.Message); Environment.Exit(1); }
            }

            if (args.Contains<string>("-bg"))
            {
                index = Array.IndexOf<string>(args, "-bg");
                background = args[index + 1];
            }

            if (args.Contains<string>("-start"))
            {
                index = Array.IndexOf<string>(args, "-start");
                start = args[index + 1];
            }

            if (args.Contains<string>("-msd"))
            {
                index = Array.IndexOf<string>(args, "-msd");
                maxSeqDuration = args[index + 1];
            } 

            if (args.Contains<string>("-g"))
            {
                index = Array.IndexOf<string>(args, "-g");
                gender = args[index + 1];
            }

            if (args.Contains<string>("-l"))
            {
                index = Array.IndexOf<string>(args, "-l");
                language = args[index + 1];
            }

            if (args.Contains<string>("-od"))
            {
                index = Array.IndexOf<string>(args, "-od");
                outputDir = args[index + 1];
            }

            if (args.Contains<string>("-of"))
            {
                index = Array.IndexOf<string>(args, "-of");
                outputFile = args[index + 1];
            }

        }

    }
}
