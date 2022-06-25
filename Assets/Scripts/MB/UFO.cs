using UnityEngine;

public class UFO : MonoBehaviour, ILocalPositionAdapter, ILocalRotationAdapter
{
    [Header("Data")]
    public UFOData ufoData;
    public SpaceShipData spaceShipData;
    public ScreenBoundsData screenBoundsData;
    
    private UFOLogic ufoLogic;
    private UFOSimulation ufoSimulation;
    private UFOSpriteRotator ufoSpriteRotator;
    
    private GameObject audioManager;
    
    public Vector3 LocalPosition
    {
        get { return transform.localPosition; }
        set { transform.localPosition = value; }
    }
    
    public Vector3 LocalRotation
    {
        get { return transform.localRotation.eulerAngles; }
        set { transform.localRotation = Quaternion.Euler(value); }
    }

    private void Awake()
    {
        ufoSimulation = new UFOSimulation();
        ufoSpriteRotator = new UFOSpriteRotator(transform.GetChild(0).gameObject);
        
        audioManager = GameObject.FindGameObjectWithTag("AudioManager");
        
        ufoLogic = new UFOLogic(this, this, screenBoundsData, this.gameObject, ufoData, spaceShipData, ufoSimulation, ufoSpriteRotator, audioManager);
    }

    private void FixedUpdate()
    {
        ufoLogic.FixedUpdate(Time.fixedDeltaTime);
    }
    
    public void Destroy()
    {
        ufoLogic.Destroy();
    }
}
