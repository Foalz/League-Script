import keyboard
import random
import time
from pynput.mouse import Listener,Button,Controller
import pyautogui
import io
import time

mouse = Controller()
class bot:
	def __init__(self, acc_dict, language):
		self.acc_dict = acc_dict
		self.language = language

	champ_file = open('resources/champions.txt')
	champ_list = []

	for i in champ_file:
		champ_list.append(i.strip('\n'))

	def client(self, button):
		button_dict = {
		"play_lobby":f"resources/{self.language}/client/play.png",
		"coop_vs_ai":f"resources/{self.language}/client/coop.png",
		"confirm":f"resources/{self.language}/client/confirm.png",
		"find_match":f"resources/{self.language}/client/find.png",
		"accept":f"resources/{self.language}/client/accept.png",
		"champ_select":f"resources/{self.language}/client/in_champ_select.png",
		
	}

		button_detection = pyautogui.locateOnScreen(button, confidence = 0.9)

		if button == "champ_select":
			if button_detection:
				return self.select_champion()
		
		elif button_detection:
			mouse.position=(pyautogui.center(button_detection))
			time.sleep(0.1)
			mouse.press(Button.left)
			time.sleep(0.1)
			mouse.release(Button.left)
			mouse.position = (163, 604)
			print(True)
			return True

	def select_champion(self):

		selection = pyautogui.locateOnScreen(f'resources/champions/{random.choice(self.champ_list)}', confidence = 0.9)


		if selection:

			mouse.position = (pyautogui.center(selection))
			time.sleep(0.1)
			mouse.press(Button.left)
			time.sleep(0.1)
			mouse.release(Button.left)
			mouse.position = (163,604)
			time.sleep(0.5)
			lock = pyautogui.locateOnScreen(f'resources/{self.language}/client/lock.png', confidence = 0.9)

			if lock:
				mouse.position=(pyautogui.center(lock))
				time.sleep(0.1)
				mouse.press(Button.left)
				time.sleep(0.1)
				mouse.release(Button.left)
				mouse.position = (163, 604)
				return True

		

class play_game(bot):

	def is_in_game(self):
		clock = pyautogui.locateOnScreen(f"resources/{self.language}/client/gameclock.png", confidence = 0.9)
		
		if clock == None:
			return self.game_result()

		else:
			self.auto_click()


	def game_result(self):
		
		victory = pyautogui.locateOnScreen(f"resources/{self.language}/client/victory.png", confidence = 0.9)
		defeat = pyautogui.locateOnScreen(f"resources/{self.language}/client/defeat.png", confidence = 0.9)
		print(victory)

		if victory:
			return "victory"
		elif defeat:
			return "defeat"
		else:
			return self.client(f"resources/{self.language}/client/accept.png")

	def close_game(self):
		pass

	def auto_click(self):
		random_move = random.randint(0,10)

		if random_move == 0:
			mouse.position = (434, 469)
			keyboard.press('a')
			keyboard.release('a')
			mouse.click(Button.left, 1)
			time.sleep(0.5)
			

		else:
			keys = ['a', 'q', 'w', 'e', 'r']
			random_key = random.randint(0,4)
			random_x = random.randint(751,851)
			random_y = random.randint(200,247)
			mouse.position = (random_x, random_y)
			keyboard.press(keys[random_key])
			keyboard.release(keys[random_key])

			keyboard.press('a')
			keyboard.release('a')
			mouse.click(Button.left, 1)
			time.sleep(0.5)
			


a = play_game(3, 'es')

while True:
	
	if a.is_in_game() == "victory":
		break
		


