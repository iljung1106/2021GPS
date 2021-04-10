using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector2 dir = new Vector2(1, 0);
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float lifeTime = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)dir * speed * Time.deltaTime;


        Vector3 scaleTmp = transform.localScale;
        if (dir.x > 0)
        {
            scaleTmp.x = Mathf.Abs(scaleTmp.x);
        }
        else if (dir.x < 0)
        {
            scaleTmp.x = -Mathf.Abs(scaleTmp.x);
        }
        transform.localScale = scaleTmp;
    }

    public IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
            transform.position = new Vector2(100000, 100000);
    }
}
