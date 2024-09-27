namespace FNSC{
    partial class frmChampionship
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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            colOut = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            browserLeft = new CefSharp.WinForms.ChromiumWebBrowser();
            btnPlayRight = new System.Windows.Forms.Button();
            btnDevtools = new System.Windows.Forms.Button();
            btnReload = new System.Windows.Forms.Button();
            btnPlayLeft = new System.Windows.Forms.Button();
            browserRight = new CefSharp.WinForms.ChromiumWebBrowser();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            btnPreview = new System.Windows.Forms.Button();
            numTotalPreviewTime = new System.Windows.Forms.NumericUpDown();
            btnCancelPreview = new System.Windows.Forms.Button();
            btnRecordPlayerLocations = new System.Windows.Forms.Button();
            btnCancelVote = new System.Windows.Forms.Button();
            numTotalVotingTime = new System.Windows.Forms.NumericUpDown();
            btnVoting = new System.Windows.Forms.Button();
            btnVoteLeft = new System.Windows.Forms.Button();
            btnVoteRight = new System.Windows.Forms.Button();
            btnRightWins = new System.Windows.Forms.Button();
            btnLeftWins = new System.Windows.Forms.Button();
            btnNextRound = new System.Windows.Forms.Button();
            gridControl1 = new DevExpress.XtraGrid.GridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            colId = new DevExpress.XtraGrid.Columns.GridColumn();
            colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            colChannel = new DevExpress.XtraGrid.Columns.GridColumn();
            colLength = new DevExpress.XtraGrid.Columns.GridColumn();
            colIsBlocked = new DevExpress.XtraGrid.Columns.GridColumn();
            colUrl = new DevExpress.XtraGrid.Columns.GridColumn();
            colViewer = new DevExpress.XtraGrid.Columns.GridColumn();
            colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            colStarttime = new DevExpress.XtraGrid.Columns.GridColumn();
            txtLogOutput = new System.Windows.Forms.TextBox();
            colNo = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numTotalPreviewTime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTotalVotingTime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            SuspendLayout();
            // 
            // colOut
            // 
            colOut.Caption = "Out";
            colOut.FieldName = "IsOut";
            colOut.Name = "colOut";
            colOut.Visible = true;
            colOut.VisibleIndex = 8;
            // 
            // repositoryItemCheckEdit1
            // 
            repositoryItemCheckEdit1.AutoHeight = false;
            repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // browserLeft
            // 
            browserLeft.ActivateBrowserOnCreation = false;
            browserLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            browserLeft.Location = new System.Drawing.Point(0, 0);
            browserLeft.Name = "browserLeft";
            browserLeft.Size = new System.Drawing.Size(750, 546);
            browserLeft.TabIndex = 0;
            // 
            // btnPlayRight
            // 
            btnPlayRight.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            btnPlayRight.Location = new System.Drawing.Point(792, 741);
            btnPlayRight.Name = "btnPlayRight";
            btnPlayRight.Size = new System.Drawing.Size(75, 23);
            btnPlayRight.TabIndex = 1;
            btnPlayRight.Text = "Play right";
            btnPlayRight.UseVisualStyleBackColor = true;
            btnPlayRight.Click += btnPlayRight_Click;
            // 
            // btnDevtools
            // 
            btnDevtools.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            btnDevtools.Location = new System.Drawing.Point(711, 605);
            btnDevtools.Name = "btnDevtools";
            btnDevtools.Size = new System.Drawing.Size(75, 23);
            btnDevtools.TabIndex = 2;
            btnDevtools.Text = "devtools";
            btnDevtools.UseVisualStyleBackColor = true;
            btnDevtools.Click += btnDevtools_Click;
            // 
            // btnReload
            // 
            btnReload.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            btnReload.Location = new System.Drawing.Point(711, 576);
            btnReload.Name = "btnReload";
            btnReload.Size = new System.Drawing.Size(75, 23);
            btnReload.TabIndex = 3;
            btnReload.Text = "reload";
            btnReload.UseVisualStyleBackColor = true;
            btnReload.Click += btnReload_Click;
            // 
            // btnPlayLeft
            // 
            btnPlayLeft.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            btnPlayLeft.Location = new System.Drawing.Point(630, 741);
            btnPlayLeft.Name = "btnPlayLeft";
            btnPlayLeft.Size = new System.Drawing.Size(75, 23);
            btnPlayLeft.TabIndex = 0;
            btnPlayLeft.Text = "Play left";
            btnPlayLeft.UseVisualStyleBackColor = true;
            btnPlayLeft.Click += btnPlayLeft_Click;
            // 
            // browserRight
            // 
            browserRight.ActivateBrowserOnCreation = false;
            browserRight.Dock = System.Windows.Forms.DockStyle.Fill;
            browserRight.Location = new System.Drawing.Point(0, 0);
            browserRight.Name = "browserRight";
            browserRight.Size = new System.Drawing.Size(761, 546);
            browserRight.TabIndex = 4;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(browserLeft);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(browserRight);
            splitContainer1.Size = new System.Drawing.Size(1515, 546);
            splitContainer1.SplitterDistance = 750;
            splitContainer1.TabIndex = 5;
            // 
            // btnPreview
            // 
            btnPreview.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            btnPreview.Location = new System.Drawing.Point(711, 634);
            btnPreview.Name = "btnPreview";
            btnPreview.Size = new System.Drawing.Size(75, 23);
            btnPreview.TabIndex = 6;
            btnPreview.Text = "Preview";
            btnPreview.UseVisualStyleBackColor = true;
            btnPreview.Click += btnPreview_Click;
            // 
            // numTotalPreviewTime
            // 
            numTotalPreviewTime.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            numTotalPreviewTime.Location = new System.Drawing.Point(654, 634);
            numTotalPreviewTime.Maximum = new decimal(new int[] { 120, 0, 0, 0 });
            numTotalPreviewTime.Name = "numTotalPreviewTime";
            numTotalPreviewTime.Size = new System.Drawing.Size(51, 23);
            numTotalPreviewTime.TabIndex = 8;
            numTotalPreviewTime.Value = new decimal(new int[] { 120, 0, 0, 0 });
            // 
            // btnCancelPreview
            // 
            btnCancelPreview.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            btnCancelPreview.Enabled = false;
            btnCancelPreview.Location = new System.Drawing.Point(792, 634);
            btnCancelPreview.Name = "btnCancelPreview";
            btnCancelPreview.Size = new System.Drawing.Size(75, 23);
            btnCancelPreview.TabIndex = 9;
            btnCancelPreview.Text = "Cancel";
            btnCancelPreview.UseVisualStyleBackColor = true;
            btnCancelPreview.Click += btnCancelPreview_Click;
            // 
            // btnRecordPlayerLocations
            // 
            btnRecordPlayerLocations.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            btnRecordPlayerLocations.Location = new System.Drawing.Point(711, 760);
            btnRecordPlayerLocations.Name = "btnRecordPlayerLocations";
            btnRecordPlayerLocations.Size = new System.Drawing.Size(75, 62);
            btnRecordPlayerLocations.TabIndex = 10;
            btnRecordPlayerLocations.Text = "Record Players";
            btnRecordPlayerLocations.UseVisualStyleBackColor = true;
            btnRecordPlayerLocations.Visible = false;
            btnRecordPlayerLocations.Click += btnRecordPlayerLocations_Click;
            // 
            // btnCancelVote
            // 
            btnCancelVote.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            btnCancelVote.Enabled = false;
            btnCancelVote.Location = new System.Drawing.Point(792, 663);
            btnCancelVote.Name = "btnCancelVote";
            btnCancelVote.Size = new System.Drawing.Size(75, 23);
            btnCancelVote.TabIndex = 13;
            btnCancelVote.Text = "Cancel";
            btnCancelVote.UseVisualStyleBackColor = true;
            btnCancelVote.Click += btnCancelVote_Click;
            // 
            // numTotalVotingTime
            // 
            numTotalVotingTime.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            numTotalVotingTime.Location = new System.Drawing.Point(654, 663);
            numTotalVotingTime.Name = "numTotalVotingTime";
            numTotalVotingTime.Size = new System.Drawing.Size(51, 23);
            numTotalVotingTime.TabIndex = 12;
            numTotalVotingTime.Value = new decimal(new int[] { 60, 0, 0, 0 });
            // 
            // btnVoting
            // 
            btnVoting.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            btnVoting.Location = new System.Drawing.Point(711, 663);
            btnVoting.Name = "btnVoting";
            btnVoting.Size = new System.Drawing.Size(75, 23);
            btnVoting.TabIndex = 11;
            btnVoting.Text = "Vote";
            btnVoting.UseVisualStyleBackColor = true;
            btnVoting.Click += btnVoting_Click;
            // 
            // btnVoteLeft
            // 
            btnVoteLeft.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            btnVoteLeft.Location = new System.Drawing.Point(630, 770);
            btnVoteLeft.Name = "btnVoteLeft";
            btnVoteLeft.Size = new System.Drawing.Size(75, 23);
            btnVoteLeft.TabIndex = 14;
            btnVoteLeft.Text = "Vote";
            btnVoteLeft.UseVisualStyleBackColor = true;
            btnVoteLeft.Click += btnVoteLeft_Click;
            // 
            // btnVoteRight
            // 
            btnVoteRight.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            btnVoteRight.Location = new System.Drawing.Point(792, 770);
            btnVoteRight.Name = "btnVoteRight";
            btnVoteRight.Size = new System.Drawing.Size(75, 23);
            btnVoteRight.TabIndex = 15;
            btnVoteRight.Text = "Vote";
            btnVoteRight.UseVisualStyleBackColor = true;
            btnVoteRight.Click += btnVoteRight_Click;
            // 
            // btnRightWins
            // 
            btnRightWins.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            btnRightWins.Location = new System.Drawing.Point(792, 799);
            btnRightWins.Name = "btnRightWins";
            btnRightWins.Size = new System.Drawing.Size(75, 23);
            btnRightWins.TabIndex = 16;
            btnRightWins.Text = "Winner";
            btnRightWins.UseVisualStyleBackColor = true;
            btnRightWins.Click += btnRightWins_Click;
            // 
            // btnLeftWins
            // 
            btnLeftWins.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            btnLeftWins.Location = new System.Drawing.Point(630, 799);
            btnLeftWins.Name = "btnLeftWins";
            btnLeftWins.Size = new System.Drawing.Size(75, 23);
            btnLeftWins.TabIndex = 17;
            btnLeftWins.Text = "Winner";
            btnLeftWins.UseVisualStyleBackColor = true;
            btnLeftWins.Click += btnLeftWins_Click;
            // 
            // btnNextRound
            // 
            btnNextRound.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            btnNextRound.Location = new System.Drawing.Point(711, 692);
            btnNextRound.Name = "btnNextRound";
            btnNextRound.Size = new System.Drawing.Size(75, 23);
            btnNextRound.TabIndex = 18;
            btnNextRound.Text = "Next round";
            btnNextRound.UseVisualStyleBackColor = true;
            btnNextRound.Click += btnNextRound_Click;
            // 
            // gridControl1
            // 
            gridControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            gridControl1.Location = new System.Drawing.Point(1171, 563);
            gridControl1.MainView = gridView1;
            gridControl1.Name = "gridControl1";
            gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemCheckEdit1 });
            gridControl1.Size = new System.Drawing.Size(344, 311);
            gridControl1.TabIndex = 3;
            gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.Appearance.FocusedRow.Options.UseBackColor = true;
            gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colId, colNo, colDescription, colChannel, colLength, colIsBlocked, colUrl, colViewer, colCode, colStarttime, colOut });
            gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            gridFormatRule1.ApplyToRow = true;
            gridFormatRule1.Column = colOut;
            gridFormatRule1.Name = "Format0";
            formatConditionRuleValue1.Appearance.BackColor = System.Drawing.Color.Red;
            formatConditionRuleValue1.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue1.Expression = "true";
            gridFormatRule1.Rule = formatConditionRuleValue1;
            gridView1.FormatRules.Add(gridFormatRule1);
            gridView1.GridControl = gridControl1;
            gridView1.Name = "gridView1";
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsCustomization.AllowColumnMoving = false;
            gridView1.OptionsCustomization.AllowFilter = false;
            gridView1.OptionsCustomization.AllowGroup = false;
            gridView1.OptionsMenu.EnableColumnMenu = false;
            gridView1.OptionsNavigation.AutoMoveRowFocus = false;
            gridView1.OptionsSelection.UseIndicatorForSelection = false;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.CustomDrawCell += gridView1_CustomDrawCell;
            gridView1.RowStyle += gridView1_RowStyle;
            gridView1.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;
            gridView1.DoubleClick += gridView1_DoubleClick;
            // 
            // colId
            // 
            colId.FieldName = "Id";
            colId.Name = "colId";
            // 
            // colDescription
            // 
            colDescription.FieldName = "Description";
            colDescription.Name = "colDescription";
            colDescription.Visible = true;
            colDescription.VisibleIndex = 1;
            // 
            // colChannel
            // 
            colChannel.FieldName = "Channel";
            colChannel.Name = "colChannel";
            colChannel.Visible = true;
            colChannel.VisibleIndex = 2;
            // 
            // colLength
            // 
            colLength.FieldName = "Length";
            colLength.Name = "colLength";
            colLength.Visible = true;
            colLength.VisibleIndex = 3;
            // 
            // colIsBlocked
            // 
            colIsBlocked.FieldName = "IsBlocked";
            colIsBlocked.Name = "colIsBlocked";
            colIsBlocked.Visible = true;
            colIsBlocked.VisibleIndex = 4;
            // 
            // colUrl
            // 
            colUrl.FieldName = "Url";
            colUrl.Name = "colUrl";
            colUrl.Visible = true;
            colUrl.VisibleIndex = 5;
            // 
            // colViewer
            // 
            colViewer.FieldName = "Viewer";
            colViewer.Name = "colViewer";
            colViewer.Visible = true;
            colViewer.VisibleIndex = 6;
            // 
            // colCode
            // 
            colCode.FieldName = "Code";
            colCode.Name = "colCode";
            colCode.OptionsColumn.ReadOnly = true;
            // 
            // colStarttime
            // 
            colStarttime.FieldName = "InitialStarttime";
            colStarttime.Name = "colStarttime";
            colStarttime.Visible = true;
            colStarttime.VisibleIndex = 7;
            // 
            // txtLogOutput
            // 
            txtLogOutput.Location = new System.Drawing.Point(0, 594);
            txtLogOutput.Multiline = true;
            txtLogOutput.Name = "txtLogOutput";
            txtLogOutput.Size = new System.Drawing.Size(338, 280);
            txtLogOutput.TabIndex = 19;
            // 
            // colNo
            // 
            colNo.Caption = "No";
            colNo.FieldName = "gridColumn1";
            colNo.Name = "colNo";
            colNo.UnboundDataType = typeof(int);
            colNo.Visible = true;
            colNo.VisibleIndex = 0;
            // 
            // frmChampionship
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1515, 874);
            Controls.Add(txtLogOutput);
            Controls.Add(gridControl1);
            Controls.Add(btnNextRound);
            Controls.Add(btnLeftWins);
            Controls.Add(btnRightWins);
            Controls.Add(btnVoteRight);
            Controls.Add(btnVoteLeft);
            Controls.Add(btnCancelVote);
            Controls.Add(numTotalVotingTime);
            Controls.Add(btnVoting);
            Controls.Add(btnRecordPlayerLocations);
            Controls.Add(btnCancelPreview);
            Controls.Add(numTotalPreviewTime);
            Controls.Add(btnPreview);
            Controls.Add(splitContainer1);
            Controls.Add(btnPlayLeft);
            Controls.Add(btnReload);
            Controls.Add(btnDevtools);
            Controls.Add(btnPlayRight);
            Name = "frmChampionship";
            Text = "Form2";
            FormClosing += Form2_FormClosing;
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit1).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numTotalPreviewTime).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTotalVotingTime).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CefSharp.WinForms.ChromiumWebBrowser browserLeft;
        private System.Windows.Forms.Button btnPlayRight;
        private System.Windows.Forms.Button btnDevtools;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnPlayLeft;
        private CefSharp.WinForms.ChromiumWebBrowser browserRight;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.NumericUpDown numTotalPreviewTime;
        private System.Windows.Forms.Button btnCancelPreview;
        private System.Windows.Forms.Button btnRecordPlayerLocations;
        private System.Windows.Forms.Button btnCancelVote;
        private System.Windows.Forms.NumericUpDown numTotalVotingTime;
        private System.Windows.Forms.Button btnVoting;
        private System.Windows.Forms.Button btnVoteLeft;
        private System.Windows.Forms.Button btnVoteRight;
        private System.Windows.Forms.Button btnRightWins;
        private System.Windows.Forms.Button btnLeftWins;
        private System.Windows.Forms.Button btnNextRound;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colChannel;
        private DevExpress.XtraGrid.Columns.GridColumn colLength;
        private DevExpress.XtraGrid.Columns.GridColumn colIsBlocked;
        private DevExpress.XtraGrid.Columns.GridColumn colUrl;
        private DevExpress.XtraGrid.Columns.GridColumn colViewer;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colStarttime;
        private DevExpress.XtraGrid.Columns.GridColumn colOut;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private System.Windows.Forms.TextBox txtLogOutput;
        private DevExpress.XtraGrid.Columns.GridColumn colNo;
    }
}