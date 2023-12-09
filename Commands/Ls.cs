using System;
using System.IO;
using LinuxFileSystemTo4.Composite;

namespace LinuxFileSystemTo4.Commands;
using Directory = LinuxFileSystemTo4.Composite.Directory;

public class Ls : Command
{
    public Ls(FileExplorer fileExplorer, string[] param) : base(fileExplorer, param)
    {
    }

    public override void Execute()
    {
        base.Execute();

        AFile d;
        if (param.Length == 1)
        {
            d = fileExplorer.CurrentDirectory;
        }
        else
        {
            d = PathChecker.GetFileByPath(param[1].Split("/"), fileExplorer.CurrentDirectory);
            if (!(d is Directory))
            {
                throw new ArgumentException("cd command invoked with incorrect arguments!");
            }
        }
        Console.WriteLine(d.Read());
    }

    public override bool CheckParameters()
    {
        if (param.Length > 1)
            return ParamChecker.CheckParams(param.Length, param[1], fileExplorer);
        return true;
    }

    public override string GetHelpString()
    {
        return @"
                ls - List Directory Contents

                Usage:
                  ls [path]

                Description:
                  The ls command is used to list the contents of a directory. If a [path] is provided,
                  it will display the contents of the specified directory; otherwise, it will show
                  the contents of the current working directory.

                Arguments:
                  [path]  The path to the directory whose contents you want to list. If not provided,
                          the current working directory will be used.

                Examples:
                  ls                  List contents of the current working directory.
                  ls Documents        List contents of the 'Documents' directory.
                  ls /var/www         List contents of the '/var/www' directory.
                ";
    }
}