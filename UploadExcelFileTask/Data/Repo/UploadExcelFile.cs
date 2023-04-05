using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using System.Data;
using UploadExcelFileTask.DTO;
using UploadExcelFileTask.Models;

namespace UploadExcelFileTask.Data.Repo
{

    public class UploadExcelFile : IUploadExcelFile
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly AAISContext context;
        public UploadExcelFile(IConfiguration _configuration, IWebHostEnvironment _hostEnvironment, AAISContext _context)
        {
            this.configuration = _configuration;
            this.hostEnvironment = _hostEnvironment;
            this.context = _context;
        }
        public FilePathandExtenstion UploadFile(ExcelFileInfo excelFileInfo)
        {
            try
            {
                if (excelFileInfo.excelFile == null)
                    throw new Exception("File is Not Received...");
                // Create the Directory if it is not exist
                string dirPath = Path.Combine(hostEnvironment.WebRootPath, "ReceivedReports");
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                // make sure that only Excel file is used 
                string dataFileName = Path.GetFileName(excelFileInfo.excelFile.FileName);
                string extension = Path.GetExtension(dataFileName);
                string[] allowedExtsnions = new string[] { ".xls", ".xlsx" };
                //make sure that file having extension as either.xls or.xlsx is uploaded.");
                if (!allowedExtsnions.Contains(extension))
                    throw new Exception("Sorry! This file is not allowed,Excel sheet only ");


                // Make a Copy of the Posted File from the Received HTTP Request
                string saveToPath = Path.Combine(dirPath, dataFileName);
                using (FileStream stream = new FileStream(saveToPath, FileMode.Create))
                {
                    excelFileInfo.excelFile.CopyTo(stream);
                }
                FilePathandExtenstion file = new FilePathandExtenstion();
                {
                    file.filepath = saveToPath;
                    file.extenstion = extension;
                }
                return file;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("because it is being used by another process"))
                {
                    throw new Exception("The file you are trying to save on is in use, please close it");
                }
                return null;
         }
        }
        public async Task ImportExceltoDatabase(FilePathandExtenstion pathandExtenstion)
        {
            IExcelDataReader reader;

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);


            using (var stream = new FileStream(pathandExtenstion.filepath, FileMode.Open))
            {
                if (pathandExtenstion.extenstion== ".xls")
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                else
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                DataSet ds = new DataSet();
                ds = reader.AsDataSet();
                reader.Close();

                if (ds != null && ds.Tables.Count > 0)
                {
                    // Read the the Table
                    DataTable serviceDetails = ds.Tables[0];
                    for (int i = 1; i < serviceDetails.Rows.Count; i++)
                    {
                        AAISDetail details = new AAISDetail();
                        details.Description = serviceDetails.Rows[i][1].ToString();
                        details.Location = serviceDetails.Rows[i][2].ToString();
                        details.Status = serviceDetails.Rows[i][3].ToString();
                        details.Type = serviceDetails.Rows[i][4].ToString();
                        details.Priority = serviceDetails.Rows[i][5].ToString();
                        details.Scheduled_FinishDate = Convert.ToDateTime(serviceDetails.Rows[i][6].ToString());
                        details.Completion_Date = Convert.ToDateTime(serviceDetails.Rows[i][7].ToString());
                        details.StartDate = Convert.ToDateTime(serviceDetails.Rows[i][8].ToString());
                        details.State = serviceDetails.Rows[i][9].ToString();
                        details.Reason = serviceDetails.Rows[i][10].ToString();
      
                        // Add the record in Database
                        await context.AAISDetails.AddAsync(details);

                    }

                }

            }

        }
        public async Task<bool> SaveAsync()
        {
           return await context.SaveChangesAsync()>0;

           
        }
    }
}
