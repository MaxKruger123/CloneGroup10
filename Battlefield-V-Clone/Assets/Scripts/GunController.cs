using System.Collections;
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

    

    void Start()
    {
        _currentAmmoInClip = clipSize;
        _ammoInReserve = reservedAmmoCapacity;
        _canShoot = true;
        
    }

    private void Update()
    {
        DetermineAim();

        if (Input.GetMouseButton(0) && _canShoot && _currentAmmoInClip > 0)
        {
            _canShoot = false;
            _currentAmmoInClip--;
            StartCoroutine(ShootGun());
        } else if(Input.GetKeyDown(KeyCode.R) && _currentAmmoInClip < clipSize && _ammoInReserve > 0)
        {
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
        }


    }

    void DetermineWeaponSway()
    {
       
    }

    void DetermineAim()
    {
        Vector3 target = normalLocalPosition;
        if (Input.GetMouseButton(1)) target = aimingLocalPosition;

        Vector3 desiredPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * aimSmoothing);

        transform.localPosition = desiredPosition;
        
    }

    IEnumerator ShootGun()
    {
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
}
