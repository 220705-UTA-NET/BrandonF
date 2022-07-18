using System;
using Exercises;
using WorkoutProgram;

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
        public List<Exercise> ExercisesToday { get; }

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

        public void updateExercise(Program p)
        {

            Console.WriteLine($"Enter the number corresponding to the operation you want to perform for {this.Day}:\n1. Add an exercise\n2. Modify existing exercise\n3. Remove exercise\nEnter -1 to exit");
            int index = p.getIndex(1, 3);

            if (index == -1)
            {
                Console.WriteLine("Invalid option selected");
                return;
            }
            else if (index == 1)
            {
                Console.WriteLine("Adding exercise...");
                Exercise ex = createExercise();
                if (ex == null) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"Couldn't add an exercise to {this.Day}"); return; }
                this.ExercisesToday.Add(ex);
                Console.WriteLine("Exercise successfully added.");
            }
            else if (index == 2)
            {

            }
            else if (index == 3)
            {

            }
            else
            {

            }

            // Console.ForegroundColor = ConsoleColor.Red;
            // this.ExercisesToday.Add(e);
            // Console.WriteLine("Exercise successfully added.");
        }

        public Exercise createExercise()
        {

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Enter an exercise name:");
            string? name = Console.ReadLine();
            if (name == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Can't have an empty name"); return null;
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("For an exercise type, enter \"cardio\" or \"weights\":");
            string? type = Console.ReadLine();
            while (type != "cardio" && type != "weights" && type != "Weights" && type != "Cardio")
            {
                if (type == "exit") return null;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Please enter valid input. Enter \"exit\" to exit operation");
                type = Console.ReadLine();
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Enter an exercise start time:");
            string? starttime = Console.ReadLine();
            if (starttime == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Can't have an empty start time"); return null;
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Enter an exercise end time:");
            string? endtime = Console.ReadLine();
            if (endtime == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Can't have an empty end time"); return null;
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Enter an exercise difficulty (Easy, Normal, Hard):");
            string? diff = Console.ReadLine();
            while (diff != null && diff != "easy" && diff != "Easy" && diff != "normal" && diff != "Normal" && diff != "hard" && diff != "Hard")
            {
                if (diff == "exit") return null;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Please enter valid input. Enter \"exit\" to exit operation");
                diff = Console.ReadLine();
            }
            if (diff == null || diff == "exit")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Can't have an empty difficulty"); return null;
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Enter date the exercise was performed, one entry at a time, where:\nEntry 1 is YEAR\nEntry 2 is the MONTH\nEntry 3 is the DAY");
            int m = 0, d = 0, y = 0;
            while (!int.TryParse(Console.ReadLine(), out y) || !int.TryParse(Console.ReadLine(), out m) || !int.TryParse(Console.ReadLine(), out d))
            {
                if (m == -1 || d == -1 || y == -1) { Console.WriteLine("exiting date operation"); return null; }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Please enter valid input. Enter -1 to exit operation");
            }
            if ((m <= 0 || m > 12) || (d <= 0 || d > 31) || y <= 1900 || y > DateTime.Now.Year)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"date can't be invalid: {m}/{d}/{y}"); return null;
            }
            DateTime date = new DateTime(y, m, d);

            Exercise ex;
            if (type == "weights")
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                int set = 0, rep = 0;
                double lb = 0.0;
                Console.WriteLine("Enter number of sets:");
                string? sets;
                while (!int.TryParse(sets = Console.ReadLine(), out set))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Please enter valid input. Enter -1 to exit operation");
                }
                if (set == -1) { return null; }


                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter number of reps:");
                string? reps;
                while (!int.TryParse(reps = Console.ReadLine(), out rep))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Please enter valid input. Enter -1 to exit operation");
                }
                if (rep == -1) { return null; }

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter number of pounds lifted:");
                string? pounds;
                while (!double.TryParse(pounds = Console.ReadLine(), out lb))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Please enter valid input. Enter -1 to exit operation");
                }
                if (lb == -1) { return null; }

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter the muscle group worked:");
                int mg = 0;
                string? muscle;
                while (int.TryParse(muscle = Console.ReadLine(), out mg))
                {
                    if (muscle == null || mg == -1) return null;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Please enter valid input. Enter -1 to exit operation");
                    // muscle = Console.ReadLine();
                }
                if (muscle == null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Can't have an empty muscle"); return null;
                }

                ex = new Weights(name, starttime, endtime, rep, set, lb, date, diff, muscle);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Exercise created!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter number of calories burned:");
                string? calories;
                int cals = 0;
                while ((calories = Console.ReadLine()) == null || (calories.ToLower().Equals("exit") == false && !int.TryParse(calories, out cals)))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Please enter valid input. Enter \"exit\" to exit operation");
                }
                ex = new Cardio(name, date, diff, starttime, endtime, cals);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Exercise created!");
            }

            return ex;
        }

    }
}