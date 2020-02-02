using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeScheme : MonoBehaviour
{
    public int waitsTime;

    void Start()
    {
        StartCoroutine(Coroutine());
    }

    IEnumerator Coroutine()
    {
      
        yield return new WaitForSeconds(waitsTime);
        SceneManager.LoadScene("MainMenu");
    }
    void Upload()
    {

    }
}
