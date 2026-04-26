namespace ScintillaPowershellExample
{
    partial class frmPowershellEditor
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
            this.scintillaBox = new ScintillaNET.Scintilla();
            this.SuspendLayout();
            // 
            // scintillaBox
            // 
            this.scintillaBox.Location = new System.Drawing.Point(12, 12);
            this.scintillaBox.Name = "scintillaBox";
            this.scintillaBox.Size = new System.Drawing.Size(776, 426);
            this.scintillaBox.TabIndex = 0;
            this.scintillaBox.Text = "scintilla1";
            // 
            // frmPowershellEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.scintillaBox);
            this.Name = "frmPowershellEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Powershell Editor";
            this.ResumeLayout(false);

        }

        #endregion

        private ScintillaNET.Scintilla scintillaBox;
    }
}