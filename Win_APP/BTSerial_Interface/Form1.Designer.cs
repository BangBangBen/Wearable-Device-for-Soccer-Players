
namespace BTSerial_Interface
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.Serial_Settings_Groupbox = new System.Windows.Forms.GroupBox();
            this.Baudrate_Select_label = new System.Windows.Forms.Label();
            this.Port_Select_label = new System.Windows.Forms.Label();
            this.Available_Ports_Dropdown = new System.Windows.Forms.ComboBox();
            this.Baudrate_Dropdown = new System.Windows.Forms.ComboBox();
            this.Serial_Receive_Groupbox = new System.Windows.Forms.GroupBox();
            this.Stop_Receive_Button = new System.Windows.Forms.Button();
            this.System_Log_Label = new System.Windows.Forms.Label();
            this.Data_Label = new System.Windows.Forms.Label();
            this.TextBox_System_Log = new System.Windows.Forms.TextBox();
            this.Received_Data_Textbox = new System.Windows.Forms.TextBox();
            this.Start_Receive_Button = new System.Windows.Forms.Button();
            this.Temperature_Graph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Heartrate_Graph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.SpO2_Graph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Step_Graph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Serial_Settings_Groupbox.SuspendLayout();
            this.Serial_Receive_Groupbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Temperature_Graph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Heartrate_Graph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpO2_Graph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Step_Graph)).BeginInit();
            this.SuspendLayout();
            // 
            // Serial_Settings_Groupbox
            // 
            this.Serial_Settings_Groupbox.Controls.Add(this.Baudrate_Select_label);
            this.Serial_Settings_Groupbox.Controls.Add(this.Port_Select_label);
            this.Serial_Settings_Groupbox.Controls.Add(this.Available_Ports_Dropdown);
            this.Serial_Settings_Groupbox.Controls.Add(this.Baudrate_Dropdown);
            this.Serial_Settings_Groupbox.Location = new System.Drawing.Point(12, 12);
            this.Serial_Settings_Groupbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Serial_Settings_Groupbox.Name = "Serial_Settings_Groupbox";
            this.Serial_Settings_Groupbox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Serial_Settings_Groupbox.Size = new System.Drawing.Size(863, 84);
            this.Serial_Settings_Groupbox.TabIndex = 0;
            this.Serial_Settings_Groupbox.TabStop = false;
            this.Serial_Settings_Groupbox.Text = "Serial Settings";
            this.Serial_Settings_Groupbox.Enter += new System.EventHandler(this.Serial_Settings_Enter);
            // 
            // Baudrate_Select_label
            // 
            this.Baudrate_Select_label.AutoSize = true;
            this.Baudrate_Select_label.Location = new System.Drawing.Point(405, 39);
            this.Baudrate_Select_label.Name = "Baudrate_Select_label";
            this.Baudrate_Select_label.Size = new System.Drawing.Size(109, 17);
            this.Baudrate_Select_label.TabIndex = 4;
            this.Baudrate_Select_label.Text = "Select Baudrate";
            // 
            // Port_Select_label
            // 
            this.Port_Select_label.AutoSize = true;
            this.Port_Select_label.Location = new System.Drawing.Point(27, 39);
            this.Port_Select_label.Name = "Port_Select_label";
            this.Port_Select_label.Size = new System.Drawing.Size(131, 17);
            this.Port_Select_label.TabIndex = 3;
            this.Port_Select_label.Text = "Select Port Number";
            // 
            // Available_Ports_Dropdown
            // 
            this.Available_Ports_Dropdown.FormattingEnabled = true;
            this.Available_Ports_Dropdown.Location = new System.Drawing.Point(172, 37);
            this.Available_Ports_Dropdown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Available_Ports_Dropdown.Name = "Available_Ports_Dropdown";
            this.Available_Ports_Dropdown.Size = new System.Drawing.Size(121, 24);
            this.Available_Ports_Dropdown.TabIndex = 2;
            this.Available_Ports_Dropdown.Text = "Available Ports";
            this.Available_Ports_Dropdown.SelectionChangeCommitted += new System.EventHandler(this.Available_Ports_Dropdown_SelectionChangeCommitted);
            // 
            // Baudrate_Dropdown
            // 
            this.Baudrate_Dropdown.FormattingEnabled = true;
            this.Baudrate_Dropdown.Items.AddRange(new object[] {
            "4800",
            "9600",
            "19200",
            "38400"});
            this.Baudrate_Dropdown.Location = new System.Drawing.Point(539, 36);
            this.Baudrate_Dropdown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Baudrate_Dropdown.Name = "Baudrate_Dropdown";
            this.Baudrate_Dropdown.Size = new System.Drawing.Size(121, 24);
            this.Baudrate_Dropdown.TabIndex = 1;
            this.Baudrate_Dropdown.Text = "Baudrate";
            this.Baudrate_Dropdown.SelectionChangeCommitted += new System.EventHandler(this.Baudrate_Dropdown_SelectionChangeCommitted);
            // 
            // Serial_Receive_Groupbox
            // 
            this.Serial_Receive_Groupbox.Controls.Add(this.Stop_Receive_Button);
            this.Serial_Receive_Groupbox.Controls.Add(this.System_Log_Label);
            this.Serial_Receive_Groupbox.Controls.Add(this.Data_Label);
            this.Serial_Receive_Groupbox.Controls.Add(this.TextBox_System_Log);
            this.Serial_Receive_Groupbox.Controls.Add(this.Received_Data_Textbox);
            this.Serial_Receive_Groupbox.Controls.Add(this.Start_Receive_Button);
            this.Serial_Receive_Groupbox.Location = new System.Drawing.Point(12, 124);
            this.Serial_Receive_Groupbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Serial_Receive_Groupbox.Name = "Serial_Receive_Groupbox";
            this.Serial_Receive_Groupbox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Serial_Receive_Groupbox.Size = new System.Drawing.Size(863, 194);
            this.Serial_Receive_Groupbox.TabIndex = 1;
            this.Serial_Receive_Groupbox.TabStop = false;
            this.Serial_Receive_Groupbox.Text = "Serial Receive";
            this.Serial_Receive_Groupbox.Enter += new System.EventHandler(this.Serial_Receive_Groupbox_Enter);
            // 
            // Stop_Receive_Button
            // 
            this.Stop_Receive_Button.Location = new System.Drawing.Point(35, 113);
            this.Stop_Receive_Button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Stop_Receive_Button.Name = "Stop_Receive_Button";
            this.Stop_Receive_Button.Size = new System.Drawing.Size(123, 53);
            this.Stop_Receive_Button.TabIndex = 3;
            this.Stop_Receive_Button.Text = "Stop Receive";
            this.Stop_Receive_Button.UseVisualStyleBackColor = true;
            this.Stop_Receive_Button.Click += new System.EventHandler(this.Stop_Receive_Button_Click);
            // 
            // System_Log_Label
            // 
            this.System_Log_Label.AutoSize = true;
            this.System_Log_Label.Location = new System.Drawing.Point(591, 15);
            this.System_Log_Label.Name = "System_Log_Label";
            this.System_Log_Label.Size = new System.Drawing.Size(82, 17);
            this.System_Log_Label.TabIndex = 3;
            this.System_Log_Label.Text = "System Log";
            // 
            // Data_Label
            // 
            this.Data_Label.AutoSize = true;
            this.Data_Label.Location = new System.Drawing.Point(324, 15);
            this.Data_Label.Name = "Data_Label";
            this.Data_Label.Size = new System.Drawing.Size(38, 17);
            this.Data_Label.TabIndex = 2;
            this.Data_Label.Text = "Data";
            this.Data_Label.Click += new System.EventHandler(this.Data_Label_Click);
            // 
            // TextBox_System_Log
            // 
            this.TextBox_System_Log.Location = new System.Drawing.Point(525, 34);
            this.TextBox_System_Log.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextBox_System_Log.Multiline = true;
            this.TextBox_System_Log.Name = "TextBox_System_Log";
            this.TextBox_System_Log.Size = new System.Drawing.Size(217, 137);
            this.TextBox_System_Log.TabIndex = 2;
            // 
            // Received_Data_Textbox
            // 
            this.Received_Data_Textbox.Location = new System.Drawing.Point(200, 34);
            this.Received_Data_Textbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Received_Data_Textbox.Multiline = true;
            this.Received_Data_Textbox.Name = "Received_Data_Textbox";
            this.Received_Data_Textbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Received_Data_Textbox.Size = new System.Drawing.Size(301, 132);
            this.Received_Data_Textbox.TabIndex = 1;
            // 
            // Start_Receive_Button
            // 
            this.Start_Receive_Button.Location = new System.Drawing.Point(35, 34);
            this.Start_Receive_Button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Start_Receive_Button.Name = "Start_Receive_Button";
            this.Start_Receive_Button.Size = new System.Drawing.Size(123, 53);
            this.Start_Receive_Button.TabIndex = 0;
            this.Start_Receive_Button.Text = "Start Receive";
            this.Start_Receive_Button.UseVisualStyleBackColor = true;
            this.Start_Receive_Button.Click += new System.EventHandler(this.Start_Receive_Button_Click);
            // 
            // Temperature_Graph
            // 
            chartArea1.Name = "ChartArea1";
            this.Temperature_Graph.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.Temperature_Graph.Legends.Add(legend1);
            this.Temperature_Graph.Location = new System.Drawing.Point(15, 346);
            this.Temperature_Graph.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Temperature_Graph.Name = "Temperature_Graph";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Temperature";
            this.Temperature_Graph.Series.Add(series1);
            this.Temperature_Graph.Size = new System.Drawing.Size(420, 340);
            this.Temperature_Graph.TabIndex = 4;
            this.Temperature_Graph.Text = "Temperature";
            this.Temperature_Graph.Click += new System.EventHandler(this.Temperature_Graph_Click);
            // 
            // Heartrate_Graph
            // 
            chartArea2.Name = "ChartArea1";
            this.Heartrate_Graph.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.Heartrate_Graph.Legends.Add(legend2);
            this.Heartrate_Graph.Location = new System.Drawing.Point(455, 346);
            this.Heartrate_Graph.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Heartrate_Graph.Name = "Heartrate_Graph";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Heart Rate";
            this.Heartrate_Graph.Series.Add(series2);
            this.Heartrate_Graph.Size = new System.Drawing.Size(420, 340);
            this.Heartrate_Graph.TabIndex = 5;
            this.Heartrate_Graph.Text = "Heart Rate";
            this.Heartrate_Graph.Click += new System.EventHandler(this.Heartrate_Graph_Click);
            // 
            // SpO2_Graph
            // 
            chartArea3.Name = "ChartArea1";
            this.SpO2_Graph.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.SpO2_Graph.Legends.Add(legend3);
            this.SpO2_Graph.Location = new System.Drawing.Point(897, 346);
            this.SpO2_Graph.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SpO2_Graph.Name = "SpO2_Graph";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "GSR";
            this.SpO2_Graph.Series.Add(series3);
            this.SpO2_Graph.Size = new System.Drawing.Size(420, 340);
            this.SpO2_Graph.TabIndex = 6;
            this.SpO2_Graph.Text = "Blood Oxygen";
            // 
            // Step_Graph
            // 
            chartArea4.Name = "ChartArea1";
            this.Step_Graph.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.Step_Graph.Legends.Add(legend4);
            this.Step_Graph.Location = new System.Drawing.Point(897, 2);
            this.Step_Graph.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Step_Graph.Name = "Step_Graph";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Step Count";
            this.Step_Graph.Series.Add(series4);
            this.Step_Graph.Size = new System.Drawing.Size(420, 340);
            this.Step_Graph.TabIndex = 7;
            this.Step_Graph.Text = "Blood Oxygen";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1345, 734);
            this.Controls.Add(this.Step_Graph);
            this.Controls.Add(this.SpO2_Graph);
            this.Controls.Add(this.Heartrate_Graph);
            this.Controls.Add(this.Temperature_Graph);
            this.Controls.Add(this.Serial_Receive_Groupbox);
            this.Controls.Add(this.Serial_Settings_Groupbox);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Serial Setting";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Serial_Settings_Groupbox.ResumeLayout(false);
            this.Serial_Settings_Groupbox.PerformLayout();
            this.Serial_Receive_Groupbox.ResumeLayout(false);
            this.Serial_Receive_Groupbox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Temperature_Graph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Heartrate_Graph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpO2_Graph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Step_Graph)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Serial_Settings_Groupbox;
        private System.Windows.Forms.ComboBox Baudrate_Dropdown;
        private System.Windows.Forms.ComboBox Available_Ports_Dropdown;
        private System.Windows.Forms.Label Baudrate_Select_label;
        private System.Windows.Forms.Label Port_Select_label;
        private System.Windows.Forms.GroupBox Serial_Receive_Groupbox;
        private System.Windows.Forms.Label Data_Label;
        private System.Windows.Forms.TextBox Received_Data_Textbox;
        private System.Windows.Forms.Button Start_Receive_Button;
        private System.Windows.Forms.TextBox TextBox_System_Log;
        private System.Windows.Forms.Label System_Log_Label;
        private System.Windows.Forms.Button Stop_Receive_Button;
        private System.Windows.Forms.DataVisualization.Charting.Chart Temperature_Graph;
        private System.Windows.Forms.DataVisualization.Charting.Chart Heartrate_Graph;
        private System.Windows.Forms.DataVisualization.Charting.Chart SpO2_Graph;
        private System.Windows.Forms.DataVisualization.Charting.Chart Step_Graph;
    }
}

