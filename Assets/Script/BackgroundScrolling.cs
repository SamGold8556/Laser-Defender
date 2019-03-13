using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour {

    [SerializeField] float backgroundScrollingSpeed = .5f;
    Material myMaterials;
    Vector2 offset;

	// Use this for initialization
	void Start () {
        myMaterials = GetComponent<Renderer>().material;
        offset = new Vector2(0f, backgroundScrollingSpeed);
	}
	
	// Update is called once per frame
	void Update () {
        myMaterials.mainTextureOffset += offset * Time.deltaTime;
	}
}
