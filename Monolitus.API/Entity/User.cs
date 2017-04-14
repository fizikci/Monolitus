using Cinar.Database;
using Monolitus.DTO;
using Monolitus.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Monolitus.API.Entity
{
    public class User : NamedEntity
    {
        public string Email {get; set;}
        public string Password {get; set;}
        public UserTypes UserType {get; set;}

        public bool EmailValidated {get; set;}

        public string Surname { get; set; }
        public string Avatar { get; set; }
        public string FacebookId {get; set;}
        public string TwitterId {get; set;}


        public string Keyword {get; set;}
        public DateTime LastLoginDate {get; set;}

        public string NewEmail {get; set;}

        public string FullName { get { return Name + " " + Surname; } }
        public bool HasRight(Rights right)
        {
            return true;
        }

        public override void BeforeSave()
        {
            base.BeforeSave();

            if (!Email.IsEmail())
                throw new Exception("Email address invalid");
        }

        internal bool IsAnonim()
        {
            return string.IsNullOrWhiteSpace(Id);
        }

        string siteAddress = "http://www.monolit.us";

        internal void SendEmailWithPasswordAndConfirmationCode()
        {
            var password = Utility.CreatePassword(6);
            this.Password = password.MD5().ToUpperInvariant().Substring(0, 16);
            this.Keyword = Utility.CreatePassword(16);
            this.Save();

            Provider.SendMail(this.Email, this.FullName, "Password and confirmation link", @"
                Hi #{FullName},<br/>
                <br/>
                Your password: #{password}<br/>
                <br/>
                Please click the link below to confirm your email address:<br/>
                #{siteAddress}/ConfirmEmail.aspx?Keyword=#{Keyword}
                ".EvaluateAsTemplate(new { FullName, password, siteAddress, Keyword }));
        }

        internal void SendPasswordRecoveryMessage()
        {
            this.Keyword = Utility.CreatePassword(16);
            this.Save();

            Provider.SendMail(this.Email, this.FullName, "Your monolit.us password", @"
                Hi #{FullName},<br/>
                <br/>
                You can change your password by using the link below:<br/>
                #{siteAddress}/ChangePassword.aspx?Keyword=#{Keyword}
                ".EvaluateAsTemplate(new { FullName, siteAddress, Keyword }));
        }

        internal void SendConfirmationCode()
        {
            Provider.SendMail(string.IsNullOrWhiteSpace(this.NewEmail) ? this.Email : this.NewEmail, this.FullName, "Confirm your email for Monolit.us", @"
                Hi #{FullName},<br/>
                <br/>
                Please click the link below to confirm your email address:<br/>
                #{siteAddress}/ConfirmEmail.aspx?Keyword=#{Keyword}
                ".EvaluateAsTemplate(new { FullName, siteAddress, Keyword }));
        }

        internal void SendWelcomeMessage()
        {
            Provider.SendMail(string.IsNullOrWhiteSpace(this.NewEmail) ? this.Email : this.NewEmail, this.FullName, "Welcome to Monolit.us", @"
                Hi #{FullName},<br>
                <br>
                Welcome to Monolit.us.<br>
                <br>
                You can add your bookmarks and classify them.<br>
                <a href=""#{siteAddress}"" target=""_blank"">Start Now!</a>
                ".EvaluateAsTemplate(new { FullName, siteAddress, Keyword }));
        }
    }

}
