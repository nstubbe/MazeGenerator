using System;

namespace MazeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var maze = new MazeGrid(10,20);
            maze.Generate();
            maze.Print();
        }
    }
}
