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
            public class FXCompilerGeometry : FXCppCompiler
            {
                protected override string GenerateCommandLineCommands()
                {
                    return base.GenerateCommandLineCommands() + " --type geometry";
                }
            }

            public class FXCSharpCompilerGeometry : FXCSharpCompiler
            {
                protected override string GenerateCommandLineCommands()
                {
                    return base.GenerateCommandLineCommands() + " --type geometry";
                }
            }


        }
    }
}
