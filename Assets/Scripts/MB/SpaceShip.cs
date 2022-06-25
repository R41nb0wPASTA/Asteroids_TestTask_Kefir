using UnityEngine;

public class SpaceShip : MonoBehaviour, ILocalPositionAdapter, ILocalRotationAdapter
{
    [Header("Data")]
    public SpaceShipData spaceShipData;
    public ScreenBoundsData screenBoundsData;
    
    [Header("Effects")]
    public GameObject flameEffect;
    
    private InputMapper inputMapper;
    
    private SpaceShipLogic spaceShipLogic;
    private SpaceShipSimulation spaceShipSimulation;
    
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
        inputMapper = new InputMapper();
        inputMapper.Player.Enable();
        
        audioManager = GameObject.FindGameObjectWithTag("AudioManager");
        
        spaceShipSimulation = new SpaceShipSimulation();
        spaceShipLogic = new SpaceShipLogic(this, this, screenBoundsData, spaceShipData, spaceShipSimulation, inputMapper, spaceShipData.laserWeapon, spaceShipData.regularWeapon, this.gameObject, flameEffect, audioManager);
    }

    private void FixedUpdate()
    {
        spaceShipLogic.FixedUpdate(Time.fixedDeltaTime);

        spaceShipData.coords = LocalPosition;
        spaceShipData.rotation = LocalRotation;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        spaceShipLogic.Hit(col.transform.tag);
    }

}
