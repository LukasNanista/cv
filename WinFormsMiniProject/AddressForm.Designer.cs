namespace Homework40WinFormsMiniProject
{
    partial class AddressForm
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
            this.streetNameLabel = new System.Windows.Forms.Label();
            this.streetNameText = new System.Windows.Forms.TextBox();
            this.streetNumberLabel = new System.Windows.Forms.Label();
            this.streetNumberText = new System.Windows.Forms.TextBox();
            this.cityLabel = new System.Windows.Forms.Label();
            this.cityText = new System.Windows.Forms.TextBox();
            this.zipLabel = new System.Windows.Forms.Label();
            this.zipCodeText = new System.Windows.Forms.TextBox();
            this.countryLabel = new System.Windows.Forms.Label();
            this.countryText = new System.Windows.Forms.TextBox();
            this.createAddressButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // streetNameLabel
            // 
            this.streetNameLabel.AutoSize = true;
            this.streetNameLabel.Location = new System.Drawing.Point(35, 35);
            this.streetNameLabel.Name = "streetNameLabel";
            this.streetNameLabel.Size = new System.Drawing.Size(77, 29);
            this.streetNameLabel.TabIndex = 0;
            this.streetNameLabel.Text = "Street";
            // 
            // streetNameText
            // 
            this.streetNameText.Location = new System.Drawing.Point(211, 32);
            this.streetNameText.Name = "streetNameText";
            this.streetNameText.Size = new System.Drawing.Size(275, 35);
            this.streetNameText.TabIndex = 1;
            // 
            // streetNumberLabel
            // 
            this.streetNumberLabel.AutoSize = true;
            this.streetNumberLabel.Location = new System.Drawing.Point(35, 82);
            this.streetNumberLabel.Name = "streetNumberLabel";
            this.streetNumberLabel.Size = new System.Drawing.Size(170, 29);
            this.streetNumberLabel.TabIndex = 0;
            this.streetNumberLabel.Text = "Street Number";
            // 
            // streetNumberText
            // 
            this.streetNumberText.Location = new System.Drawing.Point(211, 79);
            this.streetNumberText.Name = "streetNumberText";
            this.streetNumberText.Size = new System.Drawing.Size(275, 35);
            this.streetNumberText.TabIndex = 2;
            // 
            // cityLabel
            // 
            this.cityLabel.AutoSize = true;
            this.cityLabel.Location = new System.Drawing.Point(35, 126);
            this.cityLabel.Name = "cityLabel";
            this.cityLabel.Size = new System.Drawing.Size(53, 29);
            this.cityLabel.TabIndex = 0;
            this.cityLabel.Text = "City";
            // 
            // cityText
            // 
            this.cityText.Location = new System.Drawing.Point(211, 123);
            this.cityText.Name = "cityText";
            this.cityText.Size = new System.Drawing.Size(275, 35);
            this.cityText.TabIndex = 3;
            // 
            // zipLabel
            // 
            this.zipLabel.AutoSize = true;
            this.zipLabel.Location = new System.Drawing.Point(35, 172);
            this.zipLabel.Name = "zipLabel";
            this.zipLabel.Size = new System.Drawing.Size(114, 29);
            this.zipLabel.TabIndex = 0;
            this.zipLabel.Text = "ZIP Code";
            // 
            // zipCodeText
            // 
            this.zipCodeText.Location = new System.Drawing.Point(211, 169);
            this.zipCodeText.Name = "zipCodeText";
            this.zipCodeText.Size = new System.Drawing.Size(275, 35);
            this.zipCodeText.TabIndex = 4;
            // 
            // countryLabel
            // 
            this.countryLabel.AutoSize = true;
            this.countryLabel.Location = new System.Drawing.Point(35, 220);
            this.countryLabel.Name = "countryLabel";
            this.countryLabel.Size = new System.Drawing.Size(95, 29);
            this.countryLabel.TabIndex = 0;
            this.countryLabel.Text = "Country";
            // 
            // countryText
            // 
            this.countryText.Location = new System.Drawing.Point(211, 217);
            this.countryText.Name = "countryText";
            this.countryText.Size = new System.Drawing.Size(275, 35);
            this.countryText.TabIndex = 5;
            // 
            // createAddressButton
            // 
            this.createAddressButton.Location = new System.Drawing.Point(134, 288);
            this.createAddressButton.Name = "createAddressButton";
            this.createAddressButton.Size = new System.Drawing.Size(271, 79);
            this.createAddressButton.TabIndex = 6;
            this.createAddressButton.Text = "Create Address";
            this.createAddressButton.UseVisualStyleBackColor = true;
            this.createAddressButton.Click += new System.EventHandler(this.createAddressButton_Click);
            // 
            // AddressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(533, 412);
            this.Controls.Add(this.createAddressButton);
            this.Controls.Add(this.countryText);
            this.Controls.Add(this.zipCodeText);
            this.Controls.Add(this.cityText);
            this.Controls.Add(this.streetNumberText);
            this.Controls.Add(this.streetNameText);
            this.Controls.Add(this.countryLabel);
            this.Controls.Add(this.zipLabel);
            this.Controls.Add(this.cityLabel);
            this.Controls.Add(this.streetNumberLabel);
            this.Controls.Add(this.streetNameLabel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.Name = "AddressForm";
            this.Text = "AddressForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label streetNameLabel;
        private System.Windows.Forms.TextBox streetNameText;
        private System.Windows.Forms.Label streetNumberLabel;
        private System.Windows.Forms.TextBox streetNumberText;
        private System.Windows.Forms.Label cityLabel;
        private System.Windows.Forms.TextBox cityText;
        private System.Windows.Forms.Label zipLabel;
        private System.Windows.Forms.TextBox zipCodeText;
        private System.Windows.Forms.Label countryLabel;
        private System.Windows.Forms.TextBox countryText;
        private System.Windows.Forms.Button createAddressButton;
    }
}