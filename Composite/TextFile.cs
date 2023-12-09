using System.Runtime.InteropServices.JavaScript;

namespace LinuxFileSystemTo4.Composite;

public class TextFile : AFile
{
    private string content;

    public TextFile(string name, string content = "")
    {
        this.name = name;
        this.content = content;
    }
    
    public override string Read()
    {
        return this.content;
    }
    public override AFile Clone()
    {
        return new TextFile(this.name, this.content);
    }

    public void Append(string text)
    {
        this.content += text;
    }
    
    
}