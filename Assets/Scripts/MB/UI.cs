using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [Header("Data")]
    public ScoreData scoreData;
    public SpaceShipData spaceShipData;

    [Header("Canvas")]
    public GameObject topCanvas;
    public GameObject startCanvas;
    public GameObject restartCanvas;
    public GameObject botCanvas;
    
    [Header("Text")]
    public TextMeshProUGUI xCoordText;
    public TextMeshProUGUI yCoordText;
    public TextMeshProUGUI velocityText;
    public TextMeshProUGUI rotationText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI laserNumText;
    public TextMeshProUGUI scoreText;

    [Header("Prefabs")]
    public GameObject game;
    
    private InputMapper inputMapper;
    
    private UILogic uiLogic;
    private UIPresentation uiPresentation;

    private void Awake()
    {
        inputMapper = new InputMapper();
        inputMapper.Enable();
        
        uiPresentation = new UIPresentation();
        uiLogic = new UILogic(inputMapper, topCanvas, startCanvas, restartCanvas, botCanvas, this.gameObject, game, scoreData, spaceShipData, uiPresentation, xCoordText, yCoordText, velocityText, rotationText, timerText, laserNumText, scoreText);
    }

    private void FixedUpdate()
    {
        if (uiLogic.isRequiredToStart)
        {
            uiLogic.isRequiredToStart = false;
            uiLogic.newGame.GetComponent<Game>().SetUI(gameObject);
        }
    }
    
    public void UpdateCanvasOnGameOver()
    {
        uiLogic.UpdateCanvasOnGameOver();
    }
    
    public void UpdateScore()
    {
        uiLogic.UpdateScoreTextData();
    }

    public void UpdateShipData()
    {
        uiLogic.UpdateShipTextData();
    }
}
