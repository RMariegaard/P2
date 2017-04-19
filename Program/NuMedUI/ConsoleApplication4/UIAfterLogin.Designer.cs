namespace Recommender
{
    partial class UIAfterLogin
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
            this.GreetingLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.HardAddButton = new System.Windows.Forms.Button();
            this.RoskildeArtistsList = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // GreetingLabel
            // 
            this.GreetingLabel.AutoSize = true;
            this.GreetingLabel.Location = new System.Drawing.Point(13, 13);
            this.GreetingLabel.Name = "GreetingLabel";
            this.GreetingLabel.Size = new System.Drawing.Size(108, 13);
            this.GreetingLabel.TabIndex = 0;
            this.GreetingLabel.Text = "Welcome to the user!";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(12, 978);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(680, 63);
            this.button1.TabIndex = 1;
            this.button1.Text = "Find Recommendations";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // HardAddButton
            // 
            this.HardAddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.HardAddButton.Location = new System.Drawing.Point(12, 910);
            this.HardAddButton.Name = "HardAddButton";
            this.HardAddButton.Size = new System.Drawing.Size(680, 62);
            this.HardAddButton.TabIndex = 6;
            this.HardAddButton.Text = "Add to list";
            this.HardAddButton.UseVisualStyleBackColor = true;
            this.HardAddButton.Click += new System.EventHandler(this.HardAddButton_Click);
            // 
            // RoskildeArtistsList
            // 
            this.RoskildeArtistsList.FormattingEnabled = true;
            this.RoskildeArtistsList.Location = new System.Drawing.Point(12, 29);
            this.RoskildeArtistsList.Name = "RoskildeArtistsList";
            this.RoskildeArtistsList.Size = new System.Drawing.Size(680, 874);
            this.RoskildeArtistsList.TabIndex = 7;
            // 
            // UIAfterLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 1053);
            this.Controls.Add(this.RoskildeArtistsList);
            this.Controls.Add(this.HardAddButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.GreetingLabel);
            this.Name = "UIAfterLogin";
            this.Text = "UIAfterLogin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label GreetingLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button HardAddButton;
        private System.Windows.Forms.CheckedListBox RoskildeArtistsList;
    }
}