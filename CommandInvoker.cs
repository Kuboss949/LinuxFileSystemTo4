using System;
using System.IO;
using LinuxfileExplorerTo4;
using LinuxFileSystemTo4.Commands;

namespace LinuxFileSystemTo4;

public class CommandInvoker
{
    private Command command;
    private FileExplorer fileExplorer;

    public CommandInvoker(FileExplorer fileExplorer)
    {
        this.fileExplorer = fileExplorer;
    }

    public void SetCommand(string command)
    {
        string[] components = command.Split(" ");
        this.command = CommandCreator.CreateCommand(components[0], fileExplorer, components);
        if (this.command == null)
        {
            throw new InvalidDataException("No such command!");
        }
    }

    public void ExecuteCommand()
    {
        try
        {
            command.Execute();
        }
        catch (ArgumentException e)
        {
            throw e;
        }
    }

}