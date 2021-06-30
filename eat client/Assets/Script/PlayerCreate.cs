using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCreate : MonoBehaviour {
	private int m_width = 0;
	private int m_height = 0;
	public GameObject player;
	public GameObject enemy;
	bool click = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
	private void OnGUI()
	{
		if(click)
		{ 
			if (GUI.Button(new Rect(m_width / 3, m_height / 6, 300, 100), "Ready"))
			{
				StartCoroutine("Player_Creat");
				Instantiate(player, transform.position, Quaternion.identity);
				Instantiate(enemy,new Vector3(4,1,0), Quaternion.identity);
				click = false;
			}
		}
	}

	IEnumerator Player_Creat()
	{
		//PacketReady_REQ packet = new PacketReady_REQ();
		//packet.ready = true;
//		NetWork_s.Instance.ProcessSend(NetWork_s.Instance.structToByte((object)packet));

		yield return new WaitForSeconds(2.0f);
		//      if(!room_state)
		//       {
		//          Debug.Log("Join Fail");
		//          StopCoroutine("Lobby_join");
		//      }
		//      else
	}
}
