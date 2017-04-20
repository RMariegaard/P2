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
            this.GetRecommendationButton = new System.Windows.Forms.Button();
            this.InfoLabel = new System.Windows.Forms.Label();
            this.SeachBar = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // GreetingLabel
            // 
            this.GreetingLabel.AutoSize = true;
            this.GreetingLabel.Location = new System.Drawing.Point(13, 13);
            this.GreetingLabel.Name = "GreetingLabel";
            this.GreetingLabel.Size = new System.Drawing.Size(52, 13);
            this.GreetingLabel.TabIndex = 0;
            this.GreetingLabel.Text = "Greetings";
            // 
            // RoskildeArtistsList
            // 
            this.RoskildeArtistsList.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.RoskildeArtistsList.FormattingEnabled = true;
            this.RoskildeArtistsList.Location = new System.Drawing.Point(12, 99);
            this.RoskildeArtistsList.Name = "RoskildeArtistsList";
            this.RoskildeArtistsList.Size = new System.Drawing.Size(379, 329);
            this.RoskildeArtistsList.TabIndex = 1;
            // 
            // GetRecommendationButton
            // 
            this.GetRecommendationButton.Location = new System.Drawing.Point(12, 444);
            this.GetRecommendationButton.Name = "GetRecommendationButton";
            this.GetRecommendationButton.Size = new System.Drawing.Size(379, 38);
            this.GetRecommendationButton.TabIndex = 2;
            this.GetRecommendationButton.Text = "Get Recommendations";
            this.GetRecommendationButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.GetRecommendationButton.UseVisualStyleBackColor = true;
            this.GetRecommendationButton.Click += new System.EventHandler(this.GetRecommendationButton_Click);
            // 
            // InfoLabel
            // 
            this.InfoLabel.Location = new System.Drawing.Point(13, 36);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(378, 35);
            this.InfoLabel.TabIndex = 3;
            this.InfoLabel.Text = "You can now chose with artists you are sure you want to see. Press \"Get Recommend" +
    "ations\" to continue";
            // 
            // SeachBar
            // 
            this.SeachBar.Location = new System.Drawing.Point(16, 74);
            this.SeachBar.Name = "SeachBar";
            this.SeachBar.Size = new System.Drawing.Size(375, 20);
            this.SeachBar.TabIndex = 4;
            this.SeachBar.Text = "Seach";
            this.SeachBar.TextChanged += new System.EventHandler(this.SeachBar_TextChanged);
            // 
            // UIAfterLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 494);
            this.Controls.Add(this.SeachBar);
            this.Controls.Add(this.InfoLabel);
            this.Controls.Add(this.GetRecommendationButton);
            this.Controls.Add(this.RoskildeArtistsList);
            this.Controls.Add(this.GreetingLabel);
            this.Name = "UIAfterLogin";
            this.Text = "UiAfterLogin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label GreetingLabel;
        private System.Windows.Forms.CheckedListBox RoskildeArtistsList;
        private System.Windows.Forms.Button GetRecommendationButton;
        private System.Windows.Forms.Label InfoLabel;
        private System.Windows.Forms.TextBox SeachBar;
    }
}