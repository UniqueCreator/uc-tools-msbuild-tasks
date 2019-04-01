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
            public class FXCppCompiler : FXCompiler
            {
                public FXCppCompiler()
                {
                    m_switchOrderList.Add("CPPHeaderOutput");
                    m_switchOrderList.Add("CPPOutput");
                    Backend = "dev";
                }

                [Output]
                public virtual string CPPHeaderOutput
                {
                    get
                    {
                        return getProperty("CPPHeaderOutput");
                    }

                    set
                    {
                        setPropertyString("CPPHeaderOutput", value, "--header");
                    }
                }

                [Output]
                public virtual string CPPOutput
                {
                    get
                    {
                        return getProperty("CPPOutput");
                    }
                    set
                    {
                        setPropertyString("CPPOutput", value, "--cpp");
                    }
                }
            }
        }
    }
}
