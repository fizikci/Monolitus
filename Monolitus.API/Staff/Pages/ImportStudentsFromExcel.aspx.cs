using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Monolitus.DTO.Enums;
using Monolitus.API.Entity;

namespace Monolitus.API.Staff.Pages
{
    public partial class ImportStudentsFromExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            int i = 0;
            List<string> hatalar = new List<string>();

            if (!uploadFile.HasFile)
                return;
           try//Dosya formatını kontrol ediyoruz. .csv haricinde dosya kabül etmiyoruz.
           {
                string extension = System.IO.Path.GetExtension(uploadFile.FileName);
                if (extension.ToLower() != ".csv")              
                    throw new Exception("Dosya formatı uygun değildir. Lütfen csv uzantılı dosya yükleyiniz.");
           } catch (Exception ex) {
                  hatalar.Add("Dosya Formatında hata: " + ex.Message);
           }

            var relativePath = string.Format("/Assets/_imports/{0}", uploadFile.FileName.Replace(".csv","_"+DateTime.Now.ToString("yyyyMMdd")+".csv"));
            var filePath = Server.MapPath(relativePath);      

            uploadFile.SaveAs(filePath);

            var dt = Provider.GetDataTabletFromCSVFile(filePath);

           

            if (dt.Rows.Count == 0)
                hatalar.Add("Hiç kayıt bulunamadı");

            foreach (DataRow dr in dt.Rows)
            {
                i++;
                try
                {
                    string ad, soyad, email, sifre, sinifId;
                    try
                    {
                        ad = dr["Ad"].ToString();
                        soyad = dr["Soyad"].ToString();
                        email = dr["Email"].ToString();
                        sifre = dr["Sifre"].ToString();
                        sinifId = dr["SinifKodu"].ToString();
                    }
                    catch {
                        hatalar.Add("CSV dosyası Ad, Soyad, Email, Sifre, SinifKodu sütunlarına sahip olmalıdır");
                        break;
                    }


                    var u = Provider.Database.Read<User>("Email={0}", email);
                    if (u == null) u = new User() { Email = email, Keyword = Utility.CreatePassword(16), IsDeleted = false };

                    if (!string.IsNullOrWhiteSpace(ad))
                        u.Name = ad;

                    if (!string.IsNullOrWhiteSpace(soyad))
                        u.Surname = soyad;
                    
                    if(!string.IsNullOrWhiteSpace(sifre))
                        u.Password = sifre.MD5().ToUpperInvariant().Substring(0, 16);
                    u.UserType = UserTypes.User;
                    u.Save();
                }
                catch (Exception ex) {
                    hatalar.Add(i + ". kayıtta hata: " + ex.Message);
                }
            }

            lblSonuc.Text = (i - hatalar.Count) + " / " + i + " kayıt başarıyla yüklendi.<ul>";
            foreach (var err in hatalar)
                lblSonuc.Text += "<li>" + err + "</li>";
            lblSonuc.Text += "</ul>";
        }

    }
}