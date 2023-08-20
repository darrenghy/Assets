using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Blacksmith : MonoBehaviour
{
    Player playerScript;
    Enemy enemyScript;

    public float smoothing = 1f;
    public Transform target;

    public float Timer;

    public GameObject blacksmithMessage;

    public TextMeshProUGUI blacksmithHealth;

    //public int blacksmithHealth = slider4.value;

    public Slider slider4;

    public void SetMaxHealth(int health)
    {
        slider4.maxValue = health;
        slider4.value = health;
    }

    public void SetHealth(int health)
    {
        slider4.value = health;
    }

    public GameObject Sword;


    public void Damage()
    {
        Timer = 1;
        playerScript.slider1.value -= 0.01f;
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Timer <= 0)
            {
                Damage();
            }
        }
    }

    private string currentState;

    private string nextState;

    private void SwitchState()
    {
        StartCoroutine(currentState);
        Debug.Log(currentState);
        
    }

    public void SetUp()
    {
        blacksmithMessage = GameObject.FindObjectOfType<BlacksmithMessage>().gameObject;
        blacksmithMessage.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = "MyCoroutine";
        nextState = currentState;
        playerScript = GameObject.FindObjectOfType<Player>();
        Debug.Log(playerScript == null);
        SwitchState();
    }


    IEnumerator MyCoroutine()
    {
        bool condition = true;
        while (condition)
        {
            if (Vector3.Distance(transform.position, target.position) > 0.05f)
            {
                transform.position = Vector3.Lerp(transform.position, target.position, smoothing * Time.deltaTime);
                blacksmithHealth.text = slider4.value.ToString();
            }

            if (slider4.value <= 0.4f)
            {
                nextState = "MyCoroutineC";
                condition = false;
            }
            yield return new WaitForEndOfFrame();

        }
        SwitchState();
    }

    IEnumerator MyCoroutineC()
    {
        bool condition = true;
        while (condition)
        {
            if (Vector3.Distance(transform.position, target.position) > 0.05f)
            {
                smoothing = 3;
                transform.position = Vector3.Lerp(transform.position, target.position, smoothing * Time.deltaTime);
                blacksmithHealth.text = slider4.value.ToString();
            }

            if (slider4.value == 0f)
            {
                blacksmithMessage.SetActive(true);
                Instantiate(Sword, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            yield return null;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (nextState != currentState)
        {
            currentState = nextState;
        }

        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
    }
}

