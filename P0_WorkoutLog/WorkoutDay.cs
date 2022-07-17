using System;
using Exercises;

// ProgramDay will be a class with 7 days, where each day holds a collection of Exercises performed that day

namespace WorkoutDay
{

    public enum Day
    {
        Empty,
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
        private List<Exercise> ExercisesToday { get; }

        public ProgramDay(Day day)
        {
            this.Day = day;
            this.ExercisesToday = new List<Exercise>();
        }

        public void removeExercise(string name, string type)
        {
            int num = this.ExercisesToday.RemoveAll(e => e.Name == name && e.Type == type);

            if (num > 0)
            {
                Console.WriteLine("Exercise(s) successfully removed.");
            }
            else
            {
                Console.WriteLine("No matching exercise to be removed.");
            }

        }

        public void addExercise(Exercise e)
        {
            this.ExercisesToday.Add(e);
            Console.WriteLine("Exercise successfully added.");
        }

    }
}