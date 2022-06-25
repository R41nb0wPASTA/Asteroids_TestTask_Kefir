using UnityEngine;

public class UFOSpriteRotator
{
    private GameObject ufoSprite;
    
    public UFOSpriteRotator(GameObject ufoSprite)
    {
        this.ufoSprite = ufoSprite;
    }

    public void CorrectSpriteRotation(ILocalRotationAdapter localRotationAdapter)
    {
        
        Quaternion q = new Quaternion();
        q.eulerAngles = localRotationAdapter.LocalRotation;
        q.z *= -1;

        ufoSprite.transform.localRotation = q;
    }
}
