namespace NETSUITE_PRINT
{
    partial class FrmPrincipal
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnImprimir = new System.Windows.Forms.Button();
            this.CbxType = new System.Windows.Forms.ComboBox();
            this.TxtInternalId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnLogin = new System.Windows.Forms.Button();
            this.BtnLogout = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.CbxFormatoCheque = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnImprimir
            // 
            this.BtnImprimir.Location = new System.Drawing.Point(359, 9);
            this.BtnImprimir.Name = "BtnImprimir";
            this.BtnImprimir.Size = new System.Drawing.Size(114, 71);
            this.BtnImprimir.TabIndex = 0;
            this.BtnImprimir.Text = "Imprimir";
            this.BtnImprimir.UseVisualStyleBackColor = true;
            this.BtnImprimir.Click += new System.EventHandler(this.button1_Click);
            // 
            // CbxType
            // 
            this.CbxType.FormattingEnabled = true;
            this.CbxType.Items.AddRange(new object[] {
            "FACTURACION",
            "PAGO_PROVEEDOR",
            "CHEQUES",
            "NOTAS_CREDITO"});
            this.CbxType.Location = new System.Drawing.Point(199, 10);
            this.CbxType.Name = "CbxType";
            this.CbxType.Size = new System.Drawing.Size(126, 21);
            this.CbxType.TabIndex = 1;
            this.CbxType.Text = "FACTURACION";
            // 
            // TxtInternalId
            // 
            this.TxtInternalId.Location = new System.Drawing.Point(199, 63);
            this.TxtInternalId.Name = "TxtInternalId";
            this.TxtInternalId.Size = new System.Drawing.Size(77, 20);
            this.TxtInternalId.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(107, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Type:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 9;
            // 
            // BtnLogin
            // 
            this.BtnLogin.Location = new System.Drawing.Point(13, 11);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(75, 42);
            this.BtnLogin.TabIndex = 5;
            this.BtnLogin.Text = "Login NETSIUTE";
            this.BtnLogin.UseVisualStyleBackColor = true;
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // BtnLogout
            // 
            this.BtnLogout.Location = new System.Drawing.Point(12, 79);
            this.BtnLogout.Name = "BtnLogout";
            this.BtnLogout.Size = new System.Drawing.Size(75, 42);
            this.BtnLogout.TabIndex = 6;
            this.BtnLogout.Text = "Logout NETSIUTE";
            this.BtnLogout.UseVisualStyleBackColor = true;
            this.BtnLogout.Click += new System.EventHandler(this.BtnLogout_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(107, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Formato Cheque:";
            // 
            // CbxFormatoCheque
            // 
            this.CbxFormatoCheque.FormattingEnabled = true;
            this.CbxFormatoCheque.Items.AddRange(new object[] {
            "CH-BAC-BA",
            "CH-BI-BDAV-BH",
            "CH-CITI-CUS"});
            this.CbxFormatoCheque.Location = new System.Drawing.Point(199, 36);
            this.CbxFormatoCheque.Name = "CbxFormatoCheque";
            this.CbxFormatoCheque.Size = new System.Drawing.Size(126, 21);
            this.CbxFormatoCheque.TabIndex = 8;
            this.CbxFormatoCheque.Text = "CH-CITI-CUS";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(196, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Estado: No Conectado";
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 144);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CbxFormatoCheque);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BtnLogout);
            this.Controls.Add(this.BtnLogin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtInternalId);
            this.Controls.Add(this.CbxType);
            this.Controls.Add(this.BtnImprimir);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPrincipal";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnImprimir;
        private System.Windows.Forms.ComboBox CbxType;
        private System.Windows.Forms.TextBox TxtInternalId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnLogin;
        private System.Windows.Forms.Button BtnLogout;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CbxFormatoCheque;
        private System.Windows.Forms.Label label4;
    }
}

