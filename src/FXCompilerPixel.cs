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
            public class FXCompilerPixel : FXCppCompiler
            {
                protected override string GenerateCommandLineCommands()
                {
                    return base.GenerateCommandLineCommands() + " --type pixel";
                }
            }

            public class FXCSharpCompilerPixel : FXCSharpCompiler
            {
                protected override string GenerateCommandLineCommands()
                {
                    return base.GenerateCommandLineCommands() + " --type pixel";
                }
            }
        }
    }
}
