using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesign : MonoBehaviour
{
    public static LevelDesign instance;

    [SerializeField] private GameObject _rangedEnemyPrefab;
    [SerializeField] private GameObject _bossEnemyPrefab;
    public List<Unit> units;

    [SerializeField] private List<GameObject> _ammoPickupPrefabs;
    [SerializeField] private List<GameObject> _weaponPickupPrefabs;
    [SerializeField] public List<Pickups> _pickups;

    private float _spawnCollisionCheckradius;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Start at because player counts toward this value
        _spawnCollisionCheckradius = 1;
        SpawnEnemies(23, _rangedEnemyPrefab, "Arthur Ranged", 100, 6);
        SpawnEnemies(1, _bossEnemyPrefab, "Arthur Boss", 200, 4);
        SpawnPickups(50);
    }

    private void SpawnEnemies(int count, GameObject prefab, string name, int maxHealth, float speed)
    {
        float _randomX;
        float _randomY;
        Vector3 _randomPosition;

        for (int i = 0; i < count; i++)
        {
            _randomX = Random.Range(-95, 95);
            _randomY = Random.Range(-45, 45);
            _randomPosition = new Vector3(_randomX, _randomY, 0);

            if (!Physics2D.OverlapCircle(_randomPosition, _spawnCollisionCheckradius))
            {
                GameObject enemyGO = Instantiate(prefab, _randomPosition, Quaternion.identity);
                enemyGO.transform.parent = transform;

                Unit unit = enemyGO.GetComponent<Unit>();
                unit.Initialize(name, maxHealth, speed);
                units.Add(unit);
            }
            else
            {
                i--;
            }
        }
    }

    private void SpawnPickups(int count)
    {
        float pickUpChance;
        float _randomX;
        float _randomY;
        Vector3 _randomPosition;

        for (int i = 0; i < count; i++)
        {
            pickUpChance = Random.Range(1f, 100f);
            _randomX = Random.Range(-95, 95);
            _randomY = Random.Range(-45, 45);
            _randomPosition = new Vector3(_randomX, _randomY, 0);
            Weapon weapon = (Weapon)Random.Range(0, 3);

            if (pickUpChance > 30f)
            {
                if (!Physics2D.OverlapCircle(_randomPosition, _spawnCollisionCheckradius))
                {
                    GameObject pickupGO = Instantiate(_ammoPickupPrefabs[(int)weapon], _randomPosition, Quaternion.identity);
                    pickupGO.transform.parent = transform;

                    Pickups pickup = pickupGO.GetComponent<Pickups>();
                    _pickups.Add(pickup);

                    pickup.Initialize(weapon);
                }
                else
                {
                    i--;
                }
            }
            else
            {
                if (!Physics2D.OverlapCircle(_randomPosition, _spawnCollisionCheckradius))
                {
                    GameObject pickupGO = Instantiate(_weaponPickupPrefabs[(int)weapon], _randomPosition, Quaternion.identity);
                    pickupGO.transform.parent = transform;

                    Pickups pickup = pickupGO.GetComponent<Pickups>();
                    _pickups.Add(pickup);

                    pickup.Initialize(weapon);
                    pickup.isWeaponPickup = true;
                }
                else
                {
                    i--;
                }
            }
        }
    }

    public void RemovePickupFromList(Pickups pickup)
    {
        _pickups.Remove(pickup);
    }
}
