using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;

namespace MazeGenerator
{
    public class MazeGrid
    {
        private readonly int _rows;
        private readonly int _columns;

        public List<MazeCell> Cells { get; private set; }

        public MazeGrid(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
        }

        public void Generate()
        {
            Cells = new List<MazeCell>();
            CreateCells();
            CreateMaze();
        }

        //To represent cells we're using 3 lines in the console. A single cell is represented by the following:
        // ###
        // # #
        // ###
        public void Print()
        {
            for (int r = 0; r < _rows; r++)
            {
                string lineOne = "";
                string lineTwo = "";
                string lineThree = "";

                var wallchar = ((char)178).ToString();

                var rowCells = Cells.Where(c => c.Row == r).OrderBy(c => c.Column);

                foreach (var cell in rowCells)
                {
                    var cellLineOne = "###";
                    var cellLineTwo = "###";
                    var cellLineThree = "###";

                    if (!cell.NorthWall)
                        cellLineOne = "# #";

                    if (!cell.SouthWall)
                        cellLineThree = "# #";

                    if (!cell.EastWall && !cell.WestWall)
                    {
                        cellLineTwo = "   ";
                    }
                    else if (!cell.EastWall)
                    {
                        cellLineTwo = "#  ";
                    }
                    else if (!cell.WestWall)
                    {
                        cellLineTwo = "  #";
                    }
                    else
                    {
                        cellLineTwo = "# #";
                    }

                    lineOne = lineOne + cellLineOne;
                    lineTwo = lineTwo + cellLineTwo;
                    lineThree = lineThree + cellLineThree;
                }

                Console.WriteLine(lineOne);
                Console.WriteLine(lineTwo);
                Console.WriteLine(lineThree);
            }
        }

        private void CreateCells()
        {
            for (int c = 0; c < _columns; c++)
            {
                for (int r = 0; r < _rows; r++)
                {
                    Cells.Add(new MazeCell(r, c, Cells));
                }
            }
        }
        private void CreateMaze()
        {
            MazeCell previousCell = null;
            var cell = CreateEntryOrExit(CellType.Entry);

            do
            {
                RemoveWall(previousCell, cell);

                cell.Visited = true;
                previousCell = cell;
                cell = cell.GetRandomNeighbour(false);
                if (cell == null)
                {
                    cell = FindUnvisitedCell();
                    if (cell == null)
                        break;

                    previousCell = cell.GetRandomNeighbour(true);
                }
   
            } while (true);

            CreateEntryOrExit(CellType.Exit);
        }



        private MazeCell CreateEntryOrExit(CellType type)
        {
            var rowId = GetRandomRowId();
            var columnId = type == CellType.Entry ? 0 : _columns -1;

            var cell = Cells.Single(c => c.Column == columnId && c.Row == rowId);

            if (type == CellType.Entry)
                cell.WestWall = false;

            if (type == CellType.Exit)
                cell.EastWall = false;

            return cell;
        }

        private void RemoveWall(MazeCell previousCell, MazeCell cell)
        {
            if (previousCell == null)
            {
                cell.WestWall = false;
                return;
            }

            var hideNorthWall = previousCell.Row == cell.Row - 1 && previousCell.Column == cell.Column;
            var hideSouthWall = previousCell.Row == cell.Row + 1 && previousCell.Column == cell.Column;

            var hideWestWall = previousCell.Row == cell.Row && previousCell.Column == cell.Column -1;
            var hideEastWall = previousCell.Row == cell.Row && previousCell.Column == cell.Column +1;

            if (hideNorthWall)
            {
                cell.NorthWall = false;
                previousCell.SouthWall = false;
                return;
            }

            if (hideSouthWall)
            {
                cell.SouthWall = false;
                previousCell.NorthWall = false;
                return;
            }

            if (hideEastWall)
            {
                cell.EastWall = false;
                previousCell.WestWall = false;
                return;
            }

            if (hideWestWall)
            {
                cell.WestWall = false;
                previousCell.EastWall = false;
                return;
            }
        }

        private int GetRandomRowId()
        {
            Random random = new Random();
            return random.Next(0, _rows);
        }

        private MazeCell GetCellByCoordinates(int row, int column)
        {
            return Cells.SingleOrDefault(c => c.Row == row && c.Column == column);
        }

        private MazeCell FindUnvisitedCell()
        {
            MazeCell result = null;

            var unvisitedCells = Cells.Where(c => !c.Visited);

            if (!unvisitedCells.Any())
                return null;

            foreach (var cell in unvisitedCells)
            {
                if (cell.GetNeighbours().Any(c => c.Visited))
                {
                    result = cell;
                    break;
                }
            }

            return result;
        }
    }
}
