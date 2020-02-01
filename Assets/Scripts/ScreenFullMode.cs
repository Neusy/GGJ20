using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFullMode : MonoBehaviour
{

   public void BeFullScreen()
    {
        // Toggle fullscreen
        Screen.fullScreen = !Screen.fullScreen;
    }
}
