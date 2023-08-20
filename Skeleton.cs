using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skeleton : MonoBehaviour
{
    Player playerScript;

    public GameObject skeletonMessage;

    public TextMeshProUGUI skeletonHealth;

    public float smoothing = 100f;
    public Transform target;

    public Transform bulletSpawnPoint;

    public GameObject bulletPrefab;

    public float bulletSpeed = 0.1f;

    public float Timer;

    public Slider slider6;

    public void SetMaxHealth(int health)
    {
        slider6.maxValue = health;
        slider6.value = health;
    }

    public void SetHealth(int health)
    {
        slider6.value = health;
    }

    public void SetUp()
    {
        skeletonMessage = GameObject.FindObjectOfType<SkeletonMessage>().gameObject;
        skeletonMessage.SetActive(false);
    }

    public void Damage()
    {
        Timer = 1;
        playerScript.slider1.value -= 0.1f;
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Timer <= 0)
            {
                Debug.Log("P");
                Damage();
            }
        }
    }

    private string currentState;

    private string nextState;

    private void SwitchState()
    {
        StartCoroutine(currentState);
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
                var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
                transform.position = Vector3.Lerp(transform.position, target.position, smoothing * Time.deltaTime);
                skeletonHealth.text = slider6.value.ToString();
            } 

            if (slider6.value <= 0.4f)
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
                bulletSpeed = 0.2f;
                var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
                transform.position = Vector3.Lerp(transform.position, target.position, smoothing * Time.deltaTime);
                skeletonHealth.text = slider6.value.ToString();
            }

            if (slider6.value == 0)
            {
                skeletonMessage.SetActive(true);
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
