#include "stdafx.h"



CLogin::CLogin()
{
	m_UserCheck = false;
}


CLogin::~CLogin()
{
}

BOOL CLogin::CompareUserID(char * LoginID, char * Pass)
{
	//�α��� ���̺��� ��� �÷��� ��ȯ
	m_QueryState = mysql_query(Database.GetConnection(), "select * from login2");
	if (m_QueryState != 0)
	{
		fprintf(stderr, "Mysql query error : %s", mysql_error(&Database.GetConn()));
		return FALSE;
	}
	//�α��� ���̺� �÷��� �Ѳ����� ��������
	Database.SetSqlResult(mysql_store_result(Database.GetConnection()));
	
	while (Database.SetSqlRow(mysql_fetch_row(Database.GetSqlResult())) != NULL)
	{	
		if (strcmp(Database.GetSqlRow()[0], LoginID) == 0)
		{
			m_UserCheck = TRUE;
			if (strcmp(Database.GetSqlRow()[1], Pass) == 0)
			{
				printf("�α��� ����\n");
				return TRUE;
			}
			else
			{
				printf("��й�ȣ ����\n");
				return FALSE;
			}
		}
		else
		{
			m_UserCheck = FALSE;
		}
	}
	if (m_UserCheck == FALSE)
	{
		printf("���̵� �������� �ʽ��ϴ�.\n");
		return FALSE;
	}

	mysql_free_result(Database.GetSqlResult());
	return TRUE;
}

BOOL CLogin::CreateUserID(char * LoginID, char * Pass)
{
	strncpy(m_Name, LoginID, USER);
	strncpy(m_Password, Pass, PASSWORD);
	sprintf(m_Query, "insert into login2 values" "('%s', '%s')", m_Name, m_Password);
	//(m_Query, "insert into login values(1, 'm_Name', 'm_Password')");

	m_QueryState = mysql_query(Database.GetConnection(), m_Query);
	if (m_QueryState != 0)
	{
		fprintf(stderr, "Mysql query error : %s \n", mysql_error(&Database.GetConn()));
		return FALSE;
	}
	mysql_free_result(Database.GetSqlResult());
	return TRUE;
}

BOOL CLogin::DeleteUserID()
{
	printf("������ ���� ���̵� �Է� : ");
	fgets(m_Name, 12, stdin);
	CHOP(m_Name);

	sprintf(m_Query, "delete from login where id = '%s' ", m_Name);

	m_QueryState = mysql_query(Database.GetConnection(), m_Query);

	if (m_QueryState != 0)
	{
		fprintf(stderr, "Mysql query error : %s", mysql_error(&Database.GetConn()));
		return FALSE;
	}
	mysql_free_result(Database.GetSqlResult());
	return TRUE;
}
