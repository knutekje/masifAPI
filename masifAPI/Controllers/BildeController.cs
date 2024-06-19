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

    [HttpGet("{id}")]
        public async Task<ActionResult<byte[]>> GetPicture(long id)
        {
            var picture = await _context.Pictures.FindAsync(id);
            string path = @picture.FileName;
            byte[] something = System.IO.File.ReadAllBytes(path);
            if (picture == null)
            {
                return NotFound();
            }
            
            return File(something, "image/jpeg");
        }
    

    [HttpPost("/upload")]
    public  async Task<CreatedAtActionResult> PostUpload(IFormFile file){

            var fileName = Path.GetFileName(file.FileName);
            
            using var stream = System.IO.File.OpenWrite(fileName);
            Picture obj = new Picture(){ 
                FileName = file.FileName,
                FilePath = file.FileName, 
                Description = file.FileName};
            await file.CopyToAsync(stream);
            _context.Pictures.Add(obj);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetPicture", new { id = obj.Id }, obj);
    }

     

}