using UnityEngine;

public class LaserLogic
{
    private GameObject laserWeapon;
    private WeaponLogic weaponLogic;

    private AudioManager audioManager;

    private void OnEnable()
    {
        ColliderInfo colInfo = laserWeapon.AddComponent<ColliderInfo>();
        colInfo.OnCollider += OnCollider;
        
        audioManager.PlayLaserSfx();
    }
    
    public LaserLogic(GameObject laserWeapon, WeaponLogic weaponLogic, AudioManager audioManager)
    {
        this.laserWeapon = laserWeapon;
        this.weaponLogic = weaponLogic;
        this.audioManager = audioManager;

        OnEnable();
    }
    
    private void OnCollider(object sender, ColliderInfo.OnColliderEventArgs eventArgs)
    {
        weaponLogic.Hit(eventArgs.col);
    }
}
