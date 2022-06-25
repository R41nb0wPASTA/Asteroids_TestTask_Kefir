using UnityEngine;

public class Asteroid : MonoBehaviour, ILocalPositionAdapter, ILocalRotationAdapter
{
    [Header("Data")]
    public AsteroidData asteroidData;
    public ScreenBoundsData screenBoundsData;

    [Header("Parameters")]
    public AsteroidType.AsteroidTypeEnum asteroidType;

    private GameObject audioManager;

    private AsteroidLogic asteroidLogic;
    private AsteroidSimulation asteroidSimulation;
    
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
        audioManager = GameObject.FindGameObjectWithTag("AudioManager");
        
        asteroidSimulation = new AsteroidSimulation();
        asteroidLogic = new AsteroidLogic(this, this, screenBoundsData, this.gameObject, asteroidType, asteroidData, asteroidSimulation, asteroidData.smallAsteroid, audioManager);
    }

    private void FixedUpdate()
    {
        asteroidLogic.FixedUpdate(Time.fixedDeltaTime);
    }

    public void Destroy()
    {
        asteroidLogic.Destroy();
    }
}
