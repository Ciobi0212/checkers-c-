using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace checkers_mvp.Misc
{
    public static class WinningStats
    {
        public static WinningStatsModel stats;
         static WinningStats()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            string path = projectDirectory + @"\WinningStats\WinningStats.json";

            stats = JsonConvert.DeserializeObject<WinningStatsModel>(File.ReadAllText(path));
        }

        public static void saveWinningStats(string winner, int nPieces)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            string path = projectDirectory + @"\WinningStats\WinningStats.json";

            if (winner == "Red")
            {
                increaseRedWins();

                if(nPieces > stats.RedMostPieces)
                {
                    stats.RedMostPieces = nPieces;
                }
            }

            else if (winner == "Black")
            {
                increaseBlackWins();

                if(nPieces > stats.BlackMostPieces)
                {
                    stats.BlackMostPieces = nPieces;
                }
            }

            string json = JsonConvert.SerializeObject(stats);
            System.IO.File.WriteAllText(path, json);
        }

        public static void increaseRedWins()
        {
            stats.RedWins++;
        }

        public static void increaseBlackWins()
        {
            stats.BlackWins++;
        }

    }

    public class WinningStatsModel
    {
        public  int RedWins { get; set; }
        public  int BlackWins { get; set; }

        public int RedMostPieces { get; set; }

        public int BlackMostPieces { get; set; }

        [JsonConstructor]
        public WinningStatsModel(int redWins, int blackWins, int redMostPieces, int blackMostPieces)
        {
            RedWins = redWins;
            BlackWins = blackWins;

            RedMostPieces = redMostPieces;
            BlackMostPieces = blackMostPieces;
        }
    }
}
