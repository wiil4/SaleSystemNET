using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WSSale.Models;
using WSSale.Models.Response;
using WSSale.Models.Request;

namespace WSSale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly RealSaleContext _context;
        private readonly IWebHostEnvironment _environment;

        public ClientController(RealSaleContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Response oResp = new Response();
            try
            {                
                var clients = await _context.Clients.OrderByDescending(c=>c.Id).ToListAsync();
                oResp.Success = 1;
                oResp.Data = clients;
                
            }
            catch(Exception e)
            {
                oResp.Message = e.Message;
            }
            return Ok(oResp);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody]ClientRequest cModel)
        {
            var oResp = new Response();
            try
            {
                var oClient = new Client();
                oClient.Name = cModel.Name;
                _context.Add(oClient);
                await _context.SaveChangesAsync();
                oResp.Success = 1;
                oResp.Data = oClient;
            }
            catch(Exception e)
            {
                oResp.Message = e.Message;
            }

            return Ok(oResp);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, ClientRequest oModel)
        {
            var oResp = new Response();
            try
            {
                if(id != oModel.Id)
                {
                    return BadRequest();
                }
                var oClient = await _context.Clients.FindAsync(id);

                if(oClient == null)
                {
                    return NotFound();
                }

                oClient.Name = oModel.Name;
                await _context.SaveChangesAsync();
                oResp.Success = 1;
                oResp.Data = oClient;
            }
            catch(Exception e)
            {
                oResp.Message = e.Message;
            }
            return Ok(oResp);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var oResp = new Response();
            try
            {
                var oClient = await _context.Clients.FindAsync(id);
                if(oClient == null)
                {
                    return NotFound();
                }
                _context.Clients.Remove(oClient);
                _context.SaveChanges();
                oResp.Success=1;

            }
            catch(Exception e)
            {
                oResp.Message = e.Message;
            }

            return Ok(oResp);
        }




        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file selected.");

            // Save file to disk
            var filePath = Path.Combine(_environment.ContentRootPath, "uploads", file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok("File uploaded successfully.");
        }

        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> Download(string fileName)
        {
            var filePath = Path.Combine(_environment.ContentRootPath, "uploads", fileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            return File(fileStream, "application/octet-stream", fileName);
        }

    }
}
