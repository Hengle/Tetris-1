using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;
using com.tinylabproductions.TLPLib.Data;
using com.tinylabproductions.TLPLib.Extensions;
using com.tinylabproductions.TLPLib.Functional;
using JetBrains.Annotations;

//Tetris grid - supervises the positioning of bricks
public class TetrisGrid
{
    Size size;

    Option<Brick>[,] Grid;

    Option<Brick>[] activeGroup;
    Coordinate[,] activeShape;
    int currentRotation;

    Coordinate spawnPoint;

    BrickPool pool;

    public TetrisGrid(int width, int height, int groupSize, BrickPool pool)
    {
        size = new Size(width + 1, height + 1);

        Grid = new Option<Brick>[size.width, size.height];
        InitArray(Grid);

        activeGroup = new Option<Brick>[groupSize];

        //4 because of 4 rotations
        activeShape = new Coordinate[groupSize,4];

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
            var color = Random.ColorHSV();
            foreach (var brick in pooledUp)
            {
                tempList.Add(brick.GetComponent<Brick>());
                //Color the brick
                brick.GetComponent<SpriteRenderer>().color = color;
            }

            if (!AddActiveGroup(tempList.ToArray(), shape))
            {
                return false;
            }

            return true;
        }
        return false;
    }


    #region Rotation

    public bool RotateActive()
    {
        var futureRotation = currentRotation + 1;
        var rotationCenter = RotationCenter();
        if (futureRotation > activeShape.GetLength(0) - 1) futureRotation = 0;


        if (ActiveGroupCanRotate(futureRotation))
        {
            currentRotation = futureRotation;
            RiseActiveGroup();
            for (var i = 0; i < activeShape.GetLength(1); i++)
            {
                MoveActiveBrick(activeGroup[i].get, rotationCenter + activeShape[futureRotation, i]);
            }
            return true;
        }

        return false;
    }


    Coordinate RotationCenter()
    {
        //yFloor - because we don't want the active group to "rise"
        var yFloor = size.height;
        //xSum - because we want to get the center of the active group
        var xSum = 0;

        foreach (var element in activeGroup)
        {
            foreach (var brick in element)
            {
                xSum += brick.coordinate().x;
                if (yFloor > brick.coordinate().y)
                    yFloor = brick.coordinate().y;
            }
        }

        return new Coordinate(Mathf.Abs(xSum / activeGroup.Length), yFloor);
    }
    public bool ActiveGroupCanRotate(int rotationIndex)
    {
        if (activeShape.GetLength(1) > activeGroup.Length) return false;
        for (var i = 0; i < activeShape.GetLength(1); i++)
        {
            if (!IsPositionValid(RotationCenter() + activeShape[rotationIndex, i])) return false;
        }
        return true;
    }

    #endregion

    #region MoveBricks

    void MoveNormalBrick(Coordinate from, Coordinate to)
    {
        if (!IsPositionTaken(to))
        {
            var temp = Grid[from.x, from.y];
            Grid[from.x, from.y] = Option<Brick>.None;
            Grid[to.x, to.y] = temp;

            foreach (var brick in Grid[to.x, to.y])
            {
                brick.transform.position = to.ToVector2();
            }
        }
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
        return MoveActive(new Coordinate(1, 0));
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

    #endregion

    #region GridRemoval

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
            Grid[cord.x, cord.y] = Option<Brick>.None;
        }
    }

    void ClearRowFromGrid(int rowInd)
    {
        foreach (var brick in GetBricksInRow(rowInd))
        {
            Grid[brick.coordinate().x, brick.coordinate().y] = Option<Brick>.None;
            pool.PutBack(brick.gameObject);
        }
    }

    #endregion

    #region AddSetBricks
    void Add(Option<Brick> brick, Coordinate coordinate)
    {
        if (IsPositionValid(coordinate))
        {
            Grid[coordinate.x, coordinate.y] = brick;
            foreach (var value in brick)
                value.transform.position = new Vector2(coordinate.x, coordinate.y);
        }
    }

    bool AddActiveGroup(Brick[] bricks, Coordinate[,] shape)
    {
        if (bricks.Length == activeGroup.Length && bricks.Length == shape.GetLength(0))
        {
            for (var i = 0; i < bricks.Length; i++)
            {
                activeGroup[i] = bricks[i].some();
                Add(activeGroup[i], GetSpawnZoneCord(shape[0, i]));
            }
            this.activeShape = shape;
            currentRotation = 0;
            return true;
        }
        return false;
    }

    bool CanAddActiveGroup(Coordinate[,] shape)
    {
        for (var i = 0; i < shape.GetLength(0); i++)
        {
            if (!IsPositionValid(GetSpawnZoneCord(shape[0, i]))) return false;
        }
        return true;
    }


    #endregion

    #region ClearLines
    bool IsLineClear(int rowIndex)
    {
        for (var i = 0; i < Grid.GetLength(1); i++)
        {
            if (Grid[rowIndex, i].isSome) return false;
        }
        return true;
    }

    bool IsLineFilled(int ind)
    {
        if (ind >= 0 && ind < Grid.GetLength(1))
        {
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                if (!Grid[i, ind].isSome || activeGroup.Contains(Grid[i, ind])) return false;
            }
            return true;
        }
        return false;
    }

    List<int> GetFilledRows()
    {
        var indexList = new List<int>();

        for (var i = 0; i < Grid.GetLength(1); i++)
        {
            if (IsLineFilled(i)) indexList.Add(i);
        }

        return indexList;
    }

    List<Brick> GetBricksInRow(int row)
    {
        var brickList = new List<Brick>();

        if (row < 0 || row > Grid.GetLength(1)) return brickList;

        for (int collumn = 0; collumn < Grid.GetLength(0); collumn++)
        {
            foreach (var brick in Grid[collumn, row])
                brickList.Add(brick);
        }

        return brickList;
    }

    public List<Brick> GetFilledRowBricks()
    {
        var brickList = new List<Brick>();

        foreach (var rowIndex in GetFilledRows())
        {
            brickList.AddRange(GetBricksInRow(rowIndex));
        }

        return brickList;
    }

    public int GetPoints(int pointMultiplier)
    {
        var counter = 0;
        while (GetFilledRows().Count > 0)
        {
            var lineToClear = GetFilledRows().First();
            counter += size.width;
            ClearRowFromGrid(lineToClear);
            DropLineAbove(lineToClear);
        }
        //foreach (var rowInd in GetFilledRows())
        //{
        //    counter += 1;
        //    ClearRowFromGrid(rowInd);
        //}
        return counter * pointMultiplier;
    }


    void DropLineAbove(int row)
    {
        for (var collumnIndex = 0; collumnIndex < Grid.GetLength(0); collumnIndex++)
        {
            for (var rowIndex = row + 1; rowIndex < Grid.GetLength(1); rowIndex++)
            {
                MoveNormalBrick(new Coordinate(collumnIndex, rowIndex), new Coordinate(collumnIndex, rowIndex - 1));
            }
        }
    }

    void ClearGrid()
    {
        var brickList = new List<Brick>();

        for (var i = 0; i < Grid.GetLength(0); i++)
        {
            brickList.AddRange(GetBricksInRow(i));
        }

        EmptyGridCells();

        ReturnToPool(brickList);
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

    #endregion

    #region StartEndGame


    public void EndGame()
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

    //Clears the grid and active group + sets new active group
    public void StartGame()
    {
        EndGame();
        SetActiveGroup();
    }

    void ReturnToPool(List<Brick> bricks)
    {
        foreach (var brick in bricks)
        {
            pool.PutBack(brick.gameObject);
        }
    }

    #endregion

    #region ValidityChecks

    //Checks if a whole move of active group is possible
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

    //Checks if a single move to a single brick is possible
    public bool IsPositionValid(Coordinate cord)
    {
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

    #endregion

    void InitArray(Option<Brick>[,] array)
    {
        for (var i = 0; i < array.GetLength(0); i++)
        {
            for (var j = 0; j < array.GetLength(1); j++)
            {
                Grid[i, j] = Option<Brick>.None;
            }
        }
    }

    Coordinate GetSpawnZoneCord(Coordinate coordinate)
    {
        return spawnPoint + coordinate;
    }



}
