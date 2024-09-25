using FNSC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNSC.Helpers
{
    internal class GameHelper
    {
        public static int CalculateNoOfRounds(int participants)
        {
            if (participants < 2)
            {
                throw new ArgumentException("Number of participants must be at least 2.");
            }

            int rounds = 0;
            while (participants > 1)
            {
                participants = (participants + 1) / 2;
                rounds++;
            }
            
            return rounds;
        }

        public static List<Battle> GroupSongsInRounds(List<Song> songs)
        {
            var battles = new List<Battle>();
            var remainingSongs = new List<Song>(songs);
            var songsByChannel = songs.GroupBy(s => s.Channel).ToDictionary(g => g.Key, g => new List<Song>(g));
            var songsByViewer = songs.GroupBy(s => s.Viewer).ToDictionary(g => g.Key.id, g => new List<Song>(g));
            int position = 1;

            while (remainingSongs.Count > 0)
            {
                var firstSong = remainingSongs[0];
                remainingSongs.RemoveAt(0);
                UpdateSongCollections(firstSong, songsByChannel, songsByViewer);

                var secondSong = FindBestMatch(firstSong, remainingSongs, songsByChannel, songsByViewer);

                if (secondSong != null)
                {
                    remainingSongs.Remove(secondSong);
                    UpdateSongCollections(secondSong, songsByChannel, songsByViewer);
                }
                else
                {
                    // If no match found, use any available Song or reuse a Song
                    secondSong = remainingSongs.Count > 0 ? remainingSongs[0] : songs[0];
                    if (remainingSongs.Count > 0) remainingSongs.RemoveAt(0);
                }

                battles.Add(new Battle { Song1 = firstSong, Song2 = secondSong, Position = position++ });

                // If we've used all songs but have an odd number, add the last one to a new battle
                if (remainingSongs.Count == 0 && battles.Count * 2 < songs.Count)
                {
                    var lastSong = songs.First(s => !battles.Any(b => b.Song1 == s || b.Song2 == s));
                    battles.Add(new Battle { Song1 = lastSong, Song2 = songs[0], Position = position });
                }
            }

            return battles;
        }

        private static Song FindBestMatch(Song firstSong, List<Song> remainingSongs,
            Dictionary<string, List<Song>> songsByChannel, Dictionary<string, List<Song>> songsByViewer)
        {
            // Try to find a Song from a different channel and viewer
            var bestMatch = remainingSongs.FirstOrDefault(s =>
                s.Channel != firstSong.Channel && s.Viewer.id != firstSong.Viewer.id);

            if (bestMatch == null)
            {
                // Try to find a Song from a different channel
                bestMatch = remainingSongs.FirstOrDefault(s => s.Channel != firstSong.Channel);
            }

            if (bestMatch == null)
            {
                // If still no match, just get any remaining Song
                bestMatch = remainingSongs.FirstOrDefault();
            }

            return bestMatch;
        }

        private static void UpdateSongCollections(Song song,
            Dictionary<string, List<Song>> songsByChannel, Dictionary<string, List<Song>> songsByViewer)
        {
            songsByChannel[song.Channel].Remove(song);
            songsByViewer[song.Viewer.id].Remove(song);
        }
    }
}

