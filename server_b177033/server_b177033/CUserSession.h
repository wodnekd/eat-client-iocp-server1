#pragma once
enum UserState
{
	LOGIN = 0, LOBBY, GAME
};


class CUserSession : public CSession
{
private:
	typedef VOID(CUserSession::*pF)(PacketHeader *pPacket);	 
	typedef std::map<DWORD, pF>	packetList;					
	packetList					packetMap;
	packetList::iterator		iter;
	UserState state;
	int							RoomNumber;
public:
	CUserSession();
	~CUserSession();

	char userID[USER];
	VOID PacketParsing(PacketHeader *pPacket);
	VOID NewUer_ACK(PacketHeader *pPacket);
	VOID NewUer_REQ(PacketHeader *pPacket);
	VOID Auth_ACK(PacketHeader *pPacket);
	VOID Auth_REQ(PacketHeader *pPacket);
	VOID CHAT_ACK(PacketHeader *pPacket);
	VOID CHAT_REQ(PacketHeader *pPacket);
	VOID Room_ACK(PacketHeader *pPakcet);
	VOID Room_REQ(PacketHeader *pPakcet);
	VOID Start_ACK(PacketHeader *pPakcet);
	VOID Start_REQ(PacketHeader *pPakcet);
	VOID Game_ACK(PacketHeader *pPakcet);
	VOID Game_REQ(PacketHeader *pPakcat);
	VOID MOVE_ACK(PacketHeader *pPakcet);
	VOID MOVE_REQ(PacketHeader *pPakcet);
	VOID END_ACK(PacketHeader *pPakcet);
	VOID END_REQ(PacketHeader *pPakcet);

	UserState GetUserState() const { return state; }
};