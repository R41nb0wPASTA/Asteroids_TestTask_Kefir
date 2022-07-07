using UnityEngine;

public class UFOLogic
{
    private ScreenBoundsData screenBoundsData;
    private GameObject ufo;
    private UFOData ufoData;
    private SpaceShipData spaceShipData;
    private UFOSimulation ufoSimulation;
    private UFOSpriteRotator ufoSpriteRotator;

    private AudioManager audioManager;
    
    private void OnEnable()
    {
        PlayThrushtSfx();
        ufoData.isAlive = true;
    }
    
    public UFOLogic(ScreenBoundsData screenBoundsData, GameObject ufo, UFOData ufoData, SpaceShipData spaceShipData, UFOSimulation ufoSimulation, UFOSpriteRotator ufoSpriteRotator, AudioManager audioManager)
    {
        this.screenBoundsData = screenBoundsData;
        this.ufo = ufo;
        this.ufoData = ufoData;
        this.spaceShipData = spaceShipData;
        this.ufoSimulation = ufoSimulation;
        this.ufoSpriteRotator = ufoSpriteRotator;
        this.audioManager = audioManager;

        OnEnable();
    }

    public void FixedUpdate(float deltaTime)
    {
        if (!spaceShipData.isAlive)
        {
            StopPlayingThrushtSfx();
        }
        else if (ufo)
        {
            CorrectRotation(spaceShipData.coords);
            CorrectPosition(deltaTime);
            CorrectPositionWithScreenBounds();
        }
        else
        {
            ufoData.isAlive = false;
            Destroy();
        }
    }

    private void CorrectRotation(Vector3 targetCoords)
    {
        Vector3 moveDirection = targetCoords - ufo.transform.localPosition; 
        
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        
        Vector3 tmp = q.eulerAngles;
        tmp.z -= 90f;
        
        ufo.transform.localRotation = Quaternion.Euler(tmp);

        ufoSpriteRotator.CorrectSpriteRotation(ufo.transform.localRotation);
    }
    
    private void CorrectPosition(float deltaTime)
    {
        Vector3 newPosition = ufoSimulation.UpdatePosition(ufo.transform.localPosition, ufo.transform.localRotation.eulerAngles, ufoData, deltaTime);
        ufo.transform.localPosition = newPosition;
    }
    
    private void CorrectPositionWithScreenBounds()
    {
        if (Mathf.Abs(ufo.transform.localPosition.x) > screenBoundsData.MaxAsteroidX)
        {
            Vector3 newPosition = ufoSimulation.Teleport(ufo.transform.localPosition, true);
            ufo.transform.localPosition = newPosition;
        }
        
        if (Mathf.Abs(ufo.transform.localPosition.y) > screenBoundsData.MaxAsteroidY)
        {
            Vector3 newPosition = ufoSimulation.Teleport(ufo.transform.localPosition, false);
            ufo.transform.localPosition = newPosition;
        }
    }
    
    public void Destroy()
    {
        StopPlayingThrushtSfx();
        
        ufoData.isAlive = false;
        ufoSimulation.Destroy(ufo);
    }
    
    public void PlayThrushtSfx()
    {
        audioManager.PlayUfoSfx();
    }
    
    public void StopPlayingThrushtSfx()
    {
        audioManager.StopUfoSfx();
    }
}
