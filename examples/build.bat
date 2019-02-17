nuget install packages.config -o packages
nuget update packages.config -r packages

msbuild build.csproj /verbosity:diagnostic
