#include "stdafx.h"



CRoomManager::CRoomManager()
{
	roomNumber = 0;
}

CRoomManager::~CRoomManager()
{
	Clear();
}
//·ë »ý¼º
bool CRoomManager::CreateRoom()
{
	CThreadSync Sync;
	if (roomNumber<MAX_ROOMCOUNT)

		for (int i = 0; i<MAX_ROOMCOUNT; ++i)
		{
			CRoom *Temp = new CRoom(true, false);
			if (Temp == NULL)
			{
				cout << "Room Create Fail" << endl;
				return false;
			}
			on_room.push_back(Temp);
			++roomNumber;
		}
	else 
	{
		cout << "Full Room" << endl;
		return false;
	}
	return true;
}
//·ë ÆÄ±«
bool CRoomManager::DestroyRoom(CRoom *room) 
{
	CThreadSync Sync;
	if (roomNumber >0) 
	{
		for (iter = on_room.begin();
			iter != on_room.end(); ++iter)
		{
			if (*iter == room)
			{
				delete *iter;
				iter = on_room.erase(iter);
			}
		}
		off_room.push_back(room);
		--roomNumber;
		return true;
	}
	else if (roomNumber == 0)
	{
		cout << "Empty Room" << endl;
		return false;
	}
	return true;

}
VOID CRoomManager::Clear() 
{
	for (iter = on_room.begin();
		iter != on_room.end(); ++iter) 
	{
		delete *iter;
	}
	for (iter = off_room.begin();
		iter != off_room.end(); ++iter) 
	{
		delete *iter;
	}
	on_room.clear();
	off_room.clear();
}


