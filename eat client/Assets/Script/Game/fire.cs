using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour {
	public float speed = 10.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.x < 0)
		{
			transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
		}
		else
		{
			transform.Translate(new Vector3(speed * Time.deltaTime*-1, 0, 0));
		}
	}
}
