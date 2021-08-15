import json
import execute_client
import os

try:
    config_file = open('config/config.json',)
    config_data = json.load(config_file)
    config_file.close()
except FileNotFoundError:
    print("Could not find config.json file.")
    print(os.getcwd())
    raise

try:
    mode = config_data["GUI"]["session_type"]
    game_language = config_data["GUI"]["game"]["language"]
    game_server = config_data["GUI"]["game"]["server"]
    acc_keys = config_data["GUI"]["account_keys"]
    game_directory = config_data["GUI"]["game_directory"]

    account_file = open('config/accounts.json',)
    account_data = json.load(account_file)
    account_file.close()
    execute = execute_client.Execute_Program(account_data[mode]["server"][game_server], mode, game_directory, game_server, game_language)
    execute.determine_mode(acc_keys)

except:
    print("accounts.json has an error")
    raise

finally:
    config_file = open('config/config.json', 'w')
    config_data["script"]["executing"] = False
    config_file.write(json.dumps(config_data, indent=4))
    config_file.close()
