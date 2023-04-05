using Microsoft.AspNetCore.Mvc;
using UploadExcelFileTask.DTO;

namespace UploadExcelFileTask.Data.Repo
{
    public interface IUploadExcelFile
    {
      FilePathandExtenstion UploadFile(ExcelFileInfo excelFileInfo);
      Task ImportExceltoDatabase(FilePathandExtenstion filePathand);
      Task<bool> SaveAsync();
    }
}
