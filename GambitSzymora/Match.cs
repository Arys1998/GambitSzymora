using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GambitSzymora
{
    class Match
    {
        int turns;
        List<ChessBoardSnapshot> snapshots;

        public Match()
        {
            turns = 0;
            snapshots = new List<ChessBoardSnapshot>();
        }

        public void SaveSnap(ChessBoardSnapshot snapshot) { snapshots.Add(snapshot); turns++; }
        public ChessBoardSnapshot GetSnapshot(int turn)
        {
            if (turn > turns) return null;
            else return snapshots[turn - 1];
        }
    }
}
