<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportStudentsFromExcel.aspx.cs" Inherits="Monolitus.API.Staff.Pages.ImportStudentsFromExcel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="/Assets/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="/Assets/css/font-awesome.min.css" />

    <link rel="stylesheet" href="/Assets/css/datepicker.css" />
    <link rel="stylesheet" href="/Assets/css/chosen.css" />
    <link rel="stylesheet" href="/Assets/css/bootstrap-timepicker.css" />
    <link rel="stylesheet" href="/Assets/css/daterangepicker.css" />
    <link href="/Assets/css/colorbox.css" rel="stylesheet" />

    <!--[if IE 7]>
		  <link rel="stylesheet" href="/Assets/css/font-awesome-ie7.min.css" />
		<![endif]-->

    <!-- page specific plugin styles -->
    
    <!-- fonts -->

    <link rel="stylesheet" href="/Assets/css/ace-fonts.css" />

    <!-- ace styles -->

    <link rel="stylesheet" href="/Assets/css/uncompressed/ace.css" />
    <link rel="stylesheet" href="/Assets/css/uncompressed/ace-rtl.css" />
    <link rel="stylesheet" href="/Assets/css/uncompressed/ace-skins.css" />
   
</head>
<body style="background:white">       
  
    <form id="form1" runat="server">      
    <div>    
        <asp:FileUpload ID="uploadFile" runat="server" ClientIDMode="AutoID" /> <br />   
        <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload" />
        <br />  <br />    
        <asp:Literal ID="lblSonuc" runat="server"></asp:Literal>
    
    </div>
        <hr />
    <h4 >Toplu öğrenci yükleme nasıl yapılır?</h4>
    <div id="HowtoImportInfo"> 
       
        <ul>
            <li>
                <a href="/Assets/_imports/OgrenciImportSablon.csv"> Örnek şablon buradan indiriniz.</a><br />
                İndirdiğiniz örnek şablonu doldurup sisteme yükleyiniz.
            </li>          
        </ul>
        <ul>
            <li>Örnek şablon <br />
                1.Kolon : Ad<br />
                2.Kolon : Soyad<br />
                3.Kolon : Email (Her bir öğrenci için farklı farklı email adresleri girilmeli.)<br />
                4.Kolon : Şifre (standartlara uygun olmalı! En az 6 karakter olmalı ve bir harf içermeli)<br />
                5.Kolon : Sınıf Kodu (Sınıf kodunu sınıf listesine bakarak öğrenebilirsiniz.) <br />
                bilgilerini içerir. Şablondaki başlıkları değiştirmeyiniz.
            </li>
            <li>
                Öğrencileri eklendikten sonra kendi email ve şifreleri ile giriş yapıp eklendikleri sınıfın açık olan kitap ve ünitelerine ulaşabilirler.
            </li>
        </ul>
    </div>
    </form>
</body>
</html>
