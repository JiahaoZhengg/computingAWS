using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public enum ComputerSize
    {
        Small,
        Large,
        ExLarge
    }
    public enum ComputerState
    {
        NotStart,
        Started,
        OutTIme,
        Finish
    }
    public class ComputerModel
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
        public ComputerState State { get; set; }

        public ComputerSize ComputerSize { get; set; }
        public int Id { get; set; }

        public string Result { get; set; }

        public decimal RunTImes { get; set; }
        public decimal ComputrEnergy { get; set; }

        public List<string> RamValues { get; set; }
    }
}
