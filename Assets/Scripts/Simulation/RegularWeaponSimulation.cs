using UnityEngine;

public class RegularWeaponSimulation : PhysicsSimulation
{
    public Vector3 UpdatePosition(Vector3 currentPosition, Vector3 rotation, RegularWeaponData regularWeaponData, float deltaTime)
    {
        float angle = DegreeToRad(rotation.z);
            
        float x = -Mathf.Sin(angle);
        float y = Mathf.Cos(angle);
        Vector3 tmp = new Vector3(x, y, 0f);

        return currentPosition + tmp * regularWeaponData.shotSpeed * deltaTime;
    }

    public void Destroy(GameObject regularWeapon)
    {
        GameObject.Destroy(regularWeapon);
    }
    
}
