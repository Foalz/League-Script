from login import login_screen


class Execute_Program:

    def __init__(self, acc_dict, mode, game_dir, server, language="en"):
        self.acc_dict = acc_dict
        self.mode = mode
        self.game_dir = game_dir
        self.server = server
        self.language = language
        

    def determine_mode(self, acc_keys):
        try:
            new_acc_dict = self.acc_dict
            key=0
            for account in new_acc_dict:
                new_acc_dict[account] = tuple((new_acc_dict[account], acc_keys[key]))
                key += 1

            if self.mode == "normal":
                self.normal_mode(new_acc_dict, self.game_dir, self.language, self.server)

            elif self.mode == "bot":
                self.bot_mode()

        except:
            raise

    def modify_leagueconfig(self, game_dir, language, server):

        league_config = open(game_dir + "/Config/LeagueClientSettings.yaml")
        command_list = []
        language_dict = {'en': 'en_US', 'kr': 'ko_KR', 'es': 'es_MX'}
        server_dict = {'euw':'EUW', 'lan':'LA1', 'na':'NA'}

        for i in league_config:
            command_list.append(i.strip('\n '))

        try:
            command_list[12] = f'locale: "{language_dict[language]}"'
            command_list[13] = f'region: "{server_dict[server]}"'
                

        except:
            raise
        league_config.close()  #Reading the league config file

        try:
            file = open(game_dir + "/Config/LeagueClientSettings.yaml",'w')

            for i in command_list:
                file.write(i+'\n')
            file.close()
        except: 
            raise



    def normal_mode(self, new_acc_dict, game_dir, language, server):
        try:
            import os
            import login
            import keyboard
            import time
            self.modify_leagueconfig(game_dir, language, server)
            os.startfile(game_dir + f"/{language}.lnk")
            
        
        except:
            raise
        

        while True: 
            time.sleep(0.1)
            key = keyboard.read_key()
            

            for i in new_acc_dict:

                if key in new_acc_dict[i]:
                    
                    log_data = login_screen(i, new_acc_dict[i][0], language)
                    log_data.type_data()
                    break

        

       
        
        

    def bot_mode(self):
        try:
            import login 
            

        except:
            raise

        pass
        