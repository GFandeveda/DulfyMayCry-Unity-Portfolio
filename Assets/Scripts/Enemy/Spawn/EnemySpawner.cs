using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private BarrierController barrier;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform[] spawnPoints;

    private bool activated = false;
    private bool arenaFinished = false;
    private Collider triggerCollider;


    private List<GameObject> aliveEnemies = new List<GameObject>();


    private void Awake()
    {
        triggerCollider = GetComponent<Collider>();
    }
    private void Start()
    {
        barrier.OnBarrierClosed += SpawnEnemies;
    }

    public void SpawnEnemies()
    {
       

        foreach (Transform point in spawnPoints)
        {
            int randomEnemy = Random.Range(0, enemies.Length);

            GameObject enemy = Instantiate(
                enemies[randomEnemy],
                point.position,
                point.rotation
            );

            aliveEnemies.Add(enemy);

           
        }

       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activated) return;
        if (!other.CompareTag("Player")) return;

        activated = true;

        triggerCollider.enabled = false;

        barrier.CloseBarrier();
    }

    
    public void EnemyDied(GameObject enemy)
    {
        aliveEnemies.Remove(enemy);

        if (aliveEnemies.Count == 0 && !arenaFinished)
        {
            arenaFinished = true;
            StartCoroutine(OpenBarrierDelay());
        }
    }
    private IEnumerator OpenBarrierDelay()
    {
        yield return new WaitForSeconds(2f);

        barrier.OpenBarrier();
    }
}