using System;
using System.IO;

namespace MarkdownLinkedImagesMover.Tests;

public sealed class FileMoverTests : IDisposable
{
    private readonly TestDirectory _testDir;

    public FileMoverTests() => _testDir = TestDirectory.Create();

    [Fact]
    public void MoveExistingFileToValidDestination()
    {
        var sourceFile = new FileInfo(Path.Combine(_testDir.SourceDir.FullName, "Testfile.md"));
        var targetDir = _testDir.TargetDir;

        FileMover.Move(sourceFile, targetDir);

        var hasMoved = new FileInfo(Path.Combine(targetDir.FullName, sourceFile.Name)).Exists;
        Assert.True(hasMoved, "file should be moved to target folder");
    }

    private void ReleaseUnmanagedResources()
    {
        _testDir.Delete();
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~FileMoverTests()
    {
        ReleaseUnmanagedResources();
    }
}
