using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class UILogic
{
    private GameObject startCanvas;
    private GameObject gameCanvas;
    private GameObject endCanvas;

    private ScoreData scoreData;
    private SpaceShipData spaceShipData;
    private EEData eeData;
    
    private UIPresentation uiPresentation;
    
    private TextMeshProUGUI xCoordText;
    private TextMeshProUGUI yCoordText;
    private TextMeshProUGUI velocityText;
    private TextMeshProUGUI rotationText;
    private TextMeshProUGUI timerText;
    private TextMeshProUGUI laserNumText;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI eeText;

    private GameState.GameStateEnum gameState;
    
    private void OnEnable()
    {
        uiPresentation = new UIPresentation();
        gameState = GameState.GameStateEnum.startScreen;
    }
    
    public UILogic(GameObject startCanvas, GameObject gameCanvas, GameObject endCanvas, ScoreData scoreData, SpaceShipData spaceShipData, EEData eeData, TextMeshProUGUI xCoordText, TextMeshProUGUI yCoordText,
        TextMeshProUGUI velocityText, TextMeshProUGUI rotationText, TextMeshProUGUI timerText,
        TextMeshProUGUI laserNumText, TextMeshProUGUI scoreText, TextMeshProUGUI eeText)
    {

        this.startCanvas = startCanvas;
        this.gameCanvas = gameCanvas;
        this.endCanvas = endCanvas;
        this.scoreData = scoreData;
        this.spaceShipData = spaceShipData;
        this.eeData = eeData;
        this.xCoordText = xCoordText;
        this.yCoordText = yCoordText;
        this.velocityText = velocityText;
        this.rotationText = rotationText;
        this.timerText = timerText;
        this.laserNumText = laserNumText;
        this.scoreText = scoreText;
        this.eeText = eeText;

        OnEnable();
    }

    public void UpdateUI(GameState.GameStateEnum gameState)
    {
        if (gameState != this.gameState)
        {
            this.gameState = gameState;
            switch (gameState)
            {
                case GameState.GameStateEnum.startScreen:
                    startCanvas.SetActive(true);
                    gameCanvas.SetActive(false);
                    endCanvas.SetActive(false);
                    return;
                case GameState.GameStateEnum.gameOn:
                    gameCanvas.SetActive(true);
                    startCanvas.SetActive(false);
                    endCanvas.SetActive(false);
                    return;
                case GameState.GameStateEnum.gameOver:
                    UpdateEE();
                    endCanvas.SetActive(true);
                    gameCanvas.SetActive(false);
                    startCanvas.SetActive(false);
                    return;
            }
        }
        else
        {
            UpdateScoreTextData();
            UpdateShipTextData();
        }
    }

    private void UpdateEE()
    {
        if (eeData.numOfStarts % eeData.timesReqToShow == 0)
        {
            eeText.gameObject.SetActive(true);
            eeText.text = "Also try\n" + eeData.eeTexts[Random.Range(0, eeData.eeTexts.Count)] + "!";
        }
        else
        {
            eeText.gameObject.SetActive(false);
        }
    }
    
    private void UpdateScoreTextData()
    {
        uiPresentation.UpdateText(scoreText, scoreData.score.ToString());
    }
    
    private void UpdateShipTextData()
    {
        uiPresentation.UpdateText(xCoordText, Mathf.Round(spaceShipData.coords.x).ToString());
        uiPresentation.UpdateText(yCoordText, Mathf.Round(spaceShipData.coords.y).ToString());
        
        uiPresentation.UpdateText(velocityText, Mathf.Round(spaceShipData.curSpeed).ToString());
        
        uiPresentation.UpdateText(rotationText, Mathf.Round(spaceShipData.rotation.z).ToString());

        if (spaceShipData.timeLeftToRegenLaser > 0f)
        {
            float minutes = TimeSpan.FromSeconds(spaceShipData.timeLeftToRegenLaser).Minutes;
            
            float seconds = TimeSpan.FromSeconds(spaceShipData.timeLeftToRegenLaser).Seconds;
            
            uiPresentation.UpdateText(timerText,(minutes.ToString("00") + ":" + seconds.ToString("00")));
        }
        else
            uiPresentation.UpdateText(timerText, " ");
        
        uiPresentation.UpdateText(laserNumText, "x" + spaceShipData.availableLaserNum.ToString());
    }
}
