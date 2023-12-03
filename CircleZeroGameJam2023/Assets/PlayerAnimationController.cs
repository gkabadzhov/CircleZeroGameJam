using OTBG.Gameplay.Player.Interfaces;
using OTBG.Gameplay.Player.Movement;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerJump))]
public class PlayerAnimationController : MonoBehaviour, IPlayerInitialisable, IDeathHandler
{
    public Animator _anim;
    private PlayerMovement _playerMovement;
    private PlayerJump _playerJump;

    private Coroutine _PauseGroundCheckCoroutine;

    private bool _isGrounded;
    private bool _isDead;
    private bool _isDashing;
    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerJump = GetComponent<PlayerJump>();

    }

    private void Start()
    {
        SetupEvents();
    }

    public void SetupEvents()
    {
        PlayerDash.OnDashStateChanged += PlayerDash_OnDashStateChanged;
        _playerMovement.OnIdle += PlayerMovement_OnIdle;
        _playerMovement.OnMovement += PlayerMovement_OnMovement;
        _playerMovement.OnGrounded += PlayerMovement_OnGrounded;
        _playerMovement.OnYVelocityChange += PlayerMovement_OnYVelocityChange;
        _playerJump.OnJump += PlayerJump_OnJump;
    }

    private void PlayerDash_OnDashStateChanged(bool obj)
    {
        _isDashing = obj;

        if (_isDashing)
            _anim.Play("DashAnim");
    }

    public void Initialise()
    {
        

        _anim.Play("IdleAnim", -1);
        //_anim.runtimeAnimatorController = slimeData.slimeAnimator;
    }

    private void PlayerMovement_OnYVelocityChange(float yVel)
    {
        if (_isDead || _isDashing) return;
        if (_isGrounded) return;

        if (yVel > 0.1f)
            _anim.Play("JumpAnim");
       

        //_anim.Play("")
        //_anim.SetFloat("yVel", yVel);
    }

    private void PlayerJump_OnJump()
    {
        if (_isDead || _isDashing) return;

        _PauseGroundCheckCoroutine = StartCoroutine(PauseGroundCheck());

        _isGrounded = false;

        //_anim.SetBool("IsGrounded", false);

        _anim.Play("JumpAnim");
    }

    private void PlayerMovement_OnGrounded(bool isGrounded)
    {
        if (_isDead || _isDashing) return;

        if (_PauseGroundCheckCoroutine != null)
            return;

        _isGrounded = isGrounded;
    }

    private void PlayerMovement_OnMovement(int moveDirection)
    {
        if (_isDead || _isDashing) return;
        if (!_isGrounded) return;

        _anim.Play("RunAnim", -1);

    }

    private void PlayerMovement_OnIdle()
    {
        if (_isDead || _isDashing) return;
        if (_PauseGroundCheckCoroutine != null)
            return;
        if (!_isGrounded)
            return;

        _anim.Play("IdleAnim", -1);
    }



    private IEnumerator PauseGroundCheck()
    {
        yield return new WaitForSeconds(0.25f);
        _PauseGroundCheckCoroutine = null;
    }

    private void OnDestroy()
    {
        PlayerDash.OnDashStateChanged -= PlayerDash_OnDashStateChanged;
        _playerMovement.OnIdle -= PlayerMovement_OnIdle;
        _playerMovement.OnMovement -= PlayerMovement_OnMovement;
        _playerMovement.OnGrounded -= PlayerMovement_OnGrounded;
        _playerMovement.OnYVelocityChange -= PlayerMovement_OnYVelocityChange;
        _playerJump.OnJump -= PlayerJump_OnJump;
    }

    public void OnDeath()
    {
        //_anim.ToggleSpritesVisibility(false);
        _isDead = true;
        //_anim.Play("Death");

    }

    public void OnRevive()
    {
        _isDead = false;
        //_anim.ToggleSpritesVisibility(true);
    }
}

