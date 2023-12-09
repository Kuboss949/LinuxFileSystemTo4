using System.Text;
using LinuxFileSystemTo4.Composite;

namespace LinuxFileSystemTo4.Commands;
using Directory = LinuxFileSystemTo4.Composite.Directory;

public class Tree : Command
{
    public Tree(FileExplorer fileExplorer, string[] param) : base(fileExplorer, param)
    {
    }

    public override void Execute()
    {
        base.Execute();
        Directory d;
        if (param.Length == 1)
        {
            d = fileExplorer.CurrentDirectory;
        }
        else
        {
            AFile f = PathChecker.GetFileByPath(param[1].Split("/"), fileExplorer.CurrentDirectory);
            if (!(f is Directory))
            {
                throw new ArgumentException("Parameter must be a directory!");
            }

            d = (Directory)f;
        }
        
        var treeOutput = GenerateTree(d, "");
        
        Console.WriteLine(treeOutput);
    }
    private string GenerateTree(Directory directory, string prefix)
    {
        StringBuilder treeOutput = new StringBuilder();

        // Display the current directory
        treeOutput.AppendLine(prefix +  directory.GetName());

        // Iterate through the content of the directory
        foreach (var file in directory.Content)
        {
            if (file is Directory)
            {
                // Recursively generate the tree for subdirectories
                treeOutput.Append( GenerateTree((Directory)file, prefix + "   "));
            }
            else
            {
                // Display the file within the directory
                treeOutput.AppendLine(prefix  + file.GetName());
            }
            /*if (file != directory.Content.Last())
            {
                treeOutput.AppendLine("|");
            }*/
        }

        return treeOutput.ToString();
    }

    public override bool CheckParameters()
    {
        if (param.Length > 1)
            return ParamChecker.CheckParams(param.Length, param[1], fileExplorer);
        return true;
    }

    public override string GetHelpString()
    {
        throw new NotImplementedException();
    }
}