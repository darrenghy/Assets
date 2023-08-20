using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneBlacksmith : MonoBehaviour
{
    public GameObject zoneMessage;

    public Blacksmith blacksmithScript;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        zoneMessage.SetActive(true);
        blacksmithScript.enabled = true;

        yield return new WaitForSeconds(5);

        zoneMessage.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
