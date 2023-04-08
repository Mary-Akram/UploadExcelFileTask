namespace UploadExcelFileTask.DTO
{
    public class ExcelFileInfo
    {
        public string fileName { get; set; }
        public string fileExtension { get; set; }
        public IFormFile excelFile { get; set; }
    }
}
