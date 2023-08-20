using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Player playerScript;
    public Transform target;
    public Enemy enemyScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        enemyScript.Damage();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
