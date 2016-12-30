using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using com.tinylabproductions.TLPLib.Data;
using com.tinylabproductions.TLPLib.Extensions;
using com.tinylabproductions.TLPLib.Functional;
using JetBrains.Annotations;

//Tetris grid - supervises the positioning of blocks
public class TetrisGrid
{
    Size size;

    Option<Brick>[,] Grid;

    Option<Brick>[] activeGroup;

    Coordinate[,] shape;

    Coordinate spawnPoint;

    BrickPool pool;

    public TetrisGrid(int width, int height, int groupSize, BrickPool pool)
    {
        size = new Size(width + 1, height + 1);

        Grid = new Option<Brick>[size.width, size.height];
        InitArray(Grid);

        activeGroup = new Option<Brick>[groupSize];

        //4 because of 4 rotations
        shape = new Coordinate[groupSize,4];

        spawnPoint = new Coordinate((size.width - 4) / 2, size.height - 2);

        this.pool = pool;
    }

    public bool SetActiveGroup()
    {
        var shape = Shapes.RandomShape();

        if (CanAddActiveGroup(shape))
        {
            var pooledUp = pool.GetPooled(activeGroup.Length);
            var tempList = new List<Brick>();

            foreach (var brick in pooledUp)
            {
                tempList.Add(brick.GetComponent<Brick>());
            }

            if (!AddActiveGroup(tempList.ToArray(), shape))
            {
                return false;
            }

            return true;
        }
        return false;
    }

    public bool MoveActive(Coordinate cord)
    {
        if (IsMoveValid(cord))
        {
            RiseActiveGroup();
            MoveActiveGroup(cord);
            return true;
        }
        return false;
    }

    public bool MoveLeft()
    {
        return MoveActive(new Coordinate(-1, 0));
    }

    public bool MoveRight()
    {
        return MoveActive(new Coordinate(1,0));
    }

    public bool Drop()
    {
        if (!MoveActive(new Coordinate(0, -1)))
        {
            if (!SetActiveGroup())
            {
                return false;
            };
        }
        return true;
    }

    bool MoveActiveGroup(Coordinate to)
    {
        foreach (var member in activeGroup)
        {
            foreach (var brick in member)
            {
                if (!MoveActiveBrick(brick, to + brick.coordinate())) return false;
            }
        }
        return true;
    }

    bool MoveActiveBrick(Brick brick, Coordinate to)
    {
        if (IsPositionValid(to))
        {
            Grid[to.x, to.y] = brick.some();
            brick.transform.position = to.ToVector2();
            return true;
        }
        return false;
    }

    void RiseActiveGroup()
    {
        foreach (var member in activeGroup)
        {
            foreach (var brick in member)
            {
                RiseFromGrid(brick.coordinate());
            }
        }
    }

    void RiseFromGrid(Coordinate cord)
    {
        if (IsPositionValid(cord))
        {
            Grid[cord.x,cord.y] = Option<Brick>.None;
        }
    }


    void Add(Option<Brick> block, Coordinate coordinate)
    {
        if (IsPositionValid(coordinate)) {
            Grid[coordinate.x, coordinate.y] = block;
            foreach (var value in block)
                value.transform.position = new Vector2(coordinate.x, coordinate.y);
        }
    }

    bool AddActiveGroup(Brick[] blocks, Coordinate[,] shape)
    {
        if (blocks.Length == activeGroup.Length && blocks.Length == shape.GetLength(0))
        {
            for (var i = 0; i < blocks.Length; i++)
            {
                activeGroup[i] = blocks[i].some();
                Add(activeGroup[i], GetSpawnZoneCord(shape[0, i]));
            }
            this.shape = shape;
            return true;
        }
        return false;
    }

    bool CanAddActiveGroup(Coordinate[,] shape)
    {
        for (var i = 0; i < shape.GetLength(0); i++)
        {
            if(!IsPositionValid(GetSpawnZoneCord(shape[0, i]))) return false;
        }
        return true;
    }

    bool IsLineFilled(int ind)
    {
        if (ind > 0 && ind < Grid.GetLength(0))
        {
            for (int i = 0; i < Grid.GetLength(1); i++)
            {
                if (!Grid[ind, i].isSome) return false;
            }
            return true;
        }
        return false;
    }

    List<int> GetFilledRows()
    {
        var indexList = new List<int>();

        for (var i = 0; i < Grid.GetLength(0); i++)
        {
            if (IsLineFilled(i)) indexList.Add(i);
        }

        return indexList;
    }

    List<Brick> GetBlocksInRow(int row)
    {
        var brickList = new List<Brick>();

        if (row < 0 || row > Grid.GetLength(0)) return brickList;

        for (int collumn = 0; collumn < Grid.GetLength(1); collumn++)
        {
            foreach (var brick in Grid[row, collumn]) 
                brickList.Add(brick);
        }

        return brickList;
    }

    List<Brick> GetFilledRowBlocks()
    {
        var brickList = new List<Brick>();

        foreach (var rowIndex in GetFilledRows())
        {
            brickList.AddRange(GetBlocksInRow(rowIndex));
        }

        return brickList;
    }

    void ClearGrid()
    {
        var blockList = new List<Brick>();

        for (var i = 0; i < Grid.GetLength(0); i++)
        {
            blockList.AddRange(GetBlocksInRow(i));
        }

        EmptyGridCells();

        ReturnToPool(blockList);
    }

    void EmptyGridCells()
    {
        for (var row = 0; row < Grid.GetLength(0); row++)
        {
            for (var collumn = 0; collumn < Grid.GetLength(1); collumn++)
            {
                Grid[row, collumn] = Option<Brick>.None;
            }
        }
    }

    public void ClearGame()
    {
        ClearGrid();

        foreach (var element in activeGroup)
        {
            foreach (var brick in element)
            {
                pool.PutBack(brick.gameObject);
            }
        }

        activeGroup = new Option<Brick>[activeGroup.Length];
    }

    public void StartGame()
    {
        ClearGame();
        SetActiveGroup();
    }

    void ReturnToPool(List<Brick> bricks)
    {
        foreach (var brick in bricks)
        {
            pool.PutBack(brick.gameObject);
        }
    }

    public bool IsMoveValid(Coordinate cord)
    {
        foreach (var member in activeGroup)
        {
            foreach (var brick in member)
            {
                if (!IsPositionValid(brick.coordinate() + cord)) return false;
            }
        }
        return true;
    }

    public bool IsPositionValid(Coordinate cord) {
        if (IsPositionInGrid(cord))
        {
            return !IsPositionTaken(cord) || IsTakenByActiveGroupMember(cord);
        }
        return false;
    }

    public bool IsTakenByActiveGroupMember(Coordinate cord)
    {
        foreach (var member in activeGroup)
        {
            foreach (var brick in member)
            {
                if (brick.coordinate() == cord)
                    return true;
            }
        }
        return false;
    }

    public bool IsPositionTaken(Coordinate coordinate)
    {
        return Grid[coordinate.x, coordinate.y].isSome;
    }

    public bool IsPositionInGrid(Coordinate coordinate)
    {
        return (coordinate.x >= 0 && coordinate.x < size.width
                && coordinate.y >= 0 && coordinate.y < size.height);
    }

    Coordinate GetSpawnZoneCord(Coordinate coordinate)
    {
        return spawnPoint + coordinate;
    }

    void InitArray(Option<Brick>[,] array)
    {
        for (var i = 0; i < array.GetLength(0); i++)
        {
            for (var j = 0; j < array.GetLength(1); j++)
            {
                Grid[i,j] = Option<Brick>.None;
            }
        }
    }

}
