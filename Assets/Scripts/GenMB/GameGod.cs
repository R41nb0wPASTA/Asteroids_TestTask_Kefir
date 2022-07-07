using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameGod : MonoBehaviour
{
    //General
    [Header("General Data")]
    public ScreenBoundsData screenBoundsData;
    public ScoreData scoreData;
    public EEData eeData;
    
    //Asteroid
    [Header("Asteroid Data")]
    public AsteroidData asteroidData;
    
    //Space Ship
    [Header("Space Ship Data")]
    public SpaceShipData spaceShipData;

    //UFO
    [Header("UFO Data")]
    public UFOData ufoData;

    //Regular weapon
    [Header("Regular Weapon Data")]
    public RegularWeaponData regularWeaponData;
    
    //UI
    [Header("UI")]
    public GameObject startCanvas;
    public GameObject gameCanvas;
    public GameObject endCanvas;

    public TextMeshProUGUI xCoordText;
    public TextMeshProUGUI yCoordText;
    public TextMeshProUGUI velocityText;
    public TextMeshProUGUI rotationText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI laserNumText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI eeText;
    
    //Local
    private GameGodLogic gameGodLogic;
    private UILogic uiLogic;
    
    private void Awake()
    {
        uiLogic = new UILogic(startCanvas, gameCanvas, endCanvas, scoreData, spaceShipData, eeData, xCoordText, yCoordText,
            velocityText, rotationText, timerText, laserNumText, scoreText, eeText);
        gameGodLogic = new GameGodLogic(this.gameObject, screenBoundsData, scoreData, spaceShipData, ufoData, asteroidData, regularWeaponData, eeData, uiLogic);
    }

    private void FixedUpdate()
    {
        float time = Time.fixedDeltaTime;
        
        gameGodLogic.FixedUpdate(time);
    }
}
