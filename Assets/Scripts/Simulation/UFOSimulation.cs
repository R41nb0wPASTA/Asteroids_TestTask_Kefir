using UnityEngine;

public class UFOSimulation : PhysicsSimulation
{
    public Vector3 UpdatePosition(Vector3 currentPosition, Vector3 rotation, UFOData ufoData, float deltaTime)
    {
        float angle = DegreeToRad(rotation.z);
            
        float x = -Mathf.Sin(angle);
        float y = Mathf.Cos(angle);
        Vector3 tmp = new Vector3(x, y, 0f);
        
        return currentPosition + tmp * ufoData.speed * deltaTime;
    }
    
    public void Destroy(GameObject ufo)
    {
        GameObject.Destroy(ufo);
    }
}
