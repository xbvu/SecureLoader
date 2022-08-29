// SecureLoader.cpp : Defines the exported functions for the DLL application.
//
#define NOMINMAX
#include "stdafx.h"
#include "Session.h"
#include "SecureLoader.h"

// This is the code for functions exposed from the DLL

CSession* CreateSession(char* server_host, int server_port)
{
	return new CSession(server_host, server_port);
}

void DisposeSession(CSession* a_pObject)
{
	if (a_pObject != NULL)
	{
		delete a_pObject;
		a_pObject = NULL;
	}
}

int CallSessionTesting(CSession* a_pObject, int n)
{
	if (a_pObject != NULL)
	{
		//return a_pObject->Testing(n);
		return 0;
	}
}

void CallSessionLogin(CSession* a_pObject, const char* username, const char* hash)
{
	if (a_pObject != NULL)
	{
		a_pObject->Login(username, hash);
	}
}

void __GetValue__(char* str, int strlen) {
	std::string result = debugInstance.getStatus();

	result = result.substr(0, strlen);

	std::copy(result.begin(), result.end(), str);
	str[std::min<int>(strlen - 1, (int)result.size())] = 0;
}