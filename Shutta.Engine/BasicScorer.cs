using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shutta
{
    public class BasicScorer : Scorer
    {
        protected override int CalculateScore(Player p)
        {
            int score = p.Cards[0].Number + p.Cards[1].Number;
            score = score % 10;

            if (p.Cards[0].Number == p.Cards[1].Number)
                score += 100;

            if (p.Cards[0].IsKwang && p.Cards[1].IsKwang)
                score += 10000;

            return score;
        }
    }
}
