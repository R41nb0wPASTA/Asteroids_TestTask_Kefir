using System.Collections.Generic;
using UnityEngine;

public class AsteroidLogic
{
    private ScreenBoundsData screenBoundsData;
    private GameObject asteroid;
    private AsteroidType.AsteroidTypeEnum asteroidType;
    private AsteroidData asteroidData;
    private AsteroidSimulation asteroidSimulation;

    private GameObject smallAsteroid;

    private Vector3 lastKnownPos;
    private Quaternion lastKnownRot;

    private AudioManager audioManager;
    
    private void OnEnable()
    {
        AddToTotalAsteroidsNum();
    }
    
    public AsteroidLogic(ScreenBoundsData screenBoundsData, GameObject asteroid, AsteroidType.AsteroidTypeEnum asteroidType, AsteroidData asteroidData, AsteroidSimulation asteroidSimulation, AudioManager audioManager)
    {
        this.screenBoundsData = screenBoundsData;
        this.asteroid = asteroid;
        this.asteroidType = asteroidType;
        this.asteroidData = asteroidData;
        this.asteroidSimulation = asteroidSimulation;
        this.audioManager = audioManager;
        
        OnEnable();
    }

    public void FixedUpdate(float deltaTime)
    {
        CorrectPosition(deltaTime);
        CorrectPositionWithScreenBounds();

        lastKnownPos = asteroid.transform.localPosition;
        lastKnownRot = asteroid.transform.localRotation;
    }

    private void CorrectPosition(float deltaTime)
    {
        Vector3 newPosition = asteroidSimulation.UpdatePosition(asteroid.transform.localPosition, asteroid.transform.localRotation.eulerAngles, asteroidData, asteroidType, deltaTime);
        asteroid.transform.localPosition = newPosition;
    }
    
    private void CorrectPositionWithScreenBounds()
    {
        if (Mathf.Abs(asteroid.transform.localPosition.x) > screenBoundsData.MaxAsteroidX)
        {
            Vector3 newPosition = asteroidSimulation.Teleport(asteroid.transform.localPosition, true);
            asteroid.transform.localPosition = newPosition;
        }
        
        if (Mathf.Abs(asteroid.transform.localPosition.y) > screenBoundsData.MaxAsteroidY)
        {
            Vector3 newPosition = asteroidSimulation.Teleport(asteroid.transform.localPosition, false);
            asteroid.transform.localPosition = newPosition;
        }
    }

    public List<GameObject> DestroyAndRipApart()
    {
        List<GameObject> smallAsteroids = new List<GameObject>();
        
        RemoveFromTotalAsteroidsNum();
        
        if (asteroidType == AsteroidType.AsteroidTypeEnum.big) 
        { 
            audioManager.PlayBigExsplosionSfx();
            int numOfSmallAsteroids = Random.Range(1, asteroidData.maxNumOfSmallAsteroidsSpawn+1);
            
            for (int i = 0; i < numOfSmallAsteroids; i++) 
            { 
                float zRandomRotation = Random.Range(-20f, 20f); 
                //Vector3 newRotation = new Vector3(asteroid.transform.localRotation.x, asteroid.transform.localRotation.y, asteroid.transform.localRotation.z + zRandomRotation);
                Vector3 newRotation = new Vector3(lastKnownRot.x, lastKnownRot.y, lastKnownRot.z + zRandomRotation);
                
                GameObject asteroidGO = GameObject.Instantiate(asteroidData.smallAsteroid, lastKnownPos, Quaternion.Euler(newRotation));
                smallAsteroids.Add(asteroidGO);
            }
        }
        else
        {
            audioManager.PlaySmallExsplosionSfx();
        }
        
        asteroidSimulation.Destroy(asteroid);

        return smallAsteroids;
    }
    
    private void AddToTotalAsteroidsNum()
    {
        asteroidData.totalNumOfAsteroids++;
    }
    
    private void RemoveFromTotalAsteroidsNum()
    {
        asteroidData.totalNumOfAsteroids--;
    }
}
