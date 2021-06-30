using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;                    //문자열을 byte[]배열에 넣기 위해
using System.Runtime.InteropServices;//관리되지않는 코드 (포인터 사용및 구조체를 시퀜셜하게 만들기위해)
using System.Net;                    //NetWork
using System.Net.Sockets;            //NetWork Socket;
using System.IO;                     //입출력 stream
using System;
public class NetWork_s : MonoBehaviour
{

	private Socket Clientsock;
	private IPEndPoint ipep;
	private static NetWork_s _instance = null;

	public GameObject obj_Login;
	public GameObject obj_Lobby;
	public GameObject obj_GameManager;

	int datasize = 0;
	IntPtr buffer;
	byte[] data;
	int readed = 0;
	int sended = 0;
	byte[] recvBuffer;
	byte[] sendBuffer;
	private NewIdCreate NewIdCreate_event;
	private Login login_event;
	private Lobby lobby_event;
	private GameManager Game_event;
	private object temp;


	// Use this for initialization
	public static NetWork_s Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(NetWork_s)) as NetWork_s;
				if (_instance == null)
				{
					Debug.Log("Error");
				}
			}
			return _instance;
		}
	}
	//구조체를 바이트형으로
	public byte[] structToByte(object obj)
	{
		datasize = 0;
		data = null;

		datasize = Marshal.SizeOf(obj);
		buffer = Marshal.AllocHGlobal(datasize);            //지정된 바이트 수를 사용하여 프로세스의 관리되지 않는 메모리에서 메모리를 할당
		Marshal.StructureToPtr(obj, buffer, false);         //첫번째 인자(마샬링 될 데이터가 있는 개체)를 두번째 인자(관리되지 않는 메모리)로 마샬링// 관리되는 개체의 데이터를 관리되지 않는 메모리 블록으로 마샬링
		data = new byte[datasize];
		Marshal.Copy(buffer, data, 0, datasize);
		Marshal.FreeHGlobal(buffer);                        //프로세스의 관리되지 않는 메모리에서 이전에 할당한 메모리를 해제
		return data;
	}
	//바이트형을 구조체형으로
	public object ByteToStruct()
	{
		buffer = Marshal.AllocHGlobal(recvBuffer.Length); // 배열의 크기만큼 비관리 메모리 영역에 메모리를 할당한다.
		Marshal.Copy(recvBuffer, 0, buffer, recvBuffer.Length); // 배열에 저장된 데이터를 위에서 할당한 메모리 영역에 복사한다.
		object obj = Marshal.PtrToStructure(buffer, typeof(PacketHeader)); // 복사된 데이터를 구조체 객체로 변환한다.
																		   // 비관리 메모리 영역에 할당했던 메모리를 해제함
		return obj;
	}
	void Start()
	{
		Screen.SetResolution(1024, 768, false);
		DontDestroyOnLoad(this);
		recvBuffer = new byte[4096];
		sendBuffer = new byte[4096];
		Array.Clear(recvBuffer, 0, recvBuffer.Length);
		Array.Clear(sendBuffer, 0, sendBuffer.Length);
		ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9000);
		Clientsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		try
		{
			Clientsock.Connect(ipep);
			Debug.Log("connect");

		}
		catch (SocketException e)
		{
			Clear();
			Debug.Log("Not Connect");
			throw;
		}
		ProcessRecv();

	}
	void Update()
	{
		ProcessRecv();
		if (readed > 4)
		{
			Debug.Log("Read :" + readed);
			PacketHeader packet = (PacketHeader)ByteToStruct();
			ParsingRecv(packet);
			Array.Clear(recvBuffer, 0, recvBuffer.Length);
			packet = null;
			readed = 0;
			//실행
		}
	}
	private void OnDestroy()
	{
		NewIdCreate_event = null;
		login_event = null;
		Destroy(this);
		Clear();
	}
	public void Clear()
	{
		if (Clientsock != null)
			Clientsock.Close();
		recvBuffer = null;
		sendBuffer = null;
	}
	private void ProcessRecv()
	{
		Clientsock.BeginReceive(recvBuffer, 0, recvBuffer.Length, 0, ProcessRecvCallback, null);//비동기 함수
	}
	private void ProcessRecvCallback(IAsyncResult ar) //콜백 어떤함수가 불려졌을때 불러주는 함수이다.  IAsyncResult 비동기화
	{
		readed = Clientsock.EndReceive(ar);
		ProcessRecv();
	}

	private void ParsingRecv(PacketHeader mPacket)
	{
		switch (mPacket.code)
		{
			case CODE.NONE:
				Debug.Log("None Packet");
				break;

			case CODE.USER_NEWUSER_ACK:
				temp = Marshal.PtrToStructure(buffer, typeof(Packet_NewUser_ACK));
				Packet_NewUser_ACK newuser = (Packet_NewUser_ACK)temp;
				if (newuser == null)
					Debug.Log("newuser Error");
				NewIdCreate_event.LoginState_func(newuser.Isconnect);
				Marshal.FreeHGlobal(buffer);
				temp = null;
				newuser = null;
				break;

			case CODE.USER_AUTH_ACK:
				temp = Marshal.PtrToStructure(buffer, typeof(Packet_Auth_ACK));
				Packet_Auth_ACK auth = (Packet_Auth_ACK)temp;
				if (auth == null)
					Debug.Log("auth Error");
				login_event = obj_Login.GetComponent("Login") as Login;
				if (login_event == null) Debug.Log("Login event null");
				login_event.LoginState_func(auth.Isconnect);
				Marshal.FreeHGlobal(buffer);
				temp = null;
				auth = null;
				
				break;
			case CODE.USER_ROOM_ACK:
				temp = Marshal.PtrToStructure(buffer, typeof(PacketRoom_ACK));
				PacketRoom_ACK room = (PacketRoom_ACK)temp;
				if (room == null)
					Debug.Log("auth Error");
				lobby_event = GameObject.Find("Room").transform.Find("Lobby_Canvas").GetComponent<Lobby>();
				if (lobby_event == null) Debug.Log("Login event null");
				lobby_event.Lobby_set(room.join_state);
				Marshal.FreeHGlobal(buffer);
				temp = null;
				room = null;
				break;

			case CODE.USER_TALK_ACK:
				temp = Marshal.PtrToStructure(buffer, typeof(PacketTALK_REQ));
				PacketTALK_REQ talk = (PacketTALK_REQ)temp;
				UnityEngine.UI.Text textComponent = GameObject.Find("Chat_Canvas").transform.Find("Chat/Text").GetComponent<UnityEngine.UI.Text>();
				textComponent.text += textComponent.text.Length == 0 ? talk.currentMessage : "\n" + talk.currentMessage;
				Marshal.FreeHGlobal(buffer);
				temp = null;
				talk = null;
				break;


			case CODE.USER_START_ACK:
				temp = Marshal.PtrToStructure(buffer, typeof(PacketGameGO_ACK));
				PacketGameGO_ACK start = (PacketGameGO_ACK)temp;
				Game_event = obj_GameManager.GetComponent("GameManager") as GameManager;
				if (start.start == true) Game_event.GameStart();
				else Debug.Log("Start Error");
				Marshal.FreeHGlobal(buffer);
				break;

			case CODE.USER_GAME_ACK:
				temp = Marshal.PtrToStructure(buffer, typeof(PacketGame_ACK));
				PacketGame_ACK game = (PacketGame_ACK)temp;
				Game_event = obj_GameManager.GetComponent("GameManager") as GameManager;

				for(int i = 0; i < 10; i++)
				{
					var prefab = Resources.Load<GameObject>("Capsule");
					prefab = Instantiate<GameObject>(prefab);
					prefab.transform.position = new Vector3(game.x[i], 1, game.z[i]);
				}


				Marshal.FreeHGlobal(buffer);
				temp = null;
				game = null;
				break;

			case CODE.USER_MOVE_ACK:
				temp = Marshal.PtrToStructure(buffer, typeof(PacketMove_ACK));
				PacketMove_ACK move = (PacketMove_ACK)temp;
				Game_event = obj_GameManager.GetComponent("GameManager") as GameManager;
				Game_event.StateEnemy(move);
				Marshal.FreeHGlobal(buffer);
				temp = null;
				move = null;
				break;
		}
	}
	public void ProcessSend(byte[] packet)
	{
		//보낼 데이터, 데이터 보내기 시작할 위치, 보낼 바이트수 , ?, 콜백함수 대리자,상태정보
		Clientsock.BeginSend(packet, 0, packet.Length, 0, ProcessSendCallback, null);

	}
	private void ProcessSendCallback(IAsyncResult ar)
	{
		sended = Clientsock.EndSend(ar);
		if (sended > 0) Debug.Log("send size:" + sended);

	}


}
