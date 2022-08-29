#pragma once

#include "stdafx.h"

#define DEBUG_CONSOLE

using jsonf = nlohmann::json;
using json = nlohmann::json;

enum eLogType {Info, Warning, Error, Debug1, Debug2, Debug3};

class Debug
{
	std::string applicationName;
	std::string filename;
	jsonf jsonfile;
	std::ofstream file;
	std::string status;

public:
	Debug(std::string filename, std::string applicationName);
	~Debug();
	void GetCurrentSystemTime(tm& timeInfo);
	void Log(eLogType LogType, std::string log_message);
	void setStatus(std::string status);
	std::string getStatus();
};

extern Debug debugInstance;