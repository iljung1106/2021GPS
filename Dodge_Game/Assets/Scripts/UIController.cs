using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private UnityEngine.UI.Text healthText;
    [SerializeField]
    private UnityEngine.UI.Text timerText;

    private float timer = 0f;

    private bool isTimerGoing = false;

    [SerializeField]
    private GameObject endScreen;

    // Start is called before the first frame update
    void Start()
    {
        if(player==null)
        {
            player = gameObject.GetComponent<Player>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "체력 : " + player.GetHealth().ToString();
        if(isTimerGoing)
        {
            timer += Time.deltaTime;
            timerText.text = ((int)timer / 60).ToString() + ":" + ((int)timer % 60).ToString();
        }
    }

    public void StartTimer()
    {
        isTimerGoing = true;
    }

    public void StopTimer()
    {
        isTimerGoing = false;
    }

    public void ResetTimer()
    {
        timer = 0;
    }
    public void ShowEndScreen()
    {
        endScreen.SetActive(true);
    }
}
