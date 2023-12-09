using System;
using LinuxFileSystemTo4.Composite;
using Directory = LinuxFileSystemTo4.Composite.Directory;

namespace LinuxFileSystemTo4.Commands;

public class Cd : Command
{
    public Cd(FileExplorer fileExplorer, string[] param) : base(fileExplorer, param)
    {
    }

    public override void Execute()
    {
        base.Execute();

        AFile d = PathChecker.GetFileByPath(param[1].Split("/"), fileExplorer.CurrentDirectory);
        if (d is Directory directory)
        {
            this.fileExplorer.CurrentDirectory = directory;
        }
        else
        {
            throw new ArgumentException("cd command invoked with incorrect arguments!");
        }
    }

    public override bool CheckParameters()
    {
        return ParamChecker.CheckParams(param.Length, param[1], fileExplorer);
    }

    public override string GetHelpString()
    {
        return @"
                cd - Change Directory

                Usage:
                  cd [path]

                Description:
                  The cd command is used to change the current working directory. If a [path] is provided,
                  the current working directory will be set to the specified location.

                Arguments:
                  [path]  The path to the directory you want to change to. If not provided, the home
                          directory of the user will be used as the destination.

                Examples:
                  cd Documents          Change to the 'Documents' directory.
                  cd /var/www           Change to the '/var/www' directory.
                  cd                   Change to the user's home directory.
                ";
    }

    
}