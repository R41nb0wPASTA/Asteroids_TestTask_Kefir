using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameLogic
{
    private GameObject audioManager;
    
    private ScoreData scoreData;
    private SpaceShipData spaceShipData;
    private AsteroidData asteroidData;
    private UFOData ufoData;
    private ScreenBoundsData screenBoundsData;

    private GameObject spaceShip;
    private GameObject bigAsteroid;
    private GameObject ufo;
    private GameObject ui;

    private float timeLeftToSpawnUFO = 0f;

    private void OnEnable()
    {
        timeLeftToSpawnUFO = ufoData.spawnTimer;
        PrepareGameData();
    }
    
    public GameLogic(GameObject audioManager, ScoreData scoreData, SpaceShipData spaceShipData, AsteroidData asteroidData, UFOData ufoData, ScreenBoundsData screenBoundsData, GameObject spaceShip, GameObject bigAsteroid, GameObject ufo)
    {
        this.audioManager = audioManager;
        this.scoreData = scoreData;
        this.spaceShipData = spaceShipData;
        this.asteroidData = asteroidData;
        this.ufoData = ufoData;
        this.screenBoundsData = screenBoundsData;
        this.spaceShip = spaceShip;
        this.bigAsteroid = bigAsteroid;
        this.ufo = ufo;

        OnEnable();
    }

    public void SetUI(GameObject ui)
    {
        this.ui = ui;
    }

    public void FixedUpdate(float deltaTime)
    {
        if (!spaceShipData.isAlive)
        {
            ClearScene();
        }
        else
        {
            CheckAsteroidsSpawn();
            CheckUfoSpawn(deltaTime);
        }
    }

    private void CheckAsteroidsSpawn()
    {
        if (asteroidData.totalNumOfAsteroids == 0)
            SpawnAsteroids();
    }
    
    private void SpawnAsteroids()
    {
        int horSpawn = Random.Range(0, asteroidData.maxNumOfBigAsteroidsTotal / 3 * 2 + 1);
        int verSpawn = asteroidData.maxNumOfBigAsteroidsTotal - horSpawn;

        for (int i = 0; i < horSpawn; i++)
        {
            int x = (int)screenBoundsData.MaxAsteroidX;
            if (Random.value % 2 == 0)
            {x *= -1;}
            
            int y = Random.Range(-(int)screenBoundsData.MaxAsteroidY, (int)screenBoundsData.MaxAsteroidY + 1);

            float randZ = -1f;
            while ((randZ > 0 && randZ < 5) || (randZ > 85 && randZ < 95) || (randZ > 175 && randZ < 185) || (randZ > 265 && randZ < 275) || (randZ > 355 && randZ < 360))
                randZ = Random.Range(0, 360 + 1);
            
            GameObject go = GameObject.Instantiate(asteroidData.bigAsteroid, new Vector3(x, y, 0f), Quaternion.Euler(new Vector3(0f, 0f, randZ)));
            
            go.transform.rotation = CorrectAsteroidSpawnDirection(go);

        }
        
        for (int i = 0; i < verSpawn; i++)
        {
            int y = (int)screenBoundsData.MaxAsteroidY;
            if (Random.value % 2 == 0)
            {y *= -1;}
            
            int x = Random.Range(-(int)screenBoundsData.MaxAsteroidX, (int)screenBoundsData.MaxAsteroidX + 1);

            float randZ = -1f;
            //0, 90, 180, 270
            while ((randZ > 0 && randZ < 5) || (randZ > 85 && randZ < 95) || (randZ > 175 && randZ < 185) || (randZ > 265 && randZ < 275) || (randZ > 355 && randZ < 360))
                randZ = Random.Range(0, 360 + 1);
            
            GameObject go = GameObject.Instantiate(asteroidData.bigAsteroid, new Vector3(x, y, 0f), Quaternion.Euler(new Vector3(0f, 0f, randZ)));
            
            go.transform.rotation = CorrectAsteroidSpawnDirection(go);
        }
    }

    private void CheckUfoSpawn(float deltaTime)
    {
        timeLeftToSpawnUFO -= deltaTime;

        if (timeLeftToSpawnUFO < 0 && !ufoData.isAlive)
        {
            Vector3 spawnPos = spaceShipData.coords;
            spawnPos.x -= spawnPos.x * 2;
            spawnPos.y -= spawnPos.y * 2;
            
            while (Mathf.Abs(spawnPos.x) < screenBoundsData.MaxSpaceShipX || Mathf.Abs(spawnPos.y) < screenBoundsData.MaxSpaceShipY)
            {
                spawnPos.x++;
                spawnPos.y++;
            }

            GameObject.Instantiate(ufo, spawnPos, quaternion.Euler(0f, 0f, 0f));
        }
        else if (ufoData.isAlive)
        {
            timeLeftToSpawnUFO = ufoData.spawnTimer;
        }
    }
    
    private Quaternion CorrectAsteroidSpawnDirection(GameObject go)
    {
        Vector3 vectorToTarget = spaceShipData.coords - go.transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
    
    public void InitiateNewGame()
    {
        GameObject.Instantiate(audioManager);
        GameObject.Instantiate(spaceShip);
        
        SpawnAsteroids();
    }

    private void PrepareGameData()
    {
        ClearScoreData();
        ClearSpaceShipData();
        ClearAsteroidData();
        ClearUfoData();
    }

    void ClearScene()
    {
        var gos = Object.FindObjectsOfType<GameObject>();
        for (int i = 0; i < gos.Length; i++)
        {
            if (gos[i].transform.tag != "UI" && gos[i].transform.tag != "MainCamera")
                GameObject.Destroy(gos[i]);
        }
    }
    
    private void ClearScoreData()
    {
        scoreData.score = 0;
    }

    private void ClearSpaceShipData()
    {
        spaceShipData.isAlive = true;
        spaceShipData.curSpeed = 0f;
        spaceShipData.coords = Vector3.zero;
        spaceShipData.rotation = Vector3.zero;
        spaceShipData.availableLaserNum = spaceShipData.maxLaserNum;
        spaceShipData.timeLeftToRegenLaser = 0f;
    }

    private void ClearAsteroidData()
    {
        asteroidData.totalNumOfAsteroids = 0;
    }

    private void ClearUfoData()
    {
        ufoData.isAlive = false;
    }
    
    public void SetScreenData(Camera cam, ScreenBoundsData screenBoundsData)
    {
        Vector3 screenBounds = cam.ViewportToWorldPoint(new Vector3(0.0F, 0.0F, cam.nearClipPlane));
        screenBounds = new Vector3(Mathf.Abs(screenBounds.x), Mathf.Abs(screenBounds.y), Mathf.Abs(screenBounds.z));

        screenBoundsData.MaxAsteroidX = screenBounds.x + bigAsteroid.GetComponent<SpriteRenderer>().bounds.size.x;//5f;
        screenBoundsData.MaxAsteroidY = screenBounds.y + bigAsteroid.GetComponent<SpriteRenderer>().bounds.size.y;
        screenBoundsData.MaxSpaceShipX = screenBounds.x + spaceShip.GetComponent<SpriteRenderer>().bounds.size.x;//1f;
        screenBoundsData.MaxSpaceShipY = screenBounds.y + spaceShip.GetComponent<SpriteRenderer>().bounds.size.y;
    }
}
