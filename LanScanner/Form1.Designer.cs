namespace LanScanner
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Wymagana metoda obsługi projektanta — nie należy modyfikować 
        /// zawartość tej metody z edytorem kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.konsola1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // konsola1
            // 
            this.konsola1.BackColor = System.Drawing.SystemColors.WindowText;
            this.konsola1.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.konsola1.Location = new System.Drawing.Point(397, 12);
            this.konsola1.Multiline = true;
            this.konsola1.Name = "konsola1";
            this.konsola1.ReadOnly = true;
            this.konsola1.Size = new System.Drawing.Size(751, 500);
            this.konsola1.TabIndex = 0;
            this.konsola1.TextChanged += new System.EventHandler(this.konsola1_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 524);
            this.Controls.Add(this.konsola1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox konsola1;
    }
}

