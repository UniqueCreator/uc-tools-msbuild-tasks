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
            public class FXCompiler : BaseToolTask
            {
                public FXCompiler()
                {
                    m_switchOrderList.Add("EntryPointName");
                    m_switchOrderList.Add("CPPHeaderOutput");
                    m_switchOrderList.Add("CPPOutput");
                    m_switchOrderList.Add("Source");
                    m_switchOrderList.Add("AdditionalIncludeDirectories");
                    m_switchOrderList.Add("PreprocessorDefinitions");
                    m_switchOrderList.Add("Backend");
                    m_switchOrderList.Add("TypeName");
                    MinimalRebuildFromTracking = true;
                    TrackFileAccess = true;

                    Backend = "dev";
                }

                protected override string ToolName
                {
                    get
                    {
                        return m_ToolName;
                    }
                }

                public virtual string ToolNameExtended
                {
                    get
                    {
                        return m_ToolName;
                    }

                    set
                    {
                        m_ToolName = value; ;
                    }
                }

                protected string getProperty(string propertyName)
                {
                    if (IsPropertySet(propertyName))
                    {
                        return ActiveToolSwitches[propertyName].Value;
                    }
                    else
                    {
                        return null;
                    }
                }

                protected ITaskItem getPropertyTaskItem(string propertyName)
                {
                    if (IsPropertySet(propertyName))
                    {
                        return ActiveToolSwitches[propertyName].TaskItem;
                    }
                    else
                    {
                        return null;
                    }
                }

                protected void setPropertyString(string propertyName, string value, string switchName)
                {
                    ActiveToolSwitches.Remove(propertyName);

                    var s = new ToolSwitch(ToolSwitchType.String)
                    {
                        DisplayName             = propertyName,
                        Description             = propertyName,
                        ArgumentRelationList    = new ArrayList(),
                        Value                   = value,
                        Required                = true,
                        SwitchValue             = switchName,
                        Separator               = " "
                    };

                    ActiveToolSwitches.Add(propertyName, s);
                    AddActiveSwitchToolValue(s);
                }

                protected void setPropertyTaskItem(string propertyName, ITaskItem value, string switchName)
                {
                    ActiveToolSwitches.Remove(propertyName);

                    var s = new ToolSwitch(ToolSwitchType.ITaskItem)
                    {
                        DisplayName = propertyName,
                        Description = propertyName,
                        ArgumentRelationList = new ArrayList(),
                        TaskItem = value,
                        Required = true,
                        SwitchValue = switchName,
                        Separator = " "
                    };

                    ActiveToolSwitches.Add(propertyName, s);
                    AddActiveSwitchToolValue(s);
                }

             
                string PreprocessorDefinitions
                {
                    get
                    {
                        return getProperty("PreprocessorDefinitions");
                    }

                    set
                    {
                        setPropertyString("PreprocessorDefinitions", value, "--defines");
                    }
                }

                string AdditionalIncludeDirectories
                {
                    get
                    {
                        return getProperty("AdditionalIncludeDirectories");
                    }

                    set
                    {
                        setPropertyString("AdditionalIncludeDirectories", value, "--includes");
                    }
                }

                [Required]
                public virtual string EntryPointName
                {
                    get
                    {
                        return getProperty("EntryPointName");
                    }

                    set
                    {
                        setPropertyString("EntryPointName", value, "--main");
                    }
                }

                public virtual string Backend
                {
                    get
                    {
                        return getProperty("Backend");
                    }

                    set
                    {
                        setPropertyString("Backend", value, "--backend");
                    }
                }

                public virtual string TypeName
                {
                    get
                    {
                        return getProperty("TypeName");
                    }

                    set
                    {
                        setPropertyString("TypeName", value, "--type_name");
                    }
                }


                [Required]
                public override ITaskItem Source
                {
                    get
                    {
                        return getPropertyTaskItem("Source");
                    }

                    set
                    {
                        setPropertyTaskItem("Source", value, "--file");
                    }
                }

                protected override string GenerateCommandLineCommands()
                {
                    var builder = new CommandLineBuilder();

                    if (Source != null)
                    {
                        builder.AppendTextUnquoted("--file ");
                        builder.AppendFileNameIfNotNull(Source);
                    }

                    var r = base.GenerateResponseFileCommands() + " " + builder.ToString();
                    return r;
                }

                protected string m_ToolName = "uc-gpu-shader-compiler.exe";
            }
        }
    }
}
