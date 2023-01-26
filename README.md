<p align="center"><img src="https://user-images.githubusercontent.com/108560661/214959016-7ff0cf7a-04ef-4f36-9d5d-4271fc9b30ca.png" width="250" height="250"></p>

# ReddTok  

## Description
It is very common to see videos of Reddit posts and comments on the social network TikTok. Usually, people
take screenshots of Reddit posts to make those.  
However, I am a little lazy annd I like programming so, I decided to automate the process.
ReddTok is a console application written in **C#** that use **FFMPEG** to perform video related tasks and the .NET 6.0 library.

## Principle
For each comment, a video sequence is produced using the initial background video. The length of this sequence
can be configured with the -msd option to fit the time needed to read a long comment. But, it it is greater than 
the length of the initial background video, -msd is equal to this length instead.  

The Synthesizer class is used to retrieve system voices. For that, this applications will probably run only on 
windows.  

The final video has no music added, sorry. Only the voice reading the text. You can choose your own when posting on tiktok.  
But it would be great to implement it. An empty Music directory has been created in DefaultItems.

## Video format supported
MP4

## Supported languages for the voice
French "FR", English (US) "EN_US", Deutch "DE" and Russian "RU". These must be already installed on the system.
The genders are reffered as MALE and FEMALE

## Command line options
| Option |         Value         |             Description            | Required |  Default  |  
| -------| --------------------- | ---------------------------------- | -------- | --------- |  
| -url   | url to reddit post    | url to reddit post                 | Yes      |     x     |  
| -c     | comments count        | number of comments to be retrievd  | Yes      |     x     |  
| -bg    | background            | background video                   | No       | Bg1.mp4   |  
| -start | start time            | time to start trimming bg video    | No       | 00:00:00  |  
| -msd   | max sequence duration | duration for each sequence         | No       | 00:00:30  |  
| -g     | gender                | gender of the voice                | No       | MALE      |
| -l     | language              | language of the voice              | No       | EN_US     |
| -od    | output directory      | directory for final video          | Yes      |     x     |  
| -of    | output file           | name of final (with .mp4 extension | Yes      |     x     |  

## Use
> - Go to the directory containing the ReddTok.exe with a cd or chdir command
> - Do : ReddTok [ args ]

If one of -bg, -start or -msd is not specified, the generator works with all their default values.  
If one of -g or -l is not specified, the generator works with both of their default values.

#### Example : 
The command
> ReddTok -url https://www.reddit.com/r/AskReddit/comments/wdigt5/redditors_whats_something_the_internet_was_crazy/ -c 3 -od Output -of out.mp4  

produces this video :  

https://user-images.githubusercontent.com/108560661/182268164-6acdbc86-bb62-499d-baaa-cee9121d791a.mp4

After adding a song on tiktok, we have this :  

https://user-images.githubusercontent.com/108560661/182269551-847fc307-444e-4a77-96f4-6e2863b50853.mp4

## Contribution  
You can fork this repository and contribute to the project. Inspire yourself from the coding style and the commit format.  

## Evolution  
To see the evolution of the program, I made a tiktok channel :  
Name : @myreddtok  
Link : tiktok.com/@myreddtok  
