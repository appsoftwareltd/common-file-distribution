# Common File Distribution

A simple utility for copying files from a one or more source directories to one or more destination directories (per source). A need to replicate core files among applications in microservice architecture was the inspiration for this project.

#### Example distribution configuration file:

    {
      "Distributions": [
        {

          "SourceDirectory": "C:\\Users\\me\\Desktop\\SrcExample",
          "DestinationDirectories": [ "C:\\Users\\me\\Desktop\\DestExample1", "C:\\Users\\me\\Desktop\\DestExample2" ],
          "FileNameMaskRegex": ".+\\.cs"
        }
      ]
    }

## Running dotnet-script version

A version of this tool for running with dotnet-script is included in this repository at ```src/dotnet-script/app.csx```.

Full documentation for dotnet-script is here: https://github.com/filipw/dotnet-script

To run using dotnet-script, you need to have the .NET Core 2.2 runtime installed.

If you do not have dotnet-script installed you can then install the tool using:

    dotnet tool install -g dotnet-script

Navigate your terminal to the directory in this repository ```src/dotnet-script```.

Run ```> dotnet script app.csx```

### Running remotely

Since dotnet-script allows for running of scripts from remote locations, you can run the .csx script file without downloading this repository. You will need a copy of a file in the FileDistributions.json format to direct the file dsitribution.   

```> dotnet script https://raw.githubusercontent.com/garethrbrown/app-software-file-distribution/master/src/netcore-script/app.csx "C:\Path\To\FileDistributions.json"```


### Change log

2019/09/20 Added regex file name mask capability
