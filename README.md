# Markdown Linked Images Mover

[![Gitpod ready-to-code](https://img.shields.io/badge/Gitpod-ready--to--code-blue?logo=gitpod)](https://gitpod.io/#https://github.com/wonderbird/markdown-linked-images-mover)
[![Build Status Badge](https://github.com/wonderbird/markdown-linked-images-mover/workflows/.NET/badge.svg)](https://github.com/wonderbird/markdown-linked-images-mover/actions?query=workflow%3A%22.NET%22)

Move all images used by a markdown file into a folder.

## Motivation

During online talks and workshops I take notes containing many screenshots. I would like to move these screenshots into a separate folder, so that I can share them later.

## Development and Support Status

I am developing during my spare time and use this project for learning purposes. Please assume that I will need some days to answer your questions. At some point I might lose interest in the project. Please keep this in mind when using this project in a production environment.

## Thanks

Many thanks to [JetBrains](https://www.jetbrains.com/?from=markdown-linked-images-mover) who provide
an [Open Source License](https://www.jetbrains.com/community/opensource/) for this project ❤️.

## Development

### Quick-Start

Click
the [![Gitpod ready-to-code](https://img.shields.io/badge/Gitpod-ready--to--code-blue?logo=gitpod)](https://gitpod.io/#https://github.com/wonderbird/markdown-linked-images-mover)
badge (also above) to launch a web IDE.

If that does not work for you or if you'd like to have the project on your local machine, then continue reading.

### Prerequisites

To compile, test and run this project the latest [.NET SDK](https://dotnet.microsoft.com/download) is required on
your machine. For calculating code metrics I recommend [metrix++](https://github.com/metrixplusplus/metrixplusplus).
This requires python.

If you are interested in test coverage, then you'll need the following tools installed:

```shell
dotnet tool install --global coverlet.console --configfile NuGet-OfficialOnly.config
dotnet tool install --global dotnet-reportgenerator-globaltool --configfile NuGet-OfficialOnly.config
```

## Build, Test, Run

Run the following commands from the folder containing the `.sln` file in order to build and test.

### Build and Test the Solution

```sh
# Build the project
dotnet build

# Run the tests once
dotnet test
```

```shell
# Continuously watch the tests while changing code
dotnet watch -p ./MarkdownLinkedImagesMover.Tests test
```

```shell
# Produce a coverage report and open it in the default browser
rm -r MarkdownLinkedImagesMover.Tests/TestResults && \
  dotnet test --no-restore --verbosity normal /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput='./TestResults/coverage.cobertura.xml' && \
  reportgenerator "-reports:MarkdownLinkedImagesMover.Tests/TestResults/*.xml" "-targetdir:report" "-reporttypes:Html;lcov" "-title:DotnetStarter"
open report/index.html
```

### Run the Application

```shell
# Get help
dotnet run --project MarkdownLinkedImagesMover/MarkdownLinkedImagesMover.csproj -- --help
```

```shell
# Prepare test data to run the application
cp -Rv MarkdownLinkedImagesMover.Tests/data/seed MarkdownLinkedImagesMover.Tests/data/source
mkdir MarkdownLinkedImagesMover.Tests/data/target
```

```shell
# Run the application
dotnet run --project MarkdownLinkedImagesMover/MarkdownLinkedImagesMover.csproj -- --dry-run --file MarkdownLinkedImagesMover.Tests/data/source/Testfile.md --target-dir MarkdownLinkedImagesMover.Tests/data/target
```

```shell
# Inspect the result
ls -la MarkdownLinkedImagesMover.Tests/data/source
ls -la MarkdownLinkedImagesMover.Tests/data/target
```

```shell
# Cleanup test data
rm -r MarkdownLinkedImagesMover.Tests/data/source MarkdownLinkedImagesMover.Tests/data/target
```

### Before Creating a Pull Request ...

#### Apply code formatting rules

```shell
# Install https://csharpier.io globally, once
dotnet tool install -g csharpier

# Format code
dotnet csharpier .
```

#### Check Code Metrics

... check code metrics using [metrix++](https://github.com/metrixplusplus/metrixplusplus)

- Configure the location of the cloned metrix++ scripts
  ```shell
  export METRIXPP=/path/to/metrixplusplus
  ```

- Collect metrics
  ```shell
  python "$METRIXPP/metrix++.py" collect --std.code.complexity.cyclomatic --std.code.lines.code --std.code.todo.comments --std.code.maintindex.simple -- .
  ```

- Get an overview
  ```shell
  python "$METRIXPP/metrix++.py" view --db-file=./metrixpp.db
  ```

- Apply thresholds
  ```shell
  python "$METRIXPP/metrix++.py" limit --db-file=./metrixpp.db --max-limit=std.code.complexity:cyclomatic:5 --max-limit=std.code.lines:code:25:function --max-limit=std.code.todo:comments:0 --max-limit=std.code.mi:simple:1
  ```

At the time of writing, I want to stay below the following thresholds:

```text
--max-limit=std.code.complexity:cyclomatic:5
--max-limit=std.code.lines:code:25:function
--max-limit=std.code.todo:comments:0
--max-limit=std.code.mi:simple:1
```

Finally, remove all code duplication. The next section describes how to detect code duplication.

### Remove Code Duplication Where Appropriate

To detect duplicates I use the [CPD Copy Paste Detector](https://pmd.github.io/latest/pmd_userdocs_cpd.html)
tool from the [PMD Source Code Analyzer Project](https://pmd.github.io/latest/index.html).

If you have installed PMD by download & unzip, replace `pmd` by `./run.sh`.
The [homebrew pmd formula](https://formulae.brew.sh/formula/pmd) makes the `pmd` command globally available.

```sh
pmd cpd --minimum-tokens 50 --language cs --files .
```
