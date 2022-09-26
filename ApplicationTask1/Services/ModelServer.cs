using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace PrismApiTest.Services
{
    using Entity;

    public class ModelServer
    {
        public static CffEntity ReadCff(string selectedFilePath)
        {
            var strFileData = System.IO.File.ReadAllText(selectedFilePath);
            List<string> items = strFileData.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();


            items = items.Where(item => item.First() != '/').ToList();

            CffEntity cffEntity = new CffEntity() { IsSuccess = true };
            cffEntity.ErrorRows = new List<string>();
            bool isLocal = false;
            bool isRemote = false;

            foreach (var item in items)
            {
                cffEntity.ErrorRows.Add(item);
                if (item.Contains("DEFAULT-LOGFILE"))
                {
                    string[] newItems = item.Split('=');
                    cffEntity.defaultLogFile = new DefaultLogFile();
                    if (newItems.Count() == 2)
                    {
                        cffEntity.defaultLogFile.IsSuccess = true;
                        cffEntity.defaultLogFile.ConfigFile = newItems[1];
                        cffEntity.ErrorRows.Remove(item);
                    }
                    else
                    {
                        cffEntity.defaultLogFile.IsSuccess = false;
                    }
                }
                else if (item.Contains("LIMITS-TASKS"))
                {
                    cffEntity.limitsTasks = new LimitsTasks() { IsSuccess = true };
                    string[] newItems = item.Split('=');
                    if (newItems.Count() != 2)
                    {
                        cffEntity.limitsTasks.IsSuccess = false;
                    }
                    else
                    {
                        cffEntity.ErrorRows.Remove(item);
                        newItems = newItems[1].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        if (newItems.Length != 2)
                        {
                            cffEntity.limitsTasks.IsSuccess = false;
                        }
                        int nNum = 0;
                        if (int.TryParse(newItems[0], out nNum))
                        {
                            cffEntity.limitsTasks.MinTaskCount = nNum;
                        }
                        else
                        {
                            cffEntity.limitsTasks.IsSuccess = false;
                        }

                        nNum = 0;
                        if (int.TryParse(newItems[1], out nNum))
                        {
                            cffEntity.limitsTasks.MaxTaskCount = nNum;
                        }
                        else
                        {
                            cffEntity.limitsTasks.IsSuccess = false;
                        }
                    }
                }
                else if (item.Contains("LIMITS-PROCESSORS"))
                {
                    cffEntity.limitsProcessors = new LimitsProcessors() { IsSuccess = true };
                    string[] newItems = item.Split('=');
                    if (newItems.Count() != 2)
                    {
                        cffEntity.limitsProcessors.IsSuccess = false;
                    }
                    else
                    {
                        cffEntity.ErrorRows.Remove(item);
                        newItems = newItems[1].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        if (newItems.Length != 2)
                        {
                            cffEntity.limitsProcessors.IsSuccess = false;
                        }

                        int nNum = 0;
                        if (int.TryParse(newItems[0], out nNum))
                        {
                            cffEntity.limitsProcessors.MinProcessorCount = nNum;
                        }
                        else
                        {
                            cffEntity.limitsProcessors.IsSuccess = false;
                        }

                        nNum = 0;
                        if (int.TryParse(newItems[1], out nNum))
                        {
                            cffEntity.limitsProcessors.MaxProcessorCount = nNum;
                        }
                        else
                        {
                            cffEntity.limitsProcessors.IsSuccess = false;
                        }
                    }
                }
                else if (item.Contains("LIMITS-PROCESSOR-FREQUENCIES"))
                {
                    cffEntity.LimitsProcessorsFrequencies = new LimitsProcessorsFrequencies() { IsSuccess = true };
                    string[] newItems = item.Split('=');
                    if (newItems.Count() != 2)
                    {
                        cffEntity.LimitsProcessorsFrequencies.IsSuccess = false;
                    }
                    else
                    {
                        cffEntity.ErrorRows.Remove(item);
                        newItems = newItems[1].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        if (newItems.Length != 2)
                        {
                            cffEntity.LimitsProcessorsFrequencies.IsSuccess = false;
                        }

                        decimal nNum = 0;
                        if (decimal.TryParse(newItems[0], out nNum))
                        {
                            cffEntity.LimitsProcessorsFrequencies.MinProcessorsFrequencies = nNum;
                        }
                        else
                        {
                            cffEntity.LimitsProcessorsFrequencies.IsSuccess = false;
                        }

                        nNum = 0;
                        if (decimal.TryParse(newItems[1], out nNum))
                        {
                            cffEntity.LimitsProcessorsFrequencies.MaxProcessorsFrequencies = nNum;
                        }
                        else
                        {
                            cffEntity.LimitsProcessorsFrequencies.IsSuccess = false;
                        }
                    }
                }
                else if (item.Contains("LIMITS-RAM"))
                {
                    cffEntity.LimitsRAM = new LimitsRAM() { IsSuccess = true };
                    string[] newItems = item.Split('=');
                    if (newItems.Count() != 2)
                    {
                        cffEntity.LimitsRAM.IsSuccess = false;
                    }
                    else
                    {
                        cffEntity.ErrorRows.Remove(item);
                        newItems = newItems[1].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        if (newItems.Length != 2)
                        {
                            cffEntity.LimitsRAM.IsSuccess = false;
                        }

                        int nNum = 0;
                        if (int.TryParse(newItems[0], out nNum))
                        {
                            cffEntity.LimitsRAM.MinRAMCount = nNum;
                        }
                        else
                        {
                            cffEntity.LimitsRAM.IsSuccess = false;
                        }

                        nNum = 0;
                        if (int.TryParse(newItems[1], out nNum))
                        {
                            cffEntity.LimitsRAM.MaxRAMCount = nNum;
                        }
                        else
                        {
                            cffEntity.LimitsRAM.IsSuccess = false;
                        }
                    }
                }
                else if (item.Contains("PROGRAM-DATA"))
                {
                    cffEntity.programData = new ProgramData() { IsSuccess = true };
                    string[] newItems = item.Split('=');
                    if (newItems.Count() != 2)
                    {
                        cffEntity.programData.IsSuccess = false;
                    }
                    else
                    {
                        cffEntity.ErrorRows.Remove(item);
                        newItems = newItems[1].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        if (newItems.Length != 3)
                        {
                            cffEntity.programData.IsSuccess = false;
                        }

                        decimal dNum = 0.00M;
                        if (decimal.TryParse(newItems[0], out dNum))
                        {
                            cffEntity.programData.MustCompleteTime = dNum;
                        }
                        else
                        {
                            cffEntity.programData.IsSuccess = false;
                        }

                        int nNum = 0;
                        if (int.TryParse(newItems[1], out nNum))
                        {
                            cffEntity.programData.MustTaskCount = nNum;
                        }
                        else
                        {
                            cffEntity.programData.IsSuccess = false;
                        }

                        nNum = 0;
                        if (int.TryParse(newItems[2], out nNum))
                        {
                            cffEntity.programData.MustProcessorsCount = nNum;
                        }
                        else
                        {
                            cffEntity.programData.IsSuccess = false;
                        }
                    }
                }
                else if (item.Contains("REFERENCE-FREQUENCY"))
                {
                    cffEntity.referenceFrequency = new ReferenceFrequency() { IsSuccess = true };
                    string[] newItems = item.Split('=');
                    if (newItems.Count() != 2)
                    {
                        cffEntity.referenceFrequency.IsSuccess = false;
                    }
                    else
                    {
                        cffEntity.ErrorRows.Remove(item);
                        decimal dNum = 0.00M;
                        if (decimal.TryParse(newItems[1], out dNum))
                        {
                            cffEntity.referenceFrequency.Value = dNum;
                        }
                        else
                        {
                            cffEntity.referenceFrequency.IsSuccess = false;
                        }
                    }
                }
                else if (item.Contains("TASK-RUNTIME-RAM"))
                {
                    cffEntity.taskRunTimeRAM = new TaskRunTimeRAM() { IsSuccess = true };
                    string[] newItems = item.Split('=');
                    if (newItems.Count() != 2)
                    {
                        cffEntity.taskRunTimeRAM.IsSuccess = false;
                    }
                    else
                    {
                        cffEntity.ErrorRows.Remove(item);
                        newItems = newItems[1].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        if (newItems.Length == 10)
                        {
                            cffEntity.taskRunTimeRAM.runTimeRAMs = new List<RunTimeRAM>();
                            cffEntity.taskRunTimeRAM.runTimeRAMs.Add(new RunTimeRAM() { TaskNum = 1, TaskRunTime = decimal.Parse(newItems[0]), TaskRAMValue = int.Parse(newItems[1]) });
                            cffEntity.taskRunTimeRAM.runTimeRAMs.Add(new RunTimeRAM() { TaskNum = 2, TaskRunTime = decimal.Parse(newItems[2]), TaskRAMValue = int.Parse(newItems[3]) });
                            cffEntity.taskRunTimeRAM.runTimeRAMs.Add(new RunTimeRAM() { TaskNum = 3, TaskRunTime = decimal.Parse(newItems[4]), TaskRAMValue = int.Parse(newItems[5]) });
                            cffEntity.taskRunTimeRAM.runTimeRAMs.Add(new RunTimeRAM() { TaskNum = 4, TaskRunTime = decimal.Parse(newItems[6]), TaskRAMValue = int.Parse(newItems[7]) });
                            cffEntity.taskRunTimeRAM.runTimeRAMs.Add(new RunTimeRAM() { TaskNum = 5, TaskRunTime = decimal.Parse(newItems[8]), TaskRAMValue = int.Parse(newItems[9]) });
                        }
                        else
                        {
                            cffEntity.taskRunTimeRAM.IsSuccess = false;
                        }
                    }
                }
                else if (item.Contains("PROCESSORS-FREQUENCIES-RAM"))
                {
                    cffEntity.processorsFrequenciesRAM = new ProcessorsFrequenciesRAM() { IsSuccess = true };
                    string[] newItems = item.Split('=');
                    if (newItems.Count() != 2)
                    {
                        cffEntity.processorsFrequenciesRAM.IsSuccess = false;
                    }
                    else
                    {
                        cffEntity.ErrorRows.Remove(item);
                        newItems = newItems[1].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        if (newItems.Length == 9)
                        {
                            cffEntity.processorsFrequenciesRAM.pFRs = new List<PFR>();
                            cffEntity.processorsFrequenciesRAM.pFRs.Add(new PFR() { ProcessNum = 1, Name = newItems[0].Trim(), speed = decimal.Parse(newItems[1]), RamValue = int.Parse(newItems[2]) });
                            cffEntity.processorsFrequenciesRAM.pFRs.Add(new PFR() { ProcessNum = 2, Name = newItems[3].Trim(), speed = decimal.Parse(newItems[4]), RamValue = int.Parse(newItems[5]) });
                            cffEntity.processorsFrequenciesRAM.pFRs.Add(new PFR() { ProcessNum = 3, Name = newItems[6].Trim(), speed = decimal.Parse(newItems[7]), RamValue = int.Parse(newItems[8]) });
                        }
                        else
                        {
                            cffEntity.taskRunTimeRAM.IsSuccess = false;
                        }
                    }
                }
                else if (item.Contains("PROCESSORS-COEFFICIENTS"))
                {
                    string[] newItems = item.Split('=');
                    if (newItems.Count() != 2)
                    {
                        continue;
                    }
                    cffEntity.ErrorRows.Remove(item);
                    newItems = newItems[1].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    cffEntity.processorsCoefficients = new ProcessorsCoefficients();
                    cffEntity.processorsCoefficients.coefficientItems = new List<CoefficientItem>();

                    for (int i = 0; i < newItems.Length; i++)
                    {
                        cffEntity.processorsCoefficients.coefficientItems.Add(new CoefficientItem()
                        {
                            ProcessName = newItems[i],
                            c = decimal.Parse(newItems[++i]),
                            b = decimal.Parse(newItems[++i]),
                            a = decimal.Parse(newItems[++i])
                        });
                    }

                }
                else if (item.Contains("LOCAL-COMMUNICATION"))
                {
                    cffEntity.ErrorRows.Remove(item);
                    cffEntity.localCommunication = new LocalCommunication();
                    cffEntity.localCommunication.Row = new List<string>();
                    isLocal = true;
                    continue;
                }
                else if (item.Contains("REMOTE-COMMUNICATION"))
                {
                    cffEntity.ErrorRows.Remove(item);
                    cffEntity.RemoteCommunication = new RemoteCommunication();
                    cffEntity.RemoteCommunication.Row = new List<string>();

                    isLocal = false;
                    isRemote = true;
                    continue;
                }
                else if (isLocal)
                {
                    if (item.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length == 5)
                    {
                        cffEntity.localCommunication.Row.Add(item);
                        cffEntity.ErrorRows.Remove(item);
                    }
                }
                else if (isRemote)
                {
                    if (item.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length == 5)
                    {
                        cffEntity.RemoteCommunication.Row.Add(item);
                        cffEntity.ErrorRows.Remove(item);
                    }
                }
            }

            if (cffEntity.ErrorRows.Count > 0)
            {
                cffEntity.IsSuccess = false;
            }
            return cffEntity;
        }

        /// <summary>
        /// 生成计算任务,小,大,很大各四个
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<ComputerModel> GetModels(string path)
        {
            var content = System.IO.File.ReadAllText(path);
            StaticModel.CffEntity = ReadCff(content);
            string name = Path.GetFileNameWithoutExtension(path);
            List<ComputerModel> re = new List<ComputerModel>();

            List<ComputerSize> itemList = new List<ComputerSize>();
            Type t = typeof(ComputerSize);
            FieldInfo[] fieldInfos = t.GetFields();
            int num = 0;
            foreach (var item in fieldInfos)
            {
                if (item.FieldType.IsEnum)
                {
                    var size = (int)t.InvokeMember(item.Name, BindingFlags.GetField, null, null, null);

                    for (int i = 1; i <= 4; i++)
                    {
                        num++;
                        ComputerModel model = new ComputerModel();
                        model.Name = name;
                        model.Content = content;

                        model.State = ComputerState.NotStart;
                        model.ComputerSize = (ComputerSize)size;
                        model.Id = num;
                        re.Add(model);
                    }


                }

            }
            return re;
        }
        /// <summary>
        /// 生成计算任务,小,大,很大各四个
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<ComputerModel> GetModels(string name, string content)
        {
            StaticModel.CffEntity = ReadCff(content);
            List<ComputerModel> re = new List<ComputerModel>();

            List<ComputerSize> itemList = new List<ComputerSize>();
            Type t = typeof(ComputerSize);
            FieldInfo[] fieldInfos = t.GetFields();
            int num = 0;
            foreach (var item in fieldInfos)
            {
                if (item.FieldType.IsEnum)
                {
                    var size = (int)t.InvokeMember(item.Name, BindingFlags.GetField, null, null, null);

                    for (int i = 1; i <= 4; i++)
                    {
                        num++;
                        ComputerModel model = new ComputerModel();
                        model.Name = name;
                        model.Content = content;

                        model.State = ComputerState.NotStart;
                        model.ComputerSize = (ComputerSize)size;
                        model.Id = num;
                        re.Add(model);
                    }


                }

            }
            return re;
        }

        /// <summary>
        /// 计算单个计算任务结果的耗时
        /// </summary>
        /// <param name="models"></param>
        public static StringBuilder ComputerResult(List<ComputerModel> models)
        {
            List<ComputerModel> checkList = new List<ComputerModel>();
            ComputeCff computeCff = new ComputeCff(StaticModel.CffEntity);

            foreach (var item in models)
            {
                if (item.State != ComputerState.Finish) continue;
                int[,] result = new int[StaticModel.CffEntity.programData.MustProcessorsCount, StaticModel.CffEntity.programData.MustTaskCount];
                var rows = item.Result.Trim().Split('\n');
                for (int i = 0; i < rows.Length; i++)
                {
                    if (string.IsNullOrEmpty(rows[i])) continue;
                    var times = rows[i].Split(',');
                    for (int j = 0; j < times.Length; j++)
                    {
                        result[i, j] = int.Parse(times[j]);
                    }
                }
                bool isAll = true;
                for (int i = 0; i < result.GetLength(1); i++)
                {
                    int sum = 0;
                    for (int j = 0; j < result.GetLength(0); j++)
                    {
                        sum += result[j, i];
                    }
                    if (sum == 0)
                    {
                        isAll = false;
                        break;
                    }
                }
                if (!isAll) continue;
                var re = computeCff.ComputeCheck(result);
                item.RunTImes = re.Item1;
                item.ComputrEnergy = re.Item2;
                item.RamValues = re.Item3;
                checkList.Add(item);
            }
            var model = checkList.OrderBy(t => t.ComputrEnergy).ToList()[0];
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ID:" + model.Id);
            sb.AppendLine($"runtime:{model.RunTImes}");

            sb.AppendLine($"ComputrEnergy:{model.ComputrEnergy}");
            var reRows = model.Result.Trim().Split('\n');
            int k = 0;
            foreach (var row in reRows)
            {
                if (!string.IsNullOrEmpty(row))
                {
                    sb.AppendLine(row + " " + model.RamValues[k]);
                    k++;
                }
            }

            return sb;
        }
    }
}