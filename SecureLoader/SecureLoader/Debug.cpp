#include "stdafx.h"
#include "Debug.h"
#include "Session.h"


Debug::Debug(std::string filename, std::string applicationName)
{
	this->applicationName = applicationName;
	this->filename = filename;

#ifdef DEBUG_CONSOLE
	AllocConsole();										// Alloc memory and create console    
	freopen_s((FILE**)stdin, "CONIN$", "r", stdin);		// ----------------------------------------------
	freopen_s((FILE**)stdout, "CONOUT$", "w", stdout);  //  Make iostream library use our console handle
														// ----------------------------------------------
	SetConsoleTitleA(" Secure Loader - DLL console");	// Set console name to a custom one
#endif
	auto file = std::ofstream(this->filename);
	file << "";
}

Debug::~Debug()
{
	
}

void Debug::Log(eLogType logtype, std::string log_message)
{
	std::string str_logtype;
	switch (logtype)
	{
	case Info:
		str_logtype = "INFO";
		break;
	case Warning:
		str_logtype = "WARNING";
		break;
	case Error:
		str_logtype = "ERROR";
		break;
	case Debug1:
		str_logtype = "DEBUG1";
		break;
	case Debug2:
		str_logtype = "DEBUG2";
		break;
	case Debug3:
		str_logtype = "DEBUG3";
		break;
	default:
		break;
	}

	auto file = std::ofstream(this->filename, std::ofstream::app);

	tm timeInfo{ };
	GetCurrentSystemTime(timeInfo);

	std::stringstream ssTime; // Temp stringstream to keep things clean
	
	
	file << "\"" << std::put_time(&timeInfo, "%T") << "\",\"" + str_logtype + "\",\"" + log_message + "\"\n";
	
#ifdef DEBUG_CONSOLE
	ssTime << "[" << std::put_time(&timeInfo, "%T") << "] " << str_logtype << " : " << log_message << std::endl;
	std::cout << ssTime.str();
#endif
	debugInstance.setStatus("[" + str_logtype + "] " + log_message);
}

void Debug::GetCurrentSystemTime(tm& timeInfo)
{
	const std::chrono::system_clock::time_point systemNow = std::chrono::system_clock::now();
	std::time_t now_c = std::chrono::system_clock::to_time_t(systemNow);
	localtime_s(&timeInfo, &now_c); // using localtime_s as std::localtime is not thread-safe
};

void Debug::setStatus(std::string status)
{
	Debug::status = status;
}

std::string Debug::getStatus()
{
	return Debug::status;
}

Debug debugInstance("secure_loader_logs.csv", "Secure Loader");