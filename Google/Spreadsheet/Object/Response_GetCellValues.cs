#pragma warning disable CS8618
using System;

namespace TSUBASAMUSU.Google.Spreadsheet
{
    [Serializable]
    public class Response_GetCellValues
    {
        public string spreadsheetId;

        public ValueRange[] valueRanges;
    }
}