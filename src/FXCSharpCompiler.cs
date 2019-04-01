using System;
using System.Collections;
using System.Resources;

using Microsoft.Build.CPPTasks;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace UniqueCreator
{
    namespace Build
    {
        namespace Tasks
        {
            public class FXCSharpCompiler : FXCompiler
            {
                public FXCSharpCompiler()
                {
                    m_switchOrderList.Add("EntryPointName");
                    m_switchOrderList.Add("CSOutput");

                    Backend = "cs";
                }

                [Output]
                public virtual string CSOutput
                {
                    get
                    {
                        return getProperty("CSOutput");
                    }
                    set
                    {
                        setPropertyString("CSOutput", value, "--cs");
                    }
                }
            }
        }
    }
}
