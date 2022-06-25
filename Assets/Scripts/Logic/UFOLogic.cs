using UnityEngine;

public class UFOLogic
{
    private ILocalPositionAdapter localPositionAdapter;
    private ILocalRotationAdapter localRotationAdapter;
    private ScreenBoundsData screenBoundsData;
    private GameObject ufo;
    private UFOData ufoData;
    private SpaceShipData spaceShipData;
    private UFOSimulation ufoSimulation;
    private UFOSpriteRotator ufoSpriteRotator;

    private GameObject audioManager;
    
    private void OnEnable()
    {
        PlayThrushtSfx();
        ufoData.isAlive = true;
    }
    
    public UFOLogic(ILocalPositionAdapter localPositionAdapter, ILocalRotationAdapter localRotationAdapter, ScreenBoundsData screenBoundsData,
        GameObject ufo, UFOData ufoData, SpaceShipData spaceShipData, UFOSimulation ufoSimulation, UFOSpriteRotator ufoSpriteRotator, GameObject audioManager)
    {
        this.localPositionAdapter = localPositionAdapter;
        this.localRotationAdapter = localRotationAdapter;
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
        if (spaceShipData.isAlive)
        {
            CorrectRotation(spaceShipData.coords);
            CorrectPosition(deltaTime);
            CorrectPositionWithScreenBounds();
        }
    }

    private void CorrectRotation(Vector3 targetCoords)
    {
        Vector3 moveDirection = targetCoords - localPositionAdapter.LocalPosition; 
        
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        
        Vector3 tmp = q.eulerAngles;
        tmp.z -= 90f;
        
        localRotationAdapter.LocalRotation = tmp;

        ufoSpriteRotator.CorrectSpriteRotation(localRotationAdapter);
    }
    
    private void CorrectPosition(float deltaTime)
    {
        Vector3 newPosition = ufoSimulation.UpdatePosition(localPositionAdapter.LocalPosition, localRotationAdapter.LocalRotation, ufoData, deltaTime);
        localPositionAdapter.LocalPosition = newPosition;
    }
    
    private void CorrectPositionWithScreenBounds()
    {
        if (Mathf.Abs(localPositionAdapter.LocalPosition.x) > screenBoundsData.MaxAsteroidX)
        {
            Vector3 newPosition = ufoSimulation.Teleport(localPositionAdapter.LocalPosition, true);
            localPositionAdapter.LocalPosition = newPosition;
        }
        
        if (Mathf.Abs(localPositionAdapter.LocalPosition.y) > screenBoundsData.MaxAsteroidY)
        {
            Vector3 newPosition = ufoSimulation.Teleport(localPositionAdapter.LocalPosition, false);
            localPositionAdapter.LocalPosition = newPosition;
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
        audioManager.GetComponent<AudioManager>().PlayUfoSfx();
    }
    
    public void StopPlayingThrushtSfx()
    {
        audioManager.GetComponent<AudioManager>().StopUfoSfx();
    }
}
