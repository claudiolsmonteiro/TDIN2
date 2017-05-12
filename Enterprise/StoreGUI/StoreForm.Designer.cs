namespace StoreGUI
{
    partial class StoreForm
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
            this.BooksButton = new System.Windows.Forms.Button();
            this.OrdersButton = new System.Windows.Forms.Button();
            this.SellButton = new System.Windows.Forms.Button();
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // BooksButton
            // 
            this.BooksButton.Location = new System.Drawing.Point(13, 12);
            this.BooksButton.Name = "BooksButton";
            this.BooksButton.Size = new System.Drawing.Size(58, 22);
            this.BooksButton.TabIndex = 0;
            this.BooksButton.Text = "Books";
            this.BooksButton.UseVisualStyleBackColor = true;
            this.BooksButton.Click += new System.EventHandler(this.BooksButton_Click);
            // 
            // OrdersButton
            // 
            this.OrdersButton.Location = new System.Drawing.Point(77, 12);
            this.OrdersButton.Name = "OrdersButton";
            this.OrdersButton.Size = new System.Drawing.Size(58, 22);
            this.OrdersButton.TabIndex = 1;
            this.OrdersButton.Text = "Orders";
            this.OrdersButton.UseVisualStyleBackColor = true;
            this.OrdersButton.Click += new System.EventHandler(this.OrdersButton_Click);
            // 
            // SellButton
            // 
            this.SellButton.Location = new System.Drawing.Point(141, 12);
            this.SellButton.Name = "SellButton";
            this.SellButton.Size = new System.Drawing.Size(58, 22);
            this.SellButton.TabIndex = 2;
            this.SellButton.Text = "Sell";
            this.SellButton.UseVisualStyleBackColor = true;
            this.SellButton.Click += new System.EventHandler(this.SellButton_Click);
            // 
            // SearchBox
            // 
            this.SearchBox.Location = new System.Drawing.Point(13, 40);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(508, 20);
            this.SearchBox.TabIndex = 3;
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(527, 40);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(61, 20);
            this.SearchButton.TabIndex = 4;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(13, 67);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(575, 343);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // StoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 500);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.SearchBox);
            this.Controls.Add(this.SellButton);
            this.Controls.Add(this.OrdersButton);
            this.Controls.Add(this.BooksButton);
            this.Name = "StoreForm";
            this.Text = "Store";
            this.Load += new System.EventHandler(this.StoreForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BooksButton;
        private System.Windows.Forms.Button OrdersButton;
        private System.Windows.Forms.Button SellButton;
        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.ListView listView1;
    }
}

