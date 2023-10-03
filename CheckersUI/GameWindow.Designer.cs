namespace CheckersUI
{
    internal partial class GameWindow
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
            this.components = new System.ComponentModel.Container();
            this.labelTeamOne = new System.Windows.Forms.Label();
            this.labelTeamTwo = new System.Windows.Forms.Label();
            this.timerComputerMovement = new System.Windows.Forms.Timer(this.components);
            this.pictureBoxTeamOneArrow = new System.Windows.Forms.PictureBox();
            this.pictureBoxTeamTwoArrow = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTeamOneArrow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTeamTwoArrow)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTeamOne
            // 
            this.labelTeamOne.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelTeamOne.AutoSize = true;
            this.labelTeamOne.BackColor = System.Drawing.Color.White;
            this.labelTeamOne.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelTeamOne.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTeamOne.Location = new System.Drawing.Point(67, 17);
            this.labelTeamOne.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTeamOne.Name = "labelTeamOne";
            this.labelTeamOne.Size = new System.Drawing.Size(125, 31);
            this.labelTeamOne.TabIndex = 0;
            this.labelTeamOne.Text = "Player 1: 0";
            // 
            // labelTeamTwo
            // 
            this.labelTeamTwo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelTeamTwo.AutoSize = true;
            this.labelTeamTwo.BackColor = System.Drawing.Color.White;
            this.labelTeamTwo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelTeamTwo.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTeamTwo.Location = new System.Drawing.Point(196, 17);
            this.labelTeamTwo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTeamTwo.Name = "labelTeamTwo";
            this.labelTeamTwo.Size = new System.Drawing.Size(125, 31);
            this.labelTeamTwo.TabIndex = 0;
            this.labelTeamTwo.Text = "Player 2: 0";
            // 
            // timerComputerMovement
            // 
            this.timerComputerMovement.Interval = 1000;
            this.timerComputerMovement.Tick += new System.EventHandler(this.timerComputerMovement_Tick);
            // 
            // pictureBoxTeamOneArrow
            // 
            this.pictureBoxTeamOneArrow.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxTeamOneArrow.Image = global::CheckersUI.Properties.Resources.White_Arrow;
            this.pictureBoxTeamOneArrow.Location = new System.Drawing.Point(20, 12);
            this.pictureBoxTeamOneArrow.Name = "pictureBoxTeamOneArrow";
            this.pictureBoxTeamOneArrow.Size = new System.Drawing.Size(42, 40);
            this.pictureBoxTeamOneArrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxTeamOneArrow.TabIndex = 1;
            this.pictureBoxTeamOneArrow.TabStop = false;
            this.pictureBoxTeamOneArrow.WaitOnLoad = true;
            // 
            // pictureBoxTeamTwoArrow
            // 
            this.pictureBoxTeamTwoArrow.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxTeamTwoArrow.Image = global::CheckersUI.Properties.Resources.White_Arrow;
            this.pictureBoxTeamTwoArrow.Location = new System.Drawing.Point(169, 12);
            this.pictureBoxTeamTwoArrow.Name = "pictureBoxTeamTwoArrow";
            this.pictureBoxTeamTwoArrow.Size = new System.Drawing.Size(42, 40);
            this.pictureBoxTeamTwoArrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxTeamTwoArrow.TabIndex = 1;
            this.pictureBoxTeamTwoArrow.TabStop = false;
            this.pictureBoxTeamTwoArrow.Visible = false;
            this.pictureBoxTeamTwoArrow.WaitOnLoad = true;
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImage = global::CheckersUI.Properties.Resources.Background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(358, 329);
            this.Controls.Add(this.pictureBoxTeamTwoArrow);
            this.Controls.Add(this.pictureBoxTeamOneArrow);
            this.Controls.Add(this.labelTeamTwo);
            this.Controls.Add(this.labelTeamOne);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::CheckersUI.Properties.Resources.Black_King_Icon;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "GameWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Checkers";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTeamOneArrow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTeamTwoArrow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTeamOne;
        private System.Windows.Forms.Label labelTeamTwo;
        private System.Windows.Forms.Timer timerComputerMovement;
        private System.Windows.Forms.PictureBox pictureBoxTeamOneArrow;
        private System.Windows.Forms.PictureBox pictureBoxTeamTwoArrow;
    }
}