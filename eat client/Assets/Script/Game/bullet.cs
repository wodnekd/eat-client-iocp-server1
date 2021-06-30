using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

	public float speed = 30.0f;
	public GameObject BulletObject;
	public GameObject spawnPoint;



	bool canFire;
	// Use this for initialization
	void Start () {
		canFire = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("space") && canFire)
		{
			StartCoroutine("MakeMissile");
		}

	}


	IEnumerator MakeMissile()
	{
		canFire = false;
		//미사일생성
		Instantiate(BulletObject, spawnPoint.transform.position, Quaternion.identity);
		yield return new WaitForSeconds(1.0f); //0.2초간 실행을 보류한다.
		canFire = true; //canFire값 true로 변경
	}

}
