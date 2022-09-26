using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismApiTest
{
    /// <summary>
    /// cff
    /// </summary>
    public class Configuration
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

                        double nNum = 0;
                        if (double.TryParse(newItems[0], out nNum))
                        {
                            cffEntity.LimitsProcessorsFrequencies.MinProcessorsFrequencies = nNum;
                        }
                        else
                        {
                            cffEntity.LimitsProcessorsFrequencies.IsSuccess = false;
                        }

                        nNum = 0;
                        if (double.TryParse(newItems[1], out nNum))
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

                        double dNum = 0.0;
                        if (double.TryParse(newItems[0], out dNum))
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
                        double dNum = 0.0;
                        if (double.TryParse(newItems[1], out dNum))
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
                            cffEntity.taskRunTimeRAM.runTimeRAMs.Add(new RunTimeRAM() { TaskNum = 1, TaskRunTime = double.Parse(newItems[0]), TaskRAMValue = int.Parse(newItems[1]) });
                            cffEntity.taskRunTimeRAM.runTimeRAMs.Add(new RunTimeRAM() { TaskNum = 2, TaskRunTime = double.Parse(newItems[2]), TaskRAMValue = int.Parse(newItems[3]) });
                            cffEntity.taskRunTimeRAM.runTimeRAMs.Add(new RunTimeRAM() { TaskNum = 3, TaskRunTime = double.Parse(newItems[4]), TaskRAMValue = int.Parse(newItems[5]) });
                            cffEntity.taskRunTimeRAM.runTimeRAMs.Add(new RunTimeRAM() { TaskNum = 4, TaskRunTime = double.Parse(newItems[6]), TaskRAMValue = int.Parse(newItems[7]) });
                            cffEntity.taskRunTimeRAM.runTimeRAMs.Add(new RunTimeRAM() { TaskNum = 5, TaskRunTime = double.Parse(newItems[8]), TaskRAMValue = int.Parse(newItems[9]) });
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
                            cffEntity.processorsFrequenciesRAM.pFRs.Add(new PFR() { ProcessNum = 1, Name = newItems[0].Trim(), speed = double.Parse(newItems[1]), RamValue = int.Parse(newItems[2]) });
                            cffEntity.processorsFrequenciesRAM.pFRs.Add(new PFR() { ProcessNum = 2, Name = newItems[3].Trim(), speed = double.Parse(newItems[4]), RamValue = int.Parse(newItems[5]) });
                            cffEntity.processorsFrequenciesRAM.pFRs.Add(new PFR() { ProcessNum = 3, Name = newItems[6].Trim(), speed = double.Parse(newItems[7]), RamValue = int.Parse(newItems[8]) });
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
                            c = double.Parse(newItems[++i]),
                            b = double.Parse(newItems[++i]),
                            a = double.Parse(newItems[++i])
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
    }
}
