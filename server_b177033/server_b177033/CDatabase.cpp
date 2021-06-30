#include "stdafx.h"

CDatabase::CDatabase()
{
}

CDatabase::~CDatabase()
{
	mysql_close(m_Connection);
}

//mysql ¿¬°á
BOOL CDatabase::ConnectSql()
{
	mysql_init(&m_Conn);
	m_Connection = mysql_real_connect(&m_Conn, DB_HOST,
		DB_USER, DB_PASS,DB_NAME, 3306,(char*)NULL, 0);
	if (m_Connection == NULL)
	{
		fprintf(stderr, "Mysql connection error : %s", mysql_error(m_Connection));
		return FALSE;
	}
	return TRUE;
}

MYSQL_RES* CDatabase::SetSqlResult(MYSQL_RES * mysql_store_result)
{
	m_SqlResult = mysql_store_result; 
	return m_SqlResult;
}

MYSQL_ROW CDatabase::SetSqlRow(MYSQL_ROW mysql_fetch_row)
{
	m_SqlRow = mysql_fetch_row;
	return m_SqlRow;
}
