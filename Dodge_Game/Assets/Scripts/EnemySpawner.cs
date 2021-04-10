using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyBaseGameObject;
    [SerializeField]
    private int poolSize;

    public float summonTerm = 0.5f;

    public float dirRandomSize = 0.5f;

    public Vector2 direction;

    private List<Enemy> enemyPool = new List<Enemy>();

    public bool isSummonning = true;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            enemyPool.Add(Instantiate(enemyBaseGameObject, transform).GetComponent<Enemy>());
            enemyPool[i].gameObject.SetActive(false);
        }
        StartCoroutine(summonEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator summonEnemy()
    {
        while (true)
        {
            if (!isSummonning)
            {
                KillAll();
                //yield return new WaitForSeconds(summonTerm);
                yield return new WaitForSeconds(3);
                continue;
            }
            else
            {
                for (int i = 0; i < poolSize; i++)
                {
                    if (!enemyPool[i].gameObject.activeSelf)
                    {
                        enemyPool[i].transform.position = transform.position;
                        enemyPool[i].gameObject.SetActive(true);
                        enemyPool[i].StartCoroutine(enemyPool[i].DeathTimer());
                        enemyPool[i].dir = (direction + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * dirRandomSize);
                        yield return new WaitForSeconds(summonTerm);
                    }
                }
                poolSize++;
                enemyPool.Add(Instantiate(enemyBaseGameObject, transform).GetComponent<Enemy>());
                enemyPool[poolSize - 1].transform.position = transform.position;
                enemyPool[poolSize - 1].gameObject.SetActive(true);
                enemyPool[poolSize - 1].StartCoroutine(enemyPool[poolSize - 1].DeathTimer());
                enemyPool[poolSize - 1].dir = (direction + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * dirRandomSize);
                yield return new WaitForSeconds(summonTerm);
            }
        }
    }

    public void KillAll()
    {
        for (int i = 0; i < enemyPool.Count; i++)
        {
            enemyPool[i].gameObject.SetActive(false);
        }
    }
}
