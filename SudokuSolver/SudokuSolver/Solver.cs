using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Solver
    {
        public static bool IsPlacementValid(char[][] sudoku, int x, int y, char c)
        {
            // checks for horizontal collisions
            for (int i = 0; i < 9; i++)
            {
                if (sudoku[x][i] == c)
                {
                    return false;
                }
            }

            // checks for vertical collisions
            for (int i = 0; i < 9; i++)
            {
                if (sudoku[i][y] == c)
                {
                    return false;
                }
            }

            // checks for box collisions
            int xBoxPivot = x % 3;
            int yBoxPivot = y % 3;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (sudoku[xBoxPivot + i][yBoxPivot + j] == c)
                    {
                        return false;
                    }
                }
            }

            // all checks have been passed, the placement is valid
            return true;
        }

        public static void SolveSudoku(ref char[][] sudoku)
        {

        }
    }
}
