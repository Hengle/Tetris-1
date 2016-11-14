using UnityEngine;
using System.Collections;
using com.tinylabproductions.TLPLib.Functional;

public class Grid : MonoBehaviour
{
    // The Grid itself
    public static int w = 10;
    public static int h = 20;
    public static Option<Transform>[,] grid = new Option<Transform>[w, h];

    public static Option<Transform> CellNone = Option<Transform>.None;

    public static Vector2 roundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x),
                           Mathf.Round(v.y));
    }

    public static bool insideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 &&
                (int)pos.x < w &&
                (int)pos.y >= 0);
    }

    public static void deleteRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            foreach (var cell in grid[x,y] ) Destroy(cell.gameObject);
            grid[x, y] = CellNone;
        }
    }

    public static void decreaseRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x, y] != CellNone)
            {
                // Move one towards bottom
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = CellNone;

                // Update Block position
                foreach (var cell in grid[x,y-1]) cell.position += new Vector3(0,-1,0);
            }
        }
    }

    public static void decreaseRowsAbove(int y)
    {
        for (int i = y; i < h; ++i)
            decreaseRow(i);
    }

    public static bool isRowFull(int y)
    {
        for (int x = 0; x < w; ++x)
            if (grid[x, y] == CellNone)
                return false;
        return true;
    }

    public static void deleteFullRows()
    {
        for (int y = 0; y < h; ++y)
        {
            if (isRowFull(y))
            {
                deleteRow(y);
                decreaseRowsAbove(y + 1);
                --y;
            }
        }
    }
}