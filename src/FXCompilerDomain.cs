using System;
using System.Collections;
using System.Resources;

using Microsoft.Build.CPPTasks;
using Microsoft.Build.Framework;

namespace UniqueCreator
{
    namespace Build
    {
        namespace Tasks
        {
            public class FXCompilerDomain : FXCppCompiler
            {
                protected override string GenerateCommandLineCommands()
                {
                    return base.GenerateCommandLineCommands() + " --type domain";
                }
            }

            public class FXCSharpCompilerDomain : FXCSharpCompiler
            {
                protected override string GenerateCommandLineCommands()
                {
                    return base.GenerateCommandLineCommands() + " --type domain";
                }
            }
        }
    }
}
