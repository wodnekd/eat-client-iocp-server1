#pragma once
#include<winsock2.h>
#include<process.h>

#define USER				16
#define PASSWORD			32
#define CHAT_BUF			1024

enum CODE
{
	USER_NEWUSER_ACK = 50,
	USER_NEWUSER_REQ,
	USER_AUTH_ACK = 100,
	USER_AUTH_REQ,
	USER_ROOM_ACK = 200,  
	USER_ROOM_REQ,	
	USER_MOVE_ACK = 300,
	USER_MOVE_REQ,
	USER_TALK_ACK = 400, 
	USER_TALK_REQ,
	USER_GAME_ACK = 500,
	USER_GAME_REQ,
	USER_START_ACK = 600,
	USER_START_REQ,
	USER_END_ACK = 700,
	USER_END_REQ
};
enum STATE
{
	NONE = 0,
	LEFT = 1,
	RIGHT,
	UP,
	DOWN,
	BULLET,
	TRIGGER
};
struct PacketHeader
{
	DWORD code;
	DWORD size;
};

struct Packet_NewUser_ACK : public PacketHeader
{
	bool ISconnect;
	bool Create_state;

	Packet_NewUser_ACK()
	{
		code = USER_NEWUSER_ACK;
		size = sizeof(*this);
		Create_state = false;
	}
};
struct Packet_NewUser_REQ :public PacketHeader
{
	char userID[USER];
	char passWord[PASSWORD];

	Packet_NewUser_REQ()
	{
		code = USER_NEWUSER_REQ;
		size = sizeof(*this);
		memset(userID, 0, USER);
		memset(passWord, 0, PASSWORD);
	}
};
struct Packet_Auth_ACK :public PacketHeader 
{
	bool ISconnect;
	bool login_state;
	char chat[CHAT_BUF];

	Packet_Auth_ACK()
	{
		code = USER_AUTH_ACK;
		size = sizeof(*this);
		memset(chat, 0, CHAT_BUF);
		login_state = false;
	}
};
struct Packet_Auth_REQ :public PacketHeader 
{
	char userID[USER];
	char passWord[PASSWORD];

	Packet_Auth_REQ() 
	{
		code = USER_AUTH_REQ;
		size = sizeof(*this);
		memset(userID, 0, USER);
		memset(passWord, 0, PASSWORD);
	}
};
struct PacketRoom_ACK :public PacketHeader 
{
	bool join_state;
	PacketRoom_ACK()
	{
		code = USER_ROOM_ACK;
		size = sizeof(*this);
		join_state = false;
	}

};
struct PacketRoom_REQ :public PacketHeader 
{
	PacketRoom_REQ() 
	{
		code = USER_ROOM_REQ;
		size = sizeof(*this);
		room_number = 0;
	}
	DWORD room_number;

};

struct PacketCHAT_ACK :public PacketHeader
{
	char chat[CHAT_BUF];
	PacketCHAT_ACK()
	{
		code = USER_TALK_ACK;
		size = sizeof(*this);
		memset(chat, 0, CHAT_BUF);
	}
};

struct PacketCHAT_REQ :public PacketHeader
{
	PacketCHAT_REQ()
	{
		code = USER_TALK_REQ;
		size = sizeof(*this);
		memset(chat, 0, 1024);
	}

	char chat[CHAT_BUF];
};


struct PacketStart_ACK :public PacketHeader {
	PacketStart_ACK() {
		code = USER_START_ACK;
		size = sizeof(*this);
		start = false;		
	}
	bool start;	
};

struct PacketStart_REQ :public PacketHeader {
	PacketStart_REQ() {
		code = USER_START_REQ;
		size = sizeof(*this);
		gameStart = false;
	}
	bool gameStart;
};


struct PacketGame_ACK :public PacketHeader {
	PacketGame_ACK() {
		code = USER_GAME_ACK;
		size = sizeof(*this);
		start = false;
		for (int i = 0; i < 10; i++)
		{
			x[i] = rand() % 10 - 5;
			z[i] = rand() % 10 - 5;
		}
	}
	bool start;
	float x[10], z[10];
};

struct PacketGame_REQ :public PacketHeader {
	PacketGame_REQ() {
		code = USER_GAME_REQ;
		size = sizeof(*this);
	}
};

struct PacketMove_ACK : PacketHeader
{
	PacketMove_ACK()
	{
		 code = USER_MOVE_ACK;
		 size = sizeof(*this);		 
		 x = y = z = rx = ry = rz = 0;
	}
	 float x, y, z;
	 float rx, ry, rz;
};

struct PacketMove_REQ : PacketHeader
{
	 PacketMove_REQ()
	{
		code = USER_MOVE_REQ;
		size = sizeof(*this);
		x = y = z = rx = ry = rz = 0;
	}


	float x, y, z;
	float rx, ry, rz;
};

struct PacketEnd_ACK :public PacketHeader {
	PacketEnd_ACK() {
		code = USER_END_ACK;
		size = sizeof(*this);
		end = false;
	}
	bool end;

};

struct PacketEnd_REQ :public PacketHeader {
	PacketEnd_REQ() {
		code = USER_END_REQ;
		size = sizeof(*this);
	}
};