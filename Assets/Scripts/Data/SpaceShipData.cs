using UnityEngine;

[CreateAssetMenu(fileName = "SpaceShipData", menuName = "SpaceShipData", order = 1100)]
public class SpaceShipData : ScriptableObject
{
    public GameObject spaceShip;
    
    public bool isAlive;
    
    public float curSpeed;
    public float maxSpeed;

    public float turnSpeed;
    
    public float acceleration;
    public float braking;

    public Vector3 rotation;
    public Vector3 coords;

    public int availableLaserNum;
    public int maxLaserNum;
    public float timeToShootLaser;
    public float timeToRegenLaser;
    public float timeLeftToRegenLaser;

    public float timeBetweenRegularShots;
    
    public GameObject regularWeapon;
    public GameObject laserWeapon;
}
