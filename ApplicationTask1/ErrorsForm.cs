using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ApplicationTask1
{
    using Entity;
    public partial class ErrorsForm : Form
    {
        public ErrorsForm()
        {
            InitializeComponent();
        }
        public TaffEntity TaffEntity { get; set; }
        public CffEntity CffEntity { get; set; }
        private void ErrorsForm_Load(object sender, EventArgs e)
        {
            if (TaffEntity == null)
            {
                this.rTxtTaffEorrorInfo.Text = "Taff data is null\r\n\r\n";
            }
            if (CffEntity == null)
            {
                this.rTxtCffErrorInfo.Text = "Cff data is null\r\n\r\n";
                return;
            }

            int nTaffErrorCount = 0;
            if (TaffEntity != null)
            {
                if (TaffEntity.errorRows.Count > 0)
                {
                    for (int i = 0; i < TaffEntity.errorRows.Count; i++)
                    {
                        nTaffErrorCount = i + 1;
                        this.rTxtTaffEorrorInfo.AppendText(string.Format("{0,30}{1,30}", TaffEntity.errorRows[i], $"error {nTaffErrorCount} Invalid keyword"));
                        this.rTxtTaffEorrorInfo.AppendText("\r\n");
                    }
                }
                else
                {
                    nTaffErrorCount = 1;
                }
            }
            if (CffEntity.ErrorRows.Count > 0)
            {
                for (int i=0;i<CffEntity.ErrorRows.Count;i++)
                {
                    int num = i + 1;
                    this.rTxtCffErrorInfo.AppendText(string.Format("{0,30}{1,30}", CffEntity.ErrorRows[i], $"error {num} Invalid keyword"));
                    this.rTxtCffErrorInfo.AppendText("\r\n");
                }
            }

            double rf = (double)CffEntity.referenceFrequency.Value;
            bool isAppendName = false;
            double firstEnergy = 0;
            if (TaffEntity != null)
            {
                for (int i = 0; i < TaffEntity.Allocations.Count; i++)
                {
                    isAppendName = false;
                    var item = TaffEntity.Allocations[i];
                    if (!item.IsSuccess)
                    {
                        this.rTxtTaffEorrorInfo.AppendText("\r\n");
                        this.rTxtTaffEorrorInfo.AppendText(string.Format("{0,60}", "--------------------------------------------------------------------------"));
                        this.rTxtTaffEorrorInfo.AppendText("\r\n");

                        this.rTxtTaffEorrorInfo.AppendText(string.Format("{0,30}", item.ItemName));
                        this.rTxtTaffEorrorInfo.AppendText("\r\n");

                        string[] arrNames = item.ItemName.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                        //validate the format of name
                        if (arrNames.Length != 2)
                        {
                            TaffAppendText(string.Format("{0,30}{1,30}", "", $"error {nTaffErrorCount} ID valus is missing."));
                            nTaffErrorCount++;
                            continue;
                        }

                        int nId = 0;
                        if (!int.TryParse(arrNames[1], out nId))
                        {
                            TaffAppendText(string.Format("{0,30}{1,30}", "", $"error {nTaffErrorCount} Allocation ID is not an integer."));
                            nTaffErrorCount++;
                            continue;
                        }

                        foreach (var row in item.Rows)
                        {
                            this.rTxtTaffEorrorInfo.AppendText(string.Format("{0,30}", row));
                            this.rTxtTaffEorrorInfo.AppendText("\r\n");
                        }

                        //if the number of process is 3
                        if (item.Rows.Count == 3)
                        {
                            //validate the task number
                            {
                                List<int> taskNums = new List<int>();
                                foreach (var processTask in item.processTasks)
                                {
                                    foreach (var task in processTask.Tasks)
                                    {
                                        if (!taskNums.Contains(task))
                                        {
                                            taskNums.Add(task);
                                        }
                                    }
                                }


                                if (taskNums.Count != 5)
                                {
                                    TaffAppendText(string.Format("{0,30}{1,30}", "", $"error {nTaffErrorCount} Expecting only {taskNums.Count} allocations."));
                                    this.rTxtTaffEorrorInfo.AppendText("\r\n");
                                    nTaffErrorCount++;
                                }

                                if (item.processTasks.Where(u => u.Tasks.Count == 0).Count() > 0)
                                {
                                    TaffAppendText(string.Format("{0,30}{1,30}", "", $"error {nTaffErrorCount} allocation (ID={nId}) process 3 not have task."));
                                    this.rTxtTaffEorrorInfo.AppendText("\r\n");
                                    nTaffErrorCount++;
                                }
                            }
                            //validate a task in one or more process
                            {

                                var process1Task = item.processTasks[0].Tasks;
                                var process2Task = item.processTasks[1].Tasks;
                                var process3Task = item.processTasks[2].Tasks;

                                int nTask = 0;
                                foreach (var task in process1Task)
                                {
                                    bool isHas = process2Task.Count(u => u == task) > 0;
                                    if (isHas)
                                    {
                                        nTask = task;
                                        break;
                                    }

                                    isHas = process3Task.Count(u => u == task) > 0;
                                    if (isHas)
                                    {
                                        nTask = task;
                                        break;
                                    }
                                }

                                if (nTask > 0)
                                {
                                    TaffAppendText(string.Format("{0,30}{1,30}", "", $"error {nTaffErrorCount} Task {nTask} is allocated to more than 1 processor."));
                                    nTaffErrorCount++;

                                }
                            }

                        }
                        else
                        {
                            TaffAppendText(string.Format("{0,30}{1,30}", "", $"error {nTaffErrorCount} Expecting 3 processors."));
                            nTaffErrorCount++;
                        }
                    }
                    else
                    {
                        List<ProcessTask> processTasks = item.processTasks;

                        List<RunTimeRAM> runTimeRAMs = CffEntity.taskRunTimeRAM.runTimeRAMs;

                        List<PFR> pFRs = CffEntity.processorsFrequenciesRAM.pFRs;

                        //RAM 和 the max process runtime
                        RamRunTime showItem = MathFun.GetRamRunTime(processTasks, runTimeRAMs, pFRs, rf);

                        //allocation energy
                        double dTotalCoefficient = MathFun.GetTotalCoefficient(showItem.runTimes, pFRs, CffEntity.processorsCoefficients.coefficientItems);

                        //local and remote energy
                        var lrtotalCoefficient = MathFun.GetLocalAndRemoteToalCoefficient(processTasks, CffEntity.localCommunication, CffEntity.RemoteCommunication);
                        double dLocalAndRemoteToalCoefficient = lrtotalCoefficient[0] + lrtotalCoefficient[1];

                        //the sum of energy
                        double energy = double.Parse((dTotalCoefficient + dLocalAndRemoteToalCoefficient).ToString("0.00"));
                        if (i == 0)
                        {
                            firstEnergy = energy;
                        }

                        int allocationID = int.Parse(item.ItemName.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries)[1]);

                        //validate the runtime
                        if (showItem.MaxProcessRunTime > (double)CffEntity.programData.MustCompleteTime)
                        {
                            if (!isAppendName)
                            {
                                this.rTxtTaffEorrorInfo.AppendText("\r\n");
                                this.rTxtTaffEorrorInfo.AppendText(string.Format("{0,60}", "--------------------------------------------------------------------------"));
                                this.rTxtTaffEorrorInfo.AppendText("\r\n");

                                this.rTxtTaffEorrorInfo.AppendText(string.Format("{0,30}", item.ItemName));
                                this.rTxtTaffEorrorInfo.AppendText("\r\n");
                                isAppendName = true;
                                foreach (var row in item.Rows)
                                {
                                    this.rTxtTaffEorrorInfo.AppendText(string.Format("{0,30}", row));
                                    this.rTxtTaffEorrorInfo.AppendText("\r\n");
                                }
                            }



                            TaffAppendText(string.Format("{0,30}{1,30}", "", $"error {nTaffErrorCount} the runtime ({showItem.MaxProcessRunTime.ToString("0.00")}) of an allocation (ID={allocationID}) is greater than the expected program runtime ({CffEntity.programData.MustCompleteTime.ToString("0.00")})"));
                            this.rTxtTaffEorrorInfo.AppendText("\r\n");
                            nTaffErrorCount++;
                        }

                        //validate the ram
                        for (int r = 0; r < showItem.RamProportion.Count; r++)
                        {
                            var rams = showItem.RamProportion[r].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0].Split('/');
                            int maxTaskRam = int.Parse(rams[0]);
                            int ProcessRam = int.Parse(rams[1]);

                            int nNum = r + 1;
                            if (maxTaskRam > ProcessRam)
                            {
                                if (!isAppendName)
                                {
                                    this.rTxtTaffEorrorInfo.AppendText("\r\n");
                                    this.rTxtTaffEorrorInfo.AppendText(string.Format("{0,60}", "--------------------------------------------------------------------------"));
                                    this.rTxtTaffEorrorInfo.AppendText("\r\n");

                                    this.rTxtTaffEorrorInfo.AppendText(string.Format("{0,30}", item.ItemName));
                                    this.rTxtTaffEorrorInfo.AppendText("\r\n");
                                    isAppendName = true;
                                    foreach (var row in item.Rows)
                                    {
                                        this.rTxtTaffEorrorInfo.AppendText(string.Format("{0,30}", row));
                                        this.rTxtTaffEorrorInfo.AppendText("\r\n");
                                    }
                                }

                                TaffAppendText(string.Format("{0,30}{1,30}", "", $"error {nTaffErrorCount} processor (ID={allocationID}) of allocation (ID={nNum}) has {ProcessRam} GB RAM but requires {maxTaskRam} GB RAM"));
                                this.rTxtTaffEorrorInfo.AppendText("\r\n");
                                nTaffErrorCount++;
                            }
                        }

                        //validate the energy
                        if (i != 0 && firstEnergy != 0)
                        {
                            if (firstEnergy != energy)
                            {
                                if (!isAppendName)
                                {
                                    this.rTxtTaffEorrorInfo.AppendText("\r\n");
                                    this.rTxtTaffEorrorInfo.AppendText(string.Format("{0,60}", "--------------------------------------------------------------------------"));
                                    this.rTxtTaffEorrorInfo.AppendText("\r\n");

                                    this.rTxtTaffEorrorInfo.AppendText(string.Format("{0,30}", item.ItemName));
                                    this.rTxtTaffEorrorInfo.AppendText("\r\n");
                                    isAppendName = true;
                                    foreach (var row in item.Rows)
                                    {
                                        this.rTxtTaffEorrorInfo.AppendText(string.Format("{0,30}", row));
                                        this.rTxtTaffEorrorInfo.AppendText("\r\n");
                                    }
                                }
                                TaffAppendText(string.Format("{0,30}{1,30}", "", $"error {nTaffErrorCount} the energy ({energy}) of an allocation (ID={allocationID}) differs to the energy value ({firstEnergy}) of another allocation (ID=1)"));
                                this.rTxtTaffEorrorInfo.AppendText("\r\n");
                                nTaffErrorCount++;
                            }
                        }
                    }
                    //List<ProcessTask> processTasks = item.processTasks;

                    //List<RunTimeRAM> runTimeRAMs = CffEntity.taskRunTimeRAM.runTimeRAMs;

                    //List<PFR> pFRs = CffEntity.processorsFrequenciesRAM.pFRs;
                }
            }
        }

        private void TaffAppendText(string str)
        {
            this.rTxtTaffEorrorInfo.AppendText(str);
        }
    }
}
