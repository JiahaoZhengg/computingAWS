using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationTask1
{
    /// <summary>
    /// allocation
    /// </summary>
    public class TaskAllocations
    {
        public static TaffEntity ReadTaff(string selectedFilePath)
        {
            var strFileData = System.IO.File.ReadAllText(selectedFilePath);
            List<string> items = strFileData.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();


            items = items.Where(item =>
            {
                var arrChars = item.ToCharArray();
                if (arrChars.Length == 1)
                {
                    return true;
                }
                if (arrChars[0] == '/' && arrChars[1] == '/')
                {
                    return false;
                }
                return true;
            }
            ).ToList();

            TaffEntity taffEntity = new TaffEntity() { IsSuccess = true };
            taffEntity.errorRows = new List<string>();
            taffEntity.AllocationsData = new Allocations() { IsSuccess = true };
            taffEntity.Allocations = new List<AllocationItem>();
            AllocationItem allocationItem = null;

            foreach (var item in items)
            {
                taffEntity.errorRows.Add(item);
                if (item.Contains("CONFIG-FILE"))
                {
                    string[] newItems = item.Split('=');
                    taffEntity.ConfigFile = new ConfigFileModel();
                    if (newItems.Count() == 2)
                    {
                        taffEntity.errorRows.Remove(item);
                        taffEntity.ConfigFile.IsSuccess = true;
                        taffEntity.ConfigFile.ConfigFile = newItems[1];
                    }
                    else
                    {
                        taffEntity.ConfigFile.IsSuccess = false;
                    }
                }
                if (item.Contains("ALLOCATIONS-DATA"))
                {
                    string[] newItems = item.Split('=');
                    if (newItems.Length == 2)
                    {
                        newItems = newItems[1].Split(',');
                        if (newItems.Length == 3)
                        {
                            taffEntity.errorRows.Remove(item);
                            int nNum = 0;
                            if (int.TryParse(newItems[0], out nNum))
                            {
                                taffEntity.AllocationsData.DistributionNum = nNum;
                            }
                            else
                            {
                                taffEntity.AllocationsData.IsSuccess = false;
                            }
                            nNum = 0;
                            if (int.TryParse(newItems[1], out nNum))
                            {
                                taffEntity.AllocationsData.TaskNum = nNum;
                            }
                            else
                            {
                                taffEntity.AllocationsData.IsSuccess = false;
                            }
                            nNum = 0;
                            if (int.TryParse(newItems[2], out nNum))
                            {
                                taffEntity.AllocationsData.processorNum = nNum;
                            }
                            else
                            {
                                taffEntity.AllocationsData.IsSuccess = false;
                            }
                        }
                        else
                        {
                            taffEntity.AllocationsData.IsSuccess = false;
                        }
                    }
                    else
                    {
                        taffEntity.AllocationsData.IsSuccess = false;
                    }
                }
                if (item.Contains("ALLOCATION-ID") && !item.Contains("ALLOCATIONS-DATA"))
                {
                    allocationItem = new AllocationItem() { IsSuccess = true };
                    allocationItem.ItemName = item;
                    var arrItem = allocationItem.ItemName.Split('=');
                    if (arrItem.Length != 2)
                    {
                        taffEntity.IsSuccess = false;
                        allocationItem.IsSuccess = false;
                    }
                    else
                    {
                        taffEntity.errorRows.Remove(item);
                        int nNum = 0;
                        if (!int.TryParse(arrItem[1], out nNum))
                        {
                            taffEntity.IsSuccess = false;
                            allocationItem.IsSuccess = false;

                        }
                    }
                    allocationItem.Rows = new List<string>();
                    taffEntity.Allocations.Add(allocationItem);
                    continue;
                }
                if (taffEntity.Allocations.Count > 0)
                {
                    string[] arr = item.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (arr.Where(r => r == "0").Count() == arr.Count())
                    {
                        allocationItem.IsSuccess = false;
                        taffEntity.IsSuccess = false;
                    }

                    if (item.First() == '.' || item.First() == '?' || item.First() == '/') continue;

                    if (item.Contains(","))
                    {
                        taffEntity.errorRows.Remove(item);
                        allocationItem.Rows.Add(item);
                    }
                }
            }

            for (int i = 0; i < taffEntity.Allocations.Count; i++)
            {
                //three Process
                if (taffEntity.Allocations[i].Rows.Count != 3)
                {
                    taffEntity.Allocations[i].IsSuccess = false;
                    taffEntity.IsSuccess = false;
                    continue;
                }
                //if more Process
                var processTasks = taffEntity.Allocations[i].processTasks;
                List<int> nums = new List<int>() { 1, 2, 3, 4, 5 };
                foreach (var item in processTasks)
                {
                    foreach (var task in item.Tasks)
                    {
                        if (!nums.Remove(task))
                        {
                            taffEntity.Allocations[i].IsSuccess = false;
                            taffEntity.IsSuccess = false;
                            continue;
                        }
                    }
                }
                //validate every process has task
                if (taffEntity.Allocations[i].processTasks.Where(u => u.Tasks.Count == 0).Count() > 0)
                {
                    taffEntity.Allocations[i].IsSuccess = false;
                    taffEntity.IsSuccess = false;
                }
            }

            if (taffEntity.errorRows.Count > 0)
            {
                taffEntity.IsSuccess = false;
            }
            return taffEntity;
        }
    }

    #region Model
    public class TaffEntity
    {
        public ConfigFileModel ConfigFile { get; set; }
        public Allocations AllocationsData { get; set; }

        public List<AllocationItem> Allocations { get; set; }
        public bool IsSuccess { get; set; }

        public List<string> errorRows { get; set; }
    }

    public class ConfigFileModel
    {
        public string ConfigFile { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class Allocations
    {
        /// <summary>
        /// 分配个数
        /// </summary>
        public int DistributionNum { get; set; }
        /// <summary>
        /// 处理器个数
        /// </summary>
        public int TaskNum { get; set; }
        /// <summary>
        /// 处理器个数
        /// </summary>
        public int processorNum { get; set; }

        public bool IsSuccess { get; set; }
    }

    public class AllocationItem
    {
        public List<string> Rows { get; set; }
        public string ItemName { get; set; }
        public bool IsSuccess { get; set; }
        public List<ProcessTask> processTasks
        {

            get
            {
                List<ProcessTask> processTasks = new List<ProcessTask>();
                for (int i = 0; i < Rows.Count; i++)
                {
                    ProcessTask processTask = new ProcessTask();
                    processTask.Process = i + 1;
                    processTask.Tasks = FindTask(Rows[i]);
                    processTasks.Add(processTask);
                }

                return processTasks;
            }
        }

        private List<int> FindTask(string str)
        {
            List<int> ids = new List<int>();
            var arrItems = str.Split(new string[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < arrItems.Length; i++)
            {
                if (arrItems[i] == "1")
                {
                    ids.Add(i + 1);
                }
            }
            return ids;
        }
    }

    public class ProcessTask
    {
        public int Process { get; set; }
        public List<int> Tasks { get; set; }
    }

    #endregion
}
