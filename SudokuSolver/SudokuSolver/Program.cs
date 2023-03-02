﻿char[][] testInput = new char[9][] {new char[9] {'5','3','.','.','7','.','.','.','.'},
                                    new char[9] {'6','.','.','1','9','5','.','.','.'},
                                    new char[9] {'.','9','8','.','.','.','.','6','.'},
                                    new char[9] {'8','.','.','.','6','.','.','.','3'},
                                    new char[9] {'4','.','.','8','.','3','.','.','1'},
                                    new char[9] {'7','.','.','.','2','.','.','.','6'},
                                    new char[9] {'.','6','.','.','.','.','2','8','.'},
                                    new char[9] {'.','.','.','4','1','9','.','.','5'},
                                    new char[9] {'.','.','.','.','8','.','.','7','9'}};

for (int i = 0; i < 9; i++)
{
    System.Console.WriteLine(String.Join(' ', testInput[i]));
}
System.Console.WriteLine("-----------------");
SudokuSolver.Solver.SolveSudoku(ref testInput);
for (int i = 0; i < 9; i++)
{
    System.Console.WriteLine(String.Join(' ', testInput[i]));
}
