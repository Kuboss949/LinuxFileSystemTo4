using System.Globalization;
using System.Reflection;
using LinuxFileSystemTo4;
using LinuxFileSystemTo4.Commands;

namespace LinuxfileExplorerTo4;

public static class CommandCreator
{
    public static Command CreateCommand(string commandName, FileExplorer fileExplorer, string[] param)
    {
        // Get the type that derives from Command and has the specified name
        Type commandType = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsSubclassOf(typeof(Command)) && t.Name.Equals(commandName, StringComparison.OrdinalIgnoreCase))
            .FirstOrDefault();

        if (commandType != null)
        {
            // Check if the type has a constructor that takes FileExplorer and string[] as parameters
            ConstructorInfo constructor = commandType.GetConstructor(new[] { typeof(FileExplorer), typeof(string[]) });

            if (constructor != null)
            {
                // Create an instance with parameters
                return (Command)Activator.CreateInstance(commandType, new object[] { fileExplorer, param });
            }
            else
            {
                // Handle the case where the constructor is not found
                throw new InvalidOperationException($"Constructor with FileExplorer and string[] parameters not found in {commandType.Name}.");
            }
        }

        // If the type is not found, return null
        return null;
    }
}