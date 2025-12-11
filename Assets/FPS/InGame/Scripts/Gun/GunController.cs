using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    [Header("Gun Setting")]
    public float fireRate = 0.1f;
    public int clipSize = 30;
    public int reservedAmmoCapacity = 270;

    private bool _canShoot;
    private int _currentAmmoInClip;
    private int _ammoInReserve;

    public Image muzzleFlashImage;
    public Sprite[] flashes;

    public Vector3 normalLocalPosition;
    public Vector3 aimingLoaclPosition;

    private float aimSmoothing = 10f;
    [Header("Mouse Setting")]
    public float mouseSensitivity = 1f;
    Vector2 _currentRotation;

    public bool randomizeRecoil;
    public Vector2 randomRecoilConstraints;

    public Vector2[] recoilPattern;

    private void Start()
    {
        _currentAmmoInClip = clipSize;
        _ammoInReserve = reservedAmmoCapacity;
        _canShoot = true;
    }

    private void Update()
    {
        DetermineAim();
        DetermineRotation();
        if (Input.GetMouseButton(0) && _canShoot && _currentAmmoInClip > 0)
        {
            _canShoot = false;
            _currentAmmoInClip--;
            StartCoroutine(ShootGun());
        }
        else if (Input.GetKeyDown(KeyCode.R) && _currentAmmoInClip < clipSize && _ammoInReserve > 0)
        {
            Reload();
        }
    }

    private void DetermineRotation()
    {
        Vector2 mouseAxis = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseAxis *= mouseSensitivity;
        _currentRotation += mouseAxis;
        transform.root.localRotation = Quaternion.AngleAxis(-_currentRotation.x, Vector3.up);
        transform.parent.localRotation = Quaternion.AngleAxis(_currentRotation.y, Vector3.right);
    }

    private void DetermineAim()
    {
        Vector3 target = normalLocalPosition;
        if (Input.GetMouseButton(1))
        {
            target = aimingLoaclPosition;
        }
        Vector3 desiredPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * aimSmoothing);
        transform.localPosition = desiredPosition;
    }

    /// <summary>
    /// リロード処理。足りない弾数を計算して、リザーブから補充する。
    /// </summary>
    private void Reload()
    {
        int needed = clipSize - _currentAmmoInClip;

        if (_ammoInReserve >= needed)
        {
            _currentAmmoInClip = clipSize;
            _ammoInReserve -= needed;
        }
        else
        {
            _currentAmmoInClip += _ammoInReserve;
            _ammoInReserve = 0;
        }
    }

    /// <summary>
    /// 一定時間待って再度発射可能にする。連射速度(fireRate)を制御する。
    /// </summary>
    private IEnumerator ShootGun()
    {
        DeterminRecoil();
        StartCoroutine(MuzzuleFlash());
        yield return new WaitForSeconds(fireRate);
        _canShoot = true;
    }

    private void DeterminRecoil()
    {
        transform.localPosition -= Vector3.forward * 0.1f;
        if (randomizeRecoil)
        {
            float xRecoil = Random.Range(-randomRecoilConstraints.x, randomRecoilConstraints.x);
            float yRecoil = Random.Range(-randomRecoilConstraints.y, randomRecoilConstraints.y);

            Vector2 Recoil = new Vector2(xRecoil, yRecoil);
            _currentRotation += Recoil;
        }
        else
        {
            int currentStep = clipSize + 1 - _currentAmmoInClip;
            currentStep = Mathf.Clamp(currentStep, 0, recoilPattern.Length - 1);
            _currentRotation += recoilPattern[currentStep];

        }
    }

    private IEnumerator MuzzuleFlash()
    {
        muzzleFlashImage.sprite = flashes[Random.Range(0, flashes.Length)];
        muzzleFlashImage.color  = Color.white;
        yield return new WaitForSeconds(0.05f);
        muzzleFlashImage.sprite = null;
        muzzleFlashImage.color = new Color(0, 0, 0, 0);
    }
}
