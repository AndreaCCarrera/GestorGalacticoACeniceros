namespace VForms
{
    partial class FDetalles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FDetalles));
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtAcept = new System.Windows.Forms.Button();
            this.lblId = new System.Windows.Forms.Label();
            this.lblTipo = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblDistance = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtTipo = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.chbAgua = new System.Windows.Forms.CheckBox();
            this.chbVida = new System.Windows.Forms.CheckBox();
            this.chbAtmosf = new System.Windows.Forms.CheckBox();
            this.dtDate = new System.Windows.Forms.DateTimePicker();
            this.picFondoDetalles = new System.Windows.Forms.PictureBox();
            this.txtSize = new System.Windows.Forms.NumericUpDown();
            this.txtDistance = new System.Windows.Forms.NumericUpDown();
            this.lbMensajes = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btPedir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picFondoDetalles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDistance)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCancel.Location = new System.Drawing.Point(81, 324);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "CANCELAR";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtAcept
            // 
            this.txtAcept.BackColor = System.Drawing.Color.Purple;
            this.txtAcept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtAcept.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAcept.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtAcept.Location = new System.Drawing.Point(234, 324);
            this.txtAcept.Name = "txtAcept";
            this.txtAcept.Size = new System.Drawing.Size(98, 35);
            this.txtAcept.TabIndex = 1;
            this.txtAcept.Text = "ACEPTAR";
            this.txtAcept.UseVisualStyleBackColor = false;
            this.txtAcept.Click += new System.EventHandler(this.txtAcept_Click);
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblId.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblId.Location = new System.Drawing.Point(25, 28);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(20, 13);
            this.lblId.TabIndex = 2;
            this.lblId.Text = "ID";
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTipo.Location = new System.Drawing.Point(25, 64);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(36, 13);
            this.lblTipo.TabIndex = 3;
            this.lblTipo.Text = "TIPO";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblNombre.Location = new System.Drawing.Point(25, 98);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(60, 13);
            this.lblNombre.TabIndex = 4;
            this.lblNombre.Text = "NOMBRE";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDate.Location = new System.Drawing.Point(25, 129);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(183, 13);
            this.lblDate.TabIndex = 5;
            this.lblDate.Text = "FECHA DE DESCUBRIMIENTO";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSize.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSize.Location = new System.Drawing.Point(25, 160);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(59, 13);
            this.lblSize.TabIndex = 6;
            this.lblSize.Text = "TAMAÑO";
            // 
            // lblDistance
            // 
            this.lblDistance.AutoSize = true;
            this.lblDistance.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblDistance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDistance.Location = new System.Drawing.Point(25, 191);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(163, 13);
            this.lblDistance.TabIndex = 7;
            this.lblDistance.Text = "DISTANCIA DE LA TIERRA";
            // 
            // txtId
            // 
            this.txtId.Enabled = false;
            this.txtId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtId.Location = new System.Drawing.Point(51, 25);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(52, 22);
            this.txtId.TabIndex = 11;
            // 
            // txtTipo
            // 
            this.txtTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTipo.Location = new System.Drawing.Point(61, 61);
            this.txtTipo.Name = "txtTipo";
            this.txtTipo.Size = new System.Drawing.Size(103, 22);
            this.txtTipo.TabIndex = 12;
            // 
            // txtNombre
            // 
            this.txtNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.Location = new System.Drawing.Point(88, 94);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(100, 22);
            this.txtNombre.TabIndex = 13;
            // 
            // chbAgua
            // 
            this.chbAgua.AutoSize = true;
            this.chbAgua.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.chbAgua.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbAgua.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chbAgua.Location = new System.Drawing.Point(55, 243);
            this.chbAgua.Name = "chbAgua";
            this.chbAgua.Size = new System.Drawing.Size(60, 17);
            this.chbAgua.TabIndex = 17;
            this.chbAgua.Text = "AGUA";
            this.chbAgua.UseVisualStyleBackColor = false;
            // 
            // chbVida
            // 
            this.chbVida.AutoSize = true;
            this.chbVida.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.chbVida.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbVida.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chbVida.Location = new System.Drawing.Point(163, 243);
            this.chbVida.Name = "chbVida";
            this.chbVida.Size = new System.Drawing.Size(55, 17);
            this.chbVida.TabIndex = 18;
            this.chbVida.Text = "VIDA";
            this.chbVida.UseVisualStyleBackColor = false;
            // 
            // chbAtmosf
            // 
            this.chbAtmosf.AutoSize = true;
            this.chbAtmosf.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.chbAtmosf.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbAtmosf.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chbAtmosf.Location = new System.Drawing.Point(264, 243);
            this.chbAtmosf.Name = "chbAtmosf";
            this.chbAtmosf.Size = new System.Drawing.Size(101, 17);
            this.chbAtmosf.TabIndex = 19;
            this.chbAtmosf.Text = "ATMÓSFERA";
            this.chbAtmosf.UseVisualStyleBackColor = false;
            // 
            // dtDate
            // 
            this.dtDate.Location = new System.Drawing.Point(214, 125);
            this.dtDate.Name = "dtDate";
            this.dtDate.Size = new System.Drawing.Size(199, 20);
            this.dtDate.TabIndex = 20;
            this.dtDate.Value = new System.DateTime(2023, 11, 6, 15, 53, 0, 0);
            // 
            // picFondoDetalles
            // 
            this.picFondoDetalles.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picFondoDetalles.BackgroundImage")));
            this.picFondoDetalles.ErrorImage = ((System.Drawing.Image)(resources.GetObject("picFondoDetalles.ErrorImage")));
            this.picFondoDetalles.Location = new System.Drawing.Point(0, 0);
            this.picFondoDetalles.Name = "picFondoDetalles";
            this.picFondoDetalles.Size = new System.Drawing.Size(430, 390);
            this.picFondoDetalles.TabIndex = 21;
            this.picFondoDetalles.TabStop = false;
            // 
            // txtSize
            // 
            this.txtSize.DecimalPlaces = 2;
            this.txtSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSize.Location = new System.Drawing.Point(95, 156);
            this.txtSize.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.txtSize.Name = "txtSize";
            this.txtSize.Size = new System.Drawing.Size(120, 22);
            this.txtSize.TabIndex = 22;
            // 
            // txtDistance
            // 
            this.txtDistance.DecimalPlaces = 2;
            this.txtDistance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDistance.Location = new System.Drawing.Point(195, 187);
            this.txtDistance.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.txtDistance.Name = "txtDistance";
            this.txtDistance.Size = new System.Drawing.Size(120, 22);
            this.txtDistance.TabIndex = 23;
            // 
            // lbMensajes
            // 
            this.lbMensajes.AutoSize = true;
            this.lbMensajes.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMensajes.ForeColor = System.Drawing.Color.Purple;
            this.lbMensajes.Location = new System.Drawing.Point(168, 288);
            this.lbMensajes.Name = "lbMensajes";
            this.lbMensajes.Size = new System.Drawing.Size(74, 16);
            this.lbMensajes.TabIndex = 24;
            this.lbMensajes.Text = "Mensajes";
            this.lbMensajes.Visible = false;
            // 
            // txtUrl
            // 
            this.txtUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUrl.Location = new System.Drawing.Point(151, 26);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(262, 22);
            this.txtUrl.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(115, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "URL";
            // 
            // btPedir
            // 
            this.btPedir.BackColor = System.Drawing.Color.Purple;
            this.btPedir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btPedir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btPedir.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btPedir.Location = new System.Drawing.Point(234, 54);
            this.btPedir.Name = "btPedir";
            this.btPedir.Size = new System.Drawing.Size(98, 29);
            this.btPedir.TabIndex = 27;
            this.btPedir.Text = "Pedir data";
            this.btPedir.UseVisualStyleBackColor = false;
            this.btPedir.Click += new System.EventHandler(this.btPedir_Click);
            // 
            // FDetalles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 388);
            this.Controls.Add(this.btPedir);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbMensajes);
            this.Controls.Add(this.txtDistance);
            this.Controls.Add(this.txtSize);
            this.Controls.Add(this.dtDate);
            this.Controls.Add(this.chbAtmosf);
            this.Controls.Add(this.chbVida);
            this.Controls.Add(this.chbAgua);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.txtTipo);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.lblDistance);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.lblTipo);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.txtAcept);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.picFondoDetalles);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FDetalles";
            this.Text = "Detalles";
            ((System.ComponentModel.ISupportInitialize)(this.picFondoDetalles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDistance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button txtAcept;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtTipo;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.CheckBox chbAgua;
        private System.Windows.Forms.CheckBox chbVida;
        private System.Windows.Forms.CheckBox chbAtmosf;
        private System.Windows.Forms.DateTimePicker dtDate;
        private System.Windows.Forms.PictureBox picFondoDetalles;
        private System.Windows.Forms.NumericUpDown txtSize;
        private System.Windows.Forms.NumericUpDown txtDistance;
        private System.Windows.Forms.Label lbMensajes;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btPedir;
    }
}