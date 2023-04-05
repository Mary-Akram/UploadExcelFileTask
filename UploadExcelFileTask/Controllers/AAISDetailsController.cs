using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UploadExcelFileTask.Data;
using UploadExcelFileTask.Data.Repo;
using UploadExcelFileTask.DTO;
using UploadExcelFileTask.Models;

namespace UploadExcelFileTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AAISDetailsController : ControllerBase
    {
        private readonly AAISContext _context;
        private readonly IUploadExcelFile repo;


        public AAISDetailsController(AAISContext context,IUploadExcelFile _repo)
        {
            _context = context;
            repo=_repo;

        }

        [HttpPost]
        public async Task<ActionResult<AAISDetail>> PostAAISDetail([FromForm] ExcelFileInfo excelFileInfo)
        {
          if (excelFileInfo.excelFile == null)
          {
              return Problem("No Excel File .");
          }

          FilePathandExtenstion filePathandExtenstion =repo.UploadFile(excelFileInfo);
            await repo.ImportExceltoDatabase(filePathandExtenstion);
            await _context.SaveChangesAsync();

            return Ok();
        }

      
    }
}
