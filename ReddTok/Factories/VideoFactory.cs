using ReddTok.Objects;
using ReddTok.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReddTok.Factories
{
    public class VideoFactory
    {
        readonly RedditService redditService = new();
        readonly VoiceService voiceService = new();
        readonly AudioFactory audioFactory = new();
        VideoService? videoService;

        public void GenerateVideo(string url, int? commnentsCount, string? background, string? offset, string? duration, string outputDirectory, string outputFile)
        {
            Console.WriteLine("Started generating video...");

            // Create Post object
            Post post = redditService.GetPostFromReddit(url, commnentsCount);

            // Generate audio && Background
            Voice voice = voiceService.SelectMaleUsEnVoice();
            this.GenerateAudiosFromPost(post, voice);
            this.GenerateBackgroundVideos(post, background, offset, duration);

            // Merge 
            this.MergeBackgroundAndAudio(post, $@"{outputDirectory}\{outputFile}");

            Console.WriteLine(@$"Finished generating video. File at {outputDirectory}\{outputFile}");



        }

        private void GenerateAudiosFromPost(Post post, Voice voice)
        {
            int index = 0;
            audioFactory.GenerateAudioFromText(post.Text, voice, @$"Speech\generatedaudio{index}.mp3");
            foreach (Comment comment in post.Comments) audioFactory.GenerateAudioFromText(comment.Text, voice, @$"Speech\generatedaudio{++index}.mp3");
        }

        public void GenerateBackgroundVideos(Post post, string? inputvideo, string? offset, string? duration)
        {
            int index = 0;
            videoService = ((inputvideo == null) || (offset == null) || (duration == null)) ? new VideoService("Speech") : new VideoService(inputvideo, offset, duration, "Speech");
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
        
    }
}
