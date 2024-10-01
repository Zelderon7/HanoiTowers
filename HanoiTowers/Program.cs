using System;
using System.Collections.Generic;

class Program
{
    // Array to hold the three towers
    static Stack<int>[] towers = new Stack<int>[3];
    // Number of disks
    static int diskCount;

    static readonly ConsoleColor[] diskColors = new ConsoleColor[]
    {
        ConsoleColor.White,
        ConsoleColor.Gray,
        ConsoleColor.Cyan,
        ConsoleColor.DarkCyan,
        ConsoleColor.Blue,
        ConsoleColor.DarkBlue,
        ConsoleColor.Magenta,
        ConsoleColor.DarkMagenta,        
        ConsoleColor.DarkGray,        
    };

    static void Main(string[] args)
    {
        // Read number of disks from user input
        int numberOfDisks = diskCount = int.Parse(Console.ReadLine());

        // Initialize the towers
        for (int i = 0; i < towers.Length; i++)
            towers[i] = new Stack<int>();

        // Push disks onto the first tower
        for (int i = numberOfDisks - 1; i >= 0; i--)
            towers[0].Push(i);

        RedrawTowers();

        // Solve the Tower of Hanoi problem
        SolveTowerOfHanoi(numberOfDisks, 0, 2, 1);

        // Wait for user input before ending the program
        Console.ReadLine();
    }

    // Recursive method to solve the Tower of Hanoi problem
    private static void SolveTowerOfHanoi(int numberOfDisks, int a, int b, int c)
    {
        // Base case: only one disk to move
        if (numberOfDisks == 1)
        {
            // Move disk from source to target
            towers[b].Push(towers[a].Pop());
            RedrawTowers();
            return;
        }

        // Move n-1 disks from source to auxiliary
        SolveTowerOfHanoi(numberOfDisks - 1, a, c, b);

        // Move the nth disk from source to target
        towers[b].Push(towers[a].Pop());
        RedrawTowers();

        // Move n-1 disks from auxiliary to target
        SolveTowerOfHanoi(numberOfDisks - 1, c, b, a);
    }

    // Method to redraw the state of the towers in the console
    private static void RedrawTowers()
    {
        // Create a 2D array to hold the current state of each tower
        int[][] towerStates = new int[3][];
        for (int i = 0; i < 3; i++)
        {
            towerStates[i] = new int[towers[i].Count];
            towers[i].CopyTo(towerStates[i], 0);
        }

        int maxDiskSize = (diskCount - 1) * 2 + 1;

        // Print each level of the towers from top to bottom
        for (int i = diskCount - 1; i >= 0; i--)
        {
            for (int j = 0; j < 3; j++)
            {
                // If there is a disk at this level, print it
                if (towerStates[j].Length - 1 >= i)
                {
                    int diskSize = towerStates[j][towerStates[j].Length - i - 1] * 2 + 1;
                    Console.ForegroundColor = diskColors[towerStates[j][towerStates[j].Length - i - 1]];
                    Console.Write(new string(' ', (maxDiskSize - diskSize) / 2) +
                                   new string('■', diskSize) +
                                   new string(' ', (int)Math.Ceiling(((double)maxDiskSize - diskSize) / 2)));
                }
                else
                {
                    // Print spaces for empty levels
                    Console.Write(new string(' ', maxDiskSize));
                }
                Console.Write(new string(' ', 5)); // Space between towers
            }
            Console.WriteLine(); // New line for the next level
        }

        Console.ResetColor();

        Console.ReadKey(true);

        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
    }
}
