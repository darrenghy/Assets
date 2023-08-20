/*
 * Author: 
 * Date: 
 * Description: 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// The class responsible for the player mechanics.
/// </summary>
public class Player : MonoBehaviour
{
    public Enemy enemyScript;
    public Blacksmith blacksmithScript;
    public Snake snakeScript;
    public Skeleton skeletonScript;

    public GameObject SwitchMessage;

    /// <summary>
    /// The Vector3 used to store the WASD input of the user.
    /// </summary>
    Vector3 movementInput = Vector3.zero;

    /// <summary>
    /// The Vector3 used to store the left/right mouse input of the user.
    /// </summary>
    Vector3 rotationInput = Vector3.zero;

    /// <summary>
    /// The movement speed of the player per second.
    /// </summary>
    float moveSpeed = 5f;

    //int playerHealth = 0;

    /// <summary>
    /// The speed at which the player rotates
    /// </summary>
    float rotationSpeed = 30f;

    Vector3 headRotationInput;

    public GameObject playerCamera;

    public float Timer;

    public float Timer1;

    public bool SprintKey = false;

    public Slider slider;

    public Slider slider1;

    public TextMeshProUGUI playerHealthtext;

    public void SetMaxHealth(int health)
    {
        slider1.maxValue = health;
        slider1.value = health;
    }

    public void SetMaxStamina(int stamina)
    {
        slider.maxValue = stamina;
        slider.value = stamina;
    }

    float interactionDistance = 10f;

    bool interact = false;

    public void SetHealth(int health)
    {
        slider1.value = health;
    }

    public void SetStamina(int stamina)
    {
        slider.value = stamina;
    }


    public void DoDamage()
    {
        Timer1 = 0.2f;
        enemyScript.slider3.value -= 0.1f;
        Debug.Log("1");
    }

    public void DoDamage1()
    {
        Timer1 = 0.2f;
        blacksmithScript.slider4.value -= 0.1f;
        Debug.Log("1");
    }

    public void DoDamage2()
    {
        Timer1 = 0.2f;
        snakeScript.slider5.value -= 0.1f;
        Debug.Log("Work");
    }

    public void DoDamage3()
    {
        Timer1 = 0.2f;
        skeletonScript.slider6.value -= 0.1f;
        Debug.Log("Work");
    }

    public void SetUp()
    {
        playerHealthtext = GameObject.FindObjectOfType<FindScoreText>().GetComponent<TextMeshProUGUI>();
    }


        // Start is called before the first frame update
        void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        var movementSpeed = moveSpeed;

        if (SprintKey)
        {
            movementSpeed = 80;
            slider.value -= 0.2f * Time.deltaTime;
        }

        else
        {
            if (Timer <= 0)
            {
                Timer = 1;
                slider.value += 1f * Time.deltaTime;
            }
        }

        playerHealthtext.text = slider1.value.ToString();

        // Create a new Vector3
        Vector3 movementVector = Vector3.zero;

        // Add the forward direction of the player multiplied by the user's up/down input.
        movementVector += transform.forward * movementInput.y;

        // Add the right direction of the player multiplied by the user's right/left input.
        movementVector += transform.right * movementInput.x;

        // Apply the movement vector multiplied by movement speed to the player's position.
        transform.position += movementVector * movementSpeed * Time.deltaTime;

        // Apply the rotation multiplied by the rotation speed.
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + rotationInput * rotationSpeed * Time.deltaTime);
        playerCamera.transform.rotation = Quaternion.Euler(playerCamera.transform.rotation.eulerAngles + headRotationInput * rotationSpeed * Time.deltaTime);

        Debug.DrawLine(playerCamera.transform.position, playerCamera.transform.position + (playerCamera.transform.forward * interactionDistance));
        RaycastHit hitInfo;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitInfo, interactionDistance))
        {
            Debug.Log(hitInfo.transform.name);
            if (hitInfo.transform.tag == "Enemy")
            {
                if (interact)
                {
                    // Increase the player score.
                    if (Timer1 <= 0)
                    {
                        Debug.Log(Timer1);
                        enemyScript = hitInfo.transform.GetComponent<Enemy>();
                        DoDamage();
                    }
                }
            }

            if (hitInfo.transform.tag == "Sword")
            {
                if (interact)
                {
                    Debug.Log(gameObject);
                    blacksmithScript = hitInfo.transform.GetComponent<Blacksmith>();
                    blacksmithScript.blacksmithMessage.SetActive(true);
                    Destroy(gameObject);

                }
            }

            if (hitInfo.transform.tag == "Switch")
            {
                if (interact)
                {
                    if(blacksmithScript.slider4.value > 0)
                    {
                        SwitchMessage.SetActive(true);
                    }
                }

                else
                {
                    hitInfo.transform.GetComponent<Switch>().Interact();
                }
            }

                if (hitInfo.transform.tag == "Enemy1")
            {
                if (interact)
                {
                    
                    if (Timer1 <= 0)
                    {
                        Debug.Log(Timer1);
                        blacksmithScript = hitInfo.transform.GetComponent<Blacksmith>();
                        DoDamage1();
                    }
                }
            }

            if (hitInfo.transform.tag == "Enemy2")
            {
                if (interact)
                {

                    if (Timer1 <= 0)
                    {
                        Debug.Log(Timer1);
                        snakeScript = hitInfo.transform.GetComponent<Snake>();
                        DoDamage2();
                    }
                }
            }

            if (hitInfo.transform.tag == "Enemy3")
            {
                if (interact)
                {

                    if (Timer1 <= 0)
                    {
                        Debug.Log(Timer1);
                        skeletonScript = hitInfo.transform.GetComponent<Skeleton>();
                        DoDamage3();
                    }
                }
            }
        }

        interact = false;

        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
        }

        if (Timer1 > 0)
        {
            Timer1 -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Called when the Look action is detected.
    /// </summary>
    /// <param name="value"></param>
    void OnLook(InputValue value)
    {
        rotationInput.y = value.Get<Vector2>().x;
        headRotationInput.x = value.Get<Vector2>().y * -1;
    }

    /// <summary>
    /// Called when the Move action is detected.
    /// </summary>
    /// <param name="value"></param>
    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    void OnSprint(InputValue value)
    {
        SprintKey = value.Get<float>() == 1f;
        Debug.Log(SprintKey);
    }

    void OnFire()
    {
        Debug.Log("t");
        interact = true;
    }
}