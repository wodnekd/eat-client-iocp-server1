using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public enum CODE
{
	NONE = 0,
	USER_NEWUSER_ACK = 50,
	USER_NEWUSER_REQ,
	USER_AUTH_ACK = 100, //유저 ID ,PASSWORD 및 접속
	USER_AUTH_REQ,
	USER_ROOM_ACK = 200,   // 업데이트된 유저
	USER_ROOM_REQ,
	USER_MOVE_ACK = 300,
	USER_MOVE_REQ,
	USER_TALK_ACK = 400,
	USER_TALK_REQ,
	USER_GAME_ACK = 500, // 게임시작
	USER_GAME_REQ,
	USER_START_ACK = 600,
	USER_START_REQ
};
public enum STATE
{
	NONE = 0,
	LEFT = 1,
	RIGHT,
	UP,
	DOWN,
	BULLET,
	TRIGGER
};
//StructLayout(LayoutKind.Sequential) 구조체 임을 선언, 데이터를 읽는 단위를 Pack 단위로 생성
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public class PacketHeader
{
	public CODE code;
	public int PacketSize;
	public PacketHeader(){}
	public PacketHeader(CODE type,int size)
	{
		code = type;
		PacketSize = size;
	}
}
/// <summary>
/// 아이디 생성 ack
/// </summary>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public class Packet_NewUser_ACK : PacketHeader
{
	public Packet_NewUser_ACK() { }
	public Packet_NewUser_ACK(string ID_value, string Pass_value)
		: base(CODE.USER_AUTH_REQ, Marshal.SizeOf(typeof(Packet_Auth_REQ)))
	{
		Isconnect = false;
		Create_state = false;
	}
	public bool Isconnect;
	public bool Create_state;

};
/// <summary>
/// 아이디 생성 req
/// </summary>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public class Packet_NewUser_REQ : PacketHeader
{
	public Packet_NewUser_REQ() { }
	public Packet_NewUser_REQ(string ID_value, string Pass_value)
		: base(CODE.USER_NEWUSER_REQ, Marshal.SizeOf(typeof(Packet_NewUser_REQ)))
	{
		LoginID = ID_value;
		PassWord = Pass_value;
	}
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
	private string LoginID;
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
	private string PassWord;

};

/// <summary>
/// 로그인 ack
/// </summary>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public class Packet_Auth_ACK :PacketHeader
{
	public Packet_Auth_ACK() 
		:base(CODE.USER_AUTH_ACK, Marshal.SizeOf(typeof(Packet_Auth_ACK)))
	//base == 부모클래스
	{
		Isconnect = false;
		login_state = false;
	}
	public bool Isconnect;
	public bool login_state;
	//선언한 순서로 순차적으로 만들기 위해 사용
	//구조체 내에 나타나는 인라인 고정 길이 문자 배열에 사용됩니다.
	//ByValTStr과 함께 사용되는 문자 형식은 포함하는 구조체에 적용된 System.Runtime.InteropServices.StructLayoutAttribute 특성의 
	//System.Runtime.InteropServices.CharSet 인수에 의해 결정됩니다. 항상 MarshalAsAttribute.SizeConst 필드를 사용하여 배열의 크기를 나타냅니다.
	//고정 길이 문자 배열입니다. 배열 형식은 포함하는 구조체의 문자 집합에 의해 결정됩니다.
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
	public string chat;
};
/// <summary>
/// 로그인 req
/// </summary>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public class Packet_Auth_REQ :PacketHeader
{
	public Packet_Auth_REQ(){ }
	public Packet_Auth_REQ(string ID_value, string Pass_value)
		: base(CODE.USER_AUTH_REQ, Marshal.SizeOf(typeof( Packet_Auth_REQ)))
	{
		LoginID = ID_value;
		PassWord = Pass_value;
	}
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
	private string LoginID;
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
	private string PassWord;

};


[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
class PacketTALK_ACK : PacketHeader
{
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
	public string chat;
	public PacketTALK_ACK()
			: base(CODE.USER_TALK_ACK, Marshal.SizeOf(typeof(PacketTALK_ACK)))
	{

	}

};
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
class PacketTALK_REQ : PacketHeader
{
	public PacketTALK_REQ(string chat)
			: base(CODE.USER_TALK_REQ, Marshal.SizeOf(typeof(PacketTALK_REQ)))
	{
		currentMessage = chat;
	}
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
	public string currentMessage;

};
/// <summary>
/// 방 ack
/// </summary>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public class PacketRoom_ACK :PacketHeader {

	public PacketRoom_ACK() 
		:base(CODE.USER_ROOM_ACK,Marshal.SizeOf(typeof(PacketRoom_ACK)))
	{
		join_state = false;
	}
	public bool join_state;



};
/// <summary>
/// 방 req
/// </summary>

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public class PacketRoom_REQ :PacketHeader {

	public  PacketRoom_REQ() 
		:base(CODE.USER_ROOM_REQ, Marshal.SizeOf(typeof(PacketRoom_REQ)))
	{
		room_number = 0;
	}
	public int room_number;

};
/// <summary>
/// GameReady
/// </summary>

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
class PacketGameGO_ACK : PacketHeader
{
	public PacketGameGO_ACK(float _x, float _z)
			: base(CODE.USER_START_ACK, Marshal.SizeOf(typeof(PacketGameGO_ACK)))
	{
		start = false;
		x = _x;
		z = _z;
	}
	public bool start;
	public float x, z;


};
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
class PacketGameGO_REQ : PacketHeader
{
	public PacketGameGO_REQ()
			: base(CODE.USER_START_REQ, Marshal.SizeOf(typeof(PacketGameGO_REQ)))
	{
		ready = false;
	}
	public bool ready;

};
/// <summary>
/// Game Start
/// </summary>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public class PacketGame_ACK : PacketHeader
{
	public PacketGame_ACK()
			: base(CODE.USER_GAME_ACK, Marshal.SizeOf(typeof(PacketGame_ACK)))
	{
		start = false;
	}

	public bool start;
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
	public float[] x = new float[10];
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
	public float[] z = new float[10];
};
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public class PacketGame_REQ : PacketHeader
{
	public PacketGame_REQ()
			: base(CODE.USER_GAME_REQ, Marshal.SizeOf(typeof(PacketGame_REQ)))
	{

	}
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
	public string chat;
};


[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public class PacketMove_ACK : PacketHeader
{
	public PacketMove_ACK()
			: base(CODE.USER_MOVE_ACK, Marshal.SizeOf(typeof(PacketMove_ACK)))
	{
		x = y = z = rx = ry = rz = 0;
	}


	public float x, y, z;
	public float rx, ry, rz;
};

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public class PacketMove_REQ : PacketHeader
{
	public PacketMove_REQ()
			: base(CODE.USER_MOVE_REQ, Marshal.SizeOf(typeof(PacketMove_REQ)))
	{
		x = y = z = rx = ry = rz = 0;
	}

	public float x, y, z;
	public float rx, ry, rz;
};