using System.Text.RegularExpressions;

namespace LinuxFileSystemTo4.Commands;
using Directory = LinuxFileSystemTo4.Composite.Directory;
public class Mkdir : Command
{
    public Mkdir(FileExplorer fileExplorer, string[] param) : base(fileExplorer, param)
    {
    }

    public override void Execute()
    {
        base.Execute();
        Directory d;
        string[] files = param[1].Split("/");
        if (param[1].Split("/").Length == 1)
        {
            d = fileExplorer.CurrentDirectory;
        }
        else
        {
            d = (Directory)PathChecker.GetFileByPath(files.Take(files.Length-1).ToArray(), fileExplorer.CurrentDirectory);
        }

        if (d.FileExists(files[^1]))
            throw new ArgumentException(files[^1] + " already exists in " + d.GetName());
        
        d.AddFile(new Directory(files[^1]));
        
    }

    public override bool CheckParameters()
    {
        return ParamChecker.CheckParamsMkdir(param.Length, param[1], fileExplorer);
    }

    public override string GetHelpString()
    {
        return @"
                mkdir - Make Directory

                Usage:
                  mkdir [path/folderName]

                Description:
                  The mkdir command is used to create a new directory. You can specify either a [path]
                  to create a directory at a specific location or just provide the [folderName] to create
                  a directory in the current working directory.

                Arguments:
                  [path]         The path where you want to create the new directory.
                  [folderName]   The name of the new directory to be created in the current working directory.

                Examples:
                  mkdir Documents         Create a 'Documents' directory in the current working directory.
                  mkdir /var/www/new_dir  Create a 'new_dir' directory in the '/var/www' directory.
                ";
    }
}