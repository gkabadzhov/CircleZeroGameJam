using OTBG.Gameplay.Player;
using OTBG.Gameplay.Player.Movement;
using OTBG.Utilities.Data;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MissileAttack : MonoBehaviour
{
    public static event Action<ValueChange> OnWeaponTimerUpdated;
    public event Action<Vector2> OnWeaponFired;

    [FoldoutGroup("References")]
    public RotateWorldObjectToMouse _rotator;
    [FoldoutGroup("References")]
    public PlayerMovement player;
    [FoldoutGroup("References")]
    public Bullet projectilePrefab;

    [FoldoutGroup("Stats")]
    public int _abilityLevel = 1;
    [FoldoutGroup("Stats"), SerializeField]
    private float _shotDelay;
    [FoldoutGroup("Stats"), SerializeField]
    private float _reloadTime;
    [FoldoutGroup("Stats"), SerializeField]
    private float _knockbackPower;

    [FoldoutGroup("Collections"), SerializeField]
    private List<Transform> spawnLocations = new List<Transform>();

    private Coroutine _shotDelayCoroutine;

    private int _maxAmmo = 2;
    private int _currentAmmo = 0;
    public float debugTimer;

    private void Awake()
    {
        if (player == null)
            player = FindFirstObjectByType<PlayerMovement>();
        player.OnGrounded += Player_OnGrounded;
    }

    private void Start()
    {
        _currentAmmo = _maxAmmo;
    }

    private void OnDestroy()
    {
        player.OnGrounded -= Player_OnGrounded;
    }

    private void Player_OnGrounded(bool obj)
    {
        if (obj == false)
            return;
        if (_currentAmmo <= 0)
        {
            Reload();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanFire())
            return;

        if (Input.GetMouseButton(0)) {
            //CalculateDirectionVector();
            SpawnProjectile();
            _shotDelayCoroutine = StartCoroutine(ShotDelay(_shotDelay));
        }
    }

    Vector3 CalculateDirectionVector()
    {
        Vector3 mouseWorldPos;
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        Vector3 targetDirection = mouseWorldPos - transform.position;
        targetDirection.Normalize();

        // trigger ApplyForce func with targetDirection vector

        return targetDirection;
    }

    void SpawnProjectile()
    {
        switch(_abilityLevel)
        {
            case 1:
                LevelOneShot();
                break;
            case 2:
                LevelTwoShot();
                break;
            case 3:
                LevelThreeShot();
                break;
            default:
                Debug.Log("incorrect ability value");
                break;
        }

        OnWeaponFired?.Invoke(CalculateDirectionVector());
        player.TriggerForceThrow(-CalculateDirectionVector(), _knockbackPower);
    }

    public void LevelOneShot()
    {
        for (int i = 0; i < spawnLocations.Count; i++)
        {
            CreateBullet(spawnLocations[i]);
        }
        _shotDelay = 0.2f;
        ReduceAmmo();
    }

    public void LevelTwoShot()
    {
        for (int i = 0; i < spawnLocations.Count; i++)
        {
            CreateBullet(spawnLocations[i]);
        }
        _shotDelay = 0.2f;
    }

    public void LevelThreeShot()
    {
        CreateBullet(spawnLocations[0]);
        _shotDelay = 0.1f;
    }

    public void CreateBullet(Transform baseTransform)
    {
        Bullet bulletInstance = Instantiate(projectilePrefab, baseTransform.position, baseTransform.rotation);
        bulletInstance.BulletInit(_rotator._lastAimingRight? baseTransform.right:-baseTransform.right, player.GetRB().velocity.magnitude * 1.2f);
    }

    public void ReduceAmmo()
    {
        _currentAmmo--;

        if( _currentAmmo <= 0 )
        {
            if(reload_Coroutine == null)
                reload_Coroutine = StartCoroutine(Reload());
        }
    }

    Coroutine reload_Coroutine;
    public IEnumerator Reload()
    {
        float t = _reloadTime;
        while(t >= 0)
        {

            t -= Time.deltaTime;
            debugTimer = t;
            OnWeaponTimerUpdated?.Invoke(new ValueChange(t, _reloadTime));
            yield return null;
        }
        
        _currentAmmo = _maxAmmo;
        reload_Coroutine = null;
    }

    public bool CanFire()
    {
        if (_shotDelayCoroutine != null)
            return false;

        if (_currentAmmo <= 0)
            return false;

        return true;
    }

    public IEnumerator ShotDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _shotDelayCoroutine = null;
    }
}
