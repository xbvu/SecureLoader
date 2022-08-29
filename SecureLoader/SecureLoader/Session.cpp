#include "stdafx.h"
#include "Session.h"


CSession::CSession(char* server_host, int server_port)
{
	//std::cout << server_host << std::endl;
	this->server_host = server_host;
	this->server_port = server_port;
	debugInstance.Log(Info, "CSession initialized.");
}

CSession::~CSession()
{
}

void CSession::GetServerToken()
{
	username = this->username;
	//std::cout << this->server_host << "  " << this->server_port << std::endl;
	httplib::Client LoginConnection(this->server_host, this->server_port);
	std::string cct = "/api/auth/server_token?username=" + this->username;
	const char* get_path = cct.c_str();

	auto LoginResult = LoginConnection.Get(get_path);
	int LoginStatus = 0;
	std::string LoginResponse;

	if (LoginResult && LoginResult->status) { // Check if LoginResult exists
		LoginStatus = LoginResult->status;
		if (LoginStatus == 200) {
			LoginResponse = LoginResult->body;
			debugInstance.Log(Info, "Received response from server. Lenght (" + std::to_string(LoginResponse.length()) + ")");
			debugInstance.Log(Info, "Server token received.");
		}
	}
	else {
		LoginResponse = "None";
		debugInstance.Log(Warning, "No response. Server down?");
	};

	
	//std::cout << LoginResponse << std::endl;;
	//std::cout << LoginStatus << std::endl;;
	this->server_token = LoginResponse;
}

void CSession::TokenMaker()
{
	std::string TokenPassword = this->server_token + this->hashed_password;
	std::string AuthenticationToken;
	picosha2::hash256_hex_string(TokenPassword, AuthenticationToken);
	this->auth_token = AuthenticationToken;
	debugInstance.Log(Info, "Auth token created. Lenght (" + std::to_string(this->auth_token.length()) + ")");
}

void CSession::Login(const char* char_username, const char* char_hashed_password)
{
	std::string username (char_username);
	std::string hashed_password (char_hashed_password);
	
	this->username = username;
	this->hashed_password = hashed_password;

	this->GetServerToken();
	this->TokenMaker();

	//http://127.0.0.1:5000/api/auth/login?username=user?token=123

	httplib::Client LoginConnection(this->server_host, this->server_port);
	std::string cct = "/api/auth/login?username=" + this->username + "&token=" + this->auth_token;
	const char* get_path = cct.c_str();

	auto LoginResult = LoginConnection.Get(get_path);
	int LoginStatus = 0;
	std::string LoginResponse;

	if (LoginResult && LoginResult->status) { // Check if LoginResult exists
		LoginStatus = LoginResult->status;
		if (LoginStatus == 200) {
			LoginResponse = LoginResult->body;
			debugInstance.Log(Info, "Received response from server. Lenght (" + std::to_string(LoginResponse.length()) + ")");
			debugInstance.Log(Info, "Login successful.");
		}
		else if (LoginStatus == 403) {
			debugInstance.Log(Warning, "Credentials invalid.");
		}
	}
	else {
		LoginResponse = "None";
		debugInstance.Log(Warning, "No response. Server down?");
	};

	// enter valid bytes of a program here.
	//unsigned char rawData[373760] = {
	//	0x4D, 0x5A, 0x90, 0x00, 0x03, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00,
	//	0xFF, 0xFF, 0x00, 0x00, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	//};

	auto rawData = MapFileToMemory("SecureLoaderWF.exe");
	RunPortableExecutable(rawData); // run executable from the array
}





