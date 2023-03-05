using System;
using System.Collections.Generic;

class SudokuSolver
{
    /// <summary>
    /// Returns true if the placement of the given character is valid for a standard sudoku
    /// </summary>
    /// <param name="sudoku">The state of the board</param>
    /// <param name="x">X pos to check</param>
    /// <param name="y">Y pos to check</param>
    /// <param name="c">Character to check for</param>
    /// <returns>Returns true if valid, false otherwise</returns>
    private static bool IsPlacementValid(char[][] sudoku, int x, int y, char c)
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
        int xBoxPivot = (int)Math.Floor(x / 3f);
        int yBoxPivot = (int)Math.Floor(y / 3f);
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (sudoku[xBoxPivot * 3 + i][yBoxPivot * 3 + j] == c)
                {
                    return false;
                }
            }
        }

        // all checks have been passed, the placement is valid
        return true;
    }

    /// <summary>
    /// Solves the sudoku in place
    /// </summary>
    /// <param name="sudoku">Sudoku to solve</param>
    /// <returns>True if solution has found, false if the solution does not exit</returns>
    public static bool SolveSudoku(ref char[][] sudoku)
    {
        Dictionary<int, List<(int, int, List<char>)>> data = new Dictionary<int, List<(int, int, List<char>)>>();
        for (int i = 0; i < 10; i++)
        {
            data.Add(i, new List<(int, int, List<char>)>());
        }

        for (int x = 0; x < 9; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                if (sudoku[x][y] == '.')
                {
                    List<char> legalPlays = new List<char>();
                    for (char c = '1'; c < 58; c++)
                    {                        
                        if (IsPlacementValid(sudoku, x, y, c))
                        {
                            legalPlays.Add(c);
                        }
                    }
                    if (legalPlays.Count == 0)
                    {
                        return false;
                    }
                    data[legalPlays.Count].Add((x, y, legalPlays));
                }
            }
        }

        for (int i = 1; i < 10; i++)
        {
            foreach((int x, int y, List<char> legalPlays) in data[i])
            {
                foreach (char c in legalPlays)
                {
                    sudoku[x][y] = c;
                    if (SolveSudoku(ref sudoku))
                    {
                        return true;
                    }
                    else
                    {
                        sudoku[x][y] = '.';
                    }
                }
                return false;
            }
        }
        return true;
    }
}