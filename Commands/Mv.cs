using System.Text.RegularExpressions;
using LinuxFileSystemTo4.Composite;

namespace LinuxFileSystemTo4.Commands;
using Directory = LinuxFileSystemTo4.Composite.Directory;

public class Mv : Command
{
    public Mv(FileExplorer fileExplorer, string[] param) : base(fileExplorer, param)
    {
    }

    public override void Execute()
    {

        base.Execute();
        var src = PathChecker.GetFileByPath(param[1].Split("/"), fileExplorer.CurrentDirectory);
    
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

        // Remove the source file from its current parent directory
        ((Directory)src.Parent).RemoveFile(param[1].Split("/")[^1]);

        // Add the source file to the destination directory
        ((Directory)dest).AddFile(src);

        // Rename the source file to the destination name
        src.SetName(newName);
        
    }

    public override bool CheckParameters()
    {
        if (param.Length != 3)
            return false;
        string[] files = param[2].Split("/");
        if (!PathChecker.CheckPath(param[1].Split("/"), fileExplorer.CurrentDirectory))
        {
            return false;
        }

        if (files.Length != 1 &&
            !PathChecker.CheckPath(files.Take(files.Length - 1).ToArray(), fileExplorer.CurrentDirectory))
        {
            return false;
        }
        Regex regex = new Regex("^[a-zA-Z0-9.]+$");

        return regex.IsMatch(files[^1]);
    }

    public override string GetHelpString()
    {
        return @"
                mv - Move or Rename Files/Directories

                Usage:
                  mv [source] [destination]

                Description:
                  The mv command is used to move or rename files and directories. Specify the [source]
                  file or directory that you want to move or rename, and provide the [destination] path
                  where you want to move the source to or the new name for renaming.

                Arguments:
                  [source]       The file or directory to be moved or renamed.
                  [destination]  The destination path or new name for the source.

                Examples:
                  mv file.txt Documents       Move 'file.txt' to the 'Documents' directory.
                  mv /var/www/old_dir /backup  Move 'old_dir' to the '/backup' directory.
                  mv old_file.txt new_file.txt Rename 'old_file.txt' to 'new_file.txt'.
                ";
    }
}