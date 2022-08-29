import hashlib
import secrets

from flask import Flask, request
app = Flask(__name__)

valid_username = "user"
valid_password = "pass"
secret_token = "secret_session_token"

@app.route('/api/auth/server_token', methods=['GET'])
def auth_token():
	username = request.args.get('username')
	server_token = hashlib.sha256("{}".format(username).encode('utf-8')).hexdigest()
	print(server_token)
	return("{}".format(server_token))

@app.route('/api/auth/login', methods=['GET'])
def is_login_correct():

	exuser = request.args.get('username')
	login_token = request.args.get('token')
	
	expass = hashlib.sha256(valid_password.encode('utf-8')).hexdigest()
	extoken = hashlib.sha256("{}".format(valid_username).encode('utf-8')).hexdigest()
	exhash = hashlib.sha256( "{}{}".format(extoken, expass).encode('utf-8')).hexdigest()
	if (exuser == valid_username) and (exhash == login_token):
		return secret_token
	else:
		return "403", 403

if __name__ == '__main__':
	app.run(debug=True)
