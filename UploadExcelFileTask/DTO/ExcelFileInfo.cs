namespace UploadExcelFileTask.DTO
{
    public class ExcelFileInfo
    {
        public int Id { get; set; }
        public string fileName { get; set; }
        public string Extension { get; set; }
        public IFormFile excelFile { get; set; }
    }
}
