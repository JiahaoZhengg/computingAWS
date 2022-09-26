using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    #region model
    [DataContract]
    public class CffEntity
    {
        public DefaultLogFile defaultLogFile { get; set; }
        public LimitsTasks limitsTasks { get; set; }
        public LimitsProcessors limitsProcessors { get; set; }
        public LimitsProcessorsFrequencies LimitsProcessorsFrequencies { get; set; }
        public LimitsRAM LimitsRAM { get; set; }
        public ProgramData programData { get; set; }
        public ReferenceFrequency referenceFrequency { get; set; }
        public TaskRunTimeRAM taskRunTimeRAM { get; set; }
        public ProcessorsFrequenciesRAM processorsFrequenciesRAM { get; set; }
        public ProcessorsCoefficients processorsCoefficients { get; set; }
        public LocalCommunication localCommunication { get; set; }
        public RemoteCommunication RemoteCommunication { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> ErrorRows { get; set; }
    }


    [DataContract]
    public class DefaultLogFile
    {
        public string ConfigFile { get; set; }
        public bool IsSuccess { get; set; }
    }
    [DataContract]
    public class LimitsTasks
    {
        public int MinTaskCount { get; set; }
        public int MaxTaskCount { get; set; }
        public bool IsSuccess { get; set; }
    }
    [DataContract]
    public class LimitsProcessors
    {
        public int MinProcessorCount { get; set; }
        public int MaxProcessorCount { get; set; }
        public bool IsSuccess { get; set; }

    }
    [DataContract]
    public class LimitsProcessorsFrequencies
    {
        public decimal MinProcessorsFrequencies { get; set; }
        public decimal MaxProcessorsFrequencies { get; set; }
        public bool IsSuccess { get; set; }
    }
    [DataContract]
    public class LimitsRAM
    {
        public int MinRAMCount { get; set; }
        public int MaxRAMCount { get; set; }
        public bool IsSuccess { get; set; }
    }
    [DataContract]
    public class ProgramData
    {
        public decimal MustCompleteTime { get; set; }
        public int MustTaskCount { get; set; }
        public int MustProcessorsCount { get; set; }
        public bool IsSuccess { get; set; }
    }
    [DataContract]
    public class ReferenceFrequency
    {
        public bool IsSuccess { get; set; }
        public decimal Value { get; set; }
    }
    [DataContract]
    public class TaskRunTimeRAM
    {
        public List<RunTimeRAM> runTimeRAMs { get; set; }

        public bool IsSuccess { get; set; }
    }
    [DataContract]
    public class RunTimeRAM
    {
        public int TaskNum { get; set; }
        public decimal TaskRunTime { get; set; }
        public int TaskRAMValue { get; set; }

    }
    [DataContract]
    public class ProcessorsFrequenciesRAM
    {
        public List<PFR> pFRs { get; set; }
        public bool IsSuccess { get; set; }

    }
    [DataContract]
    public class PFR
    {
        public int ProcessNum { get; set; }
        public string Name { get; set; }
        public decimal speed { get; set; }
        public int RamValue { get; set; }
    }
    [DataContract]
    public class ProcessorsCoefficients
    {
        public List<CoefficientItem> coefficientItems { get; set; }
    }
    [DataContract]
    public class CoefficientItem
    {
        public string ProcessName { get; set; }
        public decimal a { get; set; }
        public decimal b { get; set; }
        public decimal c { get; set; }
    }
    /// <summary>
    /// LocalCommunication
    /// </summary>
    [DataContract]
    public class LocalCommunication
    {
        public List<string> Row { get; set; }

        public List<TaskModel> localTasks
        {
            get
            {
                List<TaskModel> taskModels = new List<TaskModel>();
                for (int i = 0; i < Row.Count; i++)
                {
                    int nTask = i + 1;

                    TaskModel taskModel = new TaskModel();
                    taskModel.Task = nTask;
                    taskModel.ToTaskValues = new List<TaskValue>();

                    decimal[] taskValues = Array.ConvertAll<string, decimal>(Row[i].Split(new string[] { "," }, System.StringSplitOptions.RemoveEmptyEntries), (s) => { return decimal.Parse(s); });

                    for (int t = 0; t < taskValues.Length; t++)
                    {
                        int nToTask = t + 1;
                        if (taskValues[t] > 0)
                        {
                            taskModel.ToTaskValues.Add(new TaskValue()
                            {
                                Task = nToTask,
                                Value = taskValues[t]
                            });
                        }
                    }

                    taskModels.Add(taskModel);
                }
                return taskModels;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class TaskModel
    {
        public int Task { get; set; }
        public List<TaskValue> ToTaskValues { get; set; }
    }
    [DataContract]
    public class TaskValue
    {
        public int Task { get; set; }
        public decimal Value { get; set; }
    }
    /// <summary>
    /// RemoteCommunication
    /// </summary>
    [DataContract]
    public class RemoteCommunication
    {
        public List<string> Row { get; set; }
        public List<TaskModel> remoteTasks
        {
            get
            {
                List<TaskModel> taskModels = new List<TaskModel>();
                for (int i = 0; i < Row.Count; i++)
                {
                    int nTask = i + 1;

                    TaskModel taskModel = new TaskModel();
                    taskModel.Task = nTask;
                    taskModel.ToTaskValues = new List<TaskValue>();

                    decimal[] taskValues = Array.ConvertAll<string, decimal>(Row[i].Split(new string[] { "," }, System.StringSplitOptions.RemoveEmptyEntries), (s) => { return decimal.Parse(s); });

                    for (int t = 0; t < taskValues.Length; t++)
                    {
                        int nToTask = t + 1;
                        if (taskValues[t] > 0)
                        {
                            taskModel.ToTaskValues.Add(new TaskValue()
                            {
                                Task = nToTask,
                                Value = taskValues[t]
                            });
                        }
                    }

                    taskModels.Add(taskModel);
                }
                return taskModels;
            }
        }
    }
    #endregion
}