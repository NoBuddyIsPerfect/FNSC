using FNSC.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.CodeParser;

namespace FNSC.Classes
{
    public class SongQueueEventArgs
    {
        public Song Song;

        public SongQueueEventArgs(Song song)
        {
            Song = song;
        }
    }
    public class SubmissionQueue : BindingList<Song>
    {
        public event EventHandler<SongQueueEventArgs> SongEnqueued ;
        public event EventHandler<SongQueueEventArgs> SongDequeued ;
        private static Random rnd = new Random();

        public void Enqueue(Song song)
        {
            base.Add(song);
            OnSongQueued(new SongQueueEventArgs(song));
        }
        public bool TryDequeue(out Song song)
        {
            song = null;
            if (base.Count > 0)
                song = base[rnd.Next(base.Count-1)];
            if (song != null)
            {
                base.Remove(song);
                OnSongDequeued(new SongQueueEventArgs(song));
                return true;
            }
            else
            {
                
                return false;
            }
        }

        private void OnSongDequeued(SongQueueEventArgs args)
        {

            EventHandler<SongQueueEventArgs> handler = SongDequeued;

            if (handler != null)
            {
                handler(this, args);
            }
        }

        public void OnSongQueued(SongQueueEventArgs args)
        {
            EventHandler<SongQueueEventArgs> handler = SongEnqueued;

            if (handler != null)
            {
                handler(this, args);
            }
        }
    }
}
