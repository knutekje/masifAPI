using System.Threading.Tasks;
using masifAPI.Data;
using masifAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;


namespace masifAPI.Controllers;



[Route("api/[controller]")]
[ApiController]
public class BildeController : ControllerBase {
    private readonly PictureContext _context;
    public class Upload{
        public IFormFile formFile;
        public string Description;
    }
    public BildeController(PictureContext context){
        _context = context;
    }
    [HttpPost("/upload")]
    public  async Task<String> PostUpload(Upload upload){

            var fileName = Path.GetFileName(upload.formFile.FileName);
    
            using var stream = System.IO.File.OpenWrite(fileName);
            Picture obj = new Picture(){ 
                FileName = upload.formFile.FileName,
                FilePath = upload.formFile.FileName, 
                Description = upload.Description};
            await upload.formFile.CopyToAsync(stream);
            _context.Pictures.Add(obj);
            await _context.SaveChangesAsync();
            
        return "(upload)file";

    }

     

}