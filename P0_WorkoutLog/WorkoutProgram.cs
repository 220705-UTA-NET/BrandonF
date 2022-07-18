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
            Console.ForegroundColor = ConsoleColor.Blue;
            Day[] days = new Day[8];
            // foreach (var d in days) Console.WriteLine(d);
            int index = 0;

            Console.WriteLine("Choose your workout days\n[1] Sun\n[2] Mon\n[3] Tue\n[4] Wed\n[5] Thu\n[6] Fri\n[7] Sat\nEnter [-1] to exit or when you've finished adding days.");
            while (true)
            {
                index = getIndex(1, 7);
                if (index == -1) break;
                else if (days[index] != Day.Empty)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Day already added. Please choose a different day, or press [-1] to complete adding days or to exit.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
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

            int day;
            do
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter the corresponding number for the Day of the workout you want to update. Enter [-1] to exit:");

                // Console.WriteLine(this.program.Count);
                day = getDays(); // what if they have 0 programDay's
                if (day == -1) { break; }
                Console.WriteLine($"day chosen: {this.program[day - 1].Day}");

                // ProgramDay pd = new ProgramDay(this.program[day - 1].Day); // create a new program day
                this.program[day - 1].updateExercise(this);
                // this.program.Add(pd);

                foreach (var e in this.program[day - 1].ExercisesToday) { Console.WriteLine(e.Name); }

            } while (day != -1);

            // Console.WriteLine("Choose a number corresponding the the following options:\n1. Add exercise to ");
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

        public int getDays()
        {
            int i = 1;
            int index;
            foreach (var e in this.program) Console.WriteLine($"{i++}. {e.Day}");

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
                else if ((index < 1 || index > this.program.Count))
                {
                    Console.WriteLine("Input not valid. Try again...");
                }
            } while (index < 1 || index > this.program.Count);

            return index;
        }

    }
}