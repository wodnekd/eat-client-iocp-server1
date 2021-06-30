using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crash : MonoBehaviour {

	private Score sc;
	void Start()
	{
		sc = GameObject.Find("Canvas").transform.Find("Text").GetComponent<Score>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "enemy")
		{
			sc.AddScore_And_PrintScore(10);
			Destroy(other.gameObject);
		}
	}

}
