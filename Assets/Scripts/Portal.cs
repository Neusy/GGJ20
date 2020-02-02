using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D coll) {
        SceneManager.LoadScene("Credits");
    }
}