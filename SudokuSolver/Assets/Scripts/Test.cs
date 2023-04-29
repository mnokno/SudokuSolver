using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Test : MonoBehaviour
{
    private char[][] data;
    private VisualElement root;
    public UIDocument pad;

    private int cx = -1;
    private int cy = -1;

    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

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

        root.Q<Button>("Solve").clicked += () => Solve();
        root.Q<Button>("Clear").clicked += () => Clear();

        for (int i = 1; i < 10; i++)
        {
            int j = i;
            pad.rootVisualElement.Q<Button>("Btn" + i.ToString()).clicked += () => EnterDigit(j);
        }
        pad.rootVisualElement.Q<Button>("BtnClear").clicked += () => ClearCell();
        pad.rootVisualElement.Q<Button>("BtnBack").clicked += () => HideDigitPad();
    }

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
    
    private void ClearCell()
    {
        if (cx != -1)
        {
            data[cx][cy] = '.';
            pad.rootVisualElement.Q<VisualElement>("root").style.display = DisplayStyle.None;
            UpdateBoard();
        }
    }
    
    private void HideDigitPad()
    {
        pad.rootVisualElement.Q<VisualElement>("root").style.display = DisplayStyle.None;
    }
    
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

    // Update is called once per frame
    void Update()
    {
        
    }

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

    private void Solve()
    {
        SudokuSolver.SolveSudoku(ref data);
        UpdateBoard();
    }

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
