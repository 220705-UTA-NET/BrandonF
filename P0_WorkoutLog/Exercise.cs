using System;


namespace Exercises
{
    public enum Difficulty
    {
        Easy,
        Normal,
        Hard
    }

    public struct Duration
    {
        public string StartTime; // HH:MM
        public string EndTime; // HH:MM
    }

    abstract class Exercise
    {

        public string? Name;
        public string? Type;
        public Difficulty Difficulty { get; set; } // EASY, NORMAL, HARD
        public DateTime DatePerformed { get; set; } // MM/DD/YYYY
        public Duration Duration { get; set; } // do I want to return DateTime or String?



    }



}