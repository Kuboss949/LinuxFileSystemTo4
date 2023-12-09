namespace LinuxFileSystemTo4.Composite;

public abstract class AFile
{
    protected string name;
    public AFile Parent { get; set; }
       
    public abstract string Read();
    public abstract AFile Clone();

    public string GetName()
    {
        return this.name;
    }

    public void SetName(string name)
    {
        this.name = name;
    }
}