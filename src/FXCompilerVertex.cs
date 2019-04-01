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
            public class FXCompilerVertex : FXCppCompiler
            {
                protected override string GenerateCommandLineCommands()
                {
                    return base.GenerateCommandLineCommands() + " --type vertex";
                }
            }

            public class FXCSharpCompilerVertex : FXCSharpCompiler
            {
                protected override string GenerateCommandLineCommands()
                {
                    return base.GenerateCommandLineCommands() + " --type vertex";
                }
            }
        }
    }
}
