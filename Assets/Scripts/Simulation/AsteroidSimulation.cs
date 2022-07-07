using UnityEngine;

public class AsteroidSimulation : PhysicsSimulation
{
    public Vector3 UpdatePosition(Vector3 currentPosition, Vector3 rotation, AsteroidData asteroidData, AsteroidType.AsteroidTypeEnum asteroidType, float deltaTime)
    {
        float angle = DegreeToRad(rotation.z);
            
        float x = -Mathf.Sin(angle);
        float y = Mathf.Cos(angle);
        Vector3 tmp = new Vector3(x, y, 0f);

        float speed = asteroidData.bigAsteroidSpeed;
        if (asteroidType == AsteroidType.AsteroidTypeEnum.small)
            speed = asteroidData.smallAsteroidSpeed;
        
        return currentPosition + tmp * speed * deltaTime;
    }

    public void Destroy(GameObject asteroid)
    {
        GameObject.Destroy(asteroid.gameObject);
    }

}
