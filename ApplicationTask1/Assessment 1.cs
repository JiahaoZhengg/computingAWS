
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTask1
{
    using Entity;
    using Entity.Services;
    using System.Threading;

    public partial class PrismApiTest : Form
    {
        private TaffEntity TaffEntity = null;
        private CffEntity CffEntity = null;

        public PrismApiTest()
        {
            InitializeComponent();
        }

        #region open

        private string taffFileName = string.Empty;
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openTaffFileDialog.Filter = "Data File (*.taff)|*.taff";
            openTaffFileDialog.RestoreDirectory = true;
            openTaffFileDialog.FileName = "";

            if (openTaffFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFile = openTaffFileDialog.FileName;
                // get the open file
                taffFileName = System.IO.Path.GetFileName(selectedFile);
                TaffEntity = TaskAllocations.ReadTaff(selectedFile);
                var strFileData = System.IO.File.ReadAllText(selectedFile.Replace(".taff", ".cff"));
                CffEntity = ModelServer.ReadCff(strFileData);
                this.errorsToolStripMenuItem.Enabled = true;
                this.allocationsToolStripMenuItem.Enabled = true;
            }
        }
        #endregion

        #region exit
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region allocations
        private void allocationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.rTxtResult.Clear();
            if (TaffEntity != null)
            {
                if (TaffEntity.IsSuccess)
                    this.rTxtResult.AppendText($"\r\n\r\nAllocations file({taffFileName}) is valid\r\n\r\n");
                else
                    this.rTxtResult.AppendText($"Allocations file({taffFileName}) is invalid\r\n\r\n");
            }
            else
            {
                this.rTxtResult.AppendText("Taff data is null\r\n\r\n");
            }
            if (CffEntity != null)
            {
                if (CffEntity.IsSuccess)
                    this.rTxtResult.AppendText($"Configuration file({taffFileName.Replace(".taff", ".cff")}) is valid\r\n\r\n\r\n");
                else
                    this.rTxtResult.AppendText($"Configuration file({taffFileName.Replace(".taff", ".cff")}) is invalid\r\n\r\n\r\n");
            }
            else
            {
                this.rTxtResult.AppendText("Cff data is null\r\n\r\n"); return;
            }
            double rf = (double)CffEntity.referenceFrequency.Value;
            if (TaffEntity != null)
            {
                foreach (var item in TaffEntity.Allocations)
                {
                    List<ProcessTask> processTasks = item.processTasks;

                    List<RunTimeRAM> runTimeRAMs = CffEntity.taskRunTimeRAM.runTimeRAMs;

                    List<PFR> pFRs = CffEntity.processorsFrequenciesRAM.pFRs;

                    //RAM and PROCESS RUNTIME
                    RamRunTime showItem = MathFun.GetRamRunTime(processTasks, runTimeRAMs, pFRs, rf);

                    //计算处理器的能量消耗
                    double dTotalCoefficient = MathFun.GetTotalCoefficient(showItem.runTimes, pFRs, CffEntity.processorsCoefficients.coefficientItems);

                    //计算本地和远程能量消耗总和
                    var lrtotalCoefficient = MathFun.GetLocalAndRemoteToalCoefficient(processTasks, CffEntity.localCommunication, CffEntity.RemoteCommunication);
                    double dLocalAndRemoteToalCoefficient = lrtotalCoefficient[0] + lrtotalCoefficient[1];

                    //处理器+本地+远程冷量总和
                    double energy = double.Parse((dTotalCoefficient + dLocalAndRemoteToalCoefficient).ToString("0.00"));

                    //output allocations
                    if (item.IsSuccess)
                    {
                        this.rTxtResult.AppendText(string.Format("{0,30}{1,20}{2,20}", item.ItemName, $"Time={showItem.MaxProcessRunTime.ToString("0.00")}", $"energy={energy}"));
                        this.rTxtResult.AppendText("\r\n");
                    }
                    else
                    {
                        this.rTxtResult.AppendText(string.Format("{0,30} is invalid", item.ItemName));
                        this.rTxtResult.AppendText($"\r\n");
                    }

                    for (int i = 0; i < showItem.RamProportion.Count; i++)
                    {

                        this.rTxtResult.AppendText(string.Format("{0,30}{1,20}", item.Rows[i], showItem.RamProportion[i]));
                        this.rTxtResult.AppendText("\r\n");
                    }


                    this.rTxtResult.AppendText($"\r\n\r\n");
                }
            }
        }
        #endregion

        #region errors
        private void errorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ErrorsForm errorsForm = new ErrorsForm();
            errorsForm.TaffEntity = TaffEntity;
            errorsForm.CffEntity = CffEntity;
            errorsForm.ShowDialog();
        }
        #endregion

        #region about
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBoxForm aboutBoxForm = new AboutBoxForm();
            aboutBoxForm.ShowDialog();
        }
        #endregion


        private void AllocationsForm_Load(object sender, EventArgs e)
        {
            this.comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        List<Task> ts = new List<Task>();
        private void button1_Click(object sender, EventArgs e)
        {
            ts = new List<Task>();
            this.TaffEntity = null;
            this.CffEntity = null;
            this.rTxtResult.Clear();
            string url = this.comboBox1.Text;
            string fileName = System.IO.Path.GetFileName(url);
            string filePath = System.Environment.CurrentDirectory + "\\DataFiles\\" + fileName;

            byte[] buff = DownloadFile(url, filePath);
            if (buff != null)
            {
                string name = Path.GetFileNameWithoutExtension(filePath);
                string content = System.Text.Encoding.Default.GetString(buff);

                taffFileName = System.IO.Path.GetFileName(filePath);
                //TaffEntity = TaskAllocations.ReadTaff(selectedFile);
                CffEntity = ModelServer.ReadCff(content);
                this.errorsToolStripMenuItem.Enabled = true;
                this.allocationsToolStripMenuItem.Enabled = true;

                if (CffEntity.IsSuccess == false)
                {
                    MessageBox.Show("cff file is not valid", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // same time,Threading
                Task task = new Task(() =>
                {
                    List<ComputerModel> models = ModelServer.GetModels(name, content);
                    int count = models.Count / 3;

                    var Small = models.Where(x => x.ComputerSize == ComputerSize.Small).ToList();
                    var Large = models.Where(x => x.ComputerSize == ComputerSize.Large).ToList();
                    var ExLarge = models.Where(x => x.ComputerSize == ComputerSize.ExLarge).ToList();

                    for (int i = 0; i < count; i++)
                    {
                        ComputerModel(Small[i]);
                        ComputerModel(Large[i]);
                        ComputerModel(ExLarge[i]);  ///call several WCF operations per target
                    }
                    int tick = 0;
                    while (tick < 1000 * 60 * 5)// get the set in 5min
                    {
                        bool isFinish = true;
                        foreach (var model in models)
                        {
                            if (model.State == ComputerState.Finish || model.State == ComputerState.OutTIme)
                            {

                            }
                            else
                            {
                                isFinish = false;
                            }
                        }

                        if (isFinish)
                        {
                            break;
                        }
                        tick += 100;
                        System.Threading.Thread.Sleep(100);
                    }

                    StringBuilder sb = ComputerCheck(models);
                    if (sb == null || string.IsNullOrEmpty(sb.ToString()))
                    {
                        this.BeginInvoke(new delegateSetrTxtResultText(this.SetrTxtResultText), "No calculation");
                    }
                    else
                    {
                        this.BeginInvoke(new delegateSetrTxtResultText(this.SetrTxtResultText), sb.ToString());
                    }
                });

                task.Start();
            }
            else
            {
                MessageBox.Show("fail to download", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        delegate void delegateSetrTxtResultText(string str);
        void SetrTxtResultText(string str)
        {
            this.rTxtResult.Text = str;
        }

        /// <summary>
        /// 输出最好的结果
        /// </summary>
        private StringBuilder ComputerCheck(List<ComputerModel> models)
        {
            return ModelServer.ComputerResult(models);
        }
        /// <summary>
        /// 单个计算的方法stop watch
        /// </summary>
        /// <param name="model"></param>
        private async void ComputerModel(ComputerModel model)
        {
            string re = "";
            TimeSpan timeSpan = new TimeSpan(0, 5, 0);  //the timeoutfault
            model.StartTime = DateTime.Now;
            model.State = ComputerState.Started;
            try
            {
                switch (model.ComputerSize)
                {
                    case ComputerSize.Small:
                        ServiceReferenceSmall.ServiceTaskClient service1Client = new ServiceReferenceSmall.ServiceTaskClient();
                        service1Client.Endpoint.Binding.SendTimeout = timeSpan; //stoptime
                        //AWAIT asynchronous
                        re = await service1Client.GetDataAsync(model.Content);
                        break;
                    case ComputerSize.Large:
                        ServiceReferenceLarge.ServiceTaskClient service2Client = new ServiceReferenceLarge.ServiceTaskClient();
                        service2Client.Endpoint.Binding.SendTimeout = timeSpan;

                        re = await service2Client.GetDataAsync(model.Content);
                        break;
                    case ComputerSize.ExLarge:
                        ServiceReferenceExLarge.ServiceTaskClient service3Client = new ServiceReferenceExLarge.ServiceTaskClient();
                        service3Client.Endpoint.Binding.SendTimeout = timeSpan;

                        re = await service3Client.GetDataAsync(model.Content);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                model.State = ComputerState.OutTIme;
            }

            if (string.IsNullOrEmpty(re))
            {
                model.State = ComputerState.OutTIme;
            }
            else
            {
                model.State = ComputerState.Finish;
            }
            model.EndTime = DateTime.Now;
            model.Result = re;
        }

        /// <summary>        
        /// 下载文件        
        /// </summary>        
        /// <param name="url">下载文件地址</param>       
        /// <param name="filePath">存放地址</param>          
        public byte[] DownloadFile(string url, string filePath)
        {
            try
            {
                System.Net.WebResponse httpResponse = System.Net.HttpWebRequest.Create(url).GetResponse();

                long length = httpResponse.ContentLength;
                using (System.IO.Stream steram = httpResponse.GetResponseStream())
                {
                    using (System.IO.Stream filesteram = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
                    {
                        byte[] buff = new byte[length];
                        int count = steram.Read(buff, 0, (int)buff.Length);

                        return buff;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    #region MathFun
    public class RamRunTime
    {
        public List<double> runTimes { get; set; }
        public double MaxProcessRunTime { get; set; }
        public List<string> RamProportion { get; set; }
    }

    public class MathFun
    {
        public static RamRunTime GetRamRunTime(List<ProcessTask> processTasks, List<RunTimeRAM> runTimeRAMs, List<PFR> pFRs, double taskRf)
        {
            RamRunTime ramRunTime = new RamRunTime();
            ramRunTime.RamProportion = new List<string>();
            ramRunTime.runTimes = new List<double>();

            double runTime = 0;
            foreach (ProcessTask item in processTasks)
            {
                //the type of process
                int nProcess = item.Process;
                // the frequency 
                //double ProcessRf = pFRs.Find(u => u.ProcessNum == nProcess).speed;
                PFR pFR = pFRs.Find(u => u.ProcessNum == nProcess);
                if (pFR == null) continue;
                double ProcessRf = (double)pFR.speed;
                //the ram of process
                double ProcessRam = pFRs.Find(u => u.ProcessNum == nProcess).RamValue;


                //the tasks in process
                List<int> tasks = item.Tasks;
                double currentRunTime = 0;
                double maxTaskRam = 0;

                foreach (int taskNum in item.Tasks)
                {
                    //the ram of tasks
                    RunTimeRAM runTimeRAMs1 = runTimeRAMs.Find(u => u.TaskNum == taskNum);

                    if (runTimeRAMs1 == null) continue;

                    double TaskRam = runTimeRAMs1.TaskRAMValue;
                    if (TaskRam > maxTaskRam)
                    {
                        maxTaskRam = TaskRam;
                    }

                    double TaskRunTime = (double)runTimeRAMs.Find(u => u.TaskNum == taskNum).TaskRunTime;
                    /*            
                       the real task runtime = Task runtime value * frequency of the task/ frequency of the process
                    */
                    currentRunTime += TaskRunTime * (taskRf / ProcessRf);
                }

                ramRunTime.runTimes.Add(currentRunTime);

                if (currentRunTime > runTime)
                {
                    runTime = currentRunTime;
                }

                ramRunTime.RamProportion.Add($"{maxTaskRam}/{ProcessRam} GB");
            }
            ramRunTime.MaxProcessRunTime = runTime;
            return ramRunTime;
        }

        public static double GetTotalCoefficient(List<double> runTimes, List<PFR> pFRs, List<CoefficientItem> coefficientItems)
        {
            double Coefficient = 0;

            for (int i = 0; i < runTimes.Count; i++)
            //for (int i = 0; i < pFRs.Count; i++)
            {
                double _runTime = double.Parse(runTimes[i].ToString("0.0"));

                var pfr = pFRs[i];
                double _speed = double.Parse(pFRs[i].speed.ToString("0.0"));

                CoefficientItem item = coefficientItems.Find(u => u.ProcessName == pfr.Name);
                if (item == null)
                {
                    item = coefficientItems[i];
                }

                if (item.b < 0)
                {
                    double dVal = ((double)item.a * _speed * _speed - Math.Abs((double)item.b) * _speed + (double)item.c) * _runTime;
                    Coefficient += double.Parse(dVal.ToString("0.00"));
                }
                else
                {
                    double dVal = ((double)item.a * _speed * _speed + Math.Abs((double)item.b) * _speed + (double)item.c) * _runTime;
                    Coefficient += double.Parse(dVal.ToString("0.00"));
                }
            }

            return Coefficient;
        }

        public static List<double> GetLocalAndRemoteToalCoefficient(List<ProcessTask> processTasks, LocalCommunication localCommunication, RemoteCommunication RemoteCommunication)
        {
            double LocalToalCoefficient = 0;
            double RemoteToalCoefficient = 0;
            List<double> list = new List<double>();

            foreach (var processTask in processTasks)
            {

                var tasks = processTask.Tasks;

                //local communication
                foreach (var currentTask in tasks)
                {

                    TaskModel taskModel = localCommunication.localTasks.Find(t => t.Task == currentTask);
                    if (taskModel == null) continue;

                    //var ToTasks = taskModel.ToTaskValues;

                    //var otherTaskOfProcess = tasks.Where(t => t != currentTask).ToList();
                    //if (otherTaskOfProcess != null && otherTaskOfProcess.Count > 0)
                    //{
                    //    foreach (var otherTask in otherTaskOfProcess)
                    //    {

                    //        var existTask = ToTasks.Where(u => u.Task == otherTask).FirstOrDefault();
                    //        if (existTask != null)
                    //        {
                    //            LocalToalCoefficient += existTask.Value;
                    //        }
                    //    }
                    //}
                    LocalToalCoefficient += TaskLocalCoefficient(taskModel, currentTask, tasks);
                }

                //remote communication
                foreach (var currentTask in tasks)
                {
                    TaskModel taskModel = RemoteCommunication.remoteTasks.Find(t => t.Task == currentTask);
                    if (taskModel == null) continue;

                    //var ToTasks = taskModel.ToTaskValues;

                    //var otherTaskOfProcess = tasks.Where(t => t != currentTask).ToList();
                    //if (otherTaskOfProcess != null && otherTaskOfProcess.Count > 0)
                    //{
                    //    foreach (var otherTask in otherTaskOfProcess)
                    //    {

                    //        var notExistTask = ToTasks.Where(u => u.Task != otherTask).FirstOrDefault();
                    //        if (notExistTask != null)
                    //        {
                    //            RemoteToalCoefficient += notExistTask.Value;
                    //        }
                    //    }
                    //}
                    RemoteToalCoefficient += TaskRemoteCofficient(taskModel, currentTask, tasks);

                }
            }

            //return local and remote energy
            list.Add(LocalToalCoefficient);
            list.Add(RemoteToalCoefficient);
            return list;
        }

        public static double TaskLocalCoefficient(TaskModel taskModel, int currentTask, List<int> tasks)
        {
            double LocalToalCoefficient = 0;
            var ToTasks = taskModel.ToTaskValues;

            var otherTaskOfProcess = tasks.Where(t => t != currentTask).ToList();
            if (otherTaskOfProcess != null && otherTaskOfProcess.Count > 0)
            {
                foreach (var otherTask in otherTaskOfProcess)
                {

                    var existTask = ToTasks.Where(u => u.Task == otherTask).FirstOrDefault();
                    if (existTask != null)
                    {
                        LocalToalCoefficient += (double)existTask.Value;
                    }
                }
            }

            return LocalToalCoefficient;
        }

        public static double TaskRemoteCofficient(TaskModel taskModel, int currentTask, List<int> tasks)
        {
            double RemoteToalCoefficient = 0;
            var ToTasks = taskModel.ToTaskValues;

            var otherTaskOfProcess = tasks.Where(t => t != currentTask).ToList();
            if (otherTaskOfProcess != null && otherTaskOfProcess.Count > 0)
            {
                foreach (var otherTask in otherTaskOfProcess)
                {

                    var notExistTask = ToTasks.Where(u => u.Task != otherTask).FirstOrDefault();
                    if (notExistTask != null)
                    {
                        RemoteToalCoefficient += (double)notExistTask.Value;
                    }
                }
            }

            return RemoteToalCoefficient;
        }
    }
    #endregion
}