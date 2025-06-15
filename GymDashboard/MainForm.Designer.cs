namespace GymDashboard
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox comboBoxDate;
        private System.Windows.Forms.Button buttonLoad;
        private LiveCharts.WinForms.CartesianChart chartBar;
        private LiveCharts.WinForms.PieChart chartPie;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.comboBoxDate = new System.Windows.Forms.ComboBox();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.chartBar = new LiveCharts.WinForms.CartesianChart();
            this.chartPie = new LiveCharts.WinForms.PieChart();
            this.SuspendLayout();

            // comboBoxDate
            this.comboBoxDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDate.Location = new System.Drawing.Point(20, 20);
            this.comboBoxDate.Size = new System.Drawing.Size(200, 24);

            // buttonLoad
            this.buttonLoad.Location = new System.Drawing.Point(240, 20);
            this.buttonLoad.Size = new System.Drawing.Size(100, 25);
            this.buttonLoad.Text = "Загрузить";
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);

            // chartBar
            this.chartBar.Location = new System.Drawing.Point(20, 60);
            this.chartBar.Size = new System.Drawing.Size(500, 300);

            // chartPie
            this.chartPie.Location = new System.Drawing.Point(540, 60);
            this.chartPie.Size = new System.Drawing.Size(300, 300);

            // MainForm
            this.ClientSize = new System.Drawing.Size(860, 400);
            this.Controls.Add(this.comboBoxDate);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.chartBar);
            this.Controls.Add(this.chartPie);
            this.Text = "Популярность тренажёров";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
        }
    }
}
