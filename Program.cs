// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using LinuxFileSystemTo4;
using LinuxFileSystemTo4.Composite;
using LinuxFileSystemTo4.FileSystem;
using Directory = LinuxFileSystemTo4.Composite.Directory;

FileSystem fileSystem = new FileSystem();
FileExplorer fileExploler = new FileExplorer(fileSystem);


fileExploler.CurrentDirectory = fileSystem.RootDirectory;
List<string> availableCommands = new List<string>() { "mv", "cpy", "mkdir", "more", "ls", "cd", "tree" };

string command;
string[] components;
while (true)
{
    Console.Write(fileExploler.GetCurrentDirectoryWithPath() + ">");
    command = Console.ReadLine();
    if (command != "")
    {
        fileExploler.PerformCommand(command, availableCommands);
    }
}



