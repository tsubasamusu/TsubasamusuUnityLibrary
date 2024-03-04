#pragma warning disable CS8618
using System;

namespace TSUBASAMUSU.Google.Spreadsheet
{
    [Serializable]
    public class UpdateValuesResponse
    {
        public string spreadsheetId;

        public string updatedRange;

        public int updatedRows;

        public int updatedColumns;

        public int updatedCells;

        public ValueRange updatedData;
    }
}