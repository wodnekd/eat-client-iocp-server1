using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour {

	public float timerDel;
	public float timerDelLim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		timerDel += Time.deltaTime;
		if (timerDel > timerDelLim)
		{
			Destroy(gameObject);
		}
	}

}
