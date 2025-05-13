using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    private const string _attackTrigger = "SwordAttack";
    private const string _facingY = "lastY";
    private const string _facingX = "lastX";

    [SerializeField] private WeaponInfo weaponInfo;

    private Animator _playerAnimator;
    public bool IsAttacking;

    private void Awake()
    {
        _playerAnimator = PlayerController.Instance.GetComponent<Animator>();
    }

    private void Update()
    {

    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    public void Attack()
    {
        if ((weaponInfo.requireEnergy == true && Energy.Instance.TryUseEnergy(weaponInfo.energyCost))
            || weaponInfo.requireEnergy == false)
        {
            IsAttacking = true;
            _playerAnimator.SetTrigger(_attackTrigger);

            if (weaponInfo.directive)
            {
                if (PlayerController.Instance.Direction() != Vector2.zero)
                {
                    if (Mathf.Abs(PlayerController.Instance.Direction().y) >= Mathf.Abs(PlayerController.Instance.Direction().x))
                    {
                        if (PlayerController.Instance.Direction().y > 0)
                        {
                            transform.eulerAngles = new Vector3(0, 0, 90);
                        }
                        else
                        {
                            transform.eulerAngles = new Vector3(0, 0, 270);
                        }
                    }
                    else
                    {
                        if (PlayerController.Instance.Direction().x > 0)
                        {
                            transform.eulerAngles = new Vector3(0, 0, 0);
                        }
                        else
                        {
                            transform.eulerAngles = new Vector3(0, 0, 180);
                        }
                    }
                }
                else
                {
                    if (Mathf.Abs(_playerAnimator.GetFloat(_facingY)) >= Mathf.Abs(_playerAnimator.GetFloat(_facingX)))
                    {
                        if (_playerAnimator.GetFloat(_facingY) >= 0)
                        {
                            transform.eulerAngles = new Vector3(0, 0, 90);
                        }
                        else
                        {
                            transform.eulerAngles = new Vector3(0, 0, 270);
                        }
                    }
                    else
                    {
                        if (_playerAnimator.GetFloat(_facingX) >= 0)
                        {
                            transform.eulerAngles = new Vector3(0, 0, 0);
                        }
                        else
                        {
                            transform.eulerAngles = new Vector3(0, 0, 180);
                        }
                    }
                }
            }
        }
    }

    public abstract void StartAttack();
    public abstract void DoneAttack();
}
