using UnityEngine;

[CreateAssetMenu(fileName = "ScoreData", menuName = "ScoreData", order = 1100)]
public class ScoreData : ScriptableObject
{
    public float score;
    public float bigAsteroidScore;
    public float smallAsteroidScore;
    public float ufoScore;
}
