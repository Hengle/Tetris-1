﻿using UnityEngine;
using System.Collections;
using com.tinylabproductions.TLPLib.Extensions;
using com.tinylabproductions.TLPLib.Functional;

public class Grid : MonoBehaviour
{
    // The Grid itself
    [SerializeField] static int w = 10;
    [SerializeField] static int h = 20;

    [SerializeField] static Option<Transform>[,] grid = new Option<Transform>[w, h];


    public static Vector2 roundVec2(Vector2 v) {
        return new Vector2(Mathf.Round(v.x),
                           Mathf.Round(v.y));
    }

    public static bool insideBorder(Vector2 pos) {
        return ((int)pos.x >= 0 &&
                (int)pos.x < w &&
                (int)pos.y >= 0);
    }

    public static void deleteRow(int y) {
        for (int x = 0; x < w; ++x) {
            foreach (var cell in grid[x,y] ) Destroy(cell.gameObject);
            grid[x, y] = Option<Transform>.None;
        }
    }

    public static void decreaseRow(int y) {
        for (int x = 0; x < w; ++x) {
            if (grid[x, y] != Option<Transform>.None) {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = Option<Transform>.None;

                foreach (var cell in grid[x,y-1]) cell.position += new Vector3(0,-1,0);
            }
        }
    }

    public static void decreaseRowsAbove(int y) {
        for (int i = y; i < h; ++i)
            decreaseRow(i);
    }

    public static bool isRowFull(int y) {
        for (int x = 0; x < w; ++x)
            if (grid[x, y] == Option<Transform>.None)
                return false;
        return true;
    }

    public static void deleteFullRows() {
        for (int y = 0; y < h; ++y) {
            if (isRowFull(y)) {
                deleteRow(y);
                decreaseRowsAbove(y + 1);
                --y;
            }
        }
    }

    public static int amountOfFullRows() {
        var accumulator = 0;

        for (int y = 0; y < h; ++y) {
            if (isRowFull(y)) accumulator++;
        }

        return accumulator;
    }

    public static void clearAll() {
        for (int y = 0; y < h; ++y) {
            deleteRow(y);
        }
    }

    public static void updateGrid(Transform current) {
        for (int y = 0; y < h; ++y)
            for (int x = 0; x < w; ++x)
                foreach (var cell in grid[x, y]) {
                    if (cell.parent == current)
                        grid[x, y] = Option<Transform>.None;
                }

        foreach (Transform child in current) {
            Vector2 v = roundVec2(child.position);
            grid[(int)v.x, (int)v.y] = child.some();
        }
    }

    public static bool isValidGridPos(Transform current) {
        foreach (Transform child in current) {
            Vector2 v = roundVec2(child.position);

            if (!insideBorder(v))
                return false;

            foreach (var cell in grid[(int)v.x, (int)v.y]) {
                if (cell.parent != current) return false;
            }
        }
        return true;
    }
}