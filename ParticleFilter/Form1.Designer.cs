using System.ComponentModel;

namespace ParticleFilter
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
            this.line = new System.Windows.Forms.Label();
            this.robot = new System.Windows.Forms.PictureBox();
            this.door1 = new System.Windows.Forms.PictureBox();
            this.door2 = new System.Windows.Forms.PictureBox();
            this.door3 = new System.Windows.Forms.PictureBox();
            this.status = new System.Windows.Forms.Label();
            this.particle_number = new System.Windows.Forms.Label();
            this.generatorBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.generateButton = new System.Windows.Forms.Button();
            this.numberOfParticles = new System.Windows.Forms.NumericUpDown();
            this.cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.robot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.door1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.door2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.door3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfParticles)).BeginInit();
            this.SuspendLayout();
            // 
            // line
            // 
            this.line.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.line.Location = new System.Drawing.Point(1, 270);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(800, 2);
            this.line.TabIndex = 0;
            this.line.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // robot
            // 
            this.robot.Image = global::ParticleFilter.Properties.Resources.Bez_naslova;
            this.robot.Location = new System.Drawing.Point(100, 203);
            this.robot.Margin = new System.Windows.Forms.Padding(0);
            this.robot.Name = "robot";
            this.robot.Size = new System.Drawing.Size(36, 67);
            this.robot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.robot.TabIndex = 1;
            this.robot.TabStop = false;
            // 
            // door1
            // 
            this.door1.Image = global::ParticleFilter.Properties.Resources.door;
            this.door1.Location = new System.Drawing.Point(156, 176);
            this.door1.Margin = new System.Windows.Forms.Padding(0);
            this.door1.Name = "door1";
            this.door1.Size = new System.Drawing.Size(46, 94);
            this.door1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.door1.TabIndex = 2;
            this.door1.TabStop = false;
            // 
            // door2
            // 
            this.door2.Image = global::ParticleFilter.Properties.Resources.door;
            this.door2.Location = new System.Drawing.Point(293, 176);
            this.door2.Margin = new System.Windows.Forms.Padding(0);
            this.door2.Name = "door2";
            this.door2.Size = new System.Drawing.Size(46, 94);
            this.door2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.door2.TabIndex = 3;
            this.door2.TabStop = false;
            this.door2.WaitOnLoad = true;
            // 
            // door3
            // 
            this.door3.Image = global::ParticleFilter.Properties.Resources.door;
            this.door3.Location = new System.Drawing.Point(548, 176);
            this.door3.Margin = new System.Windows.Forms.Padding(0);
            this.door3.Name = "door3";
            this.door3.Size = new System.Drawing.Size(46, 94);
            this.door3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.door3.TabIndex = 4;
            this.door3.TabStop = false;
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(13, 425);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(0, 13);
            this.status.TabIndex = 5;
            // 
            // particle_number
            // 
            this.particle_number.AutoSize = true;
            this.particle_number.Location = new System.Drawing.Point(591, 425);
            this.particle_number.Name = "particle_number";
            this.particle_number.Size = new System.Drawing.Size(0, 13);
            this.particle_number.TabIndex = 6;
            // 
            // generatorBackgroundWorker
            // 
            this.generatorBackgroundWorker.WorkerReportsProgress = true;
            this.generatorBackgroundWorker.WorkerSupportsCancellation = true;
            this.generatorBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_DoWork);
            this.generatorBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(376, 415);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(75, 23);
            this.generateButton.TabIndex = 7;
            this.generateButton.Text = "Start";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // numberOfParticles
            // 
            this.numberOfParticles.Location = new System.Drawing.Point(303, 418);
            this.numberOfParticles.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numberOfParticles.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numberOfParticles.Name = "numberOfParticles";
            this.numberOfParticles.Size = new System.Drawing.Size(67, 20);
            this.numberOfParticles.TabIndex = 9;
            this.numberOfParticles.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(457, 415);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 10;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.numberOfParticles);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.particle_number);
            this.Controls.Add(this.status);
            this.Controls.Add(this.robot);
            this.Controls.Add(this.line);
            this.Controls.Add(this.door1);
            this.Controls.Add(this.door2);
            this.Controls.Add(this.door3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Text = "Particle Filter";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.robot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.door1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.door2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.door3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfParticles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label line;
        private System.Windows.Forms.PictureBox robot;
        private System.Windows.Forms.PictureBox door1;
        private System.Windows.Forms.PictureBox door2;
        private System.Windows.Forms.PictureBox door3;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Label particle_number;
        private System.ComponentModel.BackgroundWorker generatorBackgroundWorker;
        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.NumericUpDown numberOfParticles;
        private System.Windows.Forms.Button cancelButton;
    }
}