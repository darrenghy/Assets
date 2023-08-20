using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Switch : MonoBehaviour
{
    /// <summary>
    /// The index of the scene to load to.
    /// </summary>
    public int sceneToLoad;

    /// <summary>
    /// The interact function called by the player.
    /// </summary>
    public void Interact()
    {
        Debug.Log(gameObject.name + " interacted");
        // use the SceneManager to load the specified scene index.
        SceneManager.LoadScene(sceneToLoad);
    }
}
