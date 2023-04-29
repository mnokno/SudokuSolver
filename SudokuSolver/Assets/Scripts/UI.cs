using UnityEngine;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    /// <summary>
    /// Current sudoku board represented using digits from 1..9 and '.' for empty cells.
    /// </summary>
    private char[][] data;
    /// <summary>
    /// Root element of the UI.
    /// </summary>
    private VisualElement root;
    /// <summary>
    /// Number pad for entering digits.
    /// </summary>
    public UIDocument pad;

    /// <summary>
    /// Currently selected x of a cell.
    /// </summary>
    private int cx = -1;
    /// <summary>
    /// Currently selected y of a cell.
    /// </summary>
    private int cy = -1;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Get root element
        root = GetComponent<UIDocument>().rootVisualElement;

        // Initiates new board
        data = new char[9][];
        for (int i = 0; i < 9; i++)
        {
            data[i] = new char[9];
            for (int j = 0; j < 9; j++)
            {
                data[i][j] = '.';
                int x = i;
                int y = j;
                root.Q<Button>("B" + i.ToString() + j.ToString()).RegisterCallback<ClickEvent>((ClickEvent evn) => Clicked(x, y, evn));
            }
        }

        // Assign button listeners to main buttons
        root.Q<Button>("Solve").clicked += () => Solve();
        root.Q<Button>("Clear").clicked += () => Clear();

        // Assign button listers to number pad buttons
        for (int i = 1; i < 10; i++)
        {
            int j = i;
            pad.rootVisualElement.Q<Button>("Btn" + i.ToString()).clicked += () => EnterDigit(j);
        }
        pad.rootVisualElement.Q<Button>("BtnClear").clicked += () => ClearCell();
        pad.rootVisualElement.Q<Button>("BtnBack").clicked += () => HideDigitPad();

        // Puts board in start state
        Clear();
        HideDigitPad();
    }

    /// <summary>
    /// Handles digit pressed on the number pad
    /// </summary>
    /// <param name="i">The pressed digit</param>
    private void EnterDigit(int i)
    {
        if (cx != -1)
        {
            if (data[cx][cy] == i.ToString().ToCharArray()[0])
            {
                data[cx][cy] = '.';
            }
            else
            {
                data[cx][cy] = i.ToString().ToCharArray()[0];
            }
            pad.rootVisualElement.Q<VisualElement>("root").style.display = DisplayStyle.None;
            UpdateBoard();
        }
    }

    /// <summary>
    /// Handles clear cell event
    /// </summary>
    private void ClearCell()
    {
        if (cx != -1)
        {
            data[cx][cy] = '.';
            pad.rootVisualElement.Q<VisualElement>("root").style.display = DisplayStyle.None;
            UpdateBoard();
        }
    }

    /// <summary>
    /// Handles hide digit pad event
    /// </summary>
    private void HideDigitPad()
    {
        pad.rootVisualElement.Q<VisualElement>("root").style.display = DisplayStyle.None;
    }

    /// <summary>
    /// Handles block being clicked
    /// </summary>
    /// <param name="x">X of the block in the grid</param>
    /// <param name="y">Y of the block in the grid</param>
    /// <param name="evn">ClickEvent</param>
    private void Clicked(int x, int y, ClickEvent evn)
    {
        VisualElement padRoot = pad.rootVisualElement.Q<VisualElement>("root");
        Vector3 pos = evn.position;
        pos.x = x > 4 ? pos.x - padRoot.layout.size.x - 50 : pos.x + 50;
        pos.y = y > 4 ? pos.y - padRoot.layout.size.y - 50 : pos.y + 50;
        padRoot.transform.position = pos;
        padRoot.style.display = DisplayStyle.Flex;

        cx = x;
        cy = y;
    }

    /// <summary>
    /// Updated the UI to match the current sudoku board
    /// </summary>
    private void UpdateBoard()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (data[i][j] == '.')
                {
                    root.Q<Button>("B" + i.ToString() + j.ToString()).text = "";
                }
                else
                {
                    root.Q<Button>("B" + i.ToString() + j.ToString()).text = data[i][j].ToString();
                }
            }
        }
    }

    /// <summary>
    /// Handles solve button click
    /// </summary>
    private void Solve()
    {
        if (SudokuSolver.IsBoardValid(ref data))
        {
            SudokuSolver.SolveSudoku(ref data);
            UpdateBoard();
        }
        else
        {
            Debug.Log("Invalid board input!");
        }
    }

    /// <summary>
    /// Clears the board
    /// </summary>
    private void Clear()
    {
        VisualElement padRoot = pad.rootVisualElement.Q<VisualElement>("root");
        padRoot.style.display = DisplayStyle.None;

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                data[i][j] = '.';
            }
        }

        UpdateBoard();
    }
}
