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
            this.RoskildeArtistsList = new System.Windows.Forms.CheckedListBox();
            this.HardAddButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GreetingLabel
            // 
            this.GreetingLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GreetingLabel.AutoSize = true;
            this.GreetingLabel.Location = new System.Drawing.Point(13, 13);
            this.GreetingLabel.Name = "GreetingLabel";
            this.GreetingLabel.Size = new System.Drawing.Size(108, 13);
            this.GreetingLabel.TabIndex = 0;
            this.GreetingLabel.Text = "Welcome to the user!";
            // 
            // RoskildeArtistsList
            // 
            this.RoskildeArtistsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RoskildeArtistsList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RoskildeArtistsList.FormattingEnabled = true;
            this.RoskildeArtistsList.Location = new System.Drawing.Point(12, 31);
            this.RoskildeArtistsList.Name = "RoskildeArtistsList";
            this.RoskildeArtistsList.Size = new System.Drawing.Size(387, 540);
            this.RoskildeArtistsList.TabIndex = 10;
            this.RoskildeArtistsList.ThreeDCheckBoxes = true;
            // 
            // HardAddButton
            // 
            this.HardAddButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.HardAddButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.HardAddButton.Location = new System.Drawing.Point(0, 571);
            this.HardAddButton.Name = "HardAddButton";
            this.HardAddButton.Size = new System.Drawing.Size(411, 62);
            this.HardAddButton.TabIndex = 9;
            this.HardAddButton.Text = "Add to list";
            this.HardAddButton.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button1.Location = new System.Drawing.Point(0, 633);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(411, 62);
            this.button1.TabIndex = 8;
            this.button1.Text = "Find Recommendations";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // UIAfterLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 695);
            this.Controls.Add(this.RoskildeArtistsList);
            this.Controls.Add(this.HardAddButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.GreetingLabel);
            this.Name = "UIAfterLogin";
            this.Text = "UIAfterLogin";
            this.Load += new System.EventHandler(this.UIAfterLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label GreetingLabel;
        private System.Windows.Forms.CheckedListBox RoskildeArtistsList;
        private System.Windows.Forms.Button HardAddButton;
        private System.Windows.Forms.Button button1;
    }
}