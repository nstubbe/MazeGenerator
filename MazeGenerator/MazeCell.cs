using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeGenerator
{
    public class MazeCell
    {
        private List<MazeCell> _cells;
        public int Row { get; }
        public int Column { get; }

        public bool WestWall { get; set; } = true;
        public bool EastWall { get; set; } = true;
        public bool NorthWall { get; set; } = true;
        public bool SouthWall { get; set; } = true;

        public bool Visited { get; set; }

        public MazeCell(int row, int column, List<MazeCell> cells)
        {
            Row = row;
            Column = column;
            _cells = cells;
        }

        public IEnumerable<MazeCell> GetNeighbours()
        {
            var neighbours = new List<MazeCell>();

            neighbours.AddRange(
                _cells.FindAll(c =>
                    (
                        ((c.Column == Column - 1 || c.Column == Column + 1) && c.Row == Row)
                        || ((c.Row == Row - 1 || c.Row == Row + 1) && c.Column == Column)
                    )
                )
            );

            return neighbours;
        }

        public MazeCell GetRandomNeighbour(bool visited)
        {
            var cells = GetNeighbours().Where(c => c.Visited == visited).ToList();
            var count = cells.Count();

            if (count == 0)
                return null;

            Random random = new Random();
            var rnd = random.Next(0, count);

            return cells[rnd];
        }
    }
}