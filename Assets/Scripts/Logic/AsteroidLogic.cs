using UnityEngine;

public class AsteroidLogic
{
    private ILocalPositionAdapter localPositionAdapter;
    private ILocalRotationAdapter localRotationAdapter;
    private ScreenBoundsData screenBoundsData;
    private GameObject asteroid;
    private AsteroidType.AsteroidTypeEnum asteroidType;
    private AsteroidData asteroidData;
    private AsteroidSimulation asteroidSimulation;

    private GameObject smallAsteroid;
    private GameObject audioManager;

    private void OnEnable()
    {
        AddToTotalAsteroidsNum();
    }
    
    public AsteroidLogic(ILocalPositionAdapter localPositionAdapter, ILocalRotationAdapter localRotationAdapter, ScreenBoundsData screenBoundsData,
        GameObject asteroid, AsteroidType.AsteroidTypeEnum asteroidType, AsteroidData asteroidData, AsteroidSimulation asteroidSimulation, GameObject smallAsteroid, GameObject audioManager)
    {
        this.localPositionAdapter = localPositionAdapter;
        this.localRotationAdapter = localRotationAdapter;
        this.screenBoundsData = screenBoundsData;
        this.asteroid = asteroid;
        this.asteroidType = asteroidType;
        this.asteroidData = asteroidData;
        this.asteroidSimulation = asteroidSimulation;
        this.smallAsteroid = smallAsteroid;
        this.audioManager = audioManager;

        OnEnable();
    }

    public void FixedUpdate(float deltaTime)
    {
        CorrectPosition(deltaTime);
        CorrectPositionWithScreenBounds();
    }

    private void CorrectPosition(float deltaTime)
    {
        Vector3 newPosition = asteroidSimulation.UpdatePosition(localPositionAdapter.LocalPosition, localRotationAdapter.LocalRotation, asteroidData, asteroidType, deltaTime);
        localPositionAdapter.LocalPosition = newPosition;
    }
    
    private void CorrectPositionWithScreenBounds()
    {
        if (Mathf.Abs(localPositionAdapter.LocalPosition.x) > screenBoundsData.MaxAsteroidX)
        {
            Vector3 newPosition = asteroidSimulation.Teleport(localPositionAdapter.LocalPosition, true);
            localPositionAdapter.LocalPosition = newPosition;
        }
        
        if (Mathf.Abs(localPositionAdapter.LocalPosition.y) > screenBoundsData.MaxAsteroidY)
        {
            Vector3 newPosition = asteroidSimulation.Teleport(localPositionAdapter.LocalPosition, false);
            localPositionAdapter.LocalPosition = newPosition;
        }
    }

    public void Destroy()
    {
        RemoveFromTotalAsteroidsNum();
        
        if (asteroidType == AsteroidType.AsteroidTypeEnum.big) 
        { 
            audioManager.GetComponent<AudioManager>().PlayBigExsplosionSfx();
            int numOfSmallAsteroids = Random.Range(1, asteroidData.maxNumOfSmallAsteroidsSpawn+1);
            
            for (int i = 0; i < numOfSmallAsteroids; i++) 
            { 
                float zRandomRotation = Random.Range(-20f, 20f); 
                Vector3 newRotation = new Vector3(localRotationAdapter.LocalRotation.x, localRotationAdapter.LocalRotation.y, localRotationAdapter.LocalRotation.z + zRandomRotation);
                
                GameObject asteroidGO = GameObject.Instantiate(smallAsteroid, localPositionAdapter.LocalPosition, Quaternion.Euler(newRotation));
            }
        }
        else
        {
            audioManager.GetComponent<AudioManager>().PlaySmallExsplosionSfx();
        }
        
        asteroidSimulation.Destroy(asteroid);
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
