using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfCompute.Services
{
    using Entity;
    public class ConvertModel
    {
        public static CffEntity ReadCff(string strFileData)
        {
            List<string> source = strFileData.Split(new string[2]
            {
                "\r",
                "\n"
            }, StringSplitOptions.RemoveEmptyEntries).ToList();
            source = source.Where((string item) => item.First() != '/').ToList();
            CffEntity cffEntity = new CffEntity
            {
                IsSuccess = true
            };
            cffEntity.ErrorRows = new List<string>();
            bool flag = false;
            bool flag2 = false;
            for (int i = 0; i < source.Count; i++)
            {
                string text = source[i];
                cffEntity.ErrorRows.Add(text);
                if (text.Contains("DEFAULT-LOGFILE"))
                {
                    string[] array = text.Split('=');
                    cffEntity.defaultLogFile = new DefaultLogFile();
                    if (array.Count() == 2)
                    {
                        cffEntity.defaultLogFile.IsSuccess = true;
                        cffEntity.defaultLogFile.ConfigFile = array[1];
                        cffEntity.ErrorRows.Remove(text);
                    }
                    else
                    {
                        cffEntity.defaultLogFile.IsSuccess = false;
                    }
                }
                else if (text.Contains("LIMITS-TASKS"))
                {
                    cffEntity.limitsTasks = new LimitsTasks
                    {
                        IsSuccess = true
                    };
                    string[] array2 = text.Split('=');
                    if (array2.Count() != 2)
                    {
                        cffEntity.limitsTasks.IsSuccess = false;
                        continue;
                    }

                    cffEntity.ErrorRows.Remove(text);
                    array2 = array2[1].Split(new string[1]
                    {
                        ","
                    }, StringSplitOptions.RemoveEmptyEntries);
                    if (array2.Length != 2)
                    {
                        cffEntity.limitsTasks.IsSuccess = false;
                    }

                    int result = 0;
                    if (int.TryParse(array2[0], out result))
                    {
                        cffEntity.limitsTasks.MinTaskCount = result;
                    }
                    else
                    {
                        cffEntity.limitsTasks.IsSuccess = false;
                    }

                    result = 0;
                    if (int.TryParse(array2[1], out result))
                    {
                        cffEntity.limitsTasks.MaxTaskCount = result;
                    }
                    else
                    {
                        cffEntity.limitsTasks.IsSuccess = false;
                    }
                }
                else if (text.Contains("LIMITS-PROCESSORS"))
                {
                    cffEntity.limitsProcessors = new LimitsProcessors
                    {
                        IsSuccess = true
                    };
                    string[] array3 = text.Split('=');
                    if (array3.Count() != 2)
                    {
                        cffEntity.limitsProcessors.IsSuccess = false;
                        continue;
                    }

                    cffEntity.ErrorRows.Remove(text);
                    array3 = array3[1].Split(new string[1]
                    {
                        ","
                    }, StringSplitOptions.RemoveEmptyEntries);
                    if (array3.Length != 2)
                    {
                        cffEntity.limitsProcessors.IsSuccess = false;
                    }

                    int result2 = 0;
                    if (int.TryParse(array3[0], out result2))
                    {
                        cffEntity.limitsProcessors.MinProcessorCount = result2;
                    }
                    else
                    {
                        cffEntity.limitsProcessors.IsSuccess = false;
                    }

                    result2 = 0;
                    if (int.TryParse(array3[1], out result2))
                    {
                        cffEntity.limitsProcessors.MaxProcessorCount = result2;
                    }
                    else
                    {
                        cffEntity.limitsProcessors.IsSuccess = false;
                    }
                }
                else if (text.Contains("LIMITS-PROCESSOR-FREQUENCIES"))
                {
                    cffEntity.LimitsProcessorsFrequencies = new LimitsProcessorsFrequencies
                    {
                        IsSuccess = true
                    };
                    string[] array4 = text.Split('=');
                    if (array4.Count() != 2)
                    {
                        cffEntity.LimitsProcessorsFrequencies.IsSuccess = false;
                        continue;
                    }

                    cffEntity.ErrorRows.Remove(text);
                    array4 = array4[1].Split(new string[1]
                    {
                        ","
                    }, StringSplitOptions.RemoveEmptyEntries);
                    if (array4.Length != 2)
                    {
                        cffEntity.LimitsProcessorsFrequencies.IsSuccess = false;
                    }

                    decimal result3 = default(decimal);
                    if (decimal.TryParse(array4[0], out result3))
                    {
                        cffEntity.LimitsProcessorsFrequencies.MinProcessorsFrequencies = result3;
                    }
                    else
                    {
                        cffEntity.LimitsProcessorsFrequencies.IsSuccess = false;
                    }

                    result3 = default(decimal);
                    if (decimal.TryParse(array4[1], out result3))
                    {
                        cffEntity.LimitsProcessorsFrequencies.MaxProcessorsFrequencies = result3;
                    }
                    else
                    {
                        cffEntity.LimitsProcessorsFrequencies.IsSuccess = false;
                    }
                }
                else if (text.Contains("LIMITS-RAM"))
                {
                    cffEntity.LimitsRAM = new LimitsRAM
                    {
                        IsSuccess = true
                    };
                    string[] array5 = text.Split('=');
                    if (array5.Count() != 2)
                    {
                        cffEntity.LimitsRAM.IsSuccess = false;
                        continue;
                    }

                    cffEntity.ErrorRows.Remove(text);
                    array5 = array5[1].Split(new string[1]
                    {
                        ","
                    }, StringSplitOptions.RemoveEmptyEntries);
                    if (array5.Length != 2)
                    {
                        cffEntity.LimitsRAM.IsSuccess = false;
                    }

                    int result4 = 0;
                    if (int.TryParse(array5[0], out result4))
                    {
                        cffEntity.LimitsRAM.MinRAMCount = result4;
                    }
                    else
                    {
                        cffEntity.LimitsRAM.IsSuccess = false;
                    }

                    result4 = 0;
                    if (int.TryParse(array5[1], out result4))
                    {
                        cffEntity.LimitsRAM.MaxRAMCount = result4;
                    }
                    else
                    {
                        cffEntity.LimitsRAM.IsSuccess = false;
                    }
                }
                else if (text.Contains("PROGRAM-DATA"))
                {
                    cffEntity.programData = new ProgramData
                    {
                        IsSuccess = true
                    };
                    string[] array6 = text.Split('=');
                    if (array6.Count() != 2)
                    {
                        cffEntity.programData.IsSuccess = false;
                        continue;
                    }

                    cffEntity.ErrorRows.Remove(text);
                    array6 = array6[1].Split(new string[1]
                    {
                        ","
                    }, StringSplitOptions.RemoveEmptyEntries);
                    if (array6.Length != 3)
                    {
                        cffEntity.programData.IsSuccess = false;
                    }

                    decimal result5 = default(decimal);
                    if (decimal.TryParse(array6[0], out result5))
                    {
                        cffEntity.programData.MustCompleteTime = result5;
                    }
                    else
                    {
                        cffEntity.programData.IsSuccess = false;
                    }

                    int result6 = 0;
                    if (int.TryParse(array6[1], out result6))
                    {
                        cffEntity.programData.MustTaskCount = result6;
                    }
                    else
                    {
                        cffEntity.programData.IsSuccess = false;
                    }

                    result6 = 0;
                    if (int.TryParse(array6[2], out result6))
                    {
                        cffEntity.programData.MustProcessorsCount = result6;
                    }
                    else
                    {
                        cffEntity.programData.IsSuccess = false;
                    }
                }
                else if (text.Contains("REFERENCE-FREQUENCY"))
                {
                    cffEntity.referenceFrequency = new ReferenceFrequency
                    {
                        IsSuccess = true
                    };
                    string[] array7 = text.Split('=');
                    if (array7.Count() != 2)
                    {
                        cffEntity.referenceFrequency.IsSuccess = false;
                        continue;
                    }

                    cffEntity.ErrorRows.Remove(text);
                    decimal result7 = default(decimal);
                    if (decimal.TryParse(array7[1], out result7))
                    {
                        cffEntity.referenceFrequency.Value = result7;
                    }
                    else
                    {
                        cffEntity.referenceFrequency.IsSuccess = false;
                    }
                }
                else if (text.Contains("TASK-RUNTIME-RAM"))
                {
                    cffEntity.taskRunTimeRAM = new TaskRunTimeRAM
                    {
                        IsSuccess = true
                    };
                    string[] array8 = text.Split('=');
                    if (array8.Count() != 2)
                    {
                        cffEntity.taskRunTimeRAM.IsSuccess = false;
                        continue;
                    }

                    cffEntity.ErrorRows.Remove(text);
                    array8 = array8[1].Split(new string[1]
                    {
                        ","
                    }, StringSplitOptions.RemoveEmptyEntries);
                    if (array8.Length % 2 == 0)
                    {
                        cffEntity.taskRunTimeRAM.runTimeRAMs = new List<RunTimeRAM>();
                        for (int j = 0; j < array8.Length - 1; j += 2)
                        {
                            cffEntity.taskRunTimeRAM.runTimeRAMs.Add(new RunTimeRAM
                            {
                                TaskNum = j / 2 + 1,
                                TaskRunTime = decimal.Parse(array8[j]),
                                TaskRAMValue = int.Parse(array8[j + 1])
                            });
                        }
                    }
                    else
                    {
                        cffEntity.taskRunTimeRAM.IsSuccess = false;
                    }
                }
                else if (text.Contains("PROCESSORS-FREQUENCIES-RAM"))
                {
                    cffEntity.processorsFrequenciesRAM = new ProcessorsFrequenciesRAM
                    {
                        IsSuccess = true
                    };
                    string[] array9 = text.Split('=');
                    if (array9.Count() != 2)
                    {
                        cffEntity.processorsFrequenciesRAM.IsSuccess = false;
                        continue;
                    }

                    cffEntity.ErrorRows.Remove(text);
                    array9 = array9[1].Split(new string[1]
                    {
                        ","
                    }, StringSplitOptions.RemoveEmptyEntries);
                    if (array9.Length % 3 == 0)
                    {
                        cffEntity.processorsFrequenciesRAM.pFRs = new List<PFR>();
                        for (int k = 0; k < array9.Length - 1; k += 3)
                        {
                            cffEntity.processorsFrequenciesRAM.pFRs.Add(new PFR
                            {
                                ProcessNum = k / 3 + 1,
                                Name = array9[k].Trim(),
                                speed = decimal.Parse(array9[k + 1]),
                                RamValue = int.Parse(array9[k + 2])
                            });
                        }
                    }
                    else
                    {
                        cffEntity.taskRunTimeRAM.IsSuccess = false;
                    }
                }
                else if (text.Contains("PROCESSORS-COEFFICIENTS"))
                {
                    string[] array10 = text.Split('=');
                    if (array10.Count() == 2)
                    {
                        cffEntity.ErrorRows.Remove(text);
                        array10 = array10[1].Split(new string[1]
                        {
                            ","
                        }, StringSplitOptions.RemoveEmptyEntries);
                        cffEntity.processorsCoefficients = new ProcessorsCoefficients();
                        cffEntity.processorsCoefficients.coefficientItems = new List<CoefficientItem>();
                        int num;
                        for (num = 0; num < array10.Length; num++)
                        {
                            cffEntity.processorsCoefficients.coefficientItems.Add(new CoefficientItem
                            {
                                ProcessName = array10[num],
                                c = decimal.Parse(array10[++num]),
                                b = decimal.Parse(array10[++num]),
                                a = decimal.Parse(array10[++num])
                            });
                        }
                    }
                }
                else if (text.Contains("LOCAL-COMMUNICATION"))
                {
                    cffEntity.ErrorRows.Remove(text);
                    cffEntity.localCommunication = new LocalCommunication();
                    cffEntity.localCommunication.Row = new List<string>();
                    flag = true;
                }
                else if (text.Contains("REMOTE-COMMUNICATION"))
                {
                    cffEntity.ErrorRows.Remove(text);
                    cffEntity.RemoteCommunication = new RemoteCommunication();
                    cffEntity.RemoteCommunication.Row = new List<string>();
                    flag = false;
                    flag2 = true;
                }
                else if (flag)
                {
                    cffEntity.localCommunication.Row.Add(text);
                    cffEntity.ErrorRows.Remove(text);
                }
                else if (flag2)
                {
                    cffEntity.RemoteCommunication.Row.Add(text);
                    cffEntity.ErrorRows.Remove(text);
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