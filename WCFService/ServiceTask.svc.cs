using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfCompute
{
    using Entity.Services;

    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 Service1.svc 或 Service1.svc.cs，然后开始调试。
    public class ServiceTask : IServiceTask
    {
        public string GetData(string value)
        {
            var startTime = DateTime.Now;
            StringBuilder sb = new StringBuilder();
            var cff = Services.ConvertModel.ReadCff(value);

            ComputeCff com = new ComputeCff(cff);
            var re = com.Compute();
            while (!check(re) && (DateTime.Now - startTime).Seconds < 300)
            {
                re = com.Compute();
            }
            var v = com.computrEnergy;
            for (int i = 0; i < re.GetLength(0); i++)
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int j = 0; j < re.GetLength(1); j++)
                {
                    stringBuilder.Append(re[i, j]);
                    if (j != re.GetLength(1) - 1)
                    {
                        stringBuilder.Append(", ");

                    }
                }
                sb.AppendLine(stringBuilder.ToString());
            }
            return sb.ToString();
        }

        bool check(int[,] result)
        {
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
            return isAll;
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}