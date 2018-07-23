using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shutta
{
    public class SimpleScorer : Scorer
    {
        protected override int CalculateScore(Player p)
        {
            return p.Cards[0].Number + p.Cards[1].Number;
        }
    }
}
