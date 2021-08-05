import keyboard
from pynput.mouse import Listener,Button,Controller
import pyautogui
import io
import time

mouse=Controller()
class login_screen:
    '''
    This class allows you to login a League Of Legends account automatically, to initialize
    this class, you must type the following code:

    name = login_screen('account name', 'password', 'game language')

    The program only allows (by the moment), two languages, english(EN) and spanish(ES)

    __________________________________________________________________________________________

    This program will automatically define that class variable, it depends on the users preferences in
    GUI.
    '''

    def __init__(self, acc_name, password, language):
        self.acc_name = acc_name
        self.password = password
        self.language = language

    def execute_game(self):
        pass
    
    def type_data(self):
        #Detecting username and password field

        files = {
            "user": f"resources/{self.language}/login/user.png", 
            "user1": f"resources/{self.language}/login/user1.png"
        }
      
        for image in files:

            location = pyautogui.locateOnScreen(files[image], confidence=0.9)
            if location:
                print(f'Username found at: {location}')

                try:
                    
                    #Clicking the username field
                    mouse.position=(pyautogui.center(location))
                    time.sleep(0.1)
                    mouse.press(Button.left)
                    time.sleep(0.1)
                    mouse.release(Button.left)
                    mouse.position = (0,0)

                    #Writing the username
                    keyboard.write(self.acc_name)

                    keyboard.press('Tab')
                    keyboard.release('Tab')
                    
                    #Writing the password
                    keyboard.write(self.password)
                    keyboard.press('Enter')
                    keyboard.release('Enter')
                    return True

                except:
                    print("Something went wrong.")
        return False
    



        

        







