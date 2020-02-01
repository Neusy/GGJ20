using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeScheme : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Coroutine());
    }

    IEnumerator Coroutine()
    {
      
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainMenu");
    }
    void Upload()
    {

    }
}
