using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Data")]
    public ScoreData scoreData;
    
    private WeaponLogic weaponLogic;
    
    private GameObject audioManager;
    
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager");
        weaponLogic = new WeaponLogic(scoreData, audioManager);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        weaponLogic.Hit(col);
        
        string tag = col.transform.tag;

        if (tag == "BigAsteroid")
        {
            col.transform.GetComponent<Asteroid>().Destroy();
        }
        else if (tag == "SmallAsteroid")
        {
            col.transform.GetComponent<Asteroid>().Destroy();
        }
        else if (tag == "UFO")
        {
            col.transform.GetComponent<UFO>().Destroy();
        }
    }
    
}
