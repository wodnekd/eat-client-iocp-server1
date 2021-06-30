// server_b177033.cpp : 콘솔 응용 프로그램에 대한 진입점을 정의합니다.
//

#include "stdafx.h"


int main()
{
	Database.ConnectSql();
	Server.InitServer();
	Server.LisenBindServer();
	SESSION.Init();
	SESSION.CreateSeccsion(30);
	
	CThread* thread = new CThread;
	thread->CreateThread();
	getchar();
	thread->DestroyThread();
	// 윈속 종료
	WSACleanup();
    return 0;
}

