import json
import os

# Importing and using classes

from configuration_file import ConfigurationFile
from distribution import Distribution

x = 1

print("x is " + str(x))

# Parsing command line arguments
# https://stackoverflow.com/questions/1009860/how-to-read-process-command-line-arguments

# Console colour (see high voted answer)
# https://stackoverflow.com/questions/287871/how-to-print-colored-text-in-terminal-in-python

# Or
# https://stackoverflow.com/questions/37340049/how-do-i-print-colored-output-to-the-terminal-in-python/37340245


# Deserialize json
# https://stackoverflow.com/questions/15476983/deserialize-a-json-string-to-an-object-in-python



from argparse import ArgumentParser

parser = ArgumentParser()
parser.add_argument("-c", "--configuration_file_path", dest="configuration_file_path", help="Configuration file path", metavar="FILE")

args = parser.parse_args()

print(str(args))
print("Configuration file is:" + args.configuration_file_path)

# https://stackoverflow.com/questions/4060221/how-to-reliably-open-a-file-in-the-same-directory-as-a-python-script

__location__ = os.path.realpath(
    os.path.join(os.getcwd(), os.path.dirname(__file__)))

configuration_json = open(os.path.join(__location__, args.configuration_file_path), 'r')

#configuration_file1 = ConfigurationFile()
configuration_file2 = ConfigurationFile(configuration_json)

distribution = Distribution()

#print(configuration_file1)
print(configuration_file2)
print(distribution)


