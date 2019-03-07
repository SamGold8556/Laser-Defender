using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] float moveSpeed = 10f;

    float xMin;
    float xMax;

	// Use this for initialization
	void Start () {

        SetUpMoveBoundaries();		
	}

    private void SetUpMoveBoundaries()
    {
       
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
    }

    // Update is called once per frame
    void Update () {

        Move();
		
	}

    public void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPOS = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPOS = transform.position.y + deltaY;

        transform.position = new Vector2(newXPOS, newYPOS);
    }
}
