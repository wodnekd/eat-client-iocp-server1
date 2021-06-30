using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour {
	public GameObject chat;
	public GameObject game;
	private int room_number = -1;
    private bool room_state = false;
	private Button LobbyButton1;
	private Button LobbyButton2;
	private Button LobbyButton3;
	private Button LobbyButton4;

	int mWidth = 0;
    int mHegiht = 0;
	// Use this for initialization
	void Start ()
	{
        mWidth = Screen.width;
        mHegiht = Screen.height;
		LobbyButton1 = GameObject.Find("Lobby_Canvas").transform.Find("Lobby_1/Button").GetComponent<Button>();
		LobbyButton2 = GameObject.Find("Lobby_Canvas").transform.Find("Lobby_2/Button").GetComponent<Button>();
		LobbyButton3 = GameObject.Find("Lobby_Canvas").transform.Find("Lobby_3/Button").GetComponent<Button>();
		LobbyButton4 = GameObject.Find("Lobby_Canvas").transform.Find("Lobby_4/Button").GetComponent<Button>();

		LobbyButton1.onClick.AddListener(() =>
		{
			//
			//chat.SetActive(true);
			//game.SetActive(true);
			//gameObject.SetActive(false);
			//
			room_number = 1;
			StartCoroutine("Lobby_join");
		});

		LobbyButton2.onClick.AddListener(() =>
		{
			room_number = 2;
			StartCoroutine("Lobby_join");
		});

		LobbyButton3.onClick.AddListener(() =>
		{
			room_number = 3;
			StartCoroutine("Lobby_join");
		});

		LobbyButton4.onClick.AddListener(() =>
		{
			room_number = 4;
			StartCoroutine("Lobby_join");
		});
	}
    public void Lobby_set(bool state)
    {
        room_state = state;
    }
    private void OnDestroy()
    {
        gameObject.SetActive(false);
    }

 
    IEnumerator Lobby_join()
    {
        PacketRoom_REQ packet = new PacketRoom_REQ();
        packet.room_number = this.room_number;
        NetWork_s.Instance.ProcessSend(NetWork_s.Instance.structToByte((object)packet));
        yield return new WaitForSeconds(2.0f);

		if(room_state)
		{
			chat.SetActive(true);
			//game.SetActive(true);
			gameObject.SetActive(false);
        }
    }
}



//public class Lobby : MonoBehaviour
//{
//	private int room_number = -1;
//	private bool room_state = false;
//	public GameObject chat;
//	private GameObject chat;

//	int mWidth = 0;
//	int mHegiht = 0;
//	 Use this for initialization
//	void Start()
//	{
//		mWidth = Screen.width;
//		mHegiht = Screen.height;
//	}
//	public void Lobby_set(bool state)
//	{
//		room_state = state;
//	}
//	private void OnDestroy()
//	{
//		gameObject.SetActive(false);
//	}
//	 Update is called once per frame
//	void OnGUI()
//	{
//		if (GUI.Button(new Rect(mWidth / 10.24f, mHegiht / 7.68f, mWidth / 3.41f, mHegiht / 2.56f), "1번방"))
//		{
			
//			chat.SetActive(true);
//			gameObject.SetActive(false);
			
//			room_number = 0;
//			StartCoroutine("Lobby_join");
//		}
//		if (GUI.Button(new Rect(mWidth / 2.0f, mHegiht / 7.68f, mWidth / 3.41f, mHegiht / 2.56f), "2번방"))
//		{
//			chat.SetActive(true);
//			gameObject.SetActive(false);
//			room_number = 1;
//			StartCoroutine("Lobby_join");
//		}


//	}

//	IEnumerator Lobby_join()
//	{
//		PacketRoom_REQ packet = new PacketRoom_REQ();
//		packet.room_number = this.room_number;
//		NetWork_s.Instance.ProcessSend(NetWork_s.Instance.structToByte((object)packet));
//		yield return new WaitForSeconds(2.0f);

//		if (room_state)
//		{
//			chat.SetActive(true);
//			gameObject.SetActive(false);
//		}
//	}
//}