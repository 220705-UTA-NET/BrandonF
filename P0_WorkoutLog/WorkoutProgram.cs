using System;
using WorkoutDay;

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

            foreach (var e in program) Console.WriteLine(e.Day);
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
                index = getIndex();
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

        // checks whether a program is Empty (has no days attached to it)
        public bool programIsEmpty()
        {
            return this.program.Any();
        }

        // this method is a helper method to get user input
        public int getIndex()
        {
            int index;
            do
            {
                // Console.WriteLine("Please enter a number corresponding to a day of the week, or enter -1 to exit.");
                bool valid = int.TryParse(Console.ReadLine(), out index);
                if (!valid)
                {
                    Console.WriteLine("Day couldn't be added. Try again...");
                    Console.WriteLine($"index is value: {index}");
                }
                else if (index == -1)
                {
                    return -1;
                }
                else if ((index < 1 || index > 7))
                {
                    Console.WriteLine("Input not valid. Try again...");
                }
            } while (index < 1 || index > 7);

            return index;
        }





    }
}