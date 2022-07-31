using System.Diagnostics;

namespace ReddTok.Services
{
    /// <summary>
    /// Performs video related singular tasks
    /// </summary>
    public class VideoService
    {
        public string Background = "";
        public string Offset = "";
        public string Duration = "";
        public string OutputDirectory = "";
        public string TempOutputDirectory;
        private string BatchDirectory = "Batch";
        private string tempTextFile = "text.txt";
        private string tempListFile = "list.txt";

        /// <summary>
        /// Configures a new video service with customized settings
        /// </summary>
        /// <param name="background">Background video</param>
        /// <param name="offset">Offset time to trim background video from</param>
        /// <param name="duration">Max duration of a trimmed sequence from background video</param>
        /// <param name="outputDirectory">Directory that contains all the files produced by this video service</param>
        public VideoService(string background, string offset, string duration, string outputDirectory)
        {
            Background = background;
            Offset = offset;
            Duration = duration;
            OutputDirectory = outputDirectory;
            TempOutputDirectory = $@"{outputDirectory}/Temp";
        }

        /// <summary>
        /// Configures a new video service with default settings
        /// </summary>
        /// <param name="outputDirectory">Directory that contains all the files produced by this video service</param>
        public VideoService(string outputDirectory)
        {
            Background = @"DefaultItems/Background/Bg1.mp4";
            Offset = "00:00:00";
            Duration = "00:00:30";
            OutputDirectory = outputDirectory;
            TempOutputDirectory = $@"{outputDirectory}/Temp";
        }

        /// <summary>
        /// Produces a video containing text from original background video
        /// </summary>
        /// <param name="text">The text to be added to the video. The text is centered</param>
        /// <param name="outputFileName">The name of the video produced. Must end with the file extension
        /// Stored in the output directory configured for this video service</param>
        public void GetBackgroundVideo(string text, string outputFileName)
        {
            this.TrimVideo(Background, Offset, Duration, @$"{TempOutputDirectory}/{outputFileName}");
            text = this.FormatText(text, 7);
            File.WriteAllText($@"{TempOutputDirectory}/{tempTextFile}", text);
            this.AddTextToVideo(@$"{TempOutputDirectory}/{outputFileName}", $@"{TempOutputDirectory}/{tempTextFile}", @$"{OutputDirectory}/{outputFileName}");
            File.Delete(@$"{TempOutputDirectory}/{outputFileName}");
            File.Delete($@"{TempOutputDirectory}/{tempTextFile}");
        }

        /// <summary>
        /// Trim/cut a video file
        /// </summary>
        /// <param name="inputvideo">Path to the video te be cut/trimmed</param>
        /// <param name="offset">Trimming start time in the video in the format hh:mm:ss</param>
        /// <param name="duration">The duration of the trimmed sequence from offset in the fromat hh:mm:ss</param>
        /// <param name="outputvideo">The name of the video produced. Must end with the file extension</param>
        private void TrimVideo(string inputvideo, string offset, string duration, string outputvideo)
        {
            if (!Directory.Exists(TempOutputDirectory)) Directory.CreateDirectory(TempOutputDirectory);

            Process p = new();
            Console.WriteLine("Init...");
            ProcessStartInfo pstart = new ProcessStartInfo(@$"{BatchDirectory}/trim.bat", $"{inputvideo} {offset} {duration} {outputvideo}");
            p.StartInfo = pstart;
            Console.WriteLine("Start...");
            p.Start();
            p.WaitForExit();
            Console.WriteLine("End...");
        }

        /// <summary>
        /// Produces a text sequence with a defined number of words by line
        /// </summary>
        /// <param name="text">The text to be formatted</param>
        /// <param name="wordsCountByLine">The number of words by line</param>
        /// <returns>The formatted text</returns>
        private string FormatText(string text, int wordsCountByLine)
        {
            var array = text.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x));
            string result = "";
            for (int index=1; index<=array.Count(); index++)
            {
                result += array.ElementAt<string>(index-1);
                if ((index != 0) && (index % wordsCountByLine == 0)) result += "\n";
                else result += " ";
            }
            return result;
        }

        /// <summary>
        /// Add a text sequence to a video
        /// </summary>
        /// <param name="inputvideo">Path to the video to be modified</param>
        /// <param name="text">The text to be added to inputvideo</param>
        /// <param name="outputvideo">Path to the produced video</param>
        private void AddTextToVideo(string inputvideo, string text, string outputvideo)
        {
            Process p = new();
            Console.WriteLine("Init...");
            ProcessStartInfo pstart = new ProcessStartInfo(@$"{BatchDirectory}/addText.bat", $"{inputvideo} \"{text}\" {outputvideo}");
            p.StartInfo = pstart;
            Console.WriteLine("Start...");
            p.Start();
            p.WaitForExit();
            Console.WriteLine("End...");
        }

        /// <summary>
        /// Remove audio track from a video
        /// </summary>
        /// <param name="inputvideo"></param>
        /// <param name="outputvideo">Name of the produced video. Must end with the file extension</param>
        public void SilentVideo(string inputvideo, string outputvideo)
        {
            Process p = new();
            Console.WriteLine("Init...");
            ProcessStartInfo pstart = new ProcessStartInfo(@$"{BatchDirectory}/silent.bat", @$"{inputvideo} {OutputDirectory}/{outputvideo}");
            p.StartInfo = pstart;
            Console.WriteLine("Start...");
            p.Start();
            p.WaitForExit();
            Console.WriteLine("End...");
        }

        /// <summary>
        /// Merge audio and video files into one video file
        /// </summary>
        /// <param name="index">Both files names end with this index</param>
        public void Merge(int index)
        {
            Process p = new();
            Console.WriteLine("Init...");
            ProcessStartInfo pstart = new ProcessStartInfo(@$"{BatchDirectory}/merge.bat", @$"{OutputDirectory}/generatedbackground{index}.mp4 {OutputDirectory}/generatedaudio{index}.mp3 {OutputDirectory}/generated{index}.mp4");
            p.StartInfo = pstart;
            Console.WriteLine("Start...");
            p.Start();
            p.WaitForExit();
            Console.WriteLine("End...");
        }

        /// <summary>
        /// Concatenate videos
        /// </summary>
        /// <param name="outputvideo"></param>
        public void AssembleVideos(string outputvideo)
        {
            Process p = new();
            Console.WriteLine("Init...");
            ProcessStartInfo pstart = new ProcessStartInfo(@$"{BatchDirectory}\assemble.bat", $"\"{outputvideo}\"");
            p.StartInfo = pstart;
            Console.WriteLine("Start...");
            p.Start();
            p.WaitForExit();
            File.Delete(tempListFile);
            Console.WriteLine("End...");
        }
    }
}
