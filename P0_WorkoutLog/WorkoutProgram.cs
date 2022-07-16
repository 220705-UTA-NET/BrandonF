using System;
using WorkoutDay;

namespace WorkoutProgram
{

    class Program
    {

        public List<ProgramDay> program = new List<ProgramDay>(); // A workout program has a 7 day list, each containing exercises for that day


        public Program()
        {
            // a program can be empty while the user does something else
        }

        public Program(Day[] days)
        {
            for (int i = 0; i < days.Length; i++)
            {
                if (days[i] != Day.Empty) this.program.Add(new ProgramDay(days[i]));
            }

            foreach (var e in program) Console.WriteLine(e.Day);
        }


        // 
        public bool programIsEmpty()
        {
            return this.program.Any();
        }



    }
}