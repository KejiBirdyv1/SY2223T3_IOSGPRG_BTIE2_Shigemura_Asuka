using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Unit
{
    public GameObject bullet;
    public bool onPrimaryWep;
    // public int playerhealth;

    [SerializeField] private Joystick _movementJoystick;
    [SerializeField] private Joystick _aimJoystick;
    private Rigidbody2D _rb2D;

    // private void Awake()
    // {
    //     playerhealth = 100;
    // }

    private void Start()
    {
        base.Initialize("Joey", 100, 10);
        _rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _hasPrimary = false;
            Destroy(_primaryGun);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            _hasSecondary = false;
            Destroy(_secondaryGun);
        }

        Movement();
        Aim();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health health = GetComponent<Health>();
        Health enemyHealth = collision.gameObject.GetComponent<Health>();

        if (health != null && enemyHealth != null)
        {
            health.TakeDamage(10);
        }
    }

    private void Movement()
    {
        _rb2D.velocity = new Vector3(_movementJoystick.Horizontal * _speed, _movementJoystick.Vertical * _speed, 0);
    }

    private void Aim()
    {
        Vector3 moveVector = (Vector3.up * _aimJoystick.Vertical - Vector3.left * _aimJoystick.Horizontal);

        if (_aimJoystick.Horizontal != 0 || _aimJoystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, moveVector);
        }
    }

    public void SetCurrentGun(Gun gun)
    {
        _currentGun = gun;
    }

    public void OnMouseDowner()
    {
        if (onPrimaryWep && _hasPrimary)
        {
            _primaryGun.GetComponent<Gun>().FiringTypePlayer(bullet);
        }
        else if (!onPrimaryWep && _hasSecondary)
        {
            _secondaryGun.GetComponent<Gun>().FiringTypePlayer(bullet);
        }
    }

    public void onMouseUpper()
    {
        if (onPrimaryWep && _hasPrimary)
        {
            _primaryGun.GetComponent<Gun>().StopFire(bullet);
        }
        else if (!onPrimaryWep && _hasSecondary)
        {
            _secondaryGun.GetComponent<Gun>().StopFire(bullet);
        }
    }

    public void reloadPlayer()
    {
        if (onPrimaryWep && _hasPrimary)
        {
            _primaryGun.GetComponent<Gun>().Reload();
        }
        else if (!onPrimaryWep && _hasSecondary)
        {
            _secondaryGun.GetComponent<Gun>().Reload();
        }
    }

    public void swapToPrimary()
    {
        onPrimaryWep = true;
        if (_primaryGun != null)
        {
            _primaryGun.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (_secondaryGun != null)
        {
            _secondaryGun.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void swapToSecondary()
    {
        onPrimaryWep = false;
        if (_primaryGun != null)
        {
            _primaryGun.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (_secondaryGun != null)
        {
            _secondaryGun.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
