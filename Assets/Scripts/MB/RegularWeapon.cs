using UnityEngine;

public class RegularWeapon : MonoBehaviour, ILocalPositionAdapter, ILocalRotationAdapter
{
    [Header("Data")]
    public RegularWeaponData regularWeaponData;
    public ScreenBoundsData screenBoundsData;

    private RegularWeaponLogic regularWeaponLogic;
    private RegularWeaponSimulation regularWeaponSimulation;

    public Vector3 LocalPosition
    {
        get { return transform.localPosition; }
        set { transform.localPosition = value; }
    }
    
    public Vector3 LocalRotation
    {
        get { return transform.localRotation.eulerAngles; }
        set { transform.localRotation = Quaternion.Euler(value); }
    }
    
    private void Awake()
    {
        regularWeaponSimulation = new RegularWeaponSimulation();
        regularWeaponLogic = new RegularWeaponLogic(this, this, screenBoundsData,this.gameObject, regularWeaponData, regularWeaponSimulation);
    }

    private void FixedUpdate()
    {
        regularWeaponLogic.FixedUpdate(Time.fixedDeltaTime);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        regularWeaponLogic.Hit(col.transform.tag);
    }
}
