using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shutta
{
    public class Card
    {
        public int Number;
        public bool IsKwang;

        public string ToText()
        {
            string text = Number.ToString(); // 3 -> "3"

            if (IsKwang)
                text += "K"; // "3" -> "3K"

            return text;
        }

        public override string ToString()
        {
            return ToText();
        }
    }
}