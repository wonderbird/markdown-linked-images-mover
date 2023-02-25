using System;
using System.IO;
using System.Linq;

namespace MarkdownLinkedImagesMover;

/// <summary>
/// Program entry point.
/// </summary>
public static class Program
{
    /// <summary>
    /// Move all images referred to by the given markdown file to a folder.
    /// </summary>
    /// <param name="file">Markdown file containing the references to image files which shall be moved</param>
    public static void Main(FileInfo file)
    {
        var fileContent = File.ReadAllText(file.FullName);
        var images = MarkdownParser.ParseLinkedImages(fileContent);
        var imageNames = string.Join(", ", images.Select(f => $"'{f.Name}'"));
        Console.WriteLine($"File '{file.Name}' contains {imageNames}.");
    }
}