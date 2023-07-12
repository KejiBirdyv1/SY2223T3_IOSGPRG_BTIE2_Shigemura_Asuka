using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public class Unit : MonoBehaviour
{
    [SerializeField] protected string _name;                   // The name of the unit.
    [SerializeField] private Health _health;
    [SerializeField] protected float _speed;
    [SerializeField] protected Gun _currentGun;

    [SerializeField] private GameObject _enemyHealthBar;
    [SerializeField] private Slider _enemyHpSlider;

    public GameObject _primaryGun;
    public GameObject _secondaryGun;

    public bool _hasPrimary;
    public bool _hasSecondary;
  
    public GameObject _handle;

    public void Initialize(string name, int maxHealth, float speed)
    {
        _name = name;
        _health = gameObject.GetComponent<Health>();
        _health.Initialize(maxHealth);
        _speed = speed;
    }

    protected void HealthBar()
    {
        GameObject healthBar = Instantiate(_enemyHealthBar, transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform);
        _enemyHpSlider = healthBar.transform.GetChild(0).GetComponentInChildren<Slider>();
    }

    protected void ManageHealth()
    {
        _enemyHpSlider.value = (float)_health.CurrentHealth / (float)_health.MaxHealth;
    }
}
