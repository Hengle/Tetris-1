using System;
using UnityEngine;
using System.Collections;

public struct Coordinate : IEquatable<Coordinate> {
    public readonly int x, y;

    public Coordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Coordinate(Vector2 point)
    {
        this.x = (int)Math.Floor(point.x);
        this.y = (int)Math.Floor(point.y);
    }

    #region Equality

    public bool Equals(Coordinate other)
    {
        return x == other.x && y == other.y;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        return obj is Coordinate && Equals((Coordinate) obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (x*397) ^ y;
        }
    }

    public static bool operator ==(Coordinate left, Coordinate right) { return left.Equals(right); }
    public static bool operator !=(Coordinate left, Coordinate right) { return !left.Equals(right); }

    public static Coordinate operator +(Coordinate left, Coordinate right) { return new Coordinate(left.x + right.x, left.y + right.y); }
    public static Coordinate operator -(Coordinate left, Coordinate right) { return new Coordinate(left.x - right.x, left.y - right.y); }
    #endregion
}
