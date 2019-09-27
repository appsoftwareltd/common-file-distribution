import yaml

class ConfigurationFile:
    def __init__(self, values):
        self.__dict__ = values
    
    #@staticmethod
    #def load(data):
    #   values = yaml.safe_load(data)
    #   return User(values["name"], values["surname"])
        
    def yaml(self):
       return yaml.dump(self.__dict__)

    distributions = []


    # https://stackoverflow.com/questions/1773805/how-can-i-parse-a-yaml-file-in-python
    # https://stackoverflow.com/questions/1773805/how-can-i-parse-a-yaml-file-in-python
    # https://stackoverflow.com/questions/53766277/installing-python-packages-for-visual-studio-code