import json

class ConfigurationFile:
    def __init__(self, j):
        self.__dict__ = json.loads(j)

    distributions = []