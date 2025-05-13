using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    private const string _horizontal = "moveX";
    private const string _vertical = "moveY";
    private const string _lastHorizontal = "lastX";
    private const string _lastVertical = "lastY";

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer myTrailRenderer;

    private PlayerControls _playerControls;
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRender;
    private Knockback _knockback;
    private ActiveWeapon _weapon;
    private float _startingMoveSpeed;

    //private bool _canMove = true; 
    private bool _isDashing = false;
    //private bool _isAttacking = false;

    public Vector2 Direction()
    {
        return _movement;
    }

    protected override void Awake() {
        base.Awake();

        _playerControls = new PlayerControls();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRender = GetComponent<SpriteRenderer>();
        _knockback = GetComponent<Knockback>();
      //  _weapon = GetComponent<ActiveWeapon>();
    }

    private void Start() {
        _playerControls.Combat.Dash.performed += _ => Dash();

        _startingMoveSpeed = moveSpeed;

        //ActiveInventory.Instance.EquipStartingWeapon();
    }

    private void OnEnable() {
        _playerControls.Enable();
    }

    private void OnDisable() {
        _playerControls.Disable();
    }

    private void Update() {
        PlayerInput();
    }

    private void FixedUpdate() {
        Move();
    }

    private void PlayerInput() {
        _movement = _playerControls.Movement.Move.ReadValue<Vector2>();

        _animator.SetFloat(_horizontal, _movement.x);
        _animator.SetFloat(_vertical, _movement.y);

    }

    private void Move() {
        if (_knockback.GettingKnockedBack || PlayerHealth.Instance.IsDead
           // || _weapon.CurrentActiveWeapon.IsAttacking
            ) { return; }

        _rb.MovePosition(_rb.position + _movement * (moveSpeed * Time.fixedDeltaTime));

        if (_movement != Vector2.zero)
        {
            _animator.SetFloat(_lastHorizontal, _movement.x);
            _animator.SetFloat(_lastVertical, _movement.y);
        }
    }

    private void Dash() {
        if (!_isDashing && Stamina.Instance.CurrentStamina > 0) {
            Stamina.Instance.UseStamina();
            _isDashing = true;
            moveSpeed *= dashSpeed;
            myTrailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine() {
        float dashTime = .2f;
        float dashCD = .25f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = _startingMoveSpeed;
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        _isDashing = false;
    }
}
