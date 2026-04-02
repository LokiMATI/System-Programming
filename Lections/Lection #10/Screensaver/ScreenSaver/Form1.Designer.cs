namespace ScreenSaver
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            JumpingLabel = new Label();
            ScreenTimer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // JumpingLabel
            // 
            JumpingLabel.AutoSize = true;
            JumpingLabel.BackColor = SystemColors.AppWorkspace;
            JumpingLabel.Font = new Font("Segoe UI", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            JumpingLabel.Location = new Point(165, 222);
            JumpingLabel.Name = "JumpingLabel";
            JumpingLabel.Size = new Size(124, 50);
            JumpingLabel.TabIndex = 0;
            JumpingLabel.Text = "Hello?";
            // 
            // ScreenTimer
            // 
            ScreenTimer.Interval = 16;
            ScreenTimer.Tick += ScreenTimer_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(JumpingLabel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            Text = "Form1";
            KeyPress += Form1_KeyPress;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label JumpingLabel;
        private System.Windows.Forms.Timer ScreenTimer;
    }
}
