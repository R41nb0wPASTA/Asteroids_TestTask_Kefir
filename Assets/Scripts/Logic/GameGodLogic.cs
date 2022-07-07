using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class GameGodLogic
{
    //Input
    private InputMapper inputMapper;
    
    //Camera
    private Camera cam;
    
    //Data
    private ScreenBoundsData screenBoundsData;
    private ScoreData scoreData;
    private SpaceShipData spaceShipData;
    private UFOData ufoData;
    private AsteroidData asteroidData;
    private RegularWeaponData regularWeaponData;
    private EEData eeData;

    //Asteroid
    private List<Tuple<GameObject, AsteroidLogic>> asteroids = new List<Tuple<GameObject, AsteroidLogic>>();
    private AsteroidSimulation asteroidSimulation;
    
    //SpaceShip
    private SpaceShipLogic spaceShipLogic;
    private SpaceShipSimulation spaceShipSimulation;

    //UFO
    private UFOLogic ufoLogic;
    private UFOSimulation ufoSimulation;
    UFOSpriteRotator ufoSpriteRotator;
    
    //Weapon
    private WeaponLogic weaponLogic;
    
    //Regular weapon
    private List<Tuple<GameObject, RegularWeaponLogic>> regularWeaponShots = new List<Tuple<GameObject, RegularWeaponLogic>>();
    private RegularWeaponSimulation regularWeaponSimulation;

    //Laser weapon
    public List<Tuple<GameObject, LaserLogic>> lasers = new List<Tuple<GameObject, LaserLogic>>();

    //UI
    private UILogic uiLogic;
    
    //Game
    private GameObject gameGod;
        
    //Other
    private float timeLeftToSpawnUFO = 0f;
    private GameState.GameStateEnum gameState;
    private AudioManager audioManager;

    private void OnEnable()
    {
        //DataPrep
        eeData.numOfStarts = 0;
        
        //GameState
        gameState = GameState.GameStateEnum.startScreen;

        //Camera
        cam = Camera.main;
        SetScreenData(cam);

        //Input
        inputMapper = new InputMapper();
        inputMapper.Player.Enable();

        //Audio
        audioManager = gameGod.GetComponent<AudioManager>();
        
        //Asteroid
        asteroidSimulation = new AsteroidSimulation();
        
        //SpaceShipPrep
        spaceShipSimulation = new SpaceShipSimulation();
        
        //UfoPrep
        ufoSimulation = new UFOSimulation();

        //WeaponPrep
        weaponLogic = new WeaponLogic(scoreData);
        
        //RegularWeaponPrep
        regularWeaponSimulation = new RegularWeaponSimulation();

        SetControls();
    }

    public GameGodLogic(GameObject gameGod, ScreenBoundsData screenBoundsData, ScoreData scoreData, SpaceShipData spaceShipData, UFOData ufoData, AsteroidData asteroidData, RegularWeaponData regularWeaponData, EEData eeData, UILogic uiLogic)
    {
        this.gameGod = gameGod;
        this.screenBoundsData = screenBoundsData;
        this.scoreData = scoreData;
        this.spaceShipData = spaceShipData;
        this.ufoData = ufoData;
        this.asteroidData = asteroidData;
        this.regularWeaponData = regularWeaponData;
        this.eeData = eeData;
        this.uiLogic = uiLogic;
        
        OnEnable();
    }

    public void StartGame()
    {   
        gameState = GameState.GameStateEnum.gameOn;
        
        PrepareGameData();
        
        SpawnSpaceShip();
        SpawnAsteroids();
    }
    
    public void FixedUpdate(float time)
    {
        UpdateGame(time);
    }
    
    private void UpdateGame(float time)
    {
        UpdateUI();

        if (!spaceShipData.isAlive && gameState == GameState.GameStateEnum.gameOn)
        {
            UpdateSpaceShip(time);
            UpdateUfo(time);
            gameState = GameState.GameStateEnum.gameOver;
            ClearScene();
        }
        
        if (gameState == GameState.GameStateEnum.gameOn)
        {
            UpdateSpaceShip(time);
            UpdateAsteroids(time);
            UpdateUfo(time);
            UpdateWeaponry(time);
        }
    }

    private void UpdateAsteroids(float time)
    {
        if (asteroidData.totalNumOfAsteroids == 0)
        {
            SpawnAsteroids();
        }
        
        for (int i = 0; i < asteroids.Count; i++)
        {
            if (asteroids[i].Item1)
                asteroids[i].Item2.FixedUpdate(Time.fixedDeltaTime);
            else
            {
                List<GameObject> smallAsteroids = asteroids[i].Item2.DestroyAndRipApart();
                asteroids.Remove(asteroids[i]);

                for (int j = 0; j < smallAsteroids.Count; j++)
                {
                    asteroids.Add(new Tuple<GameObject, AsteroidLogic>(smallAsteroids[j], new AsteroidLogic(screenBoundsData, smallAsteroids[j], AsteroidType.AsteroidTypeEnum.small, asteroidData, asteroidSimulation, audioManager)));
                }
            }
        }
    }

    private void UpdateSpaceShip(float time)
    {
        spaceShipLogic.FixedUpdate(time);
    }

    private void UpdateWeaponry(float time)
    {
        for (int i = 0; i < regularWeaponShots.Count; i++)
        {
            if (regularWeaponShots[i].Item1)
            {
                regularWeaponShots[i].Item2.FixedUpdate(Time.fixedDeltaTime);
            }
            else
                regularWeaponShots.Remove(regularWeaponShots[i]);
        }
    }

    private void UpdateUI()
    {
        uiLogic.UpdateUI(gameState);
    }
    
    public void SpawnSpaceShip()
    {
        GameObject go = GameObject.Instantiate(spaceShipData.spaceShip);
        
        spaceShipLogic = new SpaceShipLogic(screenBoundsData, spaceShipData, spaceShipSimulation, go, audioManager);
    }
    
    private void SpawnAsteroids()
    {
        List<GameObject> tmpAsteroids = InstantiateAsteroids();
        for (int i = 0; i < asteroidData.maxNumOfBigAsteroidsTotal; i++)
        {
            asteroids.Add(new Tuple<GameObject, AsteroidLogic>(tmpAsteroids[i], new AsteroidLogic(screenBoundsData, tmpAsteroids[i], AsteroidType.AsteroidTypeEnum.big, asteroidData, asteroidSimulation, audioManager)));
        }
    }
    
    public List<GameObject> InstantiateAsteroids()
    {
        List<GameObject> asteroids = new List<GameObject>();
        
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

            asteroids.Add(go);
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
            
            asteroids.Add(go);
        }

        return asteroids;
    }
    
    private Quaternion CorrectAsteroidSpawnDirection(GameObject go)
    {
        Vector3 vectorToTarget = spaceShipData.coords - go.transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
    
    private void UpdateUfo(float deltaTime)
    {
        timeLeftToSpawnUFO -= deltaTime;

        if (timeLeftToSpawnUFO < 0 && !ufoData.isAlive)
        {
            GameObject ufo = InstantiateUfo();
            ufoSpriteRotator = new UFOSpriteRotator(ufo.transform.GetChild(0).gameObject);
            ufoLogic = new UFOLogic(screenBoundsData, ufo, ufoData, spaceShipData, ufoSimulation, ufoSpriteRotator, audioManager);
        }
        else if (ufoData.isAlive)
        {
            timeLeftToSpawnUFO = ufoData.spawnTimer;
            ufoLogic.FixedUpdate(deltaTime);
        }
    }

    private GameObject InstantiateUfo()
    {
        Vector3 spawnPos = spaceShipData.coords;
        spawnPos.x -= spawnPos.x * 2;
        spawnPos.y -= spawnPos.y * 2;

        while (Mathf.Abs(spawnPos.x) < screenBoundsData.MaxSpaceShipX || Mathf.Abs(spawnPos.y) < screenBoundsData.MaxSpaceShipY)
        {
            if (spawnPos.x < 0)
                spawnPos.x--;
            else
                spawnPos.x++;

            if (spawnPos.y < 0)
                spawnPos.y--;
            else
                spawnPos.y++;
        }

        GameObject go = GameObject.Instantiate(ufoData.ufo, spawnPos, quaternion.Euler(0f, 0f, 0f));
        return go;
    }
    
    private void PrepareGameData()
    {
        UpdateUfoData();
        UpdateEEData();      
        
        ClearScoreData();
        ClearSpaceShipData();
        ClearAsteroidData();
        ClearUfoData();
    }

    private void UpdateUfoData()
    {
        timeLeftToSpawnUFO = ufoData.spawnTimer;
    }
    
    private void UpdateEEData()
    {
        eeData.numOfStarts++;
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
    
    void ClearScene()
    {
        var gos = Object.FindObjectsOfType<GameObject>();
        for (int i = 0; i < gos.Length; i++)
        {
            if (gos[i].transform.tag != "UI" && gos[i].transform.tag != "MainCamera")
                GameObject.Destroy(gos[i]);
        }
        
        asteroids.Clear();
        regularWeaponShots.Clear();
        lasers.Clear();
    }
    
    public void SetScreenData(Camera cam)
    {
        Vector3 screenBounds = cam.ViewportToWorldPoint(new Vector3(0.0F, 0.0F, cam.nearClipPlane));
        screenBounds = new Vector3(Mathf.Abs(screenBounds.x), Mathf.Abs(screenBounds.y), Mathf.Abs(screenBounds.z));

        screenBoundsData.MaxAsteroidX = screenBounds.x + asteroidData.bigAsteroid.GetComponent<SpriteRenderer>().bounds.size.x;//5f;
        screenBoundsData.MaxAsteroidY = screenBounds.y + asteroidData.bigAsteroid.GetComponent<SpriteRenderer>().bounds.size.y;
        screenBoundsData.MaxSpaceShipX = screenBounds.x + spaceShipData.spaceShip.GetComponent<SpriteRenderer>().bounds.size.x;//1f;
        screenBoundsData.MaxSpaceShipY = screenBounds.y + spaceShipData.spaceShip.GetComponent<SpriteRenderer>().bounds.size.y;
    }
    
    private void SetControls()
    {
        inputMapper.Player.GoForward.performed += context => SpaceShipGoForwardController();
        inputMapper.Player.GoForward.canceled += context => SpaceShipStopGoingController();

        inputMapper.Player.TurnLeft.performed += context => SpaceShipTurnLeftController();
        inputMapper.Player.TurnLeft.canceled += context => SpaceShipStopTurningLeftController();
        
        inputMapper.Player.TurnRight.performed += context => SpaceShipTurnRightController();
        inputMapper.Player.TurnRight.canceled += context => SpaceShipStopTurningRightController();

        inputMapper.Player.ShootLaser.performed += context => SpaceShipShootLaserController();
        inputMapper.Player.ShootRegular.performed += context => SpaceShipShootRegularWeaponShotController();
    }

    private void SpaceShipGoForwardController()
    {
        spaceShipLogic.GoForward();
    }
    
    private void SpaceShipStopGoingController()
    {
        spaceShipLogic.StopGoingForward();
    }
    
    private void SpaceShipTurnLeftController()
    {
        spaceShipLogic.TurnLeft();
    }
    
    private void SpaceShipStopTurningLeftController()
    {
        spaceShipLogic.StopTurningLeft();
    }
    
    private void SpaceShipTurnRightController()
    {
        spaceShipLogic.TurnRight();
    }
    
    private void SpaceShipStopTurningRightController()
    {
        spaceShipLogic.StopTurningRight();
    }
    
    private void SpaceShipShootLaserController()
    {
        if (gameState != GameState.GameStateEnum.gameOn)
            StartGame();
        else
        {
            GameObject go = spaceShipLogic.ShootLaser();
            if (go)
                lasers.Add(new Tuple<GameObject, LaserLogic>(go, new LaserLogic(go, weaponLogic, audioManager)));
        }
    }
    
    private void SpaceShipShootRegularWeaponShotController()
    {
        if (gameState != GameState.GameStateEnum.gameOn)
            StartGame();
        else
        {
            GameObject go = spaceShipLogic.ShootRegular();
            if (go)
                regularWeaponShots.Add(new Tuple<GameObject, RegularWeaponLogic>(go,
                    new RegularWeaponLogic(screenBoundsData, go, regularWeaponData, regularWeaponSimulation,
                        weaponLogic, audioManager)));
        }
    }
}
