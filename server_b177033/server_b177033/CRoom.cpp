#include "stdafx.h"



CRoom::CRoom(bool createRoom, bool full)
	:m_createRoom(createRoom), m_full(full)
{
	user_num = 0;
	game_state = false;
	check = false;
}

CRoom::~CRoom()
{

}
//룸에 접속할 유저
bool CRoom::JoinUser(CUserSession* user) 
{
	CThreadSync Sync;
	if (IsRoomUser(user)) 
	{
		cout << "Current Is User" << endl;
		return false;
	}
	if (game_state)
	{
		return false;
	}
	if (IsFuLL())
	{
		return false;
	}
	useMapList.insert(std::map<SOCKET, CUserSession*>::value_type(user->GetSessionSock(), user));
	Update(user, NULL, JOIN, TRUE);

	return true;
}
//룸에 나가는 유저
bool CRoom::LeaveUser(CUserSession* user, char*pPacket, DWORD size) 
{
	CThreadSync Sync;
	if (!IsRoomUser(user)) 
	{
		return false;
	}
	else 
	{
		Broadcast_User(user, pPacket, size, true);
		iterMap = useMapList.find(user->GetSessionSock());
		if (iterMap != useMapList.end()) 
		{
			iterMap = useMapList.erase(iterMap);
			Update(user, NULL, LEAVE, TRUE);
		}
		CSession *Session = dynamic_cast<CSession*>(user);
		if (Session == NULL)
		{
			cout << "dynamic_cast LeaveUser Faill" << endl;
		}
		//SESSION.GetUser()->Leave_User(user);


	}

	return true;
}
//방에 있는 유저에게 전체 전송
bool CRoom::Broadcast_User(CUserSession *user, void * pPacket, DWORD size, bool all_user)
{
	CThreadSync Sync;
	if (IsRoomUser(user)) 
	{
		if (all_user)
		{

			for (iterMap = useMapList.begin(); iterMap != useMapList.end(); ++iterMap)
			{
				if (IsRoomUser(iterMap->second))
					iterMap->second->Send(pPacket, size);
			}
		}
		else
		{
			for (iterMap = useMapList.begin(); iterMap != useMapList.end(); ++iterMap) 
			{
				if (iterMap->second == user) continue;
				iterMap->second->Send(pPacket, size);
			}
		}
	}

	else
	{

		return false;
	}

	return true;
}

//방에 유저 존재 유무
bool CRoom::IsRoomUser(CUserSession *user) 
{
	iterMap = useMapList.find(user->GetSessionSock());
	if (iterMap != useMapList.end()) 	return true;
	else
	{
		CCLog::WriteLog(_T("Is Not Room User"));
		return false;
	}

}

//업데이트
VOID CRoom::Update(CUserSession *user, void * pPacket, DWORD number, bool correct)
{
	switch (number)
	{
	case JOIN:
		++user_num;
		if (user_num == MAX_USER)
		{
			m_full = true;
		}
		break;
	case LEAVE:
		if (user_num > 0)
		{
			--user_num;
			m_full = false;
		}
		break;
	}
}