using UnityEngine;
using System.Collections;

[System.Serializable]
public class HexCoordinates
{

    public int x, y;

    public HexCoordinates(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static HexCoordinates operator +(HexCoordinates a, HexCoordinates b)
    {
        return new HexCoordinates(a.x + b.x, a.y + b.y);
    }
    public static bool operator ==(HexCoordinates a, HexCoordinates b)
    {
        if (a.x == b.x && a.y == b.y)
        {
            return true;
        }
        return false;
    }
    public static bool operator !=(HexCoordinates a, HexCoordinates b)
    {
        if (a == b)
        {
            return false;
        }
        return true;
    }
    public override bool Equals(object obj)
    {
        if (obj is HexCoordinates)
        {
            HexCoordinates coords = (HexCoordinates)obj;
            if (coords.x == x && coords.y == y)
            {
                return true;
            }
        }
        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    public override string ToString()
    {
        return "HexCoordinates X: " + x + " Y: " + y;
    }
}
