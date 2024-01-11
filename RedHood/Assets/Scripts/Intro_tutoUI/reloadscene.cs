using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reloadscene : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("Slavica Free");
    }


}
