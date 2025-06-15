using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Linq;
using System.Windows.Forms;

namespace GymDashboard
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DatabaseHelper.InitializeDatabase();

            var allDates = DatabaseHelper.GetUsageData()
                .Select(u => u.DateUsed)
                .Distinct()
                .ToList();

            comboBoxDate.Items.Clear();
            comboBoxDate.Items.Add("Все даты");
            comboBoxDate.Items.AddRange(allDates.ToArray());
            comboBoxDate.SelectedIndex = 0;
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            string selectedDate = comboBoxDate.SelectedIndex > 0
                ? comboBoxDate.SelectedItem.ToString()
                : null;

            var data = DatabaseHelper.GetUsageData(selectedDate);

            // это который стобик
            var grouped = data.GroupBy(d => d.EquipmentName)
                              .Select(g => new
                              {
                                  Name = g.Key,
                                  Total = g.Sum(x => x.DurationMinutes)
                              })
                              .ToList();

            chartBar.Series.Clear();
            chartBar.AxisX.Clear();
            chartBar.AxisY.Clear();

            chartBar.Series.Add(new ColumnSeries
            {
                Title = "Минуты",
                Values = new ChartValues<int>(grouped.Select(g => g.Total))
            });

            chartBar.AxisX.Add(new Axis
            {
                Title = "Тренажёры",
                Labels = grouped.Select(g => g.Name).ToList()
            });

            chartBar.AxisY.Add(new Axis
            {
                Title = "Минуты"
            });

            // диаграмма-пирог (кружок??)
            chartPie.Series.Clear();
            foreach (var item in grouped)
            {
                chartPie.Series.Add(new PieSeries
                {
                    Title = item.Name,
                    Values = new ChartValues<int> { item.Total },
                    DataLabels = true
                });
            }
        }
    }
}
