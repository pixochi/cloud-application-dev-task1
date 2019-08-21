namespace Task1GUIForm
{
    partial class ErrorsForm
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
            if (disposing && (components != null)) {
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
            this.errorsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // errorsLabel
            // 
            this.errorsLabel.BackColor = System.Drawing.SystemColors.Info;
            this.errorsLabel.CausesValidation = false;
            this.errorsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorsLabel.ForeColor = System.Drawing.Color.Black;
            this.errorsLabel.Location = new System.Drawing.Point(0, 0);
            this.errorsLabel.Name = "errorsLabel";
            this.errorsLabel.Size = new System.Drawing.Size(552, 253);
            this.errorsLabel.TabIndex = 0;
            this.errorsLabel.Text = "Errors:";
            // 
            // ErrorsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 253);
            this.Controls.Add(this.errorsLabel);
            this.Name = "ErrorsForm";
            this.Text = "ErrorsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ErrorsForm_FormClosing);
            this.Load += new System.EventHandler(this.ErrorsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label errorsLabel;
    }
}