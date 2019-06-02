# MazeGenerator
I wanted to try something different for a change and, having a few hours free, decided to write a maze generator in C# as a fun side project.

The maze is generated as followed:

First we generate every cell in the grid. Each of these cells has 4 walls (north, east, south, west) and a Visited boolean. The visited boolean will make sure we only visit each cell once.

Next, we randomly pick a cell from the first column of cells to be our starting point and open the west wall. 
Now we find an unvisited neighbouring cell and determine the position of that cell relative to our current cell. This way we can determine which walls we need to remove.

If there are no unvisited cells nearby, but there are unvisited cells in our grid, we jump to another unvisited cell that is adjecent to a visited cell and continue from there.

We repeat the proces until every cell is visited.

Lastly, another random cell is picked from the last row and the east wall is openened to create an exit.

Screenshots: 

![Alt text](/MazeGenerator/Screenshot/Maze1.PNG?raw=true "Example maze 1")

![Alt text](/MazeGenerator/Screenshot/Maze2.PNG?raw=true "Example maze 2")
