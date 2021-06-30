using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private float speed;
	static public bool location;
	public int num;
	public bool ready;
	public float hp;
	public GameObject bullet;
	public bool game_start;
	public float m_width;
	public float m_height;

	void OnEnable()
	{
		m_width = Screen.width;
		m_height = Screen.height;
		hp = 100.0f;
		game_start = false;
		speed = 6.0f;
		float x = Random.Range(-9, 9);
		float z = Random.Range(-9, 9);
		gameObject.transform.position = new Vector3(x, 0.5f, z);
	}


	public void SetPosition(float x)
	{
		hp = 10.0f;
		if (x < 0)
		{
			location = true;
		}
		else
		{
			location = false;
		}
		game_start = false;
	}

	private void OnDestroy()
	{
		gameObject.SetActive(false);
	}
	IEnumerator Bullet_time()
	{
		yield return new WaitForSeconds(0.5f);
		StopCoroutine("Bullet_time");
	}
	public void Collsion()
	{
		hp -= 20;
	}
	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Translate(Vector3.left * speed * Time.deltaTime);
			PacketMove_REQ packet = new PacketMove_REQ();
			packet.x = transform.position.x;
			packet.y = transform.position.y;
			packet.z = transform.position.z;
			packet.rx = transform.eulerAngles.x;
			packet.ry = transform.eulerAngles.y;
			packet.rz = transform.eulerAngles.z;
						
			NetWork_s.Instance.ProcessSend(NetWork_s.Instance.structToByte((object)packet)); Debug.Log(transform.position.x + " " + transform.position.y + " " + transform.position.z);

		}

		if (Input.GetKey(KeyCode.RightArrow))
		{
			transform.Translate(Vector3.right * speed * Time.deltaTime);

			PacketMove_REQ packet = new PacketMove_REQ();
			packet.x = transform.position.x;
			packet.y = transform.position.y;
			packet.z = transform.position.z;
			packet.rx = transform.eulerAngles.x;
			packet.ry = transform.eulerAngles.y;
			packet.rz = transform.eulerAngles.z;
			NetWork_s.Instance.ProcessSend(NetWork_s.Instance.structToByte((object)packet)); Debug.Log(transform.position.x + " " + transform.position.y + " " + transform.position.z);
		}

		if (Input.GetKey(KeyCode.UpArrow))
		{
			transform.Translate(Vector3.forward * speed * Time.deltaTime);

			PacketMove_REQ packet = new PacketMove_REQ();
			packet.x = transform.position.x;
			packet.y = transform.position.y;
			packet.z = transform.position.z;
			packet.rx = transform.eulerAngles.x;
			packet.ry = transform.eulerAngles.y;
			packet.rz = transform.eulerAngles.z;
			NetWork_s.Instance.ProcessSend(NetWork_s.Instance.structToByte((object)packet)); Debug.Log(transform.position.x + " " + transform.position.y + " " + transform.position.z);
		}

		if (Input.GetKey(KeyCode.DownArrow))
		{
			transform.Translate(Vector3.back * speed * Time.deltaTime);

			PacketMove_REQ packet = new PacketMove_REQ();
			packet.x = transform.position.x;
			packet.y = transform.position.y;
			packet.z = transform.position.z;
			packet.rx = transform.eulerAngles.x;
			packet.ry = transform.eulerAngles.y;
			packet.rz = transform.eulerAngles.z;
			NetWork_s.Instance.ProcessSend(NetWork_s.Instance.structToByte((object)packet));			
		}
	}
}