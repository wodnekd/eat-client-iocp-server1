#include "stdafx.h"


CUserSession::CUserSession()
{
	packetMap.insert(packetList::value_type(USER_NEWUSER_ACK, &CUserSession::NewUer_ACK));
	packetMap.insert(packetList::value_type(USER_NEWUSER_REQ, &CUserSession::NewUer_REQ));
	packetMap.insert(packetList::value_type(USER_AUTH_ACK, &CUserSession::Auth_ACK));
	packetMap.insert(packetList::value_type(USER_AUTH_REQ, &CUserSession::Auth_REQ));
	packetMap.insert(packetList::value_type(USER_ROOM_ACK, &CUserSession::Room_ACK));
	packetMap.insert(packetList::value_type(USER_ROOM_REQ, &CUserSession::Room_REQ));
	packetMap.insert(packetList::value_type(USER_START_ACK, &CUserSession::Start_REQ));
	packetMap.insert(packetList::value_type(USER_START_REQ, &CUserSession::Start_REQ));
	packetMap.insert(packetList::value_type(USER_GAME_ACK, &CUserSession::Game_ACK));
	packetMap.insert(packetList::value_type(USER_GAME_REQ, &CUserSession::Game_REQ));
	packetMap.insert(packetList::value_type(USER_MOVE_ACK, &CUserSession::MOVE_ACK));
	packetMap.insert(packetList::value_type(USER_MOVE_REQ, &CUserSession::MOVE_REQ));
	packetMap.insert(packetList::value_type(USER_TALK_ACK, &CUserSession::CHAT_ACK));
	packetMap.insert(packetList::value_type(USER_TALK_REQ, &CUserSession::CHAT_REQ));
	state = LOGIN;
	memset(userID, 0, sizeof(userID));
}

CUserSession::~CUserSession()
{
	packetMap.clear();
}
//패킷 파싱
VOID CUserSession::PacketParsing(PacketHeader * pPacket)
{
	if (pPacket == NULL)
	{
		cout << "In Not Packet" << endl;
		return;
	}
	//패킷맵에서 현재 들어온 패킷의 코드를 찾아 iter에 넣어준다
	cout << pPacket->code << endl;
	iter = packetMap.find(pPacket->code);
	//함수 포인터를 이용하여 패킷함수 실행
	if (iter != packetMap.end())
	{
		pF Fp = iter->second; 
		(this->*Fp)(pPacket);
	}
}

VOID CUserSession::NewUer_ACK(PacketHeader * pPacket)
{
	cout << "NewUer_ACK" << endl;
}

VOID CUserSession::NewUer_REQ(PacketHeader * pPacket)
{
	//파싱된 패킷을 저장
	Packet_NewUser_REQ *packet = (Packet_NewUser_REQ*)pPacket;
	//보낼 패킷 생성
	Packet_NewUser_ACK *send_packet = new Packet_NewUser_ACK;
	state = LOGIN;
	if (!Login.CreateUserID(packet->userID, packet->passWord))
	{
			send_packet->ISconnect = false;
			cout << "아이디 생성 실패" << endl;
			CCLog::WriteLog(_T("LoginFaill"));
	}
	else
	{
		send_packet->ISconnect = true;
		send_packet->Create_state = true;
		cout << "아이디 : [" << packet->userID <<"] 생성 "<< endl;
	}
	//패킷 보내기
	Send((char*)send_packet, send_packet->size);
	delete send_packet;
}

VOID CUserSession::Auth_ACK(PacketHeader * pPacket)
{
	cout << "Auth_ACK" << endl;
}

VOID CUserSession::Auth_REQ(PacketHeader * pPacket)
{
	//파싱된 패킷을 저장
	Packet_Auth_REQ *packet = (Packet_Auth_REQ*)pPacket;
	//보낼 패킷 생성
	Packet_Auth_ACK *send_packet = new Packet_Auth_ACK;
	state = LOGIN;
	if (!Login.CompareUserID(packet->userID, packet->passWord))
		{
			send_packet->ISconnect = false;
			cout << "로그인 실패" << endl;
			CCLog::WriteLog(_T("LoginFaill"));
		}
		else 
		{
			state = LOBBY;
			send_packet->ISconnect = true;
			send_packet->login_state = true;
			SESSION.GetUserManager()->Enter_User(this);
		//아이디,비밀번호 복사
		strncpy(this->userID, packet->userID, USER);
		cout << "LoginID : "<<packet->userID << endl;
		}
	//패킷 보내기
	Send((char*)send_packet, send_packet->size);
	delete send_packet;

}

VOID CUserSession::CHAT_ACK(PacketHeader * pPacket)
{

	CCLog::WriteLog(_T("USER_CHAT_ACK"));
}

VOID CUserSession::CHAT_REQ(PacketHeader * pPacket)
{
	PacketCHAT_ACK *packet = new PacketCHAT_ACK();
	memcpy(packet->chat, ((PacketCHAT_REQ*)pPacket)->chat, CHAT_BUF);
	SESSION.GetRoomManager()->GetRoomList()[this->RoomNumber]->Broadcast_User(this, (void *)packet, packet->size, false);
	delete packet;
	return;
}

VOID CUserSession::Room_ACK(PacketHeader * pPakcet)
{
	CCLog::WriteLog(_T("Room_ACK"));
}

VOID CUserSession::Room_REQ(PacketHeader * pPakcet)
{
	PacketRoom_REQ *packet = (PacketRoom_REQ*)pPakcet;
	PacketRoom_ACK *send_packet = new PacketRoom_ACK;
	if (!SESSION.GetRoomManager()->GetRoomList()[packet->room_number]->JoinUser(this))
	{
		CCLog::WriteLog(_T("Game_REQ :%s"));
		send_packet->join_state = false;
		
		Send((void *)send_packet, packet->size);
	}
	else
	{
		CCLog::WriteLog(_T("Game_REQ Room Join"));
		this->RoomNumber = packet->room_number;
		cout << this->RoomNumber << " 번째 방에 입장하였습니다." << endl;
		send_packet->join_state = true;
		Send((void *)send_packet, packet->size);
	}

	cout << send_packet->join_state << endl;

		delete send_packet;
	packet = NULL;
}

VOID CUserSession::Start_ACK(PacketHeader *pPacket)
{
	cout << "START_ACK Start" << endl;
}

VOID CUserSession::Start_REQ(PacketHeader * pPacket)
{	
}

VOID CUserSession::Game_ACK(PacketHeader *pPacket) 
{

}
VOID CUserSession::Game_REQ(PacketHeader *pPacket)
 {
	if (SESSION.GetRoomManager()->GetRoomList()[this->RoomNumber]->GetUserNum() < 2) return;

	PacketGame_REQ *packet = (PacketGame_REQ*)pPacket;
	for (int i = 0; i < 1; ++i)
	{
		PacketGame_ACK *send_packet = new PacketGame_ACK();
		send_packet->start = true;
		cout << "Game_ACK Start" << endl;
		SESSION.GetRoomManager()->GetRoomList()[this->RoomNumber]->Broadcast_User(this, (void *)send_packet, send_packet->size, true);
		delete send_packet;
	}
	packet = NULL;

	/*PacketGame_REQ *packet = (PacketGame_REQ*)pPacket;
	PacketGame_ACK *send_packet = new PacketGame_ACK;
	state = GAME;
	if (SESSION.GetRoomManager()->GetRoomList()[this->RoomNumber]->IsFuLL())
	{
		send_packet->start = true;
		CCLog::WriteLog(_T("Game_REQ "));
		SESSION.GetRoomManager()->GetRoomList()[this->RoomNumber]->Broadcast_User(this, (void *)send_packet, send_packet->size,true);
		delete send_packet;
		return;
	}
	send_packet->start = true;
	SESSION.GetRoomManager()->GetRoomList()[this->RoomNumber]->Broadcast_User(this, (void *)send_packet, send_packet->size, true);
	delete send_packet;
	packet = NULL;*/

}


VOID CUserSession::MOVE_ACK(PacketHeader * pPakcet)
{
	CCLog::WriteLog(_T("Ready_ACK"));
}

VOID CUserSession::MOVE_REQ(PacketHeader * pPakcat)
{
	PacketMove_REQ *packet = (PacketMove_REQ*)pPakcat;
	PacketMove_ACK *send_packet = new PacketMove_ACK;
	
	cout << packet->x << " " << packet->y << " " << packet->z << endl;
	send_packet->x = packet->x;
	send_packet->y = packet->y;
	send_packet->z = packet->z;
	send_packet->rx = packet->rx;
	send_packet->ry = packet->ry;
	send_packet->rz = packet->rz;
	SESSION.GetRoomManager()->GetRoomList()[this->RoomNumber]->Broadcast_User(this, (void *)send_packet, send_packet->size, false);
	delete send_packet;
	packet = NULL;
}

VOID CUserSession::END_ACK(PacketHeader * pPakcet)
{
	CCLog::WriteLog(_T("End_ACK"));
}

VOID CUserSession::END_REQ(PacketHeader * pPakcet)
{
	PacketEnd_REQ *packet = (PacketEnd_REQ*)pPakcet;
	PacketEnd_ACK *send_packet = new PacketEnd_ACK;

}
