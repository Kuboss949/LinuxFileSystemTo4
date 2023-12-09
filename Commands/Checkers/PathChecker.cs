using LinuxFileSystemTo4.Composite;

namespace LinuxFileSystemTo4.Commands;
using Directory = LinuxFileSystemTo4.Composite.Directory;
public static class PathChecker
{
    public static bool CheckPath(string[] files, Directory currentDirectory)
    {
        if (files[0] == "root")
        {
            return CheckPathInternal(files, currentDirectory.Clone());
        }
        return CheckPathInternal(files, currentDirectory.Clone(), true);
        
    }
    

    private static bool CheckPathInternal(string[] files, AFile current, bool isRelative = false)
    {
        int startIndex = 0;

        if (isRelative)
        {
            while (startIndex < files.Length && files[startIndex] == "..")
            {
                current = current.Parent;
                startIndex++;
            }
        }
        else
        {
            while (current.GetName() != "root")
            {
                current = current.Parent;
            }

            startIndex = 1;
        }

        for (int i = startIndex; i < files.Length; i++)
        {
            if (current is Directory && !((Directory)current).FileExists(files[i]) || !(current is Directory))
            {
                return false;
            }
            current = ((Directory)current).GetFile(files[i]);
        }

        return true;
    }
    
    
    public static AFile GetFileByPath(string[] path, Directory currentDirectory)
    {
        string[] files = path;
        if (files[0] == "root")
        {
            return GetFileByPathInternal(files, currentDirectory);
        }
        return GetFileByPathInternal(files, currentDirectory, true);
    }

    private static AFile GetFileByPathInternal(string[] files, AFile current, bool isRelative = false)
    {
        if (current.GetName() == "root" && files.Length == 1 && files[0] == "..")
        {
            return current;
        }
        
        int startIndex = 0;

        if (isRelative)
        {
            while (startIndex < files.Length && files[startIndex] == "..")
            {
                current = current.Parent;
                startIndex++;
            }
        }
        else
        {
            while (current.GetName() != "root")
            {
                current = current.Parent;
            }

            startIndex = 1;
        }

        for (int i = startIndex; i < files.Length; i++)
        {
            current = ((Directory)current).GetFile(files[i]);
        }

        return current;
    }
}