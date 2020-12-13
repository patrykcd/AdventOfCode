using System;
using System.IO;
using System.Linq;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            var instructions = File
                .ReadLines("Input.txt")
                .Select(line => line.Split(' '))
                .Select(x => (name: x[0], value: Convert.ToInt32(x[1])))
                .ToArray();

            var nopsAndJmps = instructions
                .Select((instruction, index) => new {instruction, index})
                .Where(instructionWithIndex =>
                    instructionWithIndex.instruction.name == "nop" ||
                    instructionWithIndex.instruction.name == "jmp")
                .Select(instructionWithIndex => (instructionWithIndex.instruction.name, instructionWithIndex.index))
                .ToArray();

            int position, accumulator;
            bool[] executed;
            Init();
            Exec(instructions.First());
            Console.WriteLine($"Part 1: {accumulator}");

            for (var i = 0; Exec(instructions.First()) == 1; i++)
            {
                FixAtPosition(i);
            }

            Console.WriteLine($"Part 2: {accumulator}");

            void Init()
            {
                position = 0;
                accumulator = 0;
                executed = new bool[instructions.Length];
            }

            int Exec((string name, int value) instruction)
            {
                if (executed[position]) return 1;
                executed[position] = true;

                switch (instruction.name)
                {
                    case "acc":
                        accumulator += instruction.value;
                        position++;
                        break;
                    case "jmp":
                        position += instruction.value;
                        break;
                    case "nop":
                        position++;
                        break;
                }

                return position >= instructions.Length ? 0 : Exec(instructions[position]);
            }

            void FixAtPosition(int index)
            {
                Init();
                if (index > 0) Swap(ref instructions[nopsAndJmps[index - 1].index].name);
                Swap(ref instructions[nopsAndJmps[index].index].name);

                void Swap(ref string instruction)
                {
                    instruction = instruction switch
                    {
                        "jmp" => "nop",
                        "nop" => "jmp"
                    };
                }
            }
        }
    }
}