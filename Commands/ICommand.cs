using System;

namespace LinuxFileSystemTo4.Commands;

public abstract class Command
{
    protected FileSystem.FileSystem fileSystem;
    protected FileExplorer fileExplorer;
    protected string[] param;

    public Command(FileExplorer fileExplorer, string[] param)
    {
        this.fileExplorer = fileExplorer;
        this.fileSystem = this.fileExplorer.FileSystem;
        this.param = param;
    }

    public virtual void Execute()
    {
        if(param.Length == 2 && param[1] == "?")
            throw new ArgumentException(this.GetHelpString());
        if (!CheckParameters())
        {
            throw new ArgumentException("Command invoked with incorrect arguments! Please invoke command with ? argument to get help.");
        }
    }
    public abstract bool CheckParameters();
    public abstract string GetHelpString();
}