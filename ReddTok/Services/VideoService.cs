using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
            TempOutputDirectory = $@"{outputDirectory}\Temp";
        }

        public VideoService(string outputDirectory)
        {
            Background = @"DefaultItems\Background\Bg2.mp4";
            Offset = "00:00:00";
            Duration = "00:00:30";
            OutputDirectory = outputDirectory;
            TempOutputDirectory = $@"{outputDirectory}\Temp";
        }

        public void GetBackgroundVideo(string text, string outputFileName)
        {
            this.TrimVideo(Background, Offset, Duration, @$"{TempOutputDirectory}\{outputFileName}");
            this.AddTextToVideo(@$"{TempOutputDirectory}\{outputFileName}", text, @$"{OutputDirectory}\{outputFileName}");
            File.Delete(@$"{TempOutputDirectory}\{outputFileName}");
        }

        private void TrimVideo(string inputvideo, string offset, string duration, string outputvideo)
        {
            if (!Directory.Exists(TempOutputDirectory)) Directory.CreateDirectory(TempOutputDirectory);

            Process p = new();
            Console.WriteLine("Init...");
            ProcessStartInfo pstart = new ProcessStartInfo(@"Bash\trim.bat", $"{inputvideo} {offset} {duration} {outputvideo}");
            p.StartInfo = pstart;
            Console.WriteLine("Start...");
            p.Start();
            p.WaitForExit();
            Console.WriteLine("End...");
        }

        private void AddTextToVideo(string inputvideo, string text, string outputvideo)
        {
            Process p = new();
            Console.WriteLine("Init...");
            ProcessStartInfo pstart = new ProcessStartInfo(@"Bash\addText.bat", $"{inputvideo} \"{text.Replace("\n", " ")}\" {outputvideo}");
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
            ProcessStartInfo pstart = new ProcessStartInfo(@"Bash\silent.bat", @$"{inputvideo} {OutputDirectory}\{outputvideo}");
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
            ProcessStartInfo pstart = new ProcessStartInfo(@"Bash\merge.bat", @$"Speech\generatedbackground{index}.mp4 Speech\generatedaudio{index}.mp3 {OutputDirectory}\generated{index}.mp4");
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
            Console.WriteLine("End...");
        }
    }
}
