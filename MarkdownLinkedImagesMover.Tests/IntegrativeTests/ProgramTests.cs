using System.IO;

namespace MarkdownLinkedImagesMover.Tests.IntegrativeTests;

public sealed class ProgramTests
{
    [Fact]
    public void MarkdownFileWithTwoImages()
    {
        using var testDir = TestDirectory.Create();

        var sourceFile = new FileInfo(Path.Combine(testDir.SourceDir.FullName, "Testfile.md"));

        Program.Main(sourceFile, testDir.TargetDir);

        AssertFileDoesNotExist("noun-island-1479438.png", testDir.SourceDir);
        AssertFileDoesNotExist("noun-starship-3799189.png", testDir.SourceDir);
        AssertFileExists("noun-island-1479438.png", testDir.TargetDir);
        AssertFileExists("noun-starship-3799189.png", testDir.TargetDir);
    }

    [Fact]
    public void DryRun()
    {
        using var testDir = TestDirectory.Create();

        var sourceFile = new FileInfo(Path.Combine(testDir.SourceDir.FullName, "Testfile.md"));

        Program.Main(sourceFile, testDir.TargetDir, true);

        AssertFileExists("noun-island-1479438.png", testDir.SourceDir);
        AssertFileExists("noun-starship-3799189.png", testDir.SourceDir);
        AssertFileDoesNotExist("noun-island-1479438.png", testDir.TargetDir);
        AssertFileDoesNotExist("noun-starship-3799189.png", testDir.TargetDir);
    }

    private static void AssertFileDoesNotExist(string fileName, FileSystemInfo dir) => AssertFileStatus(FileStatus.DoesNotExist, fileName, dir);

    private static void AssertFileExists(string fileName, FileSystemInfo dir) => AssertFileStatus(FileStatus.DoesExist, fileName, dir);

    private static void AssertFileStatus(FileStatus expected, string fileName, FileSystemInfo dir)
    {
        var file = new FileInfo(Path.Combine(dir.FullName, fileName));

        if (expected == FileStatus.DoesExist)
        {
            Assert.True(file.Exists, $"file '{file.FullName}' should exist");
        }
        else
        {
            Assert.False(file.Exists, $"file '{file.FullName}' should not exist");
        }
    }

    private enum FileStatus
    {
        DoesNotExist = 0,
        DoesExist
    }
}