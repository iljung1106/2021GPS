using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 3;

    private Vector2 dir = new Vector2(0, 0);

    private int health = 5;

    [SerializeField]
    private UIController uiController;

    void Start()
    {
        if(uiController == null)
        {
            uiController = GetComponent<UIController>();
        }
        StartGame();
    }
    public void StartGame()
    {
        uiController.ResetTimer();
        uiController.StartTimer();
        health = 5;
        foreach (EnemySpawner spawner in FindObjectsOfType<EnemySpawner>())
        {
            spawner.isSummonning = true;
            spawner.KillAll();
        }
    }
    private void StopGame()
    {
        transform.position = new Vector3(0, 0, 0);
        uiController.StopTimer();
        uiController.ShowEndScreen();

        foreach (EnemySpawner spawner in FindObjectsOfType<EnemySpawner>())
        {
            spawner.isSummonning = false;
            spawner.KillAll();
        }
        
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int n)
    {
        health = n;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            foreach (EnemySpawner spawner in FindObjectsOfType<EnemySpawner>())
            {
                spawner.isSummonning = false;
                spawner.KillAll();
            }
        }
        Vector3 scaleTmp = transform.localScale;
        dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if(dir.x > 0)
        {
            scaleTmp.x = Mathf.Abs(scaleTmp.x);
        }
        else if (dir.x < 0)
        {
            scaleTmp.x = -Mathf.Abs(scaleTmp.x);
        }
        transform.localScale = scaleTmp;

        if (transform.position.x > 10)
        {
            dir.x = -0.1f;
        }
        if (transform.position.x < -10)
        {
            dir.x = 0.1f;
        }
        if (transform.position.y > 4)
        {
            dir.y = -0.1f;
        }
        if (transform.position.y < -4)
        {
            dir.y = 0.1f;
        }

        transform.position += (Vector3)(dir * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            health -= 1;
            if(health <= 0)
            {
                StopGame();
            }
        }
    }
}
