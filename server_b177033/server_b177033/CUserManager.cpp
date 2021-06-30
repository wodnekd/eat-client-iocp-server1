#include "stdafx.h"


CUserManager::CUserManager()
{
	clientNum = 0;

}


CUserManager::~CUserManager()
{
	Clear();
}
//접속한 유저 확인
BOOL CUserManager::Enter_User(CUserSession *user)
{
	CThreadSync Sync;
	userMapList.insert(map<SOCKET,  CUserSession*>::value_type(user->GetSessionSock(), user));
	clientNum++;
;
	return TRUE;
}
//떠난 유저 확인
BOOL CUserManager::Leave_User(CUserSession* leave) {
	CThreadSync Sync;
	if (userMapList.find(leave->GetSessionSock()) != userMapList.end())
	{
		iter = userMapList.erase(iter);
	}
	clientNum--;
	SESSION.InActiveSession(dynamic_cast<CSession*>(leave));
	return TRUE;
}

//전체 유저 리스트 해제
VOID CUserManager::Clear() 
{
	if (clientNum>0)	userMapList.clear();
}