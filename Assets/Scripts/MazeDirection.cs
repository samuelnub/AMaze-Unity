using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum MazeDirection
{
    North,
    East,
    South,
    West,

    Count
}

public static class MazeDirections
{
    //if you wanna construct a generic container, you gotta init it with a new initt'ed array[] of it lol
    private static List<IntVector2> vectors = new List<IntVector2>(
        new IntVector2[]
        {
        new IntVector2(0, 1),
        new IntVector2(1, 0),
        new IntVector2(0, -1),
        new IntVector2(-1, 0)
        }
        );

    public static MazeDirection RandomValue
    {
        get
        {
            return (MazeDirection)Random.Range(0, (int)MazeDirection.Count);
        }
    }

    //look at the usage of "this", to treat this seemingly static method to behave like an instance's function instead
    public static IntVector2 ToIntVector2(this MazeDirection direction)
    {
        return vectors[(int)direction];
    }
}