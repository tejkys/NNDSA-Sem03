namespace NNDSA_Sem03
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            groupBox1 = new GroupBox();
            buttonClear = new Button();
            buttonKeys = new Button();
            buttonFind = new Button();
            buttonBuild = new Button();
            textBoxOutput = new TextBox();
            groupBox2 = new GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            groupBox1.Controls.Add(buttonClear);
            groupBox1.Controls.Add(buttonKeys);
            groupBox1.Controls.Add(buttonFind);
            groupBox1.Controls.Add(buttonBuild);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(138, 498);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Controls";
            // 
            // buttonClear
            // 
            buttonClear.BackgroundImageLayout = ImageLayout.None;
            buttonClear.Image = Properties.Resources.broom_fill;
            buttonClear.ImageAlign = ContentAlignment.MiddleLeft;
            buttonClear.Location = new Point(6, 149);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new Size(120, 32);
            buttonClear.TabIndex = 4;
            buttonClear.Text = "Clear Output";
            buttonClear.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.Click += buttonClear_Click;
            // 
            // buttonKeys
            // 
            buttonKeys.BackgroundImageLayout = ImageLayout.None;
            buttonKeys.Image = Properties.Resources.key_fill;
            buttonKeys.ImageAlign = ContentAlignment.MiddleLeft;
            buttonKeys.Location = new Point(6, 111);
            buttonKeys.Name = "buttonKeys";
            buttonKeys.Size = new Size(120, 32);
            buttonKeys.TabIndex = 3;
            buttonKeys.Text = "Keys";
            buttonKeys.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonKeys.UseVisualStyleBackColor = true;
            buttonKeys.Click += buttonKeys_Click;
            // 
            // buttonFind
            // 
            buttonFind.BackgroundImageLayout = ImageLayout.None;
            buttonFind.Image = Properties.Resources.binoculars_fill;
            buttonFind.ImageAlign = ContentAlignment.MiddleLeft;
            buttonFind.Location = new Point(6, 73);
            buttonFind.Name = "buttonFind";
            buttonFind.Size = new Size(120, 32);
            buttonFind.TabIndex = 2;
            buttonFind.Text = "Find";
            buttonFind.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonFind.UseVisualStyleBackColor = true;
            buttonFind.Click += buttonFind_Click;
            // 
            // buttonBuild
            // 
            buttonBuild.BackgroundImageLayout = ImageLayout.None;
            buttonBuild.Image = Properties.Resources.hammer_fill;
            buttonBuild.ImageAlign = ContentAlignment.MiddleLeft;
            buttonBuild.Location = new Point(6, 35);
            buttonBuild.Name = "buttonBuild";
            buttonBuild.Size = new Size(120, 32);
            buttonBuild.TabIndex = 1;
            buttonBuild.Text = "Build";
            buttonBuild.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonBuild.UseVisualStyleBackColor = true;
            buttonBuild.Click += buttonBuild_Click;
            // 
            // textBoxOutput
            // 
            textBoxOutput.Dock = DockStyle.Fill;
            textBoxOutput.Location = new Point(3, 19);
            textBoxOutput.Multiline = true;
            textBoxOutput.Name = "textBoxOutput";
            textBoxOutput.ReadOnly = true;
            textBoxOutput.ScrollBars = ScrollBars.Vertical;
            textBoxOutput.Size = new Size(694, 476);
            textBoxOutput.TabIndex = 1;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(textBoxOutput);
            groupBox2.Location = new Point(156, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(700, 498);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Output";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(868, 522);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Index-Sequential file";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button buttonBuild;
        private Button buttonKeys;
        private Button buttonFind;
        private TextBox textBoxOutput;
        private GroupBox groupBox2;
        private Button buttonClear;
    }
}