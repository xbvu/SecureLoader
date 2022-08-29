#pragma once
#include "Debug.h"
#include "MemoryExe.h"

class __declspec(dllexport) CSession
{
	char* server_host;
	int server_port;
	std::string username;
	std::string hashed_password;
	std::string server_token;
	std::string auth_token;
	std::string session;
	bool save_login = false;

public:
	CSession(char* server_host, int server_port);
	~CSession();

	void GetServerToken();
	void TokenMaker();
	void Login(const char* username, const char* hashed_password);
};