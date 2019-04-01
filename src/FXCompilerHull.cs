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
            public class FXCompilerHull : FXCppCompiler
            {
                protected override string GenerateCommandLineCommands()
                {
                    return base.GenerateCommandLineCommands() + " --type hull";
                }
            }

            public class FXCSharpCompilerHull : FXCSharpCompiler
            {
                protected override string GenerateCommandLineCommands()
                {
                    return base.GenerateCommandLineCommands() + " --type hull";
                }
            }
        }
    }
}
