using UnityEngine;

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
    
    private InputMapper inputMapper;
    private ScreenBoundsData screenBoundsData;
    private SpaceShipData spaceShipData;
    private SpaceShipSimulation spaceShipSimulation;
    private ILocalPositionAdapter localPositionAdapter;
    private ILocalRotationAdapter localRotationAdapter;

    private GameObject laserWeapon;
    private GameObject regularWeapon;

    private GameObject spaceShip;
    private GameObject flameEffect;
    private GameObject audioManager;

    private void OnEnable()
    {
        inputMapper.Player.GoForward.performed += context => GoForward();
        inputMapper.Player.GoForward.canceled += context => StopGoingForward();

        inputMapper.Player.TurnLeft.performed += context => TurnLeft();
        inputMapper.Player.TurnLeft.canceled += context => StopTurningLeft();
        
        inputMapper.Player.TurnRight.performed += context => TurnRight();
        inputMapper.Player.TurnRight.canceled += context => StopTurningRight();

        inputMapper.Player.ShootLaser.performed += context => ShootLaser();
        inputMapper.Player.ShootRegular.performed += context => ShootRegular();
    }
    
    public SpaceShipLogic(ILocalPositionAdapter localPositionAdapter, ILocalRotationAdapter localRotationAdapter, ScreenBoundsData screenBoundsData, SpaceShipData spaceShipData, SpaceShipSimulation spaceShipSimulation, InputMapper inputMapper, GameObject laserWeapon, GameObject regularWeapon, GameObject spaceShip, GameObject flameEffect, GameObject audioManager)
    {
        this.localPositionAdapter = localPositionAdapter;
        this.localRotationAdapter = localRotationAdapter;
        this.screenBoundsData = screenBoundsData;
        this.spaceShipData = spaceShipData;
        this.spaceShipSimulation = spaceShipSimulation;
        this.inputMapper = inputMapper;
        this.laserWeapon = laserWeapon;
        this.regularWeapon = regularWeapon;
        this.spaceShip = spaceShip;
        this.flameEffect = flameEffect;
        this.audioManager = audioManager;
        
        OnEnable();
    }
    
    public void FixedUpdate(float deltaTime)
    {
        //Ship
        CorrectPosition(deltaTime);
        CorrectRotation();
        CorrectPositionWithScreenBounds();
        
        //Weaponry
        CorrectLaser(deltaTime);
        CorrectRegularShooting(deltaTime);
    }

    public void Hit(string tag)
    {
        if (tag == "BigAsteroid" || tag == "SmallAsteroid" || tag == "UFO")
        {
            spaceShipData.isAlive = false;
            spaceShipSimulation.Destroy(spaceShip);
        }
    }

    private void GoForward()
    {
        isSpeedingUp = true;
        
        if (spaceShip)
            StartThrushtEffect();
    }

    private void StopGoingForward()
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
        
        Vector3 newPosition = spaceShipSimulation.UpdatePosition(localPositionAdapter.LocalPosition, localRotationAdapter.LocalRotation, spaceShipData.curSpeed, isSpeedingUp, deltaTime);
        localPositionAdapter.LocalPosition = newPosition;
    }

    private void CorrectPositionWithScreenBounds()
    {
        if (Mathf.Abs(localPositionAdapter.LocalPosition.x) > screenBoundsData.MaxSpaceShipX)
        {
            Vector3 newPosition = spaceShipSimulation.Teleport(localPositionAdapter.LocalPosition, true);
            localPositionAdapter.LocalPosition = newPosition;
        }
        
        if (Mathf.Abs(localPositionAdapter.LocalPosition.y) > screenBoundsData.MaxSpaceShipY)
        {
            Vector3 newPosition = spaceShipSimulation.Teleport(localPositionAdapter.LocalPosition, false);
            localPositionAdapter.LocalPosition = newPosition;
        }
    }
    
    private void TurnLeft() { isTurningLeft = true; }
    
    private void StopTurningLeft() { isTurningLeft = false; }
    
    private void TurnRight() { isTurningRight = true; }
    
    private void StopTurningRight() { isTurningRight = false; }
    
    private void CorrectRotation()
    {
        if (!isTurningLeft && !isTurningRight)
            return;
        
        float speed = isTurningRight ? spaceShipData.turnSpeed * -1 : spaceShipData.turnSpeed;
        
        Vector3 newRotation = spaceShipSimulation.UpdateRotation(localRotationAdapter.LocalRotation, speed);
        localRotationAdapter.LocalRotation = newRotation;
    }

    private void ShootLaser()
    {
        if (isLaserShooting || spaceShipData.availableLaserNum == 0 || !spaceShip)
            return;

        isLaserShooting = true;
        spaceShipData.availableLaserNum -= 1;
        
        if (spaceShipData.timeLeftToRegenLaser <= 0f)
            spaceShipData.timeLeftToRegenLaser = spaceShipData.timeToRegenLaser;
        timeLeftToShootLaser = spaceShipData.timeToShootLaser;

        if (spaceShip)
        {
            laserGO = GameObject.Instantiate(laserWeapon, new Vector3(0f, 50f, 0f),
                Quaternion.Euler(localRotationAdapter.LocalRotation), spaceShip.transform);
            laserGO.transform.localPosition = new Vector3(0f, 50f, 0f);
        }
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

    private void ShootRegular()
    {
        if (isRegularShooting || isLaserShooting)
            return;

        isRegularShooting = true;

        timeLeftToShootRegular = spaceShipData.timeBetweenRegularShots;
        
        if (spaceShip)
        {
            GameObject obj;
            obj = GameObject.Instantiate(regularWeapon, localPositionAdapter.LocalPosition, Quaternion.Euler(localRotationAdapter.LocalRotation));
        }
    }

    private void CorrectRegularShooting(float deltaTime)
    {
        timeLeftToShootRegular -= deltaTime;

        if (timeLeftToShootRegular < 0)
            isRegularShooting = false;
    }
    
    public void StartThrushtEffect()
    {
        audioManager.GetComponent<AudioManager>().PlayThrustSfx();
        flameEffect.SetActive(true);
    }
    
    public void StopThrushtEffect()
    {
        audioManager.GetComponent<AudioManager>().StopThrustSfx();
        flameEffect.SetActive(false);
    }
}
