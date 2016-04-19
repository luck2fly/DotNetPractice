using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

public class FileController : ApiController
{
    public string Get()
    {
        return DateTime.Now.ToString();
    }


    public async Task<HttpResponseMessage> Post()
    {
        // Check if the request contains multipart/form-data.
        if (!Request.Content.IsMimeMultipartContent())
        {
            throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        }

        var provider = new MultipartMemoryStreamProvider();

        try
        {
            // Read the form data and return an async task.
            await Request.Content.ReadAsMultipartAsync(provider);

            // This illustrates how to get the file names for uploaded files.
            foreach (var fileData in provider.Contents)
            {
                var filename = fileData.Headers.ContentDisposition.FileName.Trim('\"');
                if (!string.IsNullOrEmpty(filename))
                {
                    var bytes = await fileData.ReadAsStreamAsync();

                    var dir = HttpContext.Current.Server.MapPath("~/App_Data/upload/");
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    using (var stream = File.Create(dir + DateTime.Now.ToString("yyMMddHHmmss") + filename))
                    {
                        bytes.CopyTo(stream);
                    }

                    return Request.CreateResponse(filename);
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
        catch (System.Exception e)
        {
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
        }
    }

}