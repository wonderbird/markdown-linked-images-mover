using System.IO;

namespace MarkdownLinkedImagesMover;

internal class FileMover : IFileMover
{
    public void Move(FileInfo sourceFile, DirectoryInfo targetDir)
    {
        var destinationFile = Path.Combine(targetDir.FullName, sourceFile.Name);
        sourceFile.MoveTo(destinationFile);
    }
}
