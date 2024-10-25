using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using DevExpress.XtraRichEdit.API.Internal;

namespace FNSC.Data
{
    public class ChampionshipContext : DbContext
    {
        private static volatile ChampionshipContext _contextInstance;
        private static readonly object _mutex = new();

        private bool queueOpen = false;
        public static ChampionshipContext ContextInstance
        {
            get
            {
                if (_contextInstance == null)
                {
                    lock (_mutex)
                    {
                        _contextInstance = new ChampionshipContext();
                        

                    }
                }
                return _contextInstance;
            }
        }
        public DbSet<Viewer> Viewers { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Battle> Battles { get; set; }

        public string DbPath { get; } = Path.Combine(Path.GetTempPath(), "FNSC","games.db");


        public ChampionshipContext()
        {
           
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"DataSource="+DbPath).EnableSensitiveDataLogging();
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
              modelBuilder.Entity<Round>().HasOne<Battle>(r => r.CurrentBattle);

            modelBuilder.Entity<Game>().HasMany<Song>(g => g.SubmittedSongs);
            modelBuilder.Entity<Game>().HasMany<Song>(g => g.PreSubmittedSongs);
            modelBuilder.Entity<Game>().HasMany<Round>(g => g.Rounds);

            modelBuilder.Entity<Viewer>().HasData(new Viewer { id = "12640", display = "Credulus", name = "credulus", role = 3, type = "twitch", subscribed = false});
            modelBuilder.Entity<Viewer>().HasData(new Viewer { id = "79268687", display = "Karvaooppeli", name = "karvaooppeli", role = 1, type = "twitch", subscribed = false});
            modelBuilder.Entity<Viewer>().HasData(new Viewer { id = "264243225", display = "wizenedwizards", name = "wizenedwizards", role = 1, type = "twitch", subscribed = false});
            modelBuilder.Entity<Viewer>().HasData(new Viewer { id = "206992018", display = "NoBuddyIsPerfect", name = "nobuddyisperfect", role = 3, type = "twitch", subscribed = false});
            modelBuilder.Entity<Viewer>().HasData(new Viewer { id = "725622089", display = "Retisska", name = "retisska", role = 3, type = "twitch", subscribed = true });
            modelBuilder.Entity<Viewer>().HasData(new Viewer { id = "955237329", display = "FrostyToolsDotCom", name = "frostytoolsdotcom", role = 3, type = "twitch", subscribed = false });
            modelBuilder.Entity<Viewer>().HasData(new Viewer { id = "641794374", display = "hans154", name = "hans154", role = 1, type = "twitch", subscribed = false });
            modelBuilder.Entity<Viewer>().HasData(new Viewer { id = "756550096", display = "omos1", name = "omos1", role = 2, type = "twitch", subscribed = false });
            modelBuilder.Entity<Viewer>().HasData(new Viewer { id = "498666678", display = "SaltSkeggur", name = "saltskeggur", role = 1, type = "twitch", subscribed = false });
            modelBuilder.Entity<Viewer>().HasData(new Viewer { id = "674951982", display = "ghe_di", name = "ghe_di", role = 1, type = "twitch", subscribed = false });
            modelBuilder.Entity<Viewer>().HasData(new Viewer { id = "535004677", display = "ItReins", name = "itreins", role = 2, type = "twitch", subscribed = false });
            modelBuilder.Entity<Viewer>().HasData(new Viewer { id = "169517884", display = "AndyInTheFork", name = "andyinthefork", role = 3, type = "twitch", subscribed = false });
            modelBuilder.Entity<Viewer>().HasData(new Viewer { id = "100135110", display = "StreamElements", name = "streamelements", role = 3, type = "twitch", subscribed = false });
            modelBuilder.Entity<Viewer>().HasData(new Viewer { id = "401548213", display = "WildTomcat5", name = "wildtomcat5", role = 2, type = "twitch", subscribed = false });
            modelBuilder.Entity<Viewer>().HasData(new Viewer { id = "62053184", display = "pkn345", name = "pkn345", role = 0, type = "twitch", subscribed = false });
            modelBuilder.Entity<Viewer>().HasData(new Viewer { id = "43161910", display = "gamerjaym", name = "gamerjaym", role = 1, type = "twitch", subscribed = false });
            modelBuilder.Entity<Viewer>().HasData(new Viewer { id = "51010749", display = "Bai_nin", name = "bai_nin", role = 1, type = "twitch", subscribed = false });
            modelBuilder.Entity<Viewer>().HasData(new Viewer { id = "557980940", display = "dcocol", name = "dcocol", role = 3, type = "twitch", subscribed = false });
            modelBuilder.Entity<Viewer>().HasData(new Viewer { id = "106313947", display = "TheStBlaine", name = "thestblaine", role = 1, type = "twitch", subscribed = false });
            modelBuilder.Entity<Viewer>().HasData(new Viewer { id = "884443102", display = "SheriffCroco", name = "sheriffcroco", role = 1, type = "twitch", subscribed = false });
            modelBuilder.Entity<Viewer>().HasData(new Viewer { id = "579901532", display = "Milbrandt_2", name = "milbrandt_2", role = 1, type = "twitch", subscribed = false });
            modelBuilder.Entity<Viewer>().HasData(new Viewer { id = "785731504", display = "1_senior", name = "1_senior", role = 1, type = "twitch", subscribed = false });
            modelBuilder.Entity<Viewer>().HasData(new Viewer { id = "228757727", display = "bryanthecoolcat", name = "bryanthecoolcat", role = 1, type = "twitch", subscribed = false });
            modelBuilder.Entity<Viewer>().HasData(new Viewer { id = "876112028", display = "NoAltIsPerfect", name = "noaltisperfect", role = 4, type = "twitch", subscribed = false });
            modelBuilder.Entity<Viewer>().HasData(new Viewer { id = "734501812", display = "nobotisperfect", name = "nobotisperfect", role = 3, type = "twitch", subscribed = false });



          



        }
    }

}
