using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDesign : MonoBehaviour
{
    public static LevelDesign instance;

    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private GameObject BossPrefab;
    public List<Unit> units;

    [SerializeField] private List<GameObject> ammoPickupPrefabs;
    [SerializeField] private List<GameObject> weaponPickupPrefabs;
    [SerializeField] public List<Pickups> pickups;

    private float spawnCollisionCheckradius;

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
    {    //Start at because player counts toward this value
        spawnCollisionCheckradius = 1;
        SpawnEnemies(23, EnemyPrefab, "Enemy", 100, 5);
        SpawnEnemies(1, BossPrefab, "Big Boss", 200, 3);
        SpawnPickups(50);
        UIManager.instance.UpdateRemaining(units.Count);
    }

    private void SpawnEnemies(int count, GameObject prefab, string name, int maxHealth, float speed)
    {
        float randomX;
        float randomY;
        Vector3 randomPosition;

        for (int i = 0; i < count; i++)
        {
            randomX = Random.Range(-49.5f, 49.5f);
            randomY = Random.Range(-37, 37);
            randomPosition = new Vector3(randomX, randomY, 0);

            if (!Physics2D.OverlapCircle(randomPosition, spawnCollisionCheckradius))
            {
                GameObject enemyGO = Instantiate(prefab, randomPosition, Quaternion.identity);
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
        float randomX;
        float randomY;
        Vector3 randomPosition;

        for (int i = 0; i < count; i++)
        {
            pickUpChance = Random.Range(1f, 100f);
            randomX = Random.Range(-49.5f, 49.5f);
            randomY = Random.Range(-37, 37);
            randomPosition = new Vector3(randomX, randomY, 0);
            Weapon weapon = (Weapon)Random.Range(0, 3);

            if (pickUpChance > 30f)
            {
                if (!Physics2D.OverlapCircle(randomPosition, spawnCollisionCheckradius))
                {
                    GameObject pickupGO = Instantiate(ammoPickupPrefabs[(int)weapon], randomPosition, Quaternion.identity);
                    pickupGO.transform.parent = transform;

                    Pickups pickup = pickupGO.GetComponent<Pickups>();
                    pickups.Add(pickup);

                    pickup.Initialize(weapon);
                }
                else
                {
                    i--;
                }
            }
            else
            {
                if (!Physics2D.OverlapCircle(randomPosition, spawnCollisionCheckradius))
                {
                    GameObject pickupGO = Instantiate(weaponPickupPrefabs[(int)weapon], randomPosition, Quaternion.identity);
                    pickupGO.transform.parent = transform;

                    Pickups pickup = pickupGO.GetComponent<Pickups>();
                    pickups.Add(pickup);

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
        pickups.Remove(pickup);
    }

    public void DecreaseUnitCount(Unit unit)
    {
        units.Remove(unit);
        UIManager.instance.UpdateRemaining(units.Count);
        if (units.Count == 1 && units[0].GetComponent<Player>() != null)
        {
            ShowWinScreen();
        }
    }

    public void ShowWinScreen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}