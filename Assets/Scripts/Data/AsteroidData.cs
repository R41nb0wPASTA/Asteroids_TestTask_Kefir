using UnityEngine;

[CreateAssetMenu(fileName = "AsteroidData", menuName = "AsteroidData", order = 1100)]
public class AsteroidData : ScriptableObject
{
    public int totalNumOfAsteroids;
    
    public int maxNumOfBigAsteroidsTotal;
    public int maxNumOfSmallAsteroidsSpawn;
    
    public float bigAsteroidSpeed;
    public float smallAsteroidSpeed;
    
    public GameObject bigAsteroid;
    public GameObject smallAsteroid;
}
