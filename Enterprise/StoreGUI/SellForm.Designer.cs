namespace StoreGUI
{
    partial class SellForm
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
            this.client_name = new System.Windows.Forms.TextBox();
            this.client_email = new System.Windows.Forms.TextBox();
            this.client_address = new System.Windows.Forms.TextBox();
            this.book_title = new System.Windows.Forms.TextBox();
            this.client = new System.Windows.Forms.Label();
            this.email = new System.Windows.Forms.Label();
            this.address = new System.Windows.Forms.Label();
            this.book = new System.Windows.Forms.Label();
            this.confirmButton = new System.Windows.Forms.Button();
            this.quantity = new System.Windows.Forms.Label();
            this.quantityValue = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.quantityValue)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.client_name.Location = new System.Drawing.Point(53, 28);
            this.client_name.Name = "textBox1";
            this.client_name.Size = new System.Drawing.Size(203, 20);
            this.client_name.TabIndex = 0;
            // 
            // textBox2
            // 
            this.client_email.Location = new System.Drawing.Point(53, 55);
            this.client_email.Name = "textBox2";
            this.client_email.Size = new System.Drawing.Size(203, 20);
            this.client_email.TabIndex = 1;
            // 
            // textBox3
            // 
            this.client_address.Location = new System.Drawing.Point(53, 82);
            this.client_address.Name = "textBox3";
            this.client_address.Size = new System.Drawing.Size(203, 20);
            this.client_address.TabIndex = 2;
            // 
            // textBox4
            // 
            this.book_title.Location = new System.Drawing.Point(53, 109);
            this.book_title.Name = "textBox4";
            this.book_title.Size = new System.Drawing.Size(203, 20);
            this.book_title.TabIndex = 3;
            // 
            // label1
            // 
            this.client.AutoSize = true;
            this.client.Location = new System.Drawing.Point(12, 31);
            this.client.Name = "label1";
            this.client.Size = new System.Drawing.Size(35, 13);
            this.client.TabIndex = 4;
            this.client.Text = "client";
            // 
            // label2
            // 
            this.email.AutoSize = true;
            this.email.Location = new System.Drawing.Point(12, 58);
            this.email.Name = "label2";
            this.email.Size = new System.Drawing.Size(35, 13);
            this.email.TabIndex = 5;
            this.email.Text = "email";
            // 
            // label3
            // 
            this.address.AutoSize = true;
            this.address.Location = new System.Drawing.Point(12, 85);
            this.address.Name = "label3";
            this.address.Size = new System.Drawing.Size(35, 13);
            this.address.TabIndex = 6;
            this.address.Text = "address";
            // 
            // label4
            // 
            this.book.AutoSize = true;
            this.book.Location = new System.Drawing.Point(12, 112);
            this.book.Name = "label4";
            this.book.Size = new System.Drawing.Size(35, 13);
            this.book.TabIndex = 7;
            this.book.Text = "book";
            // 
            // button1
            // 
            this.confirmButton.Location = new System.Drawing.Point(105, 159);
            this.confirmButton.Name = "button1";
            this.confirmButton.Size = new System.Drawing.Size(75, 23);
            this.confirmButton.TabIndex = 8;
            this.confirmButton.Text = "confirm";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.quantity.AutoSize = true;
            this.quantity.Location = new System.Drawing.Point(12, 140);
            this.quantity.Name = "label5";
            this.quantity.Size = new System.Drawing.Size(35, 13);
            this.quantity.TabIndex = 9;
            this.quantity.Text = "quantity";
            // 
            // numericUpDown1
            // 
            this.quantityValue.Location = new System.Drawing.Point(53, 138);
            this.quantityValue.Name = "numericUpDown1";
            this.quantityValue.Size = new System.Drawing.Size(45, 20);
            this.quantityValue.TabIndex = 10;
            // 
            // SellForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 194);
            this.Controls.Add(this.quantityValue);
            this.Controls.Add(this.quantity);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.book);
            this.Controls.Add(this.address);
            this.Controls.Add(this.email);
            this.Controls.Add(this.client);
            this.Controls.Add(this.book_title);
            this.Controls.Add(this.client_address);
            this.Controls.Add(this.client_email);
            this.Controls.Add(this.client_name);
            this.Name = "SellForm";
            this.Text = "SellForm";
            this.Load += new System.EventHandler(this.SellForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.quantityValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox client_name;
        private System.Windows.Forms.TextBox client_email;
        private System.Windows.Forms.TextBox client_address;
        private System.Windows.Forms.TextBox book_title;
        private System.Windows.Forms.Label client;
        private System.Windows.Forms.Label email;
        private System.Windows.Forms.Label address;
        private System.Windows.Forms.Label book;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Label quantity;
        private System.Windows.Forms.NumericUpDown quantityValue;
    }
}