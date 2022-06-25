using UnityEngine;

public class RegularWeaponLogic
{
    private ILocalPositionAdapter localPositionAdapter;
    private ILocalRotationAdapter localRotationAdapter;
    private ScreenBoundsData screenBoundsData;
    private GameObject regularWeapon;
    private RegularWeaponData regularWeaponData;
    private RegularWeaponSimulation regularWeaponSimulation;
    
    public RegularWeaponLogic(ILocalPositionAdapter localPositionAdapter, ILocalRotationAdapter localRotationAdapter, ScreenBoundsData screenBoundsData,
        GameObject regularWeapon, RegularWeaponData regularWeaponData, RegularWeaponSimulation regularWeaponSimulation)
    {
        this.localPositionAdapter = localPositionAdapter;
        this.localRotationAdapter = localRotationAdapter;
        this.screenBoundsData = screenBoundsData;
        this.regularWeapon = regularWeapon;
        this.regularWeaponData = regularWeaponData;
        this.regularWeaponSimulation = regularWeaponSimulation;
    }

    public void FixedUpdate(float deltaTime)
    {
        CorrectPosition(deltaTime);
        CorrectPositionWithScreenBounds();
    }

    private void CorrectPosition(float deltaTime)
    {
        Vector3 newPosition = regularWeaponSimulation.UpdatePosition(localPositionAdapter.LocalPosition, localRotationAdapter.LocalRotation, regularWeaponData, deltaTime);
        localPositionAdapter.LocalPosition = newPosition;
    }
    
    private void CorrectPositionWithScreenBounds()
    {
        if (Mathf.Abs(localPositionAdapter.LocalPosition.x) > screenBoundsData.MaxSpaceShipX || Mathf.Abs(localPositionAdapter.LocalPosition.y) > screenBoundsData.MaxSpaceShipY)
        {
            regularWeaponSimulation.Destroy(regularWeapon);
        }
    }

    public void Hit(string tag)
    {
        regularWeaponSimulation.Destroy(regularWeapon);
    }
}
