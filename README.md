# eat-client-iocp-server1
-----
![오목게임 깃허브 리드미1](https://user-images.githubusercontent.com/86718283/124387811-ea04bf80-dd1a-11eb-844d-3274a6ee05c0.png)

----------------
>사용언어
```
c / c ++ / win32
```
----------------
>서버
```
iocp server
```
+ CrirticalSection.h
+ CDatabase.h
+ CCircleQueue.h
+ CResult.h
+ CSingleTon.h
+ protocal.h
+ stdafx.h
+ CLog.h
+ CLogin.h
+ CRoom.h
+ CRoomManager.h
+ CIocp.h
+ CServer.h
+ CSession.h
+ CSessionManager.h
+ CUserManager.h
+ CUserSession.h
+ CThread.h
>> main 함수

![image](https://user-images.githubusercontent.com/86718283/124422520-6096d100-dd9e-11eb-8b1f-8fc679e11903.png)
1. database 연결
2. ListenBind 연결
3. iocp 연결
4. SESSION 초기화 (userManager 생성, RoomManager 생성)
5. CreateSeccsion (메모리풀 사용, AcceptEx 접속받기)

>>CDatabase.h

![image](https://user-images.githubusercontent.com/86718283/124423297-cafc4100-dd9f-11eb-8ea9-f86fbfc5b3ef.png)
1. ConnectSql() (db연결, 한글 설정)
2. 그 외 set, get 함수 

>>CServer.h

![image](https://user-images.githubusercontent.com/86718283/124423539-5544a500-dda0-11eb-90ab-60d8e18d8e4f.png)
1. InitServer() (윈속초기화, 소켓 초기화)
2. ListenBindServer() (Listen, Bind, acceptEx 하기위한 iocp 연결)
3. 그 외 get 함수

>>CIocp.h

![image](https://user-images.githubusercontent.com/86718283/124423705-ae143d80-dda0-11eb-96f7-475e2fb4a8ad.png)
1. CreateIocp() (iocp생성 함수)
2. ConnectIocp_ListenSocket() (acceptEx 하기위한 iocp 연결 함수)
3. ConnectIocp_Session() (Session와 CreateIoCompletionPort 연결)

>>CSessionManager.h

![image](https://user-images.githubusercontent.com/86718283/124424112-6d68f400-dda1-11eb-9436-a97d01811415.png)
1. Init() (CUserManager, CRoomManger 생성)
2. CreateSeccsion() (session 생성, acceptEx 접속받기 객체 풀링 사용)
3. ActiveSession() (객체풀링 활성화 함수)
4. InActiveSession() (객체풀링 비활성화 함수)
5. 그 외 get 함수

>>CThread.h

![image](https://user-images.githubusercontent.com/86718283/124424399-ea946900-dda1-11eb-8d6f-5d3a3b5e1cf5.png)
1. CreateThread() (workerThread 생성 (GetSystemInfo*2))
2. DestroyThread() (workerThread 제거)
3. WorkerThread() (GetQueuedCompletionStatus을 통하여 데이터 처리)



----------------
>클라이언트
```
win32
```
+ singleton.h
+ fivetree.h
+ stdafx.h
+ protocal.h
+ sendPacket.h
+ server.h
+ dialog.h
+ gameWindow.h
+ loginWindow.h
+ resultWindow.h
+ waitRoomWindow.h

-----------------




