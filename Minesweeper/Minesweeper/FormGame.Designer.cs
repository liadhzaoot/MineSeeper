namespace Minesweeper
{
    partial class FormGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGame));
            this.timerGame = new System.Windows.Forms.Timer(this.components);
            this.labelTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.COMLable = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.MenuBotton = new System.Windows.Forms.PictureBox();
            this.CountCOMFlagLable = new System.Windows.Forms.Label();
            this.UserLable = new System.Windows.Forms.Label();
            this.CountUserFlagLable = new System.Windows.Forms.Label();
            this.timerPause = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MenuBotton)).BeginInit();
            this.SuspendLayout();
            // 
            // timerGame
            // 
            this.timerGame.Interval = 1000;
            this.timerGame.Tick += new System.EventHandler(this.timerGame_Tick);
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.BackColor = System.Drawing.Color.Transparent;
            this.labelTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.labelTime.Location = new System.Drawing.Point(191, 51);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(51, 55);
            this.labelTime.TabIndex = 2;
            this.labelTime.Text = "0";
            this.labelTime.Click += new System.EventHandler(this.labelTime_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(165, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 42);
            this.label1.TabIndex = 3;
            this.label1.Text = "Timer";
            // 
            // COMLable
            // 
            this.COMLable.AutoSize = true;
            this.COMLable.BackColor = System.Drawing.Color.Transparent;
            this.COMLable.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.COMLable.ForeColor = System.Drawing.Color.Maroon;
            this.COMLable.Location = new System.Drawing.Point(853, 26);
            this.COMLable.Name = "COMLable";
            this.COMLable.Size = new System.Drawing.Size(100, 78);
            this.COMLable.TabIndex = 4;
            this.COMLable.Text = "COM\r\nFlag";
            this.COMLable.Click += new System.EventHandler(this.User_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::Minesweeper.Properties.Resources.backbutton;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(28, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(89, 80);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // MenuBotton
            // 
            this.MenuBotton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.MenuBotton.Image = ((System.Drawing.Image)(resources.GetObject("MenuBotton.Image")));
            this.MenuBotton.Location = new System.Drawing.Point(900, 900);
            this.MenuBotton.Name = "MenuBotton";
            this.MenuBotton.Size = new System.Drawing.Size(100, 100);
            this.MenuBotton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MenuBotton.TabIndex = 0;
            this.MenuBotton.TabStop = false;
            this.MenuBotton.Click += new System.EventHandler(this.MenuBotton_Click);
            // 
            // CountCOMFlagLable
            // 
            this.CountCOMFlagLable.AutoSize = true;
            this.CountCOMFlagLable.BackColor = System.Drawing.Color.Transparent;
            this.CountCOMFlagLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CountCOMFlagLable.ForeColor = System.Drawing.Color.Red;
            this.CountCOMFlagLable.Location = new System.Drawing.Point(984, 26);
            this.CountCOMFlagLable.Name = "CountCOMFlagLable";
            this.CountCOMFlagLable.Size = new System.Drawing.Size(51, 55);
            this.CountCOMFlagLable.TabIndex = 5;
            this.CountCOMFlagLable.Text = "0";
            // 
            // UserLable
            // 
            this.UserLable.AutoSize = true;
            this.UserLable.BackColor = System.Drawing.Color.Transparent;
            this.UserLable.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserLable.ForeColor = System.Drawing.Color.Navy;
            this.UserLable.Location = new System.Drawing.Point(853, 108);
            this.UserLable.Name = "UserLable";
            this.UserLable.Size = new System.Drawing.Size(90, 78);
            this.UserLable.TabIndex = 6;
            this.UserLable.Text = "USER\r\nFlag";
            // 
            // CountUserFlagLable
            // 
            this.CountUserFlagLable.AutoSize = true;
            this.CountUserFlagLable.BackColor = System.Drawing.Color.Transparent;
            this.CountUserFlagLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CountUserFlagLable.ForeColor = System.Drawing.Color.Blue;
            this.CountUserFlagLable.Location = new System.Drawing.Point(984, 108);
            this.CountUserFlagLable.Name = "CountUserFlagLable";
            this.CountUserFlagLable.Size = new System.Drawing.Size(51, 55);
            this.CountUserFlagLable.TabIndex = 7;
            this.CountUserFlagLable.Text = "0";
            // 
            // timerPause
            // 
            this.timerPause.Interval = 3000;
            this.timerPause.Tick += new System.EventHandler(this.timerPause_Tick);
            // 
            // FormGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.BackgroundImage = global::Minesweeper.Properties.Resources.minefield;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1138, 831);
            this.Controls.Add(this.CountUserFlagLable);
            this.Controls.Add(this.UserLable);
            this.Controls.Add(this.CountCOMFlagLable);
            this.Controls.Add(this.COMLable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.MenuBotton);
            this.Name = "FormGame";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormGame_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.FormGame_Shown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormGame_MouseClick);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MenuBotton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox MenuBotton;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Timer timerGame;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label COMLable;
        public System.Windows.Forms.Label CountCOMFlagLable;
        private System.Windows.Forms.Label UserLable;
        public System.Windows.Forms.Label CountUserFlagLable;
        public System.Windows.Forms.Timer timerPause;
    }
}

