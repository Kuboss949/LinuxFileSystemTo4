using System.Text.RegularExpressions;

namespace LinuxFileSystemTo4.Commands;
using Directory = LinuxFileSystemTo4.Composite.Directory;
public class Cpy : Command
{
    public Cpy(FileExplorer fileExplorer, string[] param) : base(fileExplorer, param)
    {
    }

    public override void Execute()
    {
        base.Execute();
        var src = PathChecker.GetFileByPath(param[1].Split("/"), fileExplorer.CurrentDirectory).Clone();
    
        // Check if the destination is a directory or a file
        var destPath = param[2];
        var destItems = destPath.Split("/");
        var dest = PathChecker.GetFileByPath(destItems, fileExplorer.CurrentDirectory);
        string newName = src.GetName();
        // If the destination is a directory, use it as the destination
        if (dest == null && destItems.Length == 1)
        {
            dest = fileExplorer.CurrentDirectory;
            newName = destItems[^1];
        }else if(dest == null)
        {
            dest = PathChecker.GetFileByPath(destItems.Take(destItems.Length - 1).ToArray(), fileExplorer.CurrentDirectory);
            newName = destItems[^1];
        }
        

        // Check if the destination file already exists
        if (((Directory)dest).FileExists(newName))
            throw new ArgumentException(newName + " already exists in " + dest.GetName());
        
        // Add the source file to the destination directory
        ((Directory)dest).AddFile(src);

        // Rename the source file to the destination name
        src.SetName(newName);
    }

    public override bool CheckParameters()
    {
        return ParamChecker.CheckParams(param.Length, param[1], param[2], fileExplorer);
    }

    public override string GetHelpString()
    {
        return @"
                cpy - Copy Files/Directories

                Usage:
                  cpy [source] [destination]

                Description:
                  The cpy command is used to copy files and directories. Specify the [source]
                  file or directory that you want to copy, and provide the [destination] path
                  where you want to copy the source to.

                Arguments:
                  [source]       The file or directory to be copied.
                  [destination]  The destination path where the source will be copied.

                Examples:
                  cpy document.txt /Backup       Copy 'document.txt' to the '/Backup' directory.
                  cpy /var/www/source /var/www/backup  Copy 'source' directory";
    }
}