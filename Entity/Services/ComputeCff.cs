using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Services
{
    public class ComputeCff
    {
        private CffEntity model;
        private int[,] re;
        private decimal[,] runTImes;
        private decimal[,] localEnergy;
        private decimal[,] RemoteEnergy;

        private int xCount;
        private int yCount;
        private decimal[] xRuntimes;
        private decimal[] xRams;
        public decimal computrEnergy = 0;
        public decimal localComputeEnergy = 0;
        public decimal remoteComputeEnergy = 0;

        public ComputeCff(CffEntity model)
        {
            this.model = model;

            init();
            ComputeRuntimes();
            for (int i = 0; i < runTImes.GetLength(0); i++)
            {

                for (int j = 0; j < runTImes.GetLength(1); j++)
                {
                    Console.Write(runTImes[i, j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        #region 初始化
        private void init()
        {
            xCount = model.programData.MustProcessorsCount;
            yCount = model.programData.MustTaskCount;
            localEnergy = new decimal[xCount, yCount];
            RemoteEnergy = new decimal[xCount, yCount];
            re = new int[xCount, yCount];
            xRuntimes = new decimal[xCount];
            xRams = new decimal[xCount];



            for (int i = 0; i < xCount; i++)
            {
                xRuntimes[i] = 0;
                xRams[i] = 0;
                for (int j = 0; j < yCount; j++)
                {
                    re[i, j] = 0;
                }
            }
        }
        /// <summary>
        /// 计算处理器处理各个任务的时间矩阵
        /// </summary>
        private void ComputeRuntimes()
        {
            runTImes = new decimal[xCount, yCount];

            for (int i = 0; i < xCount; i++)
            {
                for (int j = 0; j < yCount; j++)
                {

                    runTImes[i, j] = model.taskRunTimeRAM.runTimeRAMs[j].TaskRunTime * model.referenceFrequency.Value / model.processorsFrequenciesRAM.pFRs[i].speed;
                }
            }

        }

        #endregion

        public (decimal, decimal, List<string>) ComputeCheck(int[,] result)
        {
            init();
            this.re = result;

            List<string> rams = new List<string>();
            for (int i = 0; i < xCount; i++)
            {
                xRuntimes[i] = 0;
                xRams[i] = 0;

                for (int j = 0; j < yCount; j++)
                {
                    xRuntimes[i] += re[i, j] * runTImes[i, j];

                    if (re[i, j] > 0 && model.taskRunTimeRAM.runTimeRAMs[j].TaskRAMValue > xRams[i])
                    {
                        xRams[i] = model.taskRunTimeRAM.runTimeRAMs[j].TaskRAMValue;
                    }
                }
                rams.Add($"{xRams[i]}/{model.processorsFrequenciesRAM.pFRs[i].RamValue} GB");
                xRuntimes[i] = Math.Round(xRuntimes[i], 2);
            }

            computrEnergy = GetTotalCoefficient(xRuntimes.ToList(), model.processorsFrequenciesRAM.pFRs, model.processorsCoefficients.coefficientItems) + ComputeLocalAndRemoteEnergy();
            computrEnergy = Math.Round(computrEnergy, 2);

            return (xRuntimes.Max(), computrEnergy, rams);
        }

        /// <summary>
        /// 获取能耗
        /// </summary>
        /// <param name="runTimes"></param>
        /// <param name="pFRs"></param>
        /// <param name="coefficientItems"></param>
        /// <returns></returns>
        public static decimal GetTotalCoefficient(List<decimal> runTimes, List<PFR> pFRs, List<CoefficientItem> coefficientItems)
        {
            decimal Coefficient = 0;

            for (int i = 0; i < runTimes.Count; i++)
            //for (int i = 0; i < pFRs.Count; i++)
            {
                decimal _runTime = runTimes[i];

                var pfr = pFRs[i];
                decimal _speed = pFRs[i].speed;

                CoefficientItem item = coefficientItems.Find(u => u.ProcessName == pfr.Name);
                if (item == null)
                {
                    item = coefficientItems[i];
                }

                decimal dVal = (item.a * _speed * _speed + item.b * _speed + item.c) * _runTime;
                Coefficient += dVal;
            }

            return Coefficient;
        }

        /// <summary>
        /// 获取通讯能耗
        /// </summary>
        /// <returns></returns>
        private decimal ComputeLocalAndRemoteEnergy()
        {
            decimal renum = 0;
            foreach (var item in model.localCommunication.localTasks)
            {
                foreach (var taskValue in item.ToTaskValues)
                {
                    if (taskValue.Value > 0)
                    {
                        bool isLocal = false;
                        for (int i = 0; i < xCount; i++)
                        {
                            if (re[i, item.Task - 1] > 0)
                            {
                                if (re[i, taskValue.Task - 1] > 0)
                                {
                                    isLocal = true;
                                    break;
                                }
                            }
                        }
                        if (isLocal) renum += taskValue.Value;

                    }
                }
            }

            foreach (var item in model.RemoteCommunication.remoteTasks)
            {
                foreach (var taskValue in item.ToTaskValues)
                {
                    if (taskValue.Value > 0)
                    {
                        bool isLocal = false;
                        for (int i = 0; i < xCount; i++)
                        {
                            if (re[i, item.Task - 1] > 0)
                            {
                                if (re[i, taskValue.Task - 1] > 0)
                                {
                                    isLocal = true;
                                    break;
                                }
                            }
                        }
                        if (!isLocal)
                        {
                            renum += taskValue.Value;
                        }

                    }
                }
            }
            return renum;
        }
    }
}