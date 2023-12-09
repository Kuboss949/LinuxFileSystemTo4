using LinuxFileSystemTo4.Composite;
using Directory = LinuxFileSystemTo4.Composite.Directory;
namespace LinuxFileSystemTo4.Commands;

public class More : Command
{
    public More(FileExplorer fileExplorer, string[] param) : base(fileExplorer, param)
    {
    }

    public override void Execute()
    {
        base.Execute();
        AFile file = PathChecker.GetFileByPath(param[1].Split("/"), fileExplorer.CurrentDirectory);
        if (file is Directory)
        {
            Console.WriteLine("\n" + "*** " + file.GetName() + ": directory ***" + "\n");
        }
        else
        {
            Console.WriteLine(((TextFile)file).Read());
        }
    }

    public override bool CheckParameters()
    {
        return ParamChecker.CheckParams(param.Length, param[1], fileExplorer);
    }

    public override string GetHelpString()
    {
        throw new NotImplementedException();
    }
}