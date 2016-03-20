using UnityEngine;

public enum TouchDirection
{
    Left,
    Right,
    Up,
    Down
}

public static class InputHelper
{
    public static TouchDirection GetTouchDirection(Vector2 start, Vector2 end)
    {
        Vector2 dirVec = end - start;

        dirVec.Normalize();

        float xDegree = Mathf.Abs(dirVec.x);


        if (Mathf.Abs(dirVec.y) < 0.5f)
        {
            if (dirVec.x > 0)
                return TouchDirection.Right;
            else
                return TouchDirection.Left;
        }
        else
        {
            if (dirVec.y > 0)
                return TouchDirection.Up;
            else
                return TouchDirection.Down;
        }
    }
}