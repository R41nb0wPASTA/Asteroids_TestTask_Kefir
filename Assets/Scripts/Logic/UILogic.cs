using System;
using TMPro;
using UnityEngine;

public class UILogic
{
    private bool enabledInput = true;
    
    private InputMapper inputMapper;
    
    private GameObject topCanvas;
    private GameObject startCanvas;
    private GameObject restartCanvas;
    private GameObject botCanvas;

    private GameObject ui;
    private GameObject game;
    
    private ScoreData scoreData;
    private SpaceShipData spaceShipData;
    
    private UIPresentation uiPresentation;
    
    private TextMeshProUGUI xCoordText;
    private TextMeshProUGUI yCoordText;
    private TextMeshProUGUI velocityText;
    private TextMeshProUGUI rotationText;
    private TextMeshProUGUI timerText;
    private TextMeshProUGUI laserNumText;
    private TextMeshProUGUI scoreText;

    public bool isRequiredToStart = false;
    public GameObject newGame;
    
    private void OnEnable()
    {
        inputMapper.Player.ShootRegular.performed += context => StartGame();
        inputMapper.Player.ShootLaser.performed += context => StartGame();
    }
    
    public UILogic(InputMapper inputMapper, GameObject topCanvas, GameObject startCanvas, GameObject restartCanvas, GameObject botCanvas, GameObject ui, GameObject game, ScoreData scoreData, SpaceShipData spaceShipData, UIPresentation uiPresentation, TextMeshProUGUI xCoordText, TextMeshProUGUI yCoordText,
        TextMeshProUGUI velocityText, TextMeshProUGUI rotationText, TextMeshProUGUI timerText,
        TextMeshProUGUI laserNumText, TextMeshProUGUI scoreText)
    {
        this.inputMapper = inputMapper;
        this.topCanvas = topCanvas;
        this.startCanvas = startCanvas;
        this.restartCanvas = restartCanvas;
        this.ui = ui;
        this.game = game;
        this.botCanvas = botCanvas;
        this.scoreData = scoreData;
        this.spaceShipData = spaceShipData;
        this.uiPresentation = uiPresentation;
        this.xCoordText = xCoordText;
        this.yCoordText = yCoordText;
        this.velocityText = velocityText;
        this.rotationText = rotationText;
        this.timerText = timerText;
        this.laserNumText = laserNumText;
        this.scoreText = scoreText;

        OnEnable();
    }

    public void UpdateScoreTextData()
    {
        uiPresentation.UpdateText(scoreText, scoreData.score.ToString());
    }
    
    public void UpdateShipTextData()
    {
        uiPresentation.UpdateText(xCoordText, Mathf.Round(spaceShipData.coords.x).ToString());
        uiPresentation.UpdateText(yCoordText, Mathf.Round(spaceShipData.coords.y).ToString());
        
        uiPresentation.UpdateText(velocityText, Mathf.Round(spaceShipData.curSpeed).ToString());
        
        uiPresentation.UpdateText(rotationText, Mathf.Round(spaceShipData.rotation.z).ToString());
        
        if (spaceShipData.timeLeftToRegenLaser > 0f)
            uiPresentation.UpdateText(timerText, TimeSpan.FromSeconds(spaceShipData.timeLeftToRegenLaser).Minutes + ":" + TimeSpan.FromSeconds(spaceShipData.timeLeftToRegenLaser).Seconds);
        else
            uiPresentation.UpdateText(timerText, " ");
        
        uiPresentation.UpdateText(laserNumText, "x" + spaceShipData.availableLaserNum.ToString());
    }

    private void StartGame()
    {
        if (!enabledInput)
            return;

        enabledInput = false;

        topCanvas.SetActive(true);
        botCanvas.SetActive(true);

        startCanvas.SetActive(false);
        restartCanvas.SetActive(false);

        newGame = GameObject.Instantiate(game);
        isRequiredToStart = true;
    }

    public void UpdateCanvasOnGameOver()
    {
        enabledInput = true;
        
        restartCanvas.SetActive(true);
        
        topCanvas.SetActive(false);
        botCanvas.SetActive(false);
    }
}
