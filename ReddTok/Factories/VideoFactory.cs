using ReddTok.Objects;
using ReddTok.Services;

namespace ReddTok.Factories
{
    /// <summary>
    /// Main factory : Performs calls to other services and factories to generate final video
    /// </summary>
    public class VideoFactory
    {
        readonly RedditService redditService = new();
        readonly VoiceService voiceService = new();
        readonly AudioFactory audioFactory = new();
        VideoService? videoService;
        string videoserviceOutputDirectory = "Temp";

        /// <summary>
        /// Generates final video
        /// </summary>
        /// <param name="url"></param>
        /// <param name="commentsCount"></param>
        /// <param name="background"></param>
        /// <param name="offset"></param>
        /// <param name="duration"></param>
        /// <param name="gender"></param>
        /// <param name="language"></param>
        /// <param name="outputDirectory"></param>
        /// <param name="outputFile"></param>
        /// <exception cref="NullReferenceException"></exception>
        public void GenerateVideo(string url, int? commentsCount, string? background, string? offset, string? duration, string? gender, string? language, string outputDirectory, string outputFile)
        {
            Console.WriteLine("Started generating video...");

            // Create Post object
            Post post = redditService.GetPostFromReddit(url, commentsCount);

            // Config video service
            this.InitVideoService(background, offset, duration);
            if (videoService == null) throw new NullReferenceException("videoservice is null");

            // Generate audio
            Voice voice = voiceService.SelectVoice(gender, language);
            this.GenerateAudiosFromPost(post, voice);

            // Generate background
            this.GenerateBackgroundVideos(post, background, offset, duration);

            // Merge 
            this.MergeBackgroundAndAudio(post, $@"{outputDirectory}\{outputFile}");

            // Clean up
            this.CleanUp();

            Console.WriteLine(@$"Finished generating video. File at {outputDirectory}\{outputFile}");
        }

        private void GenerateAudiosFromPost(Post post, Voice voice)
        {
            if (videoService == null) throw new NullReferenceException("videoservice is null");

            int index = 0;
            audioFactory.GenerateAudioFromText(post.Text, voice, @$"{videoService.OutputDirectory}/generatedaudio{index}.mp3");
            foreach (Comment comment in post.Comments) audioFactory.GenerateAudioFromText(comment.Text, voice, @$"{videoService.OutputDirectory}/generatedaudio{++index}.mp3");
        }

        private void InitVideoService(string? background, string? offset, string? duration)
        {
            this.videoService = ((background == null) || (offset == null) || (duration == null)) ? new VideoService($"{videoserviceOutputDirectory}") : new VideoService(background, offset, duration, $"{videoserviceOutputDirectory}");
            if (!Directory.Exists(videoService.OutputDirectory)) Directory.CreateDirectory(videoService.OutputDirectory);
        }


        public void GenerateBackgroundVideos(Post post, string? inputvideo, string? offset, string? duration)
        {
            if (videoService == null) throw new NullReferenceException("videoservice is null");

            int index = 0;
            videoService.GetBackgroundVideo(post.Text, $"generatedbackground{index}.mp4");
            foreach (Comment comment in post.Comments) videoService.GetBackgroundVideo(comment.Text, $"generatedbackground{++index}.mp4");
        }

        private void MergeBackgroundAndAudio(Post post, string output)
        {
            if (videoService == null) throw new NullReferenceException("videoservice is null");

            int index = 0;
            videoService.Merge(index); // Title
            foreach (Comment comment in post.Comments) videoService.Merge(++index); // Comments
            videoService.AssembleVideos(output);
        }

        private void CleanUp()
        {
            if (videoService == null) throw new NullReferenceException("videoservice is null");

            Directory.Delete(videoService.OutputDirectory, true);
        }
        
    }
}
