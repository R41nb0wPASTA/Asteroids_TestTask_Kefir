using UnityEngine;

public class RegularWeaponLogic
{
    private ScreenBoundsData screenBoundsData;
    private GameObject regularWeapon;
    private RegularWeaponData regularWeaponData;
    private RegularWeaponSimulation regularWeaponSimulation;
    private WeaponLogic weaponLogic;

    private AudioManager audioManager;
    
    private void OnEnable()
    {
        ColliderInfo colInfo = regularWeapon.AddComponent<ColliderInfo>();
        colInfo.OnCollider += OnCollider;
        
        audioManager.PlayFireSfx();
    }
    
    public RegularWeaponLogic(ScreenBoundsData screenBoundsData,
        GameObject regularWeapon, RegularWeaponData regularWeaponData, RegularWeaponSimulation regularWeaponSimulation, WeaponLogic weaponLogic, AudioManager audioManager)
    {
        this.screenBoundsData = screenBoundsData;
        this.regularWeapon = regularWeapon;
        this.regularWeaponData = regularWeaponData;
        this.regularWeaponSimulation = regularWeaponSimulation;
        this.weaponLogic = weaponLogic;
        this.audioManager = audioManager;

        OnEnable();
    }

    public void FixedUpdate(float deltaTime)
    {
        CorrectPosition(deltaTime);
        CorrectPositionWithScreenBounds();
    }

    private void CorrectPosition(float deltaTime)
    {
        Vector3 newPosition = regularWeaponSimulation.UpdatePosition(regularWeapon.transform.localPosition, regularWeapon.transform.localRotation.eulerAngles, regularWeaponData, deltaTime);
        regularWeapon.transform.localPosition = newPosition;
    }
    
    private void CorrectPositionWithScreenBounds()
    {
        if (Mathf.Abs(regularWeapon.transform.localPosition.x) > screenBoundsData.MaxSpaceShipX || Mathf.Abs(regularWeapon.transform.localPosition.y) > screenBoundsData.MaxSpaceShipY)
        {
            regularWeaponSimulation.Destroy(regularWeapon);
        }
    }
    
    private void OnCollider(object sender, ColliderInfo.OnColliderEventArgs eventArgs)
    {
        Hit(eventArgs.col);
        weaponLogic.Hit(eventArgs.col);
    }

    public void Hit(Collider2D col)
    {
        regularWeaponSimulation.Destroy(regularWeapon);
    }
}
