using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LinuxFileSystemTo4.Commands;
using LinuxFileSystemTo4.Composite;

namespace LinuxFileSystemTo4;
using Directory = LinuxFileSystemTo4.Composite.Directory;


public class FileExplorer
{
    private Directory currentDirectory;

    public FileSystem.FileSystem FileSystem { get; set; }

    private CommandInvoker commandInvoker;

    public Directory CurrentDirectory
    {
        get => currentDirectory;
        set => currentDirectory = value ?? throw new ArgumentNullException(nameof(value));
    }

    public FileExplorer(FileSystem.FileSystem fileSystem)
    {
        FileSystem = fileSystem;
        commandInvoker = new CommandInvoker(this);
        CurrentDirectory = fileSystem.RootDirectory;
    }

    public void PerformCommand(string command, List<string> availableCommands)
    {
        try
        {
            commandInvoker.SetCommand(command);
            commandInvoker.ExecuteCommand();
        }
        catch (InvalidDataException e)
        {
            Console.WriteLine(e.Message + " Available commands: " + availableCommands);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public string GetCurrentDirectoryWithPath()
    {
        if (currentDirectory.Parent == null)
            return currentDirectory.GetName();
        AFile d = currentDirectory.Parent;
        List<string> directories = new List<string>(){};
        string output = "";
        while (d != null)
        {
            directories.Add(d.GetName());
            d = d.Parent;
        }

        directories.Reverse();
        foreach (var name in directories)
        {
            output += name + "/";
        }

        return output + currentDirectory.GetName();

    }
}