using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject followPlayer;
    public float preloadHorizontalMargin = 0.5f;        // Horizontal units margin to calculate background plane clone movement
    [Range(0, 1)] public float farthest = 1f;           // Maximum parallax factor
    [Range(0, 1)] public float nearest = 0f;            // Minimum parallax factor
    public GameObject[] backgrounds;                    // Array of background planes

    private Camera cam;                                 // Camera reference
    private Vector2 screenBounds;                       // Screen boundaries (size)
    private Vector3 lastCameraPosition;                 // deltaT calculations
    private float parallaxBase;                         // Closest Z
    private float parallaxFactor;                       // Parallax factor

    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));

        // Initial Z-depth setup of the linked objects
        float min = 0, max = 0;
        if (backgrounds.Length > 0) {
            min = backgrounds[0].transform.position.z;
            max = backgrounds[0].transform.position.z;
        }

        // Clone the background planes as necessary, also detect the Z-Depth min-max
        foreach (GameObject obj in backgrounds) {
            CloneBackgroundPlane(obj);
            float z = obj.transform.position.z;
            if (min > z)
                min = z;
            if (max < z)
                max = z;
        }

        // Parallax base and factor
        parallaxBase = min;
        parallaxFactor = Mathf.Clamp01(1 / (max - min));

        lastCameraPosition = transform.position;
    }

    // Calculates the number of planar tiles needed to cover the entire screen at any one point,
    // then clones the original object accordingly and disables the SpriteRenderer for the original.
    void CloneBackgroundPlane(GameObject obj)
    {
        float w = obj.GetComponent<SpriteRenderer>().bounds.size.x;
        int childrenNeeded = (int)Mathf.Ceil(screenBounds.x * 2 / w);
        GameObject clone = Instantiate(obj) as GameObject;
        for (int i = 0; i <= childrenNeeded; i++) {
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(w * i, obj.transform.position.y, obj.transform.position.z);
            c.name = obj.name + i;
        }
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }

    // Dynamically repositions the plane tiles on the X axis, for infinite parallax
    void RepositionBackgroundPlane(GameObject obj)
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        Vector3 p;
        if (children.Length > 1) {
            GameObject firstChild = children[1].gameObject;
            GameObject lastChild = children[children.Length - 1].gameObject;
            float halfObjectWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x;
            if ((transform.position.x + screenBounds.x) > (lastChild.transform.position.x + halfObjectWidth - preloadHorizontalMargin)) {
                // Move the left-most tile to the right
                firstChild.transform.SetAsLastSibling();
                p = firstChild.transform.position;
                p.x = lastChild.transform.position.x + halfObjectWidth * 2;
                firstChild.transform.position = p;
            } else if ((transform.position.x - screenBounds.x) < (firstChild.transform.position.x - halfObjectWidth + preloadHorizontalMargin)) {
                // Move the right-most tile to the left
                lastChild.transform.SetAsFirstSibling();
                p = lastChild.transform.position;
                p.x = firstChild.transform.position.x - halfObjectWidth * 2;
                lastChild.transform.position = p;
            }
        }
    }

    void LateUpdate()
    {
        Vector3 newCameraPosition = transform.position;
        // Update the (temporary) camera position if we're following the player
        if (followPlayer) {
            newCameraPosition.x = followPlayer.transform.position.x;
        }
        foreach(GameObject obj in backgrounds) {
            RepositionBackgroundPlane(obj);
            // Calculate the parallax speed, starting from the Z distribution and applying
            // the min/max movement provided as UI parameters.
            float parallaxSpeed = Mathf.Clamp01(parallaxFactor * (obj.transform.position.z - parallaxBase));
            if (farthest >= nearest) {
                // Regular movement, farthest closest to 1 (being still)
                parallaxSpeed = nearest + parallaxSpeed * (farthest - nearest);
            } else {
                // Inverted movement, nearest closest to 1 (being still)
                parallaxSpeed = nearest - parallaxSpeed * (nearest - farthest);
            }
            float difference = newCameraPosition.x - lastCameraPosition.x;
            obj.transform.Translate(Vector3.right * difference * parallaxSpeed);
        }
        // Update the camera, and its last position
        if (followPlayer)
            transform.position = newCameraPosition;
        lastCameraPosition = transform.position;
    }
}
