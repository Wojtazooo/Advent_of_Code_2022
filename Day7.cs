namespace Advent_of_Code_2022;

public class Day7 : Day
{
    private const char CommandChar = '$';
    private static readonly string ChangeDirectoryCommand = $"{CommandChar} cd";
    private static readonly string ListCommand = $"{CommandChar} ls";
    private const string ParentDirectoryNavigation = "..";
    private const string DirectoryPrefix = "dir ";

    private const int PartOneSizeLimit = 100_000;

    private const int TotalMemory = 70_000_000;
    private const int RequiredMemory = 30_000_000;

    public Day7() : base(7)
    {
        var exampleData = Properties.Resources.ExampleDataDay7.Replace("\r\n", "\n").Split("\n");
        var data = Properties.Resources.DataDay7.Replace("\r\n", "\n").Split("\n");

        Compute(exampleData, out ExamplePart1, out ExamplePart2);
        Compute(data, out Part1, out Part2);
    }

    public class Directory
    {
        public string Name { get; set; }
        public Directory? ParentDirectory { get; private set; } = null;

        public HashSet<Directory> ChildDirectories { get; set; }

        public HashSet<File> Files;

        public Directory(string name)
        {
            Name = name;
            ChildDirectories = new HashSet<Directory>();
            Files = new HashSet<File>();
        }

        public long GetSize(IDictionary<Directory, long> dictionarySizes)
        {
            var size = Files.Sum(x => x.Size);

            foreach (var child in ChildDirectories)
            {
                if (dictionarySizes.ContainsKey(child))
                {
                    size += dictionarySizes[child];
                }
                else
                {
                    size += child.GetSize(dictionarySizes);
                }
            }

            dictionarySizes.Add(this, size);

            return size;
        }

        public void AssignParentDirectory(Directory directory)
        {
            ParentDirectory = directory;
        }

        public void AddChildDirectory(Directory directory)
        {
            ChildDirectories.Add(directory);
            directory.AssignParentDirectory(this);
        }

        public void AddFile(File file)
        {
            Files.Add(file);
        }

        public void Print(int depth = 0)
        {
            var tabs = new string(' ', 2 * depth);

            foreach (Directory directory in ChildDirectories)
            {
                ConsoleWrapper.WriteLineColored($"{tabs} {directory.Name} (dir)", ConsoleColor.DarkYellow);
                directory.Print(depth + 1);
            }

            foreach (var file in Files)
            {
                ConsoleWrapper.WriteLineColored($"{tabs} {file.Name} (file, size={file.Size})", ConsoleColor.Green);
            }
        }
    }

    public record File(string Name, long Size);

    public void Compute(string[] data, out string partOne, out string partTwo)
    {
        var dataRootDirectory = ParseFileSystem(data);

        var sizesDictionary = new Dictionary<Directory, long>();
        var rootSize = dataRootDirectory.GetSize(sizesDictionary);

        partOne = sizesDictionary.Where(x => x.Value < PartOneSizeLimit).Sum(x => x.Value).ToString();

        var freeMemory = TotalMemory - rootSize;
        var minimumMemoryToUpdate = RequiredMemory - freeMemory;

        partTwo = sizesDictionary.Where(x => x.Value > minimumMemoryToUpdate).MinBy(x => x.Value).Value.ToString();
    }

    private static Directory ParseFileSystem(string[] data)
    {
        var rootDirectory = new Directory("root");
        rootDirectory.AddChildDirectory(new Directory("/"));
        var currentDirectory = rootDirectory;

        var index = 0;
        while (index < data.Length)
        {
            var line = data[index];
            if (line.StartsWith(ChangeDirectoryCommand))
            {
                currentDirectory = ChangeDirectory(line, currentDirectory);
                index++;
            }
            else if (line.StartsWith(ListCommand))
            {
                index = AddFilesAndDirectoriesFromList(data, index, currentDirectory);
            }
        }

        return rootDirectory;
    }

    private static int AddFilesAndDirectoriesFromList(string[] data, int listCommandIndex, Directory currentDirectory)
    {
        var currentIndex = listCommandIndex + 1;
        while (currentIndex < data.Length && !data[currentIndex].StartsWith(CommandChar))
        {
            if (data[currentIndex].StartsWith(DirectoryPrefix))
            {
                currentDirectory.AddChildDirectory(new Directory(data[currentIndex]
                    .Replace(DirectoryPrefix, string.Empty)
                    .Trim()));
            }
            else
            {
                var splitFile = data[currentIndex].Split(' ');
                currentDirectory.AddFile(new File(splitFile[1], long.Parse(splitFile[0])));
            }

            currentIndex++;
        }

        return currentIndex;
    }

    private static Directory? ChangeDirectory(string line, Directory? currentDirectory)
    {
        if (line.Contains(ParentDirectoryNavigation))
        {
            currentDirectory = currentDirectory.ParentDirectory;
        }
        else
        {
            currentDirectory =
                currentDirectory.ChildDirectories.First(x =>
                    x.Name == line.Replace(ChangeDirectoryCommand, string.Empty).Trim());
        }

        return currentDirectory;
    }
}