using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class RegistracijaModel
    {
        private string username;
        private string password;
        private string originalPassword;
        private string name;
        private string lastname;
        private DateTime birthday;
        private string sendBackBirthday;
        private string adresa;
        private string tipPutnika;

        public RegistracijaModel()
        {

        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        public string OriginalPassword
        {
            get { return originalPassword; }
            set { originalPassword = value; }
        }
        public string SendBackBirthday
        {
            get { return sendBackBirthday; }
            set { sendBackBirthday = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Lastname
        {
            get { return lastname; }
            set { lastname = value; }
        }
        public DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }
        public string Adresa
        {
            get { return adresa; }
            set { adresa = value; }
        }
        public string TipPutnika
        {
            get { return tipPutnika; }
            set { tipPutnika = value; }
        }
    }
}