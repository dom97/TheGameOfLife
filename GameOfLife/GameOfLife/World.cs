using System;

namespace GameOfLife
{
    public class World
    {
        private Boolean[,] world;
        private Boolean[,] nextGeneration;

        public World(Int32 rows, Int32 columns)
        {
            world = new Boolean[rows, columns];
            nextGeneration = new Boolean[rows, columns];
        }

        public void BringToLife(Int32 row, Int32 column)
        {
            world[row - 1, column - 1] = true;
        }

        public void Tick()
        {
            Array.Copy(world, nextGeneration, world.Length);

            for (var i = 0; i < world.GetLength(0); i++)
            {
                for (var j = 0; j < world.GetLength(1); j++)
                {
                    var neighbors = LiveNeighbors(i, j);

                    if (neighbors < 2 || neighbors > 3)
                        nextGeneration[i, j] = false;
                    else if (neighbors == 3)
                        nextGeneration[i, j] = true;
                }
            }

            Array.Copy(nextGeneration, world, nextGeneration.Length);
        }

        public Boolean IsAlive(Int32 row, Int32 column)
        {
            return world[row - 1, column - 1];
        }

        private Int32 LiveNeighbors(Int32 row, Int32 column)
        {
            var alive = 0;

            for (var i = row - 1; i <= row + 1; i++)
            {
                for (var j = column - 1; j <= column + 1; j++)
                {
                    if (IsOutOfBounds(i, j) || IsSameCell(row, column, i, j))
                        continue;

                    if (world[i, j])
                        alive++;
                }
            }

            return alive;
        }

        private Boolean IsOutOfBounds(Int32 rowIndex, Int32 columnIndex)
        {
            return rowIndex < 0 || rowIndex >= world.GetLength(0) || columnIndex < 0 || columnIndex >= world.GetLength(1);
        }

        private static Boolean IsSameCell(Int32 row, Int32 column, Int32 rowIndex, Int32 columnIndex)
        {
            return rowIndex == row && columnIndex == column;
        }
    }
}
