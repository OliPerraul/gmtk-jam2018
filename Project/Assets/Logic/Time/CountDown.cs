using System.Collections;
using UnityEngine.UI;
using UnityEngine;



public class CountDown : MonoBehaviour
{
    public bool loop = false;

  //  UnityEvent


    public float timeLeft = 300.0f;
    public bool stop = true;


    float choosenTime = 0;

    private float minutes;
    private float seconds;

    public Text text;

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    public void startTimer(float withTime)
    {
        choosenTime = withTime;
        stop = false;
        timeLeft = withTime;
        Update();
        StartCoroutine(updateCoroutine());
    }


    void Update()
    {
        if (stop) return;
        timeLeft -= Time.deltaTime;

        minutes = Mathf.Floor(timeLeft / 60);
        seconds = timeLeft % 60;
        if (seconds > 59) seconds = 59;
        if (minutes < 0)
        {
            stop = true;
            minutes = 0;
            seconds = 0;

            if (loop)
            {
                startTimer(choosenTime);
            }
        }
        //        fraction = (timeLeft * 100) % 100;
    }

    private IEnumerator updateCoroutine()
    {
        while (!stop)
        {
          //  text.text = string.Format("{0:0}:{1:00}", minutes, seconds);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void Kill()
    {
        Object.Destroy(gameObject);
    }

    public static CountDown Create()
    {

        GameObject obj = Object.Instantiate(NSGame.Resources.Instance.countDown);
        CountDown countDown = obj.GetComponent<CountDown>();
        return countDown;
    }

}