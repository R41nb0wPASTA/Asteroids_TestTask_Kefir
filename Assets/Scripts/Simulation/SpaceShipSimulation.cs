using UnityEngine;

public class SpaceShipSimulation : PhysicsSimulation
{
    private Vector3 prevResultingVector = Vector3.zero;
    
    public Vector3 UpdateRotation(Vector3 currentRotation, float speed)
    {
        return new Vector3(0f, 0f, currentRotation.z + speed);
    }
    
    public Vector3 UpdatePosition(Vector3 currentPosition, Vector3 rotation, float speed, bool isSpeedingUp, float deltaTime)
    {
        if (isSpeedingUp)
        {
            float angle = DegreeToRad(rotation.z);
            
            float x = -Mathf.Sin(angle);
            float y = Mathf.Cos(angle);
            prevResultingVector = new Vector3(x, y, 0f);
        }
        
        return currentPosition + prevResultingVector * speed * deltaTime;
    }
    
    public void Destroy(GameObject spaceShip)
    {
        GameObject.Destroy(spaceShip);
    }
}
