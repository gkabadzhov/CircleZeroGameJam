using OTBG.Gameplay.Player;
using OTBG.Gameplay.Player.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MissileAttack : MonoBehaviour
{
    public event Action<Vector2> OnWeaponFired;
    public GameObject projectilePrefab;
    public int _abilityLevel = 1;

    public PlayerMovement player;

    private Coroutine _shotDelayCoroutine;
    [SerializeField]
    private float _shotDelay;
    [SerializeField]
    private float _reloadDelay;
    [SerializeField]
    private List<Transform> spawnLocations = new List<Transform>();

    private int _fullMagazine = 2;
    private int _magazineCounter = 0;
    
    private bool reloadFlag;

    private void Awake()
    {
        player.OnGrounded += Player_OnGrounded;
    }

    private void Player_OnGrounded(bool obj)
    {
        if (obj == false)
            return;
        if (_magazineCounter <= 0)
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
            if (reloadFlag) 
            { 
                _shotDelayCoroutine = StartCoroutine(ShotDelay(_reloadDelay));
            } else
            {
                _shotDelayCoroutine = StartCoroutine(ShotDelay(_shotDelay));
            }
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
        GameObject projectileInstance;        

        switch(_abilityLevel)
        {
            case 1:
                for (int i = 0; i < spawnLocations.Count; i++)
                {
                    projectileInstance = Instantiate(projectilePrefab, spawnLocations[i].position, spawnLocations[i].rotation) as GameObject;
                    projectileInstance.GetComponent<Bullet>().BulletInit(spawnLocations[i].right);
                }
                _shotDelay = 0.2f;
                ReduceAmmo();
                break;
            case 2:
                for (int i = 0; i < spawnLocations.Count; i++)
                {
                    projectileInstance = Instantiate(projectilePrefab, spawnLocations[i].position, spawnLocations[i].rotation) as GameObject;
                    projectileInstance.GetComponent<Bullet>().BulletInit(spawnLocations[i].right);
                }
                _shotDelay = 0.2f;
                break;
            case 3:
                projectileInstance = Instantiate(projectilePrefab, spawnLocations[0].position, spawnLocations[0].rotation) as GameObject;
                projectileInstance.GetComponent<Bullet>().BulletInit(spawnLocations[0].right);

                _shotDelay = 0.1f;

                break;
            default:
                Debug.Log("incorrect ability value");
                break;
        }

        OnWeaponFired?.Invoke(CalculateDirectionVector());
    }

    public void ReduceAmmo()
    {
        _magazineCounter--;
        

    }

    public void Reload()
    {
        _magazineCounter = _fullMagazine;
    }

    private void ShootLevelOne()
    {

    }

    public bool CanFire()
    {
        if (_shotDelayCoroutine != null)
            return false;

        if (_magazineCounter <= 0)
            return false;

        return true;
    }

    public IEnumerator ShotDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _shotDelayCoroutine = null;
    }
}
