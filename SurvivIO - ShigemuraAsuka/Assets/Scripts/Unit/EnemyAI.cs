using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private bool enemyNearby;
    private int enemiesDetected;
    private Vector2 randomPosition;
    private Unit unit;
    private GameObject enemyDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Unit>() != null)
        {
            StopCoroutine(ChangePosition());
            enemyDetected = collision.gameObject;
            enemyNearby = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Unit>() != null && enemiesDetected == 0)
        {
            enemiesDetected++;
            enemyDetected = collision.gameObject;
        }

        if (collision.GetComponent<Unit>() != null && enemyDetected != null)
        {
            if (Vector3.Distance(transform.position, enemyDetected.transform.position) < 5)
            {
                unit.Shoot();
            }

            if (Vector3.Distance(transform.position, enemyDetected.transform.position) > 3)
            {
                transform.position = Vector2.MoveTowards(transform.position, enemyDetected.transform.position,
                    unit.GetSpeed() * Time.deltaTime * 0.25f);
            }
            transform.up = enemyDetected.transform.position - transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Unit>() != null)
        {
            enemiesDetected--;
            enemyDetected = null;
            enemyNearby = false;
            Wander();
        }
    }

    private void Start()
    {
        enemiesDetected = 0;
        unit = GetComponent<Unit>();
        Wander();
    }

    private void Update()
    {
        if (!enemyNearby)
        {
            transform.Translate(randomPosition * unit.GetSpeed() * Time.deltaTime);
        }
    }

    public void Wander()
    {
        enemyNearby = false;
        randomPosition = Random.insideUnitCircle;
        StartCoroutine(ChangePosition());
    }

    private IEnumerator ChangePosition()
    {
        while (!enemyNearby)
        {
            yield return new WaitForSeconds(3f);
            randomPosition = Random.insideUnitCircle;
        }
    }
}
