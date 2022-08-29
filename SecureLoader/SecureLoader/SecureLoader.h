#pragma once
#include "Session.h"

// This is the headers for functions exposed from the DLL



extern "C" __declspec(dllexport) CSession* CreateSession(char* server_host, int server_port);

extern "C" __declspec(dllexport) void DisposeSession(CSession* a_pObject);

extern "C" __declspec(dllexport) void CallSessionLogin(CSession* a_pObject, const char* username, const char* hash);

extern "C" { __declspec(dllexport) void __GetValue__(char* str, int strlen); }
