import os
def CheckingPyFiles():
    try:
        import execute_client

    except:
        print('Missing file: execute_client.py')
        return False

    try:
        from login import login_screen

    except:
        print('Missing file: login.py')
        return False

    try:
        import keyboard
        
    except:
        os.system('pip install keyboard')

    try:
        from pynput.mouse import Listener,Button,Controller

    except:
        os.system('pip install pynput')

    try:
        import pyautogui

    except:
        os.system('pip install pyautogui')
    return True
