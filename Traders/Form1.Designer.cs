namespace Traders
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.nudNumTraders = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudPortfolioSize = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudNumTypes = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nudNumPasses = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudReps = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumTraders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPortfolioSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumPasses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReps)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Black;
            this.textBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(1, 81);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(799, 369);
            this.textBox1.TabIndex = 0;
            // 
            // nudNumTraders
            // 
            this.nudNumTraders.Location = new System.Drawing.Point(92, 12);
            this.nudNumTraders.Name = "nudNumTraders";
            this.nudNumTraders.Size = new System.Drawing.Size(61, 20);
            this.nudNumTraders.TabIndex = 1;
            this.nudNumTraders.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(698, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Run";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Num Traders";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Portfolio Size";
            // 
            // nudPortfolioSize
            // 
            this.nudPortfolioSize.Location = new System.Drawing.Point(92, 45);
            this.nudPortfolioSize.Name = "nudPortfolioSize";
            this.nudPortfolioSize.Size = new System.Drawing.Size(61, 20);
            this.nudPortfolioSize.TabIndex = 8;
            this.nudPortfolioSize.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(194, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Num Types";
            // 
            // nudNumTypes
            // 
            this.nudNumTypes.Location = new System.Drawing.Point(261, 15);
            this.nudNumTypes.Name = "nudNumTypes";
            this.nudNumTypes.Size = new System.Drawing.Size(61, 20);
            this.nudNumTypes.TabIndex = 10;
            this.nudNumTypes.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(358, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Num Passes";
            // 
            // nudNumPasses
            // 
            this.nudNumPasses.Location = new System.Drawing.Point(425, 15);
            this.nudNumPasses.Name = "nudNumPasses";
            this.nudNumPasses.Size = new System.Drawing.Size(68, 20);
            this.nudNumPasses.TabIndex = 12;
            this.nudNumPasses.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(516, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Replications";
            // 
            // nudReps
            // 
            this.nudReps.Location = new System.Drawing.Point(587, 13);
            this.nudReps.Name = "nudReps";
            this.nudReps.Size = new System.Drawing.Size(68, 20);
            this.nudReps.TabIndex = 14;
            this.nudReps.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudReps);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nudNumPasses);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudNumTypes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudPortfolioSize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.nudNumTraders);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.nudNumTraders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPortfolioSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumPasses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReps)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.NumericUpDown nudNumTraders;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudPortfolioSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudNumTypes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudNumPasses;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudReps;
    }
}