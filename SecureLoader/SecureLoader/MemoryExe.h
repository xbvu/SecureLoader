#pragma once

#include <iostream> // Standard C++ library for console I/O
#include <string> // Standard C++ Library for string manip

#include <Windows.h> // WinAPI Header
#include <TlHelp32.h> //WinAPI Process API

#include <fstream>

HANDLE MapFileToMemory(LPCSTR filename);
int RunPortableExecutable(void* Image);