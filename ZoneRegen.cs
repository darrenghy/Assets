using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneRegen : MonoBehaviour
{
    public GameObject regenMessage;

    public Player playerScript;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        regenMessage.SetActive(true);
        playerScript.slider1.value += 0.1f;

        yield return new WaitForSeconds(5);

        regenMessage.SetActive(false);
    }
}