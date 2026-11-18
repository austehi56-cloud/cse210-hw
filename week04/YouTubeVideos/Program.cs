using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        Video video1 = new Video("C# Object-Oriented Programming Primer", "CodeCraft Academy", 720);
        video1.AddComment(new Comment("Alice Johnson", "This made abstraction click instantly for me. Thanks!"));
        video1.AddComment(new Comment("DevGuy99", "Clear and concise explanation of classes and fields."));
        video1.AddComment(new Comment("SarahConnor", "Looking forward to the next video on inheritance."));
        videos.Add(video1);

        Video video2 = new Video("Top 10 Productivity Tips for Software Engineers", "TechFlow", 450);
        video2.AddComment(new Comment("Mark_Dev", "The Eisenhower Matrix tip changed how I plan my day."));
        video2.AddComment(new Comment("Elena_R", "Great pacing throughout the video. Very practical!"));
        video2.AddComment(new Comment("CodeNewbie", "Bookmarked! Gonna apply these starting tomorrow."));
        video2.AddComment(new Comment("JohnDoe", "Short, sweet, and right to the point."));
        videos.Add(video2);

        Video video3 = new Video("Building Responsive Layouts with CSS Grid", "WebStyle Guide", 940);
        video3.AddComment(new Comment("FrontendNinja", "Finally understand grid-template-columns. Awesome tutorial."));
        video3.AddComment(new Comment("PixelArtist", "Could you do a follow-up on subgrids?"));
        video3.AddComment(new Comment("Jessica_W", "The visual examples were super helpful."));
        videos.Add(video3);

        Video video4 = new Video("Introduction to Python Automation Scripts", "AutomateEverything", 610);
        video4.AddComment(new Comment("ScriptKiddie", "Saved me hours of manual data entry at work!"));
        video4.AddComment(new Comment("Pythonista", "Clean code practices shown here are top notch."));
        video4.AddComment(new Comment("AmaraO", "Can you cover pytest integration next?"));
        videos.Add(video4);

        foreach (Video video in videos)
        {
            Console.WriteLine(new string('=', 60));
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.LengthInSeconds} seconds");
            Console.WriteLine($"Total Comments: {video.GetCommentCount()}");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine("Comments:");

            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"  - {comment.CommenterName}: \"{comment.Text}\"");
            }
            Console.WriteLine();
        }
    }
}

class Video
{
    private readonly List<Comment> _comments = new List<Comment>();

    public string Title { get; private set; }
    public string Author { get; private set; }
    public int LengthInSeconds { get; private set; }

    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
    }

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return _comments.Count;
    }

    public List<Comment> GetComments()
    {
        return _comments;
    }
}

class Comment
{
    public string CommenterName { get; private set; }
    public string Text { get; private set; }

    public Comment(string commenterName, string text)
    {
        CommenterName = commenterName;
        Text = text;
    }
}