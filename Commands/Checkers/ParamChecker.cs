using System.Text.RegularExpressions;
using LinuxFileSystemTo4.Composite;

namespace LinuxFileSystemTo4.Commands;

public static class ParamChecker
{
    public static bool CheckParams(int numberOfParams, string path1, FileExplorer fileExplorer)
    {
        if (numberOfParams != 2)
            return false;
        if (!PathChecker.CheckPath(path1.Split("/"), fileExplorer.CurrentDirectory))
        {
            return false;
        }

        return true;

    }

    public static bool CheckParams(int numberOfParams, string path1, string path2, FileExplorer fileExplorer)
    {
        if (numberOfParams != 3)
            return false;
        string[] files = path2.Split("/");
        if (!PathChecker.CheckPath(path1.Split("/"), fileExplorer.CurrentDirectory))
        {
            return false;
        }

        if (files.Length != 1 &&
            !PathChecker.CheckPath(files.Take(files.Length - 1).ToArray(), fileExplorer.CurrentDirectory))
        {
            return false;
        }
        
        return CheckName(files[^1]);
    }

    public static bool CheckParamsMkdir(int numberOfParams, string path1, FileExplorer fileExplorer)
    {
        if (numberOfParams != 2)
            return false;
        string[] files = path1.Split("/");
        if (files.Length > 1 && !PathChecker.CheckPath(files.Take(files.Length-1).ToArray(), fileExplorer.CurrentDirectory))
        {
            return false;
        }

        return CheckName(files[^1]);
    }

    public static bool CheckName(string name)
    {
        Regex regex = new Regex("^(?!\\.\\.?$)[a-zA-Z0-9.]+$");

        return regex.IsMatch(name);
    }
}