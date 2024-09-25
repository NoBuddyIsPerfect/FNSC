namespace FNSC{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            btnStartChampionship = new System.Windows.Forms.Button();
            gridControl1 = new DevExpress.XtraGrid.GridControl();
            songBindingSource = new System.Windows.Forms.BindingSource(components);
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            colId = new DevExpress.XtraGrid.Columns.GridColumn();
            colViewer = new DevExpress.XtraGrid.Columns.GridColumn();
            colChannel = new DevExpress.XtraGrid.Columns.GridColumn();
            colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            colLength = new DevExpress.XtraGrid.Columns.GridColumn();
            colUrl = new DevExpress.XtraGrid.Columns.GridColumn();
            colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            colStarttime = new DevExpress.XtraGrid.Columns.GridColumn();
            colIsBlocked = new DevExpress.XtraGrid.Columns.GridColumn();
            btnOpenSubmissions = new System.Windows.Forms.Button();
            btnCloseSubmissions = new System.Windows.Forms.Button();
            txtSubmissionUrl = new System.Windows.Forms.TextBox();
            comboSubmissionViewer = new System.Windows.Forms.ComboBox();
            btnSubmit = new System.Windows.Forms.Button();
            btnSaveGame = new System.Windows.Forms.Button();
            btnDummyData = new System.Windows.Forms.Button();
            btnResetChampionship = new System.Windows.Forms.Button();
            numNoOfSongs = new System.Windows.Forms.NumericUpDown();
            btnLoadGame = new System.Windows.Forms.Button();
            cboGames = new DevExpress.XtraEditors.GridLookUpEdit();
            gameBindingSource = new System.Windows.Forms.BindingSource(components);
            gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            colComboBoxText = new DevExpress.XtraGrid.Columns.GridColumn();
            colNoOfSongs = new DevExpress.XtraGrid.Columns.GridColumn();
            colNoOfSongsPerPerson = new DevExpress.XtraGrid.Columns.GridColumn();
            colSubmissionsOpen = new DevExpress.XtraGrid.Columns.GridColumn();
            colTheme = new DevExpress.XtraGrid.Columns.GridColumn();
            colGameFinished = new DevExpress.XtraGrid.Columns.GridColumn();
            colInitTimestamp = new DevExpress.XtraGrid.Columns.GridColumn();
            txtTheme = new System.Windows.Forms.TextBox();
            numSongsPerPerson = new System.Windows.Forms.NumericUpDown();
            numPreviewTime = new System.Windows.Forms.NumericUpDown();
            numVotingTime = new System.Windows.Forms.NumericUpDown();
            numChampionshipNumber = new System.Windows.Forms.NumericUpDown();
            btnInitChampionship = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            chkAllowDoubles = new System.Windows.Forms.CheckBox();
            chkAllowVoteCorrection = new System.Windows.Forms.CheckBox();
            label7 = new System.Windows.Forms.Label();
            txtMaxLength = new System.Windows.Forms.TextBox();
            txtMinLength = new System.Windows.Forms.TextBox();
            label8 = new System.Windows.Forms.Label();
            btnModifyChampionship = new System.Windows.Forms.Button();
            chkWhispers = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)gridControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)songBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numNoOfSongs).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cboGames.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gameBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridLookUpEdit1View).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSongsPerPerson).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPreviewTime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numVotingTime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numChampionshipNumber).BeginInit();
            SuspendLayout();
            // 
            // btnStartChampionship
            // 
            btnStartChampionship.Enabled = false;
            btnStartChampionship.Location = new System.Drawing.Point(453, 299);
            btnStartChampionship.Name = "btnStartChampionship";
            btnStartChampionship.Size = new System.Drawing.Size(214, 40);
            btnStartChampionship.TabIndex = 0;
            btnStartChampionship.Text = "Start Championship";
            btnStartChampionship.UseVisualStyleBackColor = true;
            btnStartChampionship.Click += btnStartChampionship_Click;
            // 
            // gridControl1
            // 
            gridControl1.DataSource = songBindingSource;
            gridControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            gridControl1.Location = new System.Drawing.Point(0, 345);
            gridControl1.MainView = gridView1;
            gridControl1.Name = "gridControl1";
            gridControl1.Size = new System.Drawing.Size(1208, 368);
            gridControl1.TabIndex = 2;
            gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // songBindingSource
            // 
            songBindingSource.DataSource = typeof(Data.Song);
            // 
            // gridView1
            // 
            gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colId, colViewer, colChannel, colDescription, colLength, colUrl, colCode, colStarttime, colIsBlocked });
            gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            gridView1.GridControl = gridControl1;
            gridView1.Name = "gridView1";
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsCustomization.AllowColumnMoving = false;
            gridView1.OptionsCustomization.AllowColumnResizing = false;
            gridView1.OptionsCustomization.AllowFilter = false;
            gridView1.OptionsCustomization.AllowGroup = false;
            gridView1.OptionsCustomization.AllowSort = false;
            gridView1.OptionsMenu.EnableColumnMenu = false;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.PopupMenuShowing += gridView1_PopupMenuShowing;
            gridView1.KeyDown += gridView1_KeyDown;
            gridView1.DoubleClick += gridView1_DoubleClick;
            // 
            // colId
            // 
            colId.FieldName = "Id";
            colId.Name = "colId";
            // 
            // colViewer
            // 
            colViewer.FieldName = "Viewer";
            colViewer.Name = "colViewer";
            colViewer.Visible = true;
            colViewer.VisibleIndex = 0;
            // 
            // colChannel
            // 
            colChannel.FieldName = "Channel";
            colChannel.Name = "colChannel";
            colChannel.Visible = true;
            colChannel.VisibleIndex = 1;
            // 
            // colDescription
            // 
            colDescription.FieldName = "Description";
            colDescription.Name = "colDescription";
            colDescription.Visible = true;
            colDescription.VisibleIndex = 2;
            // 
            // colLength
            // 
            colLength.FieldName = "Length";
            colLength.Name = "colLength";
            colLength.Visible = true;
            colLength.VisibleIndex = 3;
            // 
            // colUrl
            // 
            colUrl.FieldName = "Url";
            colUrl.Name = "colUrl";
            colUrl.Visible = true;
            colUrl.VisibleIndex = 4;
            // 
            // colCode
            // 
            colCode.FieldName = "Code";
            colCode.Name = "colCode";
            colCode.OptionsColumn.ReadOnly = true;
            colCode.Visible = true;
            colCode.VisibleIndex = 5;
            // 
            // colStarttime
            // 
            colStarttime.FieldName = "InitialStarttime";
            colStarttime.Name = "colStarttime";
            colStarttime.Visible = true;
            colStarttime.VisibleIndex = 6;
            // 
            // colIsBlocked
            // 
            colIsBlocked.FieldName = "IsBlocked";
            colIsBlocked.Name = "colIsBlocked";
            colIsBlocked.Visible = true;
            colIsBlocked.VisibleIndex = 7;
            // 
            // btnOpenSubmissions
            // 
            btnOpenSubmissions.Location = new System.Drawing.Point(125, 12);
            btnOpenSubmissions.Name = "btnOpenSubmissions";
            btnOpenSubmissions.Size = new System.Drawing.Size(110, 23);
            btnOpenSubmissions.TabIndex = 3;
            btnOpenSubmissions.Text = "Open Submissions";
            btnOpenSubmissions.UseVisualStyleBackColor = true;
            btnOpenSubmissions.Click += btnOpenSubmissions_Click;
            // 
            // btnCloseSubmissions
            // 
            btnCloseSubmissions.Enabled = false;
            btnCloseSubmissions.Location = new System.Drawing.Point(125, 41);
            btnCloseSubmissions.Name = "btnCloseSubmissions";
            btnCloseSubmissions.Size = new System.Drawing.Size(110, 23);
            btnCloseSubmissions.TabIndex = 4;
            btnCloseSubmissions.Text = "Close Submissions";
            btnCloseSubmissions.UseVisualStyleBackColor = true;
            btnCloseSubmissions.Click += btnCloseSubmissions_Click;
            // 
            // txtSubmissionUrl
            // 
            txtSubmissionUrl.Location = new System.Drawing.Point(285, 40);
            txtSubmissionUrl.Name = "txtSubmissionUrl";
            txtSubmissionUrl.Size = new System.Drawing.Size(121, 21);
            txtSubmissionUrl.TabIndex = 5;
            // 
            // comboSubmissionViewer
            // 
            comboSubmissionViewer.FormattingEnabled = true;
            comboSubmissionViewer.Location = new System.Drawing.Point(285, 12);
            comboSubmissionViewer.Name = "comboSubmissionViewer";
            comboSubmissionViewer.Size = new System.Drawing.Size(121, 21);
            comboSubmissionViewer.TabIndex = 6;
            // 
            // btnSubmit
            // 
            btnSubmit.Location = new System.Drawing.Point(285, 67);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new System.Drawing.Size(121, 23);
            btnSubmit.TabIndex = 7;
            btnSubmit.Text = "Submit";
            btnSubmit.UseVisualStyleBackColor = true;
            btnSubmit.Click += btnSubmit_Click;
            // 
            // btnSaveGame
            // 
            btnSaveGame.Location = new System.Drawing.Point(115, 98);
            btnSaveGame.Name = "btnSaveGame";
            btnSaveGame.Size = new System.Drawing.Size(134, 23);
            btnSaveGame.TabIndex = 8;
            btnSaveGame.Text = "Save Game";
            btnSaveGame.UseVisualStyleBackColor = true;
            btnSaveGame.Click += btnSaveGame_Click;
            // 
            // btnDummyData
            // 
            btnDummyData.Location = new System.Drawing.Point(125, 69);
            btnDummyData.Name = "btnDummyData";
            btnDummyData.Size = new System.Drawing.Size(110, 23);
            btnDummyData.TabIndex = 9;
            btnDummyData.Text = "Fill with dummy";
            btnDummyData.UseVisualStyleBackColor = true;
            btnDummyData.Click += btnDummyData_Click;
            // 
            // btnResetChampionship
            // 
            btnResetChampionship.BackColor = System.Drawing.Color.Red;
            btnResetChampionship.Enabled = false;
            btnResetChampionship.Location = new System.Drawing.Point(673, 253);
            btnResetChampionship.Name = "btnResetChampionship";
            btnResetChampionship.Size = new System.Drawing.Size(82, 86);
            btnResetChampionship.TabIndex = 10;
            btnResetChampionship.Text = "Reset Championship";
            btnResetChampionship.UseVisualStyleBackColor = false;
            btnResetChampionship.Click += BtnResetChampionshipClick;
            // 
            // numNoOfSongs
            // 
            numNoOfSongs.Location = new System.Drawing.Point(547, 145);
            numNoOfSongs.Name = "numNoOfSongs";
            numNoOfSongs.Size = new System.Drawing.Size(120, 21);
            numNoOfSongs.TabIndex = 11;
            numNoOfSongs.Value = new decimal(new int[] { 16, 0, 0, 0 });
            // 
            // btnLoadGame
            // 
            btnLoadGame.Enabled = false;
            btnLoadGame.Location = new System.Drawing.Point(115, 155);
            btnLoadGame.Name = "btnLoadGame";
            btnLoadGame.Size = new System.Drawing.Size(134, 23);
            btnLoadGame.TabIndex = 13;
            btnLoadGame.Text = "Load Game";
            btnLoadGame.UseVisualStyleBackColor = true;
            btnLoadGame.Click += btnLoadGame_Click;
            // 
            // cboGames
            // 
            cboGames.Location = new System.Drawing.Point(115, 131);
            cboGames.Name = "cboGames";
            cboGames.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cboGames.Properties.DataSource = gameBindingSource;
            cboGames.Properties.DisplayMember = "ComboBoxText";
            cboGames.Properties.PopupView = gridLookUpEdit1View;
            cboGames.Properties.ValueMember = "Id";
            cboGames.Size = new System.Drawing.Size(134, 20);
            cboGames.TabIndex = 15;
            // 
            // gameBindingSource
            // 
            gameBindingSource.DataSource = typeof(Data.Game);
            // 
            // gridLookUpEdit1View
            // 
            gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colComboBoxText, colNoOfSongs, colNoOfSongsPerPerson, colSubmissionsOpen, colTheme, colGameFinished, colInitTimestamp });
            gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // colComboBoxText
            // 
            colComboBoxText.FieldName = "ComboBoxText";
            colComboBoxText.Name = "colComboBoxText";
            colComboBoxText.Visible = true;
            colComboBoxText.VisibleIndex = 0;
            // 
            // colNoOfSongs
            // 
            colNoOfSongs.FieldName = "NoOfSongs";
            colNoOfSongs.Name = "colNoOfSongs";
            colNoOfSongs.Visible = true;
            colNoOfSongs.VisibleIndex = 1;
            // 
            // colNoOfSongsPerPerson
            // 
            colNoOfSongsPerPerson.FieldName = "NoOfSongsPerPerson";
            colNoOfSongsPerPerson.Name = "colNoOfSongsPerPerson";
            colNoOfSongsPerPerson.Visible = true;
            colNoOfSongsPerPerson.VisibleIndex = 2;
            // 
            // colSubmissionsOpen
            // 
            colSubmissionsOpen.FieldName = "SubmissionsOpen";
            colSubmissionsOpen.Name = "colSubmissionsOpen";
            colSubmissionsOpen.Visible = true;
            colSubmissionsOpen.VisibleIndex = 3;
            // 
            // colTheme
            // 
            colTheme.FieldName = "Theme";
            colTheme.Name = "colTheme";
            colTheme.Visible = true;
            colTheme.VisibleIndex = 4;
            // 
            // colGameFinished
            // 
            colGameFinished.FieldName = "GameFinished";
            colGameFinished.Name = "colGameFinished";
            colGameFinished.Visible = true;
            colGameFinished.VisibleIndex = 5;
            // 
            // colInitTimestamp
            // 
            colInitTimestamp.FieldName = "InitTimestamp";
            colInitTimestamp.Name = "colInitTimestamp";
            colInitTimestamp.Visible = true;
            colInitTimestamp.VisibleIndex = 6;
            // 
            // txtTheme
            // 
            txtTheme.Location = new System.Drawing.Point(546, 14);
            txtTheme.Name = "txtTheme";
            txtTheme.Size = new System.Drawing.Size(121, 21);
            txtTheme.TabIndex = 16;
            // 
            // numSongsPerPerson
            // 
            numSongsPerPerson.Location = new System.Drawing.Point(546, 172);
            numSongsPerPerson.Name = "numSongsPerPerson";
            numSongsPerPerson.Size = new System.Drawing.Size(121, 21);
            numSongsPerPerson.TabIndex = 17;
            numSongsPerPerson.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numPreviewTime
            // 
            numPreviewTime.Location = new System.Drawing.Point(547, 199);
            numPreviewTime.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numPreviewTime.Name = "numPreviewTime";
            numPreviewTime.Size = new System.Drawing.Size(120, 21);
            numPreviewTime.TabIndex = 18;
            numPreviewTime.Value = new decimal(new int[] { 120, 0, 0, 0 });
            // 
            // numVotingTime
            // 
            numVotingTime.Location = new System.Drawing.Point(547, 226);
            numVotingTime.Name = "numVotingTime";
            numVotingTime.Size = new System.Drawing.Size(120, 21);
            numVotingTime.TabIndex = 19;
            numVotingTime.Value = new decimal(new int[] { 60, 0, 0, 0 });
            // 
            // numChampionshipNumber
            // 
            numChampionshipNumber.Location = new System.Drawing.Point(547, 41);
            numChampionshipNumber.Name = "numChampionshipNumber";
            numChampionshipNumber.Size = new System.Drawing.Size(120, 21);
            numChampionshipNumber.TabIndex = 20;
            numChampionshipNumber.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnInitChampionship
            // 
            btnInitChampionship.Location = new System.Drawing.Point(453, 253);
            btnInitChampionship.Name = "btnInitChampionship";
            btnInitChampionship.Size = new System.Drawing.Size(105, 40);
            btnInitChampionship.TabIndex = 21;
            btnInitChampionship.Text = "Prepare Championship";
            btnInitChampionship.UseVisualStyleBackColor = true;
            btnInitChampionship.Click += btnInitChampionship_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(453, 17);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(43, 13);
            label1.TabIndex = 22;
            label1.Text = "Theme:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(453, 45);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(93, 13);
            label2.TabIndex = 23;
            label2.Text = "Championship No:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(453, 147);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(68, 13);
            label3.TabIndex = 24;
            label3.Text = "No of songs:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(453, 174);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(77, 13);
            label4.TabIndex = 25;
            label4.Text = "Songs/person:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(453, 201);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(73, 13);
            label5.TabIndex = 26;
            label5.Text = "Preview secs:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(453, 228);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(65, 13);
            label6.TabIndex = 27;
            label6.Text = "Voting secs:";
            // 
            // chkAllowDoubles
            // 
            chkAllowDoubles.AutoSize = true;
            chkAllowDoubles.Location = new System.Drawing.Point(458, 67);
            chkAllowDoubles.Name = "chkAllowDoubles";
            chkAllowDoubles.Size = new System.Drawing.Size(64, 17);
            chkAllowDoubles.TabIndex = 32;
            chkAllowDoubles.Text = "Doubles";
            chkAllowDoubles.UseVisualStyleBackColor = true;
            // 
            // chkAllowVoteCorrection
            // 
            chkAllowVoteCorrection.AutoSize = true;
            chkAllowVoteCorrection.Location = new System.Drawing.Point(528, 67);
            chkAllowVoteCorrection.Name = "chkAllowVoteCorrection";
            chkAllowVoteCorrection.Size = new System.Drawing.Size(94, 17);
            chkAllowVoteCorrection.TabIndex = 33;
            chkAllowVoteCorrection.Text = "Vote changing";
            chkAllowVoteCorrection.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(453, 120);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(64, 13);
            label7.TabIndex = 35;
            label7.Text = "Max length:";
            // 
            // txtMaxLength
            // 
            txtMaxLength.Location = new System.Drawing.Point(547, 117);
            txtMaxLength.Name = "txtMaxLength";
            txtMaxLength.Size = new System.Drawing.Size(120, 21);
            txtMaxLength.TabIndex = 37;
            txtMaxLength.Text = "0";
            // 
            // txtMinLength
            // 
            txtMinLength.Location = new System.Drawing.Point(547, 90);
            txtMinLength.Name = "txtMinLength";
            txtMinLength.Size = new System.Drawing.Size(120, 21);
            txtMinLength.TabIndex = 40;
            txtMinLength.Text = "2:30";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(453, 93);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(60, 13);
            label8.TabIndex = 38;
            label8.Text = "Min length:";
            // 
            // btnModifyChampionship
            // 
            btnModifyChampionship.Enabled = false;
            btnModifyChampionship.Location = new System.Drawing.Point(562, 253);
            btnModifyChampionship.Name = "btnModifyChampionship";
            btnModifyChampionship.Size = new System.Drawing.Size(105, 40);
            btnModifyChampionship.TabIndex = 39;
            btnModifyChampionship.Text = "Modify Championship";
            btnModifyChampionship.UseVisualStyleBackColor = true;
            btnModifyChampionship.Click += btnModifyChampionship_Click;
            // 
            // chkWhispers
            // 
            chkWhispers.AutoSize = true;
            chkWhispers.Location = new System.Drawing.Point(628, 67);
            chkWhispers.Name = "chkWhispers";
            chkWhispers.Size = new System.Drawing.Size(95, 17);
            chkWhispers.TabIndex = 41;
            chkWhispers.Text = "Send whispers";
            chkWhispers.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1208, 713);
            Controls.Add(chkWhispers);
            Controls.Add(txtMinLength);
            Controls.Add(btnModifyChampionship);
            Controls.Add(label8);
            Controls.Add(txtMaxLength);
            Controls.Add(label7);
            Controls.Add(chkAllowVoteCorrection);
            Controls.Add(chkAllowDoubles);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(btnInitChampionship);
            Controls.Add(numChampionshipNumber);
            Controls.Add(numVotingTime);
            Controls.Add(numPreviewTime);
            Controls.Add(numSongsPerPerson);
            Controls.Add(txtTheme);
            Controls.Add(cboGames);
            Controls.Add(btnLoadGame);
            Controls.Add(numNoOfSongs);
            Controls.Add(btnResetChampionship);
            Controls.Add(btnDummyData);
            Controls.Add(btnSaveGame);
            Controls.Add(btnSubmit);
            Controls.Add(comboSubmissionViewer);
            Controls.Add(txtSubmissionUrl);
            Controls.Add(btnCloseSubmissions);
            Controls.Add(btnOpenSubmissions);
            Controls.Add(gridControl1);
            Controls.Add(btnStartChampionship);
            Controls.Add(label2);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            MouseClick += Form1_MouseClick;
            ((System.ComponentModel.ISupportInitialize)gridControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)songBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numNoOfSongs).EndInit();
            ((System.ComponentModel.ISupportInitialize)cboGames.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)gameBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridLookUpEdit1View).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSongsPerPerson).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPreviewTime).EndInit();
            ((System.ComponentModel.ISupportInitialize)numVotingTime).EndInit();
            ((System.ComponentModel.ISupportInitialize)numChampionshipNumber).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnStartChampionship;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource songBindingSource;
        private System.Windows.Forms.Button btnOpenSubmissions;
        private System.Windows.Forms.Button btnCloseSubmissions;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colChannel;
        private DevExpress.XtraGrid.Columns.GridColumn colLength;
        private DevExpress.XtraGrid.Columns.GridColumn colIsBlocked;
        private DevExpress.XtraGrid.Columns.GridColumn colUrl;
        private DevExpress.XtraGrid.Columns.GridColumn colViewer;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colStarttime;
        private System.Windows.Forms.TextBox txtSubmissionUrl;
        private System.Windows.Forms.ComboBox comboSubmissionViewer;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnSaveGame;
        private System.Windows.Forms.Button btnDummyData;
        private System.Windows.Forms.Button btnResetChampionship;
        private System.Windows.Forms.NumericUpDown numNoOfSongs;
        private System.Windows.Forms.Button btnLoadGame;
        private DevExpress.XtraEditors.GridLookUpEdit cboGames;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private System.Windows.Forms.TextBox txtTheme;
        private System.Windows.Forms.NumericUpDown numSongsPerPerson;
        private System.Windows.Forms.NumericUpDown numPreviewTime;
        private System.Windows.Forms.NumericUpDown numVotingTime;
        private System.Windows.Forms.NumericUpDown numChampionshipNumber;
        private System.Windows.Forms.Button btnInitChampionship;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.BindingSource gameBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colComboBoxText;
        private DevExpress.XtraGrid.Columns.GridColumn colNoOfSongs;
        private DevExpress.XtraGrid.Columns.GridColumn colNoOfSongsPerPerson;
        private DevExpress.XtraGrid.Columns.GridColumn colSubmissionsOpen;
        private DevExpress.XtraGrid.Columns.GridColumn colTheme;
        private DevExpress.XtraGrid.Columns.GridColumn colGameFinished;
        private DevExpress.XtraGrid.Columns.GridColumn colInitTimestamp;
        private System.Windows.Forms.CheckBox chkAllowDoubles;
        private System.Windows.Forms.CheckBox chkAllowVoteCorrection;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMaxLength;
        private System.Windows.Forms.TextBox txtMinLength;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnModifyChampionship;
        private System.Windows.Forms.CheckBox chkWhispers;
    }
}

