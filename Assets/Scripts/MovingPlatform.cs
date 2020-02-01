using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public float xSpeed = 0f, ySpeed = 0f, rightBoundary = 0f, leftBoundary = 0f, upperBoundary = 0f, bottomBo = 0f;
    bool moveRight = true, moveUp = true;
    float x = 0f, y = 0f;


    void Start() {
        x = transform.position.x;
        y = transform.position.y;
    }

    void Update () {

        if (transform.position.x > rightBoundary + x) 
            moveRight = false;
        if (transform.position.y > upperBoundary + y)
            moveUp = false;
        if (transform.position.x < x - rightBoundary) 
            moveRight = true;
        if (transform.position.y < y - upperBoundary)
            moveUp = true;
    

        if (moveRight)
            transform.position = new Vector2(transform.position.x + xSpeed * Time.deltaTime, transform.position.y);
        else
            transform.position = new Vector2(transform.position.x - xSpeed * Time.deltaTime, transform.position.y);

        if (moveUp)
            transform.position = new Vector2(transform.position.x, transform.position.y + ySpeed * Time.deltaTime);
        else
            transform.position = new Vector2(transform.position.x, transform.position.y - ySpeed * Time.deltaTime);
        
        
    }

}