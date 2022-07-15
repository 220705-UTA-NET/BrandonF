using System;


namespace Exercises
{
    public enum Difficulty
    {
        Easy,
        Normal,
        Hard
    }

    abstract class Exercise
    {
        public string? Name;
        public string? Type;
        public string StartTime { get; set; } // HH:MM
        public string EndTime { get; set; } // HH:MM
        public Difficulty Difficulty { get; set; } // EASY, NORMAL, HARD
        public DateTime DatePerformed { get; set; } // MM/DD/YYYY

    }

    class Weights : Exercise
    {

        public int Reps { get; set; }
        public int Sets { get; set; }
        public double Pounds { get; set; }



        public Weights(string name, string startTime, string endTime, int reps, int sets, double pounds, DateTime datePerformed, Difficulty difficulty, Duration duration)
        {
            this.Name = name;
            this.Type = "Weights";
            this.Difficulty = difficulty;
            this.DatePerformed = datePerformed;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Reps = reps;
            this.Sets = sets;
            this.Pounds = pounds;
        }

    }

    class Cardio : Exercise
    {

        public double Miles { get; set; }
        public int Steps { get; set; }
        public int CaloriesBurned { get; set; }


        public Cardio(string name, DateTime datePerformed, Difficulty difficulty, string startTime, string endTime, int caloriesBurned, double Miles = 12.23, int Steps = 0)
        {
            this.Name = name;
            this.Type = "Cardio";
            this.Difficulty = difficulty;
            this.DatePerformed = datePerformed;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.CaloriesBurned = caloriesBurned;
        }



    }





}