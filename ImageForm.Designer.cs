namespace Palette
{
    partial class ImageForm
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
            panel1 = new Panel();
            imageDisplay = new PictureBox();
            panel2 = new Panel();
            panelPalette = new FlowLayoutPanel();
            panel3 = new Panel();
            trackBar1 = new TrackBar();
            buttonGeneratePalette = new Button();
            label1 = new Label();
            label2 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)imageDisplay).BeginInit();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(imageDisplay);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(550, 450);
            panel1.TabIndex = 0;
            // 
            // imageDisplay
            // 
            imageDisplay.Dock = DockStyle.Fill;
            imageDisplay.Location = new Point(0, 0);
            imageDisplay.Name = "imageDisplay";
            imageDisplay.Size = new Size(550, 450);
            imageDisplay.SizeMode = PictureBoxSizeMode.Zoom;
            imageDisplay.TabIndex = 0;
            imageDisplay.TabStop = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(panelPalette);
            panel2.Controls.Add(panel3);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(550, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(250, 450);
            panel2.TabIndex = 1;
            // 
            // panelPalette
            // 
            panelPalette.Dock = DockStyle.Fill;
            panelPalette.Location = new Point(0, 172);
            panelPalette.Margin = new Padding(2);
            panelPalette.Name = "panelPalette";
            panelPalette.Size = new Size(250, 278);
            panelPalette.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.Controls.Add(label2);
            panel3.Controls.Add(trackBar1);
            panel3.Controls.Add(buttonGeneratePalette);
            panel3.Controls.Add(label1);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(250, 172);
            panel3.TabIndex = 0;
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(51, 58);
            trackBar1.Maximum = 16;
            trackBar1.Minimum = 2;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(130, 56);
            trackBar1.TabIndex = 5;
            trackBar1.Value = 2;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // buttonGeneratePalette
            // 
            buttonGeneratePalette.Location = new Point(76, 132);
            buttonGeneratePalette.Name = "buttonGeneratePalette";
            buttonGeneratePalette.Size = new Size(94, 29);
            buttonGeneratePalette.TabIndex = 4;
            buttonGeneratePalette.Text = "Get Palette!";
            buttonGeneratePalette.UseVisualStyleBackColor = true;
            buttonGeneratePalette.Click += buttonGeneratePalette_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(62, 32);
            label1.Name = "label1";
            label1.Size = new Size(86, 20);
            label1.TabIndex = 1;
            label1.Text = "Palette size:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(152, 32);
            label2.Name = "label2";
            label2.Size = new Size(18, 20);
            label2.TabIndex = 6;
            label2.Text = "2";
            // 
            // ImageForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Name = "ImageForm";
            Text = "K-Means";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)imageDisplay).EndInit();
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox imageDisplay;
        private Panel panel2;
        private Panel panel3;
        private Label label1;
        private Button buttonGeneratePalette;
        private FlowLayoutPanel panelPalette;
        private TrackBar trackBar1;
        private Label label2;
    }
}