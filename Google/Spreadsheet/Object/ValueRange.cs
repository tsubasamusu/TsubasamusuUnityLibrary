#pragma warning disable CS8618
using System;
using System.Collections.Generic;

namespace TSUBASAMUSU.Google.Spreadsheet
{
    [Serializable]
    public class ValueRange
    {
        public string range;

        public Dimension majorDimension;

        public List<List<string>> values;
    }
}