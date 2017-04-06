using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace Monolitus.API.Staff.Handlers
{
    /// <summary>
    /// Summary description for Report
    /// </summary>
    public class SystemInfo : IHttpHandler, IRequiresSessionState
    {
        HttpContext context;

        public void ProcessRequest(HttpContext context)
        {
            this.context = context;

            switch (context.Request["method"])
            {
                case "getFileList":
                    {
                        getFileList();
                        break;
                    }
                case "uploadFile":
                    {
                        uploadFile();
                        break;
                    }
                case "deleteFile":
                    {
                        deleteFile();
                        break;
                    }
                case "renameFile":
                    {
                        renameFile();
                        break;
                    }
                case "createFolder":
                    {
                        createFolder();
                        break;
                    }
                case "getTextFile":
                    {
                        getTextFile();
                        break;
                    }
                case "saveTextFile":
                    {
                        saveTextFile();
                        break;
                    }
                default:
                    {
                        sendErrorMessage("Henüz " + context.Request["method"] + " isimli metod yazılmadı.");
                        break;
                    }
            }
        }
        protected void sendErrorMessage(string message)
        {
            context.Response.Write("ERR: " + message);
        }
        protected void sendErrorMessage(Exception ex)
        {
            sendErrorMessage(ex.ToStringBetter());
        }

        private void getFileList()
        {
            string folderName = context.Request["folder"] ?? "";
            string path = InternalProvider.MapPath(folderName);
            if (!path.StartsWith(InternalProvider.MapPath(InternalProvider.AppSettings["userFilesDir"])))
                path = InternalProvider.MapPath(InternalProvider.AppSettings["userFilesDir"]);

            List<string> resList = new List<string>();

            string[] items = Directory.GetDirectories(path).OrderBy(s => s).ToArray();
            for (int i = 0; i < items.Length; i++)
            {
                bool isHidden = ((File.GetAttributes(items[i]) & FileAttributes.Hidden) == FileAttributes.Hidden);
                if (!isHidden)
                {
                    DirectoryInfo d = new DirectoryInfo(items[i]);
                    resList.Add("{name:" + d.Name.ToJS() + ", size:-1, date:" + d.LastWriteTime.ToJS() + "}");
                }
            }

            items = Directory.GetFiles(path).OrderBy(s => s).ToArray();

            for (int i = 0; i < items.Length; i++)
            {
                bool isHidden = ((File.GetAttributes(items[i]) & FileAttributes.Hidden) == FileAttributes.Hidden);
                if (!isHidden)
                {
                    FileInfo f = new FileInfo(items[i]);
                    resList.Add("{name:" + f.Name.ToJS() + ", size:" + f.Length.ToJS() + ", date:" + f.LastWriteTime.ToJS() + "}");
                }
            }

            context.Response.Write("{success:true, root:[" + String.Join(",", resList.ToArray()) + "]}");
        }
        private void uploadFile()
        {
            try
            {
                string folderName = context.Request["folder"] ?? "";
                string path = InternalProvider.MapPath(folderName);
                if (!path.StartsWith(InternalProvider.MapPath(InternalProvider.AppSettings["userFilesDir"])))
                    path = InternalProvider.MapPath(InternalProvider.AppSettings["userFilesDir"]);

                if ((File.GetAttributes(path) & FileAttributes.Directory) != FileAttributes.Directory)
                    path = Path.GetDirectoryName(path);

                for (int i = 0; i < context.Request.Files.Count; i++)
                {
                    string fileName = Path.GetFileName(context.Request.Files[i].FileName).MakeFileName();
                    string imgPath = Path.Combine(path, fileName);
                    try
                    {
                        // eğer dosya resim ise resize edelim
                        Image bmp = Image.FromStream(context.Request.Files[i].InputStream);
                        if (bmp.Width > InternalProvider.Configuration.ImageUploadMaxWidth)
                        {
                            Image bmp2 = bmp.ScaleImage(InternalProvider.Configuration.ImageUploadMaxWidth, 0);
                            //imgUrl = imgUrl.Substring(0, imgUrl.LastIndexOf('.')) + ".jpg";
                            bmp2.SaveImage(imgPath, InternalProvider.Configuration.ThumbQuality);
                        }
                        else
                            context.Request.Files[i].SaveAs(imgPath);
                    }
                    catch
                    {
                        context.Request.Files[i].SaveAs(imgPath);
                    }

                    //context.Request.Files[i].SaveAs(Path.Combine(path, fileName));
                    context.Response.Write(@"<script>window.parent.fileBrowserUploadFeedback('Dosya yüklendi.', '" + folderName + "/" + fileName + "');</script>");
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(@"<script>window.parent.fileBrowserUploadFeedback('Yükleme başarısız. '+" + ex.Message.ToJS() + ");</script>");
            }
        }
        private void deleteFile()
        {
            string folderName = context.Request["folder"] ?? "";
            if (!folderName.StartsWith("/")) folderName = "/" + folderName;
            string path = InternalProvider.MapPath(folderName);
            if ((File.GetAttributes(path) & FileAttributes.Directory) != FileAttributes.Directory)
                path = Path.GetDirectoryName(path);
            if (!path.StartsWith(InternalProvider.MapPath(InternalProvider.AppSettings["userFilesDir"])))
                path = InternalProvider.MapPath(InternalProvider.AppSettings["userFilesDir"]);

            string fileNames = context.Request["name"];
            if (string.IsNullOrEmpty(fileNames) || fileNames.Trim() == "")
                throw new Exception("Dosya seçiniz");

            foreach (string fileName in fileNames.Split(new[] { "#NL#" }, StringSplitOptions.RemoveEmptyEntries))
            {
                string filePath = Path.Combine(path, fileName);
                if (File.Exists(filePath))
                {
                    File.SetAttributes(filePath, FileAttributes.Normal);
                    File.Delete(filePath);
                }
                else if (Directory.Exists(filePath))
                    Directory.Delete(filePath, true);
            }
            context.Response.Write(@"<script>window.parent.fileBrowserUploadFeedback('Dosya silindi');</script>");
        }
        private void renameFile()
        {
            string folderName = context.Request["folder"] ?? "";
            if (!folderName.StartsWith("/")) folderName = "/" + folderName.Substring(1);
            //else folderName = "~/" + folderName;
            string path = InternalProvider.MapPath(folderName);
            if ((File.GetAttributes(path) & FileAttributes.Directory) != FileAttributes.Directory)
                path = Path.GetDirectoryName(path);
            if (!path.StartsWith(InternalProvider.MapPath(InternalProvider.AppSettings["userFilesDir"])))
                path = InternalProvider.MapPath(InternalProvider.AppSettings["userFilesDir"]);

            string fileNames = context.Request["name"];
            if (string.IsNullOrEmpty(fileNames) || fileNames.Trim() == "")
                throw new Exception("Dosya seçiniz");
            string fileName = fileNames.Split(new[] { "#NL#" }, StringSplitOptions.RemoveEmptyEntries)[0];

            string newFileName = context.Request["newName"].MakeFileName();
            string newPath = Path.Combine(path, newFileName);

            string filePath = Path.Combine(path, fileName);
            if (File.Exists(filePath))
                File.Move(filePath, newPath);
            else if (Directory.Exists(filePath))
                Directory.Move(filePath, newPath);

            context.Response.Write(@"<script>window.parent.fileBrowserUploadFeedback('Dosya adı değiştirildi');</script>");
        }
        private void createFolder()
        {
            try
            {
                string folderName = context.Request["folder"] ?? "";
                string path = InternalProvider.MapPath(folderName);
                if (!path.StartsWith(InternalProvider.MapPath(InternalProvider.AppSettings["userFilesDir"])))
                {
                    path = InternalProvider.MapPath(InternalProvider.AppSettings["userFilesDir"]);
                    context.Response.Write(@"<script>window.parent.alert('Error. There is no such folder.');</script>");
                    return;
                }

                string newFolderName = context.Request["name"].MakeFileName();
                path = Path.Combine(path, newFolderName);
                Directory.CreateDirectory(path);

                context.Response.Write(@"<script>window.parent.fileBrowserUploadFeedback('Klasör oluşturuldu.');</script>");
            }
            catch
            {
                context.Response.Write(@"<script>window.parent.fileBrowserUploadFeedback('Hata');</script>");
            }
        }

        private void getTextFile()
        {
            string folderName = context.Request["folder"] ?? "";
            if (!folderName.StartsWith("/")) folderName = "/" + folderName.Substring(1);

            string path = InternalProvider.MapPath(folderName);
            if ((File.GetAttributes(path) & FileAttributes.Directory) != FileAttributes.Directory)
                path = Path.GetDirectoryName(path);
            if (!path.StartsWith(InternalProvider.MapPath(InternalProvider.AppSettings["userFilesDir"])))
                path = InternalProvider.MapPath(InternalProvider.AppSettings["userFilesDir"]);

            string fileNames = context.Request["name"];
            if (string.IsNullOrEmpty(fileNames) || fileNames.Trim() == "")
                throw new Exception("Dosya seçiniz");
            string fileName = fileNames.Split(new[] { "#NL#" }, StringSplitOptions.RemoveEmptyEntries)[0];

            string filePath = Path.Combine(path, fileName);
            if (File.Exists(filePath))
                context.Response.Write(File.ReadAllText(filePath, Encoding.UTF8));
        }
        private void saveTextFile()
        {
            string folderName = context.Request["folder"] ?? "";
            if (!folderName.StartsWith("/")) folderName = "/" + folderName.Substring(1);

            string path = InternalProvider.MapPath(folderName);
            if ((File.GetAttributes(path) & FileAttributes.Directory) != FileAttributes.Directory)
                path = Path.GetDirectoryName(path);
            if (!path.StartsWith(InternalProvider.MapPath(InternalProvider.AppSettings["userFilesDir"])))
                path = InternalProvider.MapPath(InternalProvider.AppSettings["userFilesDir"]);

            string fileNames = context.Request["name"];
            if (string.IsNullOrEmpty(fileNames) || fileNames.Trim() == "")
                throw new Exception("Dosya seçiniz");
            string fileName = fileNames.Split(new[] { "#NL#" }, StringSplitOptions.RemoveEmptyEntries)[0];

            string filePath = Path.Combine(path, fileName);
            if (File.Exists(filePath))
            {
                string backupFileName = "_old_" + fileName;
                string backupPath = Path.Combine(path, backupFileName);

                if (File.Exists(backupPath))
                    File.Delete(backupPath);

                File.Move(filePath, backupPath);
                File.WriteAllText(filePath, context.Request["source"], Encoding.UTF8);

                context.Response.Write("ok");
                return;
            }
            context.Response.Write("ERR:File not found.");
        }




        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public class InternalProvider
        {
            public class Configuration
            {

                public static int ImageUploadMaxWidth { get { return 1000; } }

                public static int ThumbQuality { get { return 100; } }
            }

            internal static string MapPath(string folderName)
            {
                return HttpContext.Current.Server.MapPath(folderName);
            }

            public static NameValueCollection AppSettings { get { return ConfigurationManager.AppSettings; } }
        }
    }


}