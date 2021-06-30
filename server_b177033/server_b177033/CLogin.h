#pragma once
class CLogin
{
private:
	BOOL		 m_UserCheck;
	char		 m_Name[12];
	char		 m_Password[80];
	char		 m_Tel[12];
	char		 m_Query[255];
	int			 m_QueryState;

public:
	CLogin();
	~CLogin();
	BOOL CompareUserID (char *LoginID, char *Pass);
	BOOL CreateUserID  (char *LoginID, char *Pass);
	BOOL DeleteUserID  ();
};

