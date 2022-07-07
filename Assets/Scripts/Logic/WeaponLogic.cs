using UnityEngine;

public class WeaponLogic
{
    private ScoreData scoreData;
    public WeaponLogic(ScoreData scoreData)
    {
        this.scoreData = scoreData;
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
        
        GameObject.Destroy(col.gameObject);
    }
}
