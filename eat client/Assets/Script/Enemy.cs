using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public GameObject bullet;
	public GameObject prefab_bullet;
	static public float hp;
	static public bool location;
	public bool game_start;
	public float m_width;
	public float m_height;

	void OnEnable()
	{
		hp = 100.0f;
		game_start = false;
		m_width = Screen.width;
		m_height = Screen.height;
	}
	private void OnDisable()
	{
		game_start = false;
	}

	public void SetPosition(float x)
	{
		hp = 100.0f;
	}
	// Use this for initialization
	void Start () {

	}

	public void State_func(PacketMove_ACK packet)
	{
		if (!gameObject.activeSelf) gameObject.SetActive(true);

		transform.position = new Vector3(packet.x, packet.y, packet.z);
		transform.rotation = Quaternion.Euler(packet.rx, packet.ry, packet.rz);
		Debug.Log(transform.position.x + " " + transform.position.y + " " + transform.position.z);
	}
	// Update is called once per frame
	void Update () {

	
	
	}
	
}
