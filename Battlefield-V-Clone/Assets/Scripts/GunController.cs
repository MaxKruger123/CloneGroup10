using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    [Header("Gund Settings")]
    public float fireRate = 0.1f;
    public int clipSize = 30;
    public int reservedAmmoCapacity = 270;

    //Variables that change througout the code
    bool _canShoot;
    int _currentAmmoInClip;
    int _ammoInReserve;

    //Muzzle Flash
    public Image muzzleFlashImage;
    public Sprite[] flashes;

    //Aiming
    public Vector3 normalLocalPosition;
    public Vector3 aimingLocalPosition;

    public float aimSmoothing = 10f;
    public float weaponSwayAmount = 10f;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 1f;
    Vector2 _currentRotation;

    //Weapon Recoil
    public bool randomizeRecoil;
    public Vector2 randomRecoilConstraints;
    // You only need to assign this if randomized recoil is off
    public Vector2[] recoilPattern;

    //Reloading
    public bool reloading;

    //Animator
    public Animator weaponAnimations;

    //Checking movement distance for walkking/running animations
    Vector3 lastPosition;
    float moveMinimum = 0.1f;
    private bool isMoving;

    void Start()
    {
        _currentAmmoInClip = clipSize;
        _ammoInReserve = reservedAmmoCapacity;
        _canShoot = true;
        muzzleFlashImage.sprite = null;
    }

    void FixedUpdate()
    {
        
       
    }

    private void Update()
    {
        
        DetermineAim();
        DetermineRotation();

        if (Input.GetMouseButton(0) && _canShoot && _currentAmmoInClip > 0)
        {
            weaponAnimations.Play("Idle", 0, 0f);
            weaponAnimations.enabled = false;
            _canShoot = false;
            _currentAmmoInClip--;
            StartCoroutine(ShootGun());
        } else if(Input.GetKeyDown(KeyCode.R) && _currentAmmoInClip < clipSize && _ammoInReserve > 0)
        {
            StartCoroutine(Reload());
        }


       

        

        
    }

   
    
     
            
           
            
        
    

    
    void DetermineRotation()
    {
        Vector2 mouseAxis = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        mouseAxis *= mouseSensitivity;
        _currentRotation += mouseAxis;

        _currentRotation.y = Mathf.Clamp(_currentRotation.y, -90, 90);

        transform.localPosition += (Vector3)mouseAxis * weaponSwayAmount / 1000;

        transform.root.localRotation = Quaternion.AngleAxis(_currentRotation.x, Vector3.up);
        transform.parent.localRotation = Quaternion.AngleAxis(-_currentRotation.y, Vector3.right);
    }

    void DetermineAim()
    {
        Vector3 target = normalLocalPosition;
        if (Input.GetMouseButton(1))
        {
            
            weaponAnimations.Play("Idle", 0, 0f);
            weaponAnimations.enabled = false;
            gameObject.transform.localRotation = Quaternion.Euler(0,0,0);


            target = aimingLocalPosition;
            
            Vector3 desiredPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * aimSmoothing);
            Debug.Log(desiredPosition);
            transform.localPosition = desiredPosition;
        }


        else
        {
            
            target = normalLocalPosition;
            Vector3 desiredPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * aimSmoothing);
            transform.localPosition = desiredPosition;
            if (transform.localPosition == normalLocalPosition)
            {
                weaponAnimations.enabled = true;
                
            }
            
        }
    }

    void DetermineRecoil()
    {
        transform.localPosition -= Vector3.forward * 0.025f;

        if (randomizeRecoil)
        {
            float xRecoil = Random.Range(-randomRecoilConstraints.x, randomRecoilConstraints.x);
            float yRecoil = Random.Range(-randomRecoilConstraints.y, randomRecoilConstraints.y);

            Vector2 recoil = new Vector2(xRecoil, yRecoil);

            _currentRotation += recoil;
        }
        else
        {
            int currentstep = clipSize + 1 - _currentAmmoInClip;
            currentstep = Mathf.Clamp(currentstep, 0, recoilPattern.Length - 1);

            _currentRotation += recoilPattern[currentstep];
        }
    }

    IEnumerator ShootGun()
    {

        DetermineRecoil();
        StartCoroutine(MuzzleFlash());
        
        yield return new WaitForSeconds(fireRate);
        _canShoot = true;
    }

    IEnumerator MuzzleFlash()
    {
        muzzleFlashImage.sprite = flashes[Random.Range(0, flashes.Length)];
        muzzleFlashImage.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        muzzleFlashImage.sprite = null;
        muzzleFlashImage.color = new Color(0, 0, 0, 0);
    }

    IEnumerator Reload()
    {
        _canShoot = false;
        reloading = true;
        yield return new WaitForSeconds(2);

        int amountNeeded = clipSize - _currentAmmoInClip;
        if (amountNeeded >= _ammoInReserve)
        {
            _currentAmmoInClip += _ammoInReserve;
            _ammoInReserve -= amountNeeded;
        }
        else
        {
            _currentAmmoInClip = clipSize;
            _ammoInReserve -= amountNeeded;
        }

        reloading = false;
        _canShoot = true;
    }

    
}
