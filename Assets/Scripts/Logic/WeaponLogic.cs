using UnityEngine;

public class WeaponLogic
{
    private ScoreData scoreData;
    private GameObject audioManager;

    private void OnEnable()
    {
        audioManager.GetComponent<AudioManager>().PlayFireSfx();
    }
    
    public WeaponLogic(ScoreData scoreData, GameObject audioManager)
    {
        this.scoreData = scoreData;
        this.audioManager = audioManager;

        OnEnable();
    }
    
    public void Hit(Collider2D col)
    {
        string tag = col.transform.tag;

        if (tag == "BigAsteroid")
        {
            scoreData.score += scoreData.bigAsteroidScore;
        }
        else if (tag == "SmallAsteroid")
        {
            scoreData.score += scoreData.smallAsteroidScore;
        }
        else if (tag == "UFO")
        {
            scoreData.score += scoreData.ufoScore;
        }
    }
}
