using UnityEngine;

public class PhysicsSimulation
{
    public Vector3 Teleport(Vector3 currentPosition, bool isHorizontal)
    {
        if (isHorizontal)
        {
            currentPosition.x -= currentPosition.x * 2;
        }
        else
        {
            currentPosition.y -= currentPosition.y * 2;
        }
        
        return currentPosition;
    }

    protected float DegreeToRad(float angle)
    {
        return (float) (angle * 3.14 / 180);
    }
}
