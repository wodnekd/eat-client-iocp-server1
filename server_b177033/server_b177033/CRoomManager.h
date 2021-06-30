#pragma once

class CRoomManager : public CMultiThreadSync <CRoomManager>
{

private:
	int roomNumber;
	std::vector<CRoom*> off_room;
	std::vector<CRoom*> on_room;
	std::vector<CRoom*>::iterator iter;

public:
	CRoomManager();
	~CRoomManager();
	bool CreateRoom();
	bool DestroyRoom(CRoom *room);
	std::vector<CRoom*> GetRoomList(){return on_room;}
	VOID Clear();
};

