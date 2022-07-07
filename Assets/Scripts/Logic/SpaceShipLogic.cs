using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class SpaceShipLogic
{
    private bool isSpeedingUp = false;
    private bool isTurningLeft = false;
    private bool isTurningRight = false;

    private GameObject laserGO;
    private bool isLaserShooting = false;
    private float timeLeftToShootLaser = 0f;

    private bool isRegularShooting = false;
    private float timeLeftToShootRegular = 0f;
    
    private ScreenBoundsData screenBoundsData;
    private SpaceShipData spaceShipData;
    private SpaceShipSimulation spaceShipSimulation;

    private GameObject spaceShip;
    private GameObject flameEffect;
    private AudioManager audioManager;

    private void OnEnable()
    {
        ColliderInfo colInfo = spaceShip.AddComponent<ColliderInfo>();
        colInfo.OnCollider += OnCollider;

        spaceShipData.isAlive = true;
        flameEffect = spaceShip.transform.GetChild(0).gameObject;
    }
    
    public SpaceShipLogic(ScreenBoundsData screenBoundsData, SpaceShipData spaceShipData, SpaceShipSimulation spaceShipSimulation, GameObject spaceShip, AudioManager audioManager)
    {
        this.screenBoundsData = screenBoundsData;
        this.spaceShipData = spaceShipData;
        this.spaceShipSimulation = spaceShipSimulation;
        this.spaceShip = spaceShip;
        this.audioManager = audioManager;

        OnEnable();
    }
    
    public void FixedUpdate(float deltaTime)
    {
        if (spaceShip)
        {
            //Ship
            CorrectPosition(deltaTime);
            CorrectRotation();
            CorrectPositionWithScreenBounds();

            //Weaponry
            CorrectLaser(deltaTime);
            CorrectRegularShooting(deltaTime);

            //Data
            UpdateData();
        }
        else
        {
            StopThrushtEffect();
        }
    }

    private void OnCollider(object sender, ColliderInfo.OnColliderEventArgs eventArgs)
    {
        Hit(eventArgs.col);
    }
    
    public void Hit(Collider2D col)
    {
        String tag = col.tag;
        
        if (tag == "BigAsteroid" || tag == "SmallAsteroid" || tag == "UFO")
        {
            spaceShipData.isAlive = false;
            spaceShipSimulation.Destroy(spaceShip);
        }
    }

    private void UpdateData()
    {
        spaceShipData.coords = spaceShip.transform.localPosition;
        spaceShipData.rotation = spaceShip.transform.localRotation.eulerAngles;
    }
    
    public void GoForward()
    {
        isSpeedingUp = true;
        
        if (spaceShip)
            StartThrushtEffect();
    }

    public void StopGoingForward()
    {
        isSpeedingUp = false;
        
        if (spaceShip)
            StopThrushtEffect();
    }

    private void CorrectPosition(float deltaTime)
    {
        if (isSpeedingUp) 
        { float f = spaceShipData.curSpeed < spaceShipData.maxSpeed ? spaceShipData.curSpeed += spaceShipData.acceleration : 0; }
        else
        { float f = spaceShipData.curSpeed > 0f ? spaceShipData.curSpeed -= spaceShipData.braking : 0; }
        
        Vector3 newPosition = spaceShipSimulation.UpdatePosition(spaceShip.transform.localPosition, spaceShip.transform.localRotation.eulerAngles, spaceShipData.curSpeed, isSpeedingUp, deltaTime);
        spaceShip.transform.localPosition = newPosition;
    }

    private void CorrectPositionWithScreenBounds()
    {
        if (Mathf.Abs(spaceShip.transform.localPosition.x) > screenBoundsData.MaxSpaceShipX)
        {
            Vector3 newPosition = spaceShipSimulation.Teleport(spaceShip.transform.localPosition, true);
            spaceShip.transform.localPosition = newPosition;
        }
        
        if (Mathf.Abs(spaceShip.transform.localPosition.y) > screenBoundsData.MaxSpaceShipY)
        {
            Vector3 newPosition = spaceShipSimulation.Teleport(spaceShip.transform.localPosition, false);
            spaceShip.transform.localPosition = newPosition;
        }
    }
    
    public void TurnLeft() { isTurningLeft = true; }
    
    public void StopTurningLeft() { isTurningLeft = false; }
    
    public void TurnRight() { isTurningRight = true; }
    
    public void StopTurningRight() { isTurningRight = false; }
    
    private void CorrectRotation()
    {
        if (!isTurningLeft && !isTurningRight)
            return;
        
        float speed = isTurningRight ? spaceShipData.turnSpeed * -1 : spaceShipData.turnSpeed;
        
        Vector3 newRotation = spaceShipSimulation.UpdateRotation(spaceShip.transform.localRotation.eulerAngles, speed);
        spaceShip.transform.localRotation = Quaternion.Euler(newRotation);
    }

    public GameObject ShootLaser()
    {
        GameObject returnObj = null;

        if (!isLaserShooting && spaceShipData.availableLaserNum != 0 && spaceShip)
        {
            isLaserShooting = true;
            spaceShipData.availableLaserNum -= 1;

            if (spaceShipData.timeLeftToRegenLaser <= 0f)
                spaceShipData.timeLeftToRegenLaser = spaceShipData.timeToRegenLaser;
            timeLeftToShootLaser = spaceShipData.timeToShootLaser;

            if (spaceShip)
            {
                laserGO = GameObject.Instantiate(spaceShipData.laserWeapon, new Vector3(0f, 50f, 0f),
                    Quaternion.Euler(spaceShip.transform.localRotation.eulerAngles), spaceShip.transform);
                laserGO.transform.localPosition = new Vector3(0f, 50f, 0f);

                returnObj =  laserGO;
            }
        }

        return returnObj;
    }

    private void CancelLaser()
    {
        if (timeLeftToShootLaser < 0)
        {
            isLaserShooting = false;
            Object.Destroy(laserGO);
        }
    }

    private void RegenLaser()
    {
        if (spaceShipData.timeLeftToRegenLaser < 0 && spaceShipData.availableLaserNum < spaceShipData.maxLaserNum)
        {
            spaceShipData.availableLaserNum++;

            if (spaceShipData.availableLaserNum < spaceShipData.maxLaserNum)
                spaceShipData.timeLeftToRegenLaser = spaceShipData.timeToRegenLaser;
        }
    }
    
    private void CorrectLaser(float deltaTime)
    {
        timeLeftToShootLaser -= deltaTime;
        spaceShipData.timeLeftToRegenLaser -= deltaTime;
        
        CancelLaser();
        RegenLaser();
    }

    public GameObject ShootRegular()
    {
        GameObject returnObj = null;

        if (!isRegularShooting && !isLaserShooting)
        {
            isRegularShooting = true;

            timeLeftToShootRegular = spaceShipData.timeBetweenRegularShots;

            if (spaceShip)
            {
                 GameObject obj = GameObject.Instantiate(spaceShipData.regularWeapon, spaceShip.transform.localPosition,
                    Quaternion.Euler(spaceShip.transform.localRotation.eulerAngles));
                 returnObj = obj;
            }
        }

        return returnObj;
    }

    private void CorrectRegularShooting(float deltaTime)
    {
        timeLeftToShootRegular -= deltaTime;

        if (timeLeftToShootRegular < 0)
            isRegularShooting = false;
    }
    
    public void StartThrushtEffect()
    {
        audioManager.PlayThrustSfx();
        
        if (flameEffect)
            flameEffect.SetActive(true);
    }
    
    public void StopThrushtEffect()
    {
        audioManager.StopThrustSfx();
        
        if (flameEffect)
            flameEffect.SetActive(false);
    }
}
