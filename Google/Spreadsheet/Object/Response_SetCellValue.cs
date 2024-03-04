#pragma warning disable CS8618
using System;
using System.Collections.Generic;

namespace TSUBASAMUSU.Google.Spreadsheet
{
    [Serializable]
    public class Response_SetCellValue
    {
        public string spreadsheetId;

        public int totalUpdatedRows;

        public int totalUpdatedColumns;

        public int totalUpdatedCells;

        public int totalUpdatedSheets;

        public List<UpdateValuesResponse> responses;
    }
}