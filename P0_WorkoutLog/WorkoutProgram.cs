using System;
using WorkoutDay;
using Exercises;
using System.Collections;

namespace WorkoutProgram
{

    class Program
    {

        public string? Name { get; set; }
        public List<ProgramDay> program = new List<ProgramDay>(); // A workout program has a 7 day list, each containing exercises for that day

        public Program(string name)//Day[] days, string name
        {
            this.Name = name;
            Day[] days = createProgram();
            for (int i = 0; i < days.Length; i++)
            {
                if (days[i] != Day.Empty) this.program.Add(new ProgramDay(days[i]));
            }

        }

        // this method creates a Workout Program for the user
        public Day[] createProgram()
        {
            Day[] days = new Day[8];
            foreach (var d in days) Console.WriteLine(d);
            int index = 0;

            Console.WriteLine("Choose your workout days\n1. Sun\n2. Mon\n3. Tue\n4. Wed\n5. Thu\n6. Fri\n7. Sat\nEnter -1 to exit or when you've finished adding days.");
            while (true)
            {
                index = getIndex(1, 7);
                if (index == -1) break;
                else if (days[index] != Day.Empty)
                {
                    Console.WriteLine("Day already added. Please choose a different day, or press -1 to complete adding days or to exit.");
                }
                else
                {
                    Console.WriteLine($"{(Day)index} added to workout program.");
                    days[index] = (Day)index; // cast the index value to a day value
                }
            }

            return days;
        }

        public void updateProgram()
        {

            // add new exercise
            // modify exercise information

            Console.WriteLine("Enter the corresponding number for the Day of the workout you want to update. Enter -1 to exit:");
            printDays();
            int day;
            do
            {
                // Console.WriteLine(this.program.Count);
                day = getIndex(1, this.program.Count); // what if they have 0 programDay's


                Console.WriteLine("Enter the number corresponding to the operation you want to perform for {(Day)day}:\n1. Add an exercise\n2. Modify existing exercise\n3. Remove exercise\nEnter -1 to exit");
                int index = getIndex(1, 3);

                if (index == -1)
                {
                    break;
                }
                else if (index == 1)
                {
                    Console.WriteLine();
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


                // if (this.program.Find(e => e.Day == ))

            } while (day != -1);

            Console.WriteLine("Choose a number corresponding the the following options:\n1. Add exercise to ");



        }


        public Exercise createExercise()
        {
            Console.WriteLine("Enter an exercise name:");
            string? name = Console.ReadLine();
            if (name == null) { Console.WriteLine("Can't have an empty name"); return null; }

            Console.WriteLine("For an exercise type, enter \"cardio\" or \"weights\":");
            string? type = Console.ReadLine();
            while (type != "cardio" || type != "weights" || type != "Weights" || type != "Cardio")
            {
                if (type == "exit") return null;
                Console.WriteLine("Please enter valid input. Enter \"exit\" to exit operation");
                type = Console.ReadLine();
            }

            Console.WriteLine("Enter an exercise start time:");
            string? starttime = Console.ReadLine();
            if (starttime == null) { Console.WriteLine("Can't have an empty start time"); return null; }

            Console.WriteLine("Enter an exercise end time:");
            string? endtime = Console.ReadLine();
            if (endtime == null) { Console.WriteLine("Can't have an empty end time"); return null; }

            Console.WriteLine("Enter an exercise difficulty:");
            string? diff = Console.ReadLine();
            if (diff == null) { Console.WriteLine("Can't have an empty difficulty"); return null; }

            Console.WriteLine("Enter date the exercise was performed, one entry at a time, where:\nEntry 1 is MONTH\nEntry 2 is the DAY\nEntry 3 is the YEAR");
            int m = 0, d = 0, y = 0;
            while (!int.TryParse(Console.ReadLine(), out m) || !int.TryParse(Console.ReadLine(), out d) || !int.TryParse(Console.ReadLine(), out y))
            {
                if (m == -1 || d == -1 || y == -1) { Console.WriteLine("exiting date operation"); return null; }
                Console.WriteLine("Please enter valid input. Enter -1 to exit operation");
            }
            if (m == 0 || d == 0 || y == 0) { Console.WriteLine("date can't be invalid"); return null; }
            DateTime date = new DateTime(m, d, y);


            Exercise ex;
            if (type == "weights")
            {
                int set = 0, rep = 0;
                double lb = 0.0;
                Console.WriteLine("Enter number of sets:");
                string? sets;
                while (!int.TryParse(sets = Console.ReadLine(), out set))
                {
                    Console.WriteLine("Please enter valid input. Enter -1 to exit operation");
                }
                if (set == -1) { return null; }

                Console.WriteLine("Enter number of reps:");
                string? reps;
                while (!int.TryParse(reps = Console.ReadLine(), out rep))
                {
                    Console.WriteLine("Please enter valid input. Enter -1 to exit operation");
                }
                if (rep == -1) { return null; }

                Console.WriteLine("Enter number of pounds lifted:");
                string? pounds;
                while (!double.TryParse(pounds = Console.ReadLine(), out lb))
                {
                    Console.WriteLine("Please enter valid input. Enter -1 to exit operation");
                }
                if (lb == -1) { return null; }

                Console.WriteLine("Enter the muscle group worked:");
                string? muscle = Console.ReadLine();
                if (muscle == null) { Console.WriteLine("Can't have an empty muscle"); return null; }

                ex = new Weights(name, starttime, endtime, rep, set, lb, date, diff, muscle);
            }
            else
            {
                Console.WriteLine("Enter number of reps:");
                string? calories;
                int cals = 0;
                while ((calories = Console.ReadLine()) == null || (calories.ToLower().Equals("exit") == false && !int.TryParse(calories, out cals)))
                {
                    Console.WriteLine("Please enter valid input. Enter \"exit\" to exit operation");
                }
                ex = new Cardio(name, date, diff, starttime, endtime, cals);
            }


            return ex;
        }


        // checks whether a program is Empty (has no days attached to it)
        public bool programIsEmpty()
        {
            return this.program.Any();
        }

        // this method is a helper method to get user input
        public int getIndex(int i1, int i2)
        {
            int index;
            do
            {
                // Console.WriteLine("Please enter a number corresponding to a day of the week, or enter -1 to exit.");
                bool valid = int.TryParse(Console.ReadLine(), out index);
                if (!valid)
                {
                    Console.WriteLine("Operation unsuccessful. Try again...");
                    Console.WriteLine($"index is value: {index}");
                }
                else if (index == -1)
                {
                    return -1;
                }
                else if ((index < i1 || index > i2))
                {
                    Console.WriteLine("Input not valid. Try again...");
                }
            } while (index < i1 || index > i2);

            return index;
        }



        public void printDays()
        {
            int i = 1;
            foreach (var e in this.program) Console.WriteLine($"{i++}. {e.Day}");
        }

    }
}