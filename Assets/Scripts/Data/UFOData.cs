using UnityEngine;

[CreateAssetMenu(fileName = "UFOData", menuName = "UFOData", order = 1100)]
public class UFOData : ScriptableObject
{
    public bool isAlive;
    public float speed;
    public float spawnTimer;
    public GameObject ufo;
}
