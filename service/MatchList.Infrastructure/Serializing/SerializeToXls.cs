using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace MatchList.Infrastructure.Serializing
{
    [SuppressMessage("ReSharper", "RedundantAssignment")]
    [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
    public class SerializeToXls : IDeSerializer
    {
        public T Deserialize<T>(IFormFile data)
        {
            const bool hasHeader = true;

            //Load excel stream
            using var stream = new MemoryStream();
            data.CopyToAsync(stream);
            using var excelPack = new ExcelPackage(stream);

            if (!excelPack.Workbook.Worksheets.Any())
            {
                return default;
            }

            //Lets Deal with first worksheet.(You may iterate here if dealing with multiple sheets)
            var ws = excelPack.Workbook.Worksheets[0];

            //Get all details as DataTable -because Datatable make life easy :)
            var excelasTable = new DataTable();
            foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
            {
                //Get colummn details
                if (!string.IsNullOrEmpty(firstRowCell.Text))
                {
                    var firstColumn = $"Column {firstRowCell.Start.Column}";
                    excelasTable.Columns.Add(hasHeader ? firstRowCell.Text : firstColumn);
                }
            }

            const int startRow = hasHeader ? 2 : 1;
            //Get row details
            for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
            {
                var wsRow = ws.Cells[rowNum, 1, rowNum, excelasTable.Columns.Count];
                var row   = excelasTable.Rows.Add();
                foreach (var cell in wsRow)
                {
                    row[cell.Start.Column - 1] = cell.Text;
                }
            }

            //Get everything as generics and let end user decides on casting to required type
            var serializeObject = JsonConvert.SerializeObject(excelasTable);
            var generatedType   = JsonConvert.DeserializeObject<T>(serializeObject,new JsonSerializerSettings
                                                                                   { 
                                                                                       DefaultValueHandling = DefaultValueHandling.Populate, 
                                                                                       NullValueHandling    = NullValueHandling.Ignore 
                                                                                   });
            return (T) Convert.ChangeType(generatedType, typeof(T));
        }
    }
}