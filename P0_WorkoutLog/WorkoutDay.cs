using System;
using Exercises;

// WorkoutDay will be a class with 7 days, where each day holds a collection of Exercises performed that day

namespace WorkoutDay
{

    public enum Day
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }


    class ProgramDay
    {
        public Day Day { get; set; }
        private List<Exercise> ExercisesToday;

        public ProgramDay(Day day)
        {
            this.Day = day;
            this.ExercisesToday = new List<Exercise>();
        }


        public List<Exercise> GetExercisesToday()
        {
            return this.ExercisesToday;
        }

        // public Exercise removeExercise()

    }
}