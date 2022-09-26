using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfCompute
{
    using Entity;
    public class ComputeCff
    {
        private CffEntity model;
        private decimal[,] runTImes;
        //private decimal[,] allEnergy;
        private decimal[,] localEnergy;
        private decimal[,] RemoteEnergy;
        private int[,] re;
        private Dictionary<string, CoefficientItem> coefficientItems;
        private Dictionary<int, int> runtimerOrder;
        private Dictionary<int, int> speedOrder;
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
            runTImes = new decimal[xCount, yCount];
            localEnergy = new decimal[xCount, yCount];
            RemoteEnergy = new decimal[xCount, yCount];
            re = new int[xCount, yCount];
            xRuntimes = new decimal[xCount];
            xRams = new decimal[xCount];
            coefficientItems = new Dictionary<string, CoefficientItem>();
            runtimerOrder = new Dictionary<int, int>();
            speedOrder = new Dictionary<int, int>();
            foreach (var item in model.processorsCoefficients.coefficientItems)
            {
                coefficientItems[item.ProcessName] = item;
            }
            ComputeRuntimes();
            var runList = model.taskRunTimeRAM.runTimeRAMs.OrderBy(t => t.TaskRunTime).ToList();
            var speedList = model.processorsFrequenciesRAM.pFRs.OrderBy(t => t.speed).ToList();
            for (int i = 0; i < runList.Count; i++)
            {
                runtimerOrder[i] = model.taskRunTimeRAM.runTimeRAMs.IndexOf(runList[i]);
            }
            for (int i = 0; i < speedList.Count; i++)
            {
                speedOrder[i] = model.processorsFrequenciesRAM.pFRs.IndexOf(speedList[i]);
            }
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
        /// 计算处理器处理任务的时间矩阵
        /// </summary>
        private void ComputeRuntimes()
        {
            for (int i = 0; i < xCount; i++)
            {
                for (int j = 0; j < yCount; j++)
                {

                    runTImes[i, j] = model.taskRunTimeRAM.runTimeRAMs[j].TaskRunTime * model.referenceFrequency.Value / model.processorsFrequenciesRAM.pFRs[i].speed;
                }
            }
        }

        #endregion
        /// <summary>
        /// 计算主方法
        /// </summary>
        /// <returns></returns>
        public int[,] Compute()
        {
            for (int i = 0; i < xCount; i++)
            {
                xRuntimes[i] = 0;
                xRams[i] = 0;
                for (int j = 0; j < yCount; j++)
                {
                    re[i, j] = 0;
                }
            }

            List<int> ylist = new List<int>();
            for (int i = 0; i < xCount; i++)
            {
                int pfrNumber = i;
                List<decimal> mklist = new List<decimal>();
                List<decimal> xlist = new List<decimal>();

                for (int j = 0; j < yCount; j++)
                {
                    int taskNum = j;
                    xlist.Add(runTImes[i, j]);
                    if (ylist.Contains(j)) continue;
                    if (model.taskRunTimeRAM.runTimeRAMs[j].TaskRAMValue <= model.processorsFrequenciesRAM.pFRs[i].RamValue)
                    {
                        mklist.Add(runTImes[i, j]);

                    }
                }
                //使用蒙特卡洛法寻找剩余任务中计算时间之和最接近极限时间的组合
                var sumlist = FindMaxSum(mklist, model.programData.MustCompleteTime);
                foreach (var item in sumlist)
                {
                    int ylistNum = -1;
                    for (int k = 0; k < xlist.Count; k++)
                    {
                        if (item == xlist[k] && !ylist.Contains(k))
                        {
                            ylistNum = k;
                            break;
                        }
                    }
                    ylist.Add(ylistNum);
                    re[i, ylistNum] = 1;
                }
            }

            for (int i = 0; i < xCount; i++)
            {
                xRuntimes[i] = 0;
                for (int j = 0; j < yCount; j++)
                {
                    xRuntimes[i] += re[i, j] * runTImes[i, j];
                }
            }

            computrEnergy = GetTotalCoefficient(xRuntimes.ToList(), model.processorsFrequenciesRAM.pFRs, model.processorsCoefficients.coefficientItems) + ComputeLocalAndRemoteEnergy();

            return re;
        }
        /// <summary>
        /// 使用蒙特卡洛法寻找数组中小于target且和最接近target的组合
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private List<decimal> FindMaxSum(List<decimal> nums, decimal target)
        {
            if (nums.Sum() < target) return nums;
            List<decimal> res = new List<decimal>();
            decimal lapse = 10000;
            Random random = new Random();
            for (int i = 0; i < 1000000; i++)
            {
                int numsize = random.Next(nums.Count + 1);
                List<decimal> ares = new List<decimal>();
                Dictionary<int, int> map = new Dictionary<int, int>();
                decimal thissum = 0;
                for (int j = 0; j < numsize; j++)
                {
                    int anumberindex = random.Next(nums.Count);
                    while (map.ContainsKey(anumberindex))
                    {
                        anumberindex = random.Next(nums.Count());
                    }
                    map[anumberindex] = 1;
                    ares.Add(nums[anumberindex]);
                    thissum += nums[anumberindex];
                }
                if (target > thissum && (target - thissum < lapse))
                {
                    lapse = Math.Abs(target - thissum);
                    res = new List<decimal>(ares);
                }
                if (lapse < (decimal)0.000001)
                    break;
            }
            return res;
        }
        /// <summary>
        /// 计算能耗
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
        /// 计算通讯能耗
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