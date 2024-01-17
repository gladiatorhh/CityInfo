using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
    private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;

    public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
    {
        _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider;
    }

    [HttpGet("{fileName}")]
    public ActionResult GetFile(string fileName)
    {
        const string filePath = "arian-haghighi-cv-fa.pdf";

        if (!System.IO.File.Exists(filePath))
            return NotFound();

        byte[] byteFile = System.IO.File.ReadAllBytes(filePath);

        if (!_fileExtensionContentTypeProvider.TryGetContentType(filePath, out string contentType))
        {
            contentType = "application/octet-stream";
        }

        return File(byteFile, contentType, fileName + Path.GetExtension(filePath));
    }
}
