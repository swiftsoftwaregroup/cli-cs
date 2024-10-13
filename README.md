# cli-cs
Template for Command Line Interface (CLI) tool in C# 

## Development

### Setup for macOS

Download the `dotnet-install.sh` script

```bash
curl -sSL https://dot.net/v1/dotnet-install.sh > dotnet-install.sh
chmod +x dotnet-install.sh
```

#### .NET Runtime 8.0 (LTS)

```
./dotnet-install.sh --runtime dotnet --channel 8.0 --version latest
```

or download and install from [Microsoft](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

#### .NET SDK 8.0

Install with `dotnet-install.sh` script:

```bash
./dotnet-install.sh --channel 8.0
```

#### Test

Test that you can run the `dotnet` CLI (command line interface)

```bash
~/.dotnet/dotnet --version
~/.dotnet/dotnet new console --help
```

#### DocFX

Install DocFX for docs generation:

```bash
dotnet tool install -g docfx
```

### Work on macOS

Configure project:

```bash
source configure.sh
```

Open the project in Visual Studio Code:

```bash
code .
```

####  Run

Build:

```bash
# build
dotnet build

# create alias for easy calling
alias cli-cs=cli-cs/bin/Debug/net8.0/cli-cs
```

Run the compiled program:

```bash
echo "John" > name.txt

cli-cs greet name.txt
cli-cs greet --language es name.txt
cli-cs greet -l bg name.txt
```

Run via the `dotnet` CLI without building:

```bash
echo "John" > name.txt

dotnet run --project cli-cs -- greet name.txt
dotnet run --project cli-cs -- greet --language es name.txt
dotnet run --project cli-cs -- greet -l bg name.txt
```

#### Test

```bash
dotnet test cli-cs-tests
```

#### Docs

```bash
# build
docfx docfx/docfx.json

# serve
docfx docfx/docfx.json --serve --open-browser
```

### How to create a new project

#### Create .net solution

```bash
dotnet new sln --name cli-cs
```

#### Create C# project

```bash
# add cli project
dotnet new console --name cli-cs --framework net8.0
dotnet sln add cli-cs

# add packages
dotnet add cli-cs package System.CommandLine --prerelease

# add test project
dotnet new xunit --name cli-cs-tests
dotnet sln add cli-cs-tests

# add reference to the main project
dotnet add cli-cs-tests reference cli-cs
```

#### Create docfx project

```bash
mkdir -p docfx 
cd docfx
docfx init --yes
```

