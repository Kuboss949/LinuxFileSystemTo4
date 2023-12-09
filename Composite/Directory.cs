using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;

namespace LinuxFileSystemTo4.Composite;

public class Directory : AFile 
{

    public List<AFile> Content { get; set; }


    public Directory(string name)
    {
        this.name = name;
        this.Content = new List<AFile>();
    }

    
    public override string Read()
    {
        StringBuilder outputBuilder = new StringBuilder();
        string fname;
        string type = "";

        foreach (var file in Content)
        {
            if (file is Directory)
                type = "d";
            else
                type = "f";

            fname = file.GetName();
            outputBuilder.Append(type + ": " + fname + "\n");
        }

        // Remove the trailing newline character if the string is not empty
        if (outputBuilder.Length > 0)
        {
            outputBuilder.Length--; // Remove the last character (newline)
        }

        return outputBuilder.ToString();
    }
    
    public override AFile Clone()
    {
        Directory clone = new Directory(this.name);
        foreach (var file in Content)
        {
            clone.AddFile(file.Clone()); // Clone the child files recursively
        }

        clone.Parent = this.Parent;
        return clone;
    }

    public void AddFile(AFile file)
    {
        Content.Add(file);
        file.Parent = this;
    }
    

    public bool RemoveFile(string name)
    {
        AFile fileToRemove = Content.FirstOrDefault(file => file.GetName() == name);

        if (fileToRemove != null)
        {
            Content.Remove(fileToRemove);
            return true; // File successfully removed
        }

        return false; // File not found
    }

    public AFile GetFile(string name)
    {
        return Content.FirstOrDefault(file => file.GetName() == name);
    }
    public bool FileExists(string name)
    {
        if (Content.FirstOrDefault(file => file.GetName() == name) != null)
        {
            return true;
        }

        return false;
    }
    
    
}