using UnityEngine;

public class Game : MonoBehaviour
{
    [Header("Data")]
    public ScreenBoundsData screenBoundsData;
    public ScoreData scoreData;
    public SpaceShipData spaceShipData;
    public AsteroidData asteroidData;
    public UFOData ufoData;

    [Header("Prefabs")]
    public GameObject audioManager;
    public GameObject spaceShip;
    public GameObject bigAsteroid;
    public GameObject ufo;
    
    private Camera cam;
    
    private GameLogic gameLogic;
    private GameObject ui;
    
    private void Awake()
    {
        gameLogic = new GameLogic(audioManager, scoreData, spaceShipData, asteroidData, ufoData, screenBoundsData, spaceShip, bigAsteroid, ufo);

        cam = Camera.main;
        gameLogic.SetScreenData(cam, screenBoundsData);
    }

    private void Start()
    {
        gameLogic.InitiateNewGame();
    }

    private void FixedUpdate()
    {
        gameLogic.FixedUpdate(Time.fixedDeltaTime);
        
        if (!spaceShipData.isAlive)
        {
            ui.GetComponent<UI>().UpdateCanvasOnGameOver();
        }
        else
        {
            ui.GetComponent<UI>().UpdateScore();
            ui.GetComponent<UI>().UpdateShipData();
        }
    }

    public void SetUI(GameObject ui)
    {
        this.ui = ui;
        gameLogic.SetUI(this.ui);
    }

}
