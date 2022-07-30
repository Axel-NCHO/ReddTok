using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReddTok.Services
{
    public class VideoService
    {
        public string Background = "";
        public string Offset = "";
        public string Duration = "";
        public string OutputDirectory = "";
        public string TempOutputDirectory;

        public VideoService(string background, string offset, string duration, string outputDirectory)
        {
            Background = background;
            Offset = offset;
            Duration = duration;
            OutputDirectory = outputDirectory;
            TempOutputDirectory = $@"{outputDirectory}/Temp";
        }

        public VideoService(string outputDirectory)
        {
            Background = @"DefaultItems/Background/Bg1.mp4";
            Offset = "00:00:00";
            Duration = "00:00:30";
            OutputDirectory = outputDirectory;
            TempOutputDirectory = $@"{outputDirectory}/Temp";
        }

        public void GetBackgroundVideo(string text, string outputFileName)
        {
            this.TrimVideo(Background, Offset, Duration, @$"{TempOutputDirectory}/{outputFileName}");
            text = this.FormatText(text);
            File.WriteAllText($@"{TempOutputDirectory}/text.txt", text);
            this.AddTextToVideo(@$"{TempOutputDirectory}\{outputFileName}", $@"{TempOutputDirectory}/text.txt", @$"{OutputDirectory}/{outputFileName}");
            File.Delete(@$"{TempOutputDirectory}\{outputFileName}");
            File.Delete($@"{TempOutputDirectory}/text.txt");
        }

        private void TrimVideo(string inputvideo, string offset, string duration, string outputvideo)
        {
            if (!Directory.Exists(TempOutputDirectory)) Directory.CreateDirectory(TempOutputDirectory);

            Process p = new();
            Console.WriteLine("Init...");
            ProcessStartInfo pstart = new ProcessStartInfo(@"Bash/trim.bat", $"{inputvideo} {offset} {duration} {outputvideo}");
            p.StartInfo = pstart;
            Console.WriteLine("Start...");
            p.Start();
            p.WaitForExit();
            Console.WriteLine("End...");
        }

        private string FormatText(string text)
        {
            var array = text.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x));
            string result = "";
            for (int index=1; index<=array.Count(); index++)
            {
                result += array.ElementAt<string>(index-1);
                if ((index != 0) && (index % 7 == 0)) result += "\n";
                else result += " ";
            }
            return result;
        }

        private void AddTextToVideo(string inputvideo, string text, string outputvideo)
        {
            Process p = new();
            Console.WriteLine("Init...");
            ProcessStartInfo pstart = new ProcessStartInfo(@"Bash/addText.bat", $"{inputvideo} \"{text}\" {outputvideo}");
            p.StartInfo = pstart;
            Console.WriteLine("Start...");
            p.Start();
            p.WaitForExit();
            Console.WriteLine("End...");
        }

        public void SilentVideo(string inputvideo, string outputvideo)
        {
            Process p = new();
            Console.WriteLine("Init...");
            ProcessStartInfo pstart = new ProcessStartInfo(@"Bash/silent.bat", @$"{inputvideo} {OutputDirectory}/{outputvideo}");
            p.StartInfo = pstart;
            Console.WriteLine("Start...");
            p.Start();
            p.WaitForExit();
            Console.WriteLine("End...");
        }

        public void Merge(int index)
        {
            Process p = new();
            Console.WriteLine("Init...");
            ProcessStartInfo pstart = new ProcessStartInfo(@"Bash\merge.bat", @$"Speech\generatedbackground{index}.mp4 Speech\generatedaudio{index}.mp3 {OutputDirectory}/generated{index}.mp4");
            p.StartInfo = pstart;
            Console.WriteLine("Start...");
            p.Start();
            p.WaitForExit();
            Console.WriteLine("End...");
        }

        public void AssembleVideos(string outputvideo)
        {
            Process p = new();
            Console.WriteLine("Init...");
            ProcessStartInfo pstart = new ProcessStartInfo(@"Bash\assemble.bat", $"\"{outputvideo}\"");
            p.StartInfo = pstart;
            Console.WriteLine("Start...");
            p.Start();
            p.WaitForExit();
            File.Delete("list.txt");
            Console.WriteLine("End...");
        }
    }
}
