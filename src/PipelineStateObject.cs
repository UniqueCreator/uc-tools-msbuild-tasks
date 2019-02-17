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
            public class PipelineStateObject : BaseToolTask
            {
                public PipelineStateObject()
                {
                    m_switchOrderList.Add("EntryPointName");
                    m_switchOrderList.Add("CPPHeaderOutput");
                    m_switchOrderList.Add("CPPOutput");
                    m_switchOrderList.Add("Source");
                    MinimalRebuildFromTracking = true;
                    TrackFileAccess = true;
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

                private string getProperty(string propertyName)
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

                private ITaskItem getPropertyTaskItem(string propertyName)
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

                private void setPropertyString(string propertyName, string value, string switchName)
                {
                    ActiveToolSwitches.Remove(propertyName);

                    var s = new ToolSwitch(ToolSwitchType.String)
                    {
                        DisplayName = propertyName,
                        Description = propertyName,
                        ArgumentRelationList = new ArrayList(),
                        Value = value,
                        Required = true,
                        SwitchValue = switchName,
                        Separator = " "
                    };

                    ActiveToolSwitches.Add(propertyName, s);
                    AddActiveSwitchToolValue(s);
                }

                private void setPropertyTaskItem(string propertyName, ITaskItem value, string switchName)
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


                protected string m_ToolName = "cmd.exe";
            }
        }
    }
}
