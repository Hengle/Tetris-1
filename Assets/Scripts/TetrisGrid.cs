using UnityEngine;
using com.tinylabproductions.TLPLib.Data;
using com.tinylabproductions.TLPLib.Extensions;
using com.tinylabproductions.TLPLib.Functional;

//Tetris grid - supervises the positioning of blocks
public class TetrisGrid
{
    Size size;

    Option<Brick>[,] Grid;

    Option<Brick>[] activeGroup;
    Coordinate[,] shape;

    Coordinate spawnPoint;

    public TetrisGrid(int width, int height, int groupSize)
    {
        size = new Size(width, height);

        Grid = new Option<Brick>[width, height];
        InitArray(Grid);

        activeGroup = new Option<Brick>[groupSize];
        //4 because of 4 rotations
        shape = new Coordinate[groupSize,4];

        spawnPoint = new Coordinate((size.width - 4) / 2, size.height - 2);
    }


    //Adds a single static block to a position on grid
    //On success - returns Option<block>
    //On failure - return Option.None
    public Option<Brick> Add(Option<Brick> block, Coordinate coordinate)
    {
        if (IsPositionValid(coordinate)) {
            Grid[coordinate.x, coordinate.y] = block;
            foreach (var value in block)
                value.transform.position = new Vector2(coordinate.x, coordinate.y);

            return Grid[coordinate.x, coordinate.y];
        }

        return Option<Brick>.None;
    }

    public bool AddActiveGroup(Brick[] blocks, Coordinate[,] shape)
    {
        if (blocks.Length == activeGroup.Length && blocks.Length == shape.GetLength(1))
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

    //None - if there's no activeGroup
    //false - if the group can't be moved to the coordinates
    //true - if the group can be moved
    public Option<bool> CanActiveGroupMoveTo(Coordinate coordinate)
    {
        foreach (var brickOption in activeGroup) {
            if (!brickOption.isSome) return Option<bool>.None;

            foreach (var brick in brickOption)
            {
                if (!IsPositionValid(brick.coordinate() + coordinate))
                    return false.some();
            }
        }
        return true.some();
    }


    public bool IsPositionValid(Coordinate cord) {
        return IsPositionInGrid(cord) && !IsPositionTaken(cord);
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
