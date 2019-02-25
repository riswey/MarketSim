using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Traders
{
    public partial class Histogram : Form
    {

        public Histogram(string title, List<double> data)
        {
            InitializeComponent();

            Report.SortBuckets(data, 10, out List<int> histogram, out List<double> xaxis);

            chart1.Titles.Add(title);
            chart1.Series[0].IsVisibleInLegend = false;
            chart1.ChartAreas[0].AxisX.Interval = 1;

            xaxis.ForEachWithIndex( (val, index) => {
                CustomLabel label = new CustomLabel(index - 0.5, index + 1.5, val.ToString("F1"), 0, LabelMarkStyle.LineSideMark);
                chart1.ChartAreas[0].AxisX.CustomLabels.Add(label);
            });

            chart1.Series[0].Points.DataBindY(histogram);
            
        }
    }
}
