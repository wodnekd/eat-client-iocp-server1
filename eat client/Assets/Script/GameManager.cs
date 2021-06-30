using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public GameObject Win_Score;
	public GameObject Lose_Score;
	public GameObject player;
	public GameObject enemy;
	private Player py;
	private Enemy ey;
	private Score score_total;
	//private Button ready;
	//private Button start;
	float m_width;
	float m_height;
	
	public bool enemystart = false;
	public bool game_ready = false;
	public bool game_start = false;
	public static bool ready_state = false;
	// Use this for initialization
	void Start () {
		m_width = Screen.width;
		m_height = Screen.height;
		player.SetActive(true);
		py = player.gameObject.GetComponent("Player") as Player;
		ey = enemy.gameObject.GetComponent("Enemy") as Enemy;
		score_total = GameObject.Find("Canvas").transform.Find("Text").GetComponent<Score>();
		//ready = GameObject.Find("Canvas").transform.Find("Ready").GetComponent<Button>();
		//start = GameObject.Find("Canvas").transform.Find("Start").GetComponent<Button>();
		//if (!game_ready)
		//{ 
		//	ready.onClick.AddListener(() =>
		//	{
		//		PacketGameGO_REQ packet = new PacketGameGO_REQ();
		//		packet.ready = true;
		//		NetWork_s.Instance.ProcessSend(NetWork_s.Instance.structToByte((object)packet));
		//		ready_state = packet.ready;
		//		packet = null;
		//		game_ready = true;
		//		ready.enabled(false);
		//	});
		//}
		//if (game_ready && !enemystart)
		//{ 
		//	start.onClick.AddListener(() =>
		//	{
		//		PacketGame_REQ packet = new PacketGame_REQ();
		//		NetWork_s.Instance.ProcessSend(NetWork_s.Instance.structToByte((object)packet));
		//		PacketMove_REQ movePacket = new PacketMove_REQ();
		//		movePacket.x = player.transform.position.x;
		//		movePacket.y = player.transform.position.y;
		//		movePacket.z = player.transform.position.z;
		//		movePacket.rx = player.transform.eulerAngles.x;
		//		movePacket.ry = player.transform.eulerAngles.y;
		//		movePacket.rz = player.transform.eulerAngles.z;
		//		NetWork_s.Instance.ProcessSend(NetWork_s.Instance.structToByte((object)packet));
		//		enemystart = true;
		//	});
		//}
	}

	public void SetEnermy(float x_pos)
	{
		//enemy.SetActive(true);
		//ey = enemy.gameObject.GetComponent("Enemy") as Enemy;
		//ey.SetPosition(-x_pos);
	}

	public void StateEnemy(PacketMove_ACK packet)
	{
		ey.State_func(packet);
	}

	public void GameStart()
	{
		py.game_start = true;
		ey.game_start = true;
	}

	private void OnGUI()
	{
		if (!game_ready)
		{
			if (GUI.Button(new Rect(m_width / 3, m_height / 6, 300, 100), "Ready") && ready_state == false)
			{
				PacketGameGO_REQ packet = new PacketGameGO_REQ();
				packet.ready = true;
				NetWork_s.Instance.ProcessSend(NetWork_s.Instance.structToByte((object)packet));
				ready_state = packet.ready;
				packet = null;
				game_ready = true;
			}
		}

		if (game_ready && !enemystart)
		{
			if (GUI.Button(new Rect(m_width / 3, m_height / 6, 300, 100), "Game Start") && ready_state == true)
			{
				PacketGame_REQ packet = new PacketGame_REQ();
				NetWork_s.Instance.ProcessSend(NetWork_s.Instance.structToByte((object)packet));
				PacketMove_REQ movePacket = new PacketMove_REQ();
				movePacket.x = player.transform.position.x;
				movePacket.y = player.transform.position.y;
				movePacket.z = player.transform.position.z;
				movePacket.rx = player.transform.eulerAngles.x;
				movePacket.ry = player.transform.eulerAngles.y;
				movePacket.rz = player.transform.eulerAngles.z;
				NetWork_s.Instance.ProcessSend(NetWork_s.Instance.structToByte((object)packet));
				enemystart = true;
			}

		}
	}

	// Update is called once per frame
	void Update () {
		if (score_total.m_total_Score > 100)
		{
			Win_Score.SetActive(true);
			gameObject.SetActive(false);
			score_total.m_total_Score = 0;
			score_total.m_score_Bar.text = "Score : " + score_total.m_total_Score.ToString();
		}
	}
}
