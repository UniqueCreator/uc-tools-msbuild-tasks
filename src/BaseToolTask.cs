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
            internal class ResourceManagerFactory
            {
                public static ResourceManager CreateResourceManager()
                {
                    var resources = System.Reflection.Assembly.Load("Microsoft.Build.CPPTasks.Common");
                    return new ResourceManager("Microsoft.Build.CPPTasks.Strings", resources);
                }
            }

            public abstract class BaseToolTask : TrackedVCToolTask
            {
                public BaseToolTask() : base(ResourceManagerFactory.CreateResourceManager())
                {
                    m_switchOrderList = new ArrayList();
                    MinimalRebuildFromTracking = true;
                    TrackFileAccess = true;
                    ToolArchitecture = "Native64Bit";
                }

                protected override string GenerateFullPathToTool()
                {
                    var path = ToolPath;
                    if (String.IsNullOrEmpty(path))
                    {
                        return base.GenerateFullPathToTool();
                    }
                    else
                    {
                        var p = EnsureTrailingSlash(path);
                        return System.IO.Path.GetFullPath(p + ToolName);
                    }
                }

                protected override string[] ReadTLogNames
                {
                    get
                    {
                        string[] r = new string[1];
                        var fileName = System.IO.Path.GetFileNameWithoutExtension(ToolExe);
                        r[0] = fileName + ".read.1.tlog";
                        return r;
                    }
                }

                protected override string[] WriteTLogNames
                {
                    get
                    {
                        string[] r = new string[1];
                        var fileName = System.IO.Path.GetFileNameWithoutExtension(ToolExe);
                        r[0] = fileName + ".write.1.tlog";
                        return r;
                    }
                }

                protected override string CommandTLogName
                {
                    get
                    {
                        var fileName = System.IO.Path.GetFileNameWithoutExtension(ToolExe);
                        return fileName + ".command.1.tlog";
                    }
                }

                protected override string TrackerIntermediateDirectory
                {
                    get
                    {
                        var log = TrackerLogDirectory;
                        if (log != null)
                        {
                            return log;
                        }
                        else
                        {
                            return String.Empty;
                        }
                    }
                }

                public virtual string TrackerLogDirectory
                {
                    get
                    {
                        if (IsPropertySet("TrackerLogDirectory"))
                        {
                            return base.ActiveToolSwitches["TrackerLogDirectory"].Value;
                        }
                        else
                        {
                            return null;
                        }
                    }

                    set
                    {
                        ActiveToolSwitches.Remove("TrackerLogDirectory");

                        var s = new ToolSwitch(ToolSwitchType.Directory)
                        {
                            DisplayName = "Tracker Log Directory",
                            Description = "Tracker Log Directory",

                            ArgumentRelationList = new ArrayList(),
                            Value = EnsureTrailingSlash(value),
                            Separator = " "
                        };

                        ActiveToolSwitches.Add("TrackerLogDirectory", s);
                        AddActiveSwitchToolValue(s);
                    }
                }

                [Required]
                public abstract ITaskItem Source { get; set; }

                protected override ITaskItem[] TrackedInputFiles
                {
                    get
                    {

                        ITaskItem[] r = new ITaskItem[1];
                        r[0] = Source;
                        return r;
                    }
                }

                protected override ArrayList SwitchOrderList
                {
                    get
                    {
                        return m_switchOrderList;
                    }
                }

                protected ArrayList m_switchOrderList;
            }
        }
    }
}
