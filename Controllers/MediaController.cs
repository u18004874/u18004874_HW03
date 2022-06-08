using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INF_HW3.Models;

namespace INF_HW3.Controllers
{
    public class MediaController : Controller
    {
        // GET: Media

        [HttpGet]
        public ActionResult Home()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Home(HttpPostedFileBase file, FormCollection collection) // HttpPostedFileBase is the filre we're receiving
        { //Posted info that expects to receive this data -> (HttpPostedFileBase file, string Radios)

            // Receive option from radio button using form collection
            string Radiosvalue = Convert.ToString(collection["Radios"]);

            // Check options
            if(Radiosvalue == "Document")
            {
                file.SaveAs(Path.Combine(HttpContext.Server.MapPath("~/Content/Media/Documents"), file.FileName));  // Save file to Documents folder
            }
            else if(Radiosvalue == "Image")
            {
                file.SaveAs(Path.Combine(HttpContext.Server.MapPath("~/Content/Media/Images"), file.FileName));
            }
            else if(Radiosvalue == "Video")
            {
                file.SaveAs(Path.Combine(HttpContext.Server.MapPath("~/Content/Media/Videos"), file.FileName));
            }

            return RedirectToAction("Home"); // Redirect to the default get[HttpGet] action and return view
        }
        public ActionResult Files()
        {
            List<FileModel> files = new List<FileModel>();
            //  List of FileModel objects that is read from system memory 
            string[] Documents = Directory.GetFiles(Server.MapPath("~/Content/Media/Documents")); // Map path in relation to server
            string[] Images = Directory.GetFiles(Server.MapPath("~/Content/Media/Images"));
            string[] Videos = Directory.GetFiles(Server.MapPath("~/Content/Media/Videos"));

            // Populate List
            foreach (var file in Documents) // for each file found in documents
            {
                FileModel foundFile = new FileModel(); // create new FileModel object
                foundFile.FileName = Path.GetFileName(file); // set it's name
                foundFile.FileType = "doc";
                files.Add(foundFile);// Add foundFile object to files list
            }

            foreach (var file in Images) // for each file found in documents
            {
                FileModel foundFile = new FileModel(); // create new FileModel object
                foundFile.FileName = Path.GetFileName(file); // set it's name
                foundFile.FileType = "img";
                files.Add(foundFile);// Add foundFile object to files list
            }

            foreach (var file in Videos) // for each file found in documents
            {
                FileModel foundFile = new FileModel(); // create new FileModel object
                foundFile.FileName = Path.GetFileName(file); // set it's name
                foundFile.FileType = "vid";
                files.Add(foundFile);// Add foundFile object to files list
            }
            return View(files); // Sends files to view, gets received in Files.cshtml
        }

        public FileResult DownloadFile(string filename, string filetype)
        {
            byte[] bytes = null;

            if(filetype == "doc") // check what filetype is before populating
            {
                bytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Media/Documents/") + filename);// build file path
            }
            else if (filetype == "img") // check what filetype is before populating
            {
                bytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Media/Images/") + filename);
            }
            else
            {
                bytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Media/Videos/") + filename);
            }
            // read file I want to download

            return File(bytes, "application/octet-stream", filename);
        }

        public ActionResult DeleteFile(string filename, string filetype)
        {
            string filepath = null;
            if(filetype == "doc")
            {
                filepath = Server.MapPath("~/Content/Media/Documents/") + filename; // Directory if document
            }
            else if (filetype == "img")
            {
                filepath = Server.MapPath("~/Content/Media/Images/") + filename;
            }
            else
            {
                filepath = Server.MapPath("~/Content/Media/Videos/") + filename;
            }

            System.IO.File.Delete(filepath);
            return RedirectToAction("Home");
        }

        public ActionResult Images()
        {
            List<FileModel> Images = new List<FileModel>(); // Instantiate list of FileModel objects

            string[] Imagepaths = Directory.GetFiles(Server.MapPath("~/Content/Media/Images"));

            foreach (var file in Imagepaths)
            {
                FileModel foundFile = new FileModel(); // create new file object for each image in Imagepaths
                foundFile.FileName = Path.GetFileName(file);
                foundFile.FileType = "img";
                Images.Add(foundFile); // Add to Images list
            }

            return View(Images); // Return Image list to view
        }
        public ActionResult Videos()
        {
            List<FileModel> Videos = new List<FileModel>(); // Instantiate list of FileModel objects

            string[] Videopaths = Directory.GetFiles(Server.MapPath("~/Content/Media/Videos"));

            foreach (var file in Videopaths)
            {
                FileModel foundFile = new FileModel(); // create new file object for each video in Videopaths
                foundFile.FileName = Path.GetFileName(file);
                foundFile.FileType = "img";
                Videos.Add(foundFile); // Add to Videos list
            }

            return View(Videos); // Return Video list to view
        }
        public ActionResult AboutMe()
        {
            return View();
        }
    }
}