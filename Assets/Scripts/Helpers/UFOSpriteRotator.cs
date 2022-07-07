using UnityEngine;

public class UFOSpriteRotator
{
    private GameObject ufoSprite;
    
    public UFOSpriteRotator(GameObject ufoSprite)
    {
        this.ufoSprite = ufoSprite;
    }

    public void CorrectSpriteRotation(Quaternion rotation)
    {
        
        Quaternion q = new Quaternion();
        q.eulerAngles = rotation.eulerAngles;
        q.z *= -1;

        ufoSprite.transform.localRotation = q;
    }
}
