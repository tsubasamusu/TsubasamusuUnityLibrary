#pragma warning disable CS8618
using System;
using System.Collections.Generic;

namespace TSUBASAMUSU.Google.Spreadsheet
{
    [Serializable]
    public class Request_SetCellValue
    {
        public ValueInputOption valueInputOption;

        public List<ValueRange> data;

        public bool includeValuesInResponse;

        public ValueRenderOption responseValueRenderOption;

        public DateTimeRenderOption responseDateTimeRenderOption;
    }
}