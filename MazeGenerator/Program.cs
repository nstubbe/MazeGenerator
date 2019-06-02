using System;

namespace MazeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var maze = new MazeGrid(15,30);
            maze.Generate();
            maze.Print();
        }
    }
}
