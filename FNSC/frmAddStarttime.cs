using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FNSC.Data;
using FNSC.Externals;

namespace FNSC{
    public partial class frmAddStarttime : Form
    {
        public bool SongChanged = false;
        public Song Song;
        private TimeSpan minLength;
        private TimeSpan maxLength;
        public frmAddStarttime(Song song, TimeSpan minLength, TimeSpan maxLength)
        {
            InitializeComponent();
            numStarttime.Value = song.InitialStarttime;
            txtCode.Text = song.Code;
            this.Song = song;
            this.numStarttime.Maximum = song.Length.Seconds;
            StartPosition = FormStartPosition.CenterParent;
            this.minLength = minLength;
            this.maxLength = maxLength;
            
        }

        private void btnSaveStarttime_Click(object sender, EventArgs e)
        {
            string code = txtCode.Text;
            if (code.Contains("http"))
            {
                Uri uri = new Uri(code);
                if (code.Contains("w"))
                    code = uri.Query.Replace("?v=", "");
                else
                    code = uri.Segments[uri.Segments.Length - 1];

            }
            if (txtCode.Text != Song.Code)
            {
                Song newSong = YouTubeClient.GetYTVideoDetails(txtCode.Text.Contains("youtu") ? txtCode.Text : "https://www.youtube.com/watch?v=" + txtCode.Text, Properties.Settings.Default.YtApiKey);
                if (newSong.Description == "NO TITLE")
                {
                    MessageBox.Show("Song not found, try again");
                    return;
                }
                if (newSong.IsBlocked)
                {

                    MessageBox.Show("Sorry, that video is either age restricted or blocked in Germany! Please submit a different video!");
                    return;
                }
                if (newSong.Length.Subtract(new TimeSpan(0, 0, newSong.InitialStarttime)) < minLength)
                {
                    MessageBox.Show("that Song is too short.");
                    return;
                }
                if (maxLength.TotalMilliseconds > 0 && maxLength < newSong.Length)
                {

                    MessageBox.Show("Sorry, Your Song could not be added because it is too long!");
                    return;
                }

                Song.Code = newSong.Code;
                Song.Url = newSong.Url;
                Song.Channel = newSong.Channel;
                Song.Description = newSong.Description;
                Song.Length = newSong.Length;
                SongChanged = true;

            }
            else
            {
                Song.InitialStarttime = (int)numStarttime.Value;
            }

            this.Close();
        }

        private void numStarttime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Song.InitialStarttime = (int)numStarttime.Value;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
