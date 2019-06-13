using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Http;
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [RoutePrefix("api/Registracija")]
    public class RegistracijaController : ApiController
    {
        ApplicationDbContext context = new ApplicationDbContext();
        IUnitOfWork unitOfWork;

        public RegistracijaController(IUnitOfWork uw)
        {
            unitOfWork = uw;
        }

        [HttpPost, Route("registracijaKorisnika")]
        public IHttpActionResult RegistracijaKorisnika(RegistracijaModel userToRegister)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (userToRegister != null)
            {
                try
                {
                    var userStore = new UserStore<ApplicationUser>(context);
                    var userManager = new UserManager<ApplicationUser>(userStore);
                    string returnMessage = "";

                    if (context.Users.Any(u => u.UserName == userToRegister.Username))
                    {
                        returnMessage = "Nalog sa ovim email-om vec postoji....";
                        return Ok(returnMessage);
                    }

                    var user = new ApplicationUser() { Id = userToRegister.Username, UserName = userToRegister.Username, Email = userToRegister.Username, PasswordHash = ApplicationUser.HashPassword(userToRegister.Password) };
                    userManager.Create(user);
                    userManager.AddToRole(user.Id, "AppUser");

                    Korisnik passenger = new Korisnik();
                    passenger.Email = userToRegister.Username;
                    passenger.IDUser = user.Id;
                    passenger.DatumRodjenja = userToRegister.Birthday;
                    passenger.Stanje = ProcesVerifikacije.Procesira;
                    passenger.Prezime = userToRegister.Lastname;
                    passenger.Ime = userToRegister.Name;
                    passenger.Adresa = userToRegister.Adresa;

                    if (String.Compare(userToRegister.TipPutnika, "Regularni") == 0)
                        passenger.TipKorisnika = TipPutnika.Regularni;
                    else if (String.Compare(userToRegister.TipPutnika, "Djak") == 0)
                        passenger.TipKorisnika = TipPutnika.Djak;
                    else
                        passenger.TipKorisnika = TipPutnika.Penzioner;

                    unitOfWork.KorisnikRepository.Add(passenger);
                    unitOfWork.Complete();
                    returnMessage = "Uspesno registrovani...";
                    return Ok(returnMessage);
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }
            else
            {
                ModelState.AddModelError("", "Not found!");
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [ActionName("ubaciSliku")]
        public HttpResponseMessage UploadPhoto()
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        var filePath = HttpContext.Current.Server.MapPath("~/UploadFile/" + postedFile.FileName);

                        postedFile.SaveAs(filePath);

                        Korisnik k = unitOfWork.KorisnikRepository.Find(x => String.Compare(x.Email, file) == 0).FirstOrDefault();
                        k.Slika = filePath;
                        k.Stanje = ProcesVerifikacije.Procesira;

                        unitOfWork.KorisnikRepository.Update(k);
                        unitOfWork.Complete();
                    }
                }
                return response;
            }
            catch (Exception)
            {
                return new HttpResponseMessage();
            }

        }

        [Route("getProfil")]
        [Authorize(Roles = "AppUser")]
        public IHttpActionResult GetProfil()
        {
            if (User.Identity.IsAuthenticated)
            {
                RegistracijaModel registerUser = new RegistracijaModel();
                string s = User.Identity.GetUserId();
                var user = context.Users.Any(u => u.UserName == s);
                Korisnik p = unitOfWork.KorisnikRepository.Find(u => u.IDUser == s).FirstOrDefault();
                registerUser.SendBackBirthday = Convert.ToDateTime(p.DatumRodjenja).ToString("yyyy-MM-dd");
                registerUser.Adresa = p.Adresa;
                registerUser.Lastname = p.Prezime;
                registerUser.Name = p.Ime;
                registerUser.Username = User.Identity.Name;
                registerUser.ID = p.ID;

                registerUser.TipPutnika = p.TipKorisnika.ToString();

                return Ok(registerUser);
            }
            return Ok("Niste autentifikovani....");
        }

        [Route("izmeniProfil")]
        [Authorize(Roles = "AppUser")]
        public IHttpActionResult IzmeniProfil(RegistracijaModel userToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (userToUpdate != null)
            {
                try
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        var userStore = new UserStore<ApplicationUser>(context);
                        var userManager = new UserManager<ApplicationUser>(userStore);
                        string returnMessage = "";

                        string s = User.Identity.GetUserId();
                        var user = context.Users.Any(u => u.Id == s);
                        ApplicationUser apu = new ApplicationUser();
                        apu = userManager.FindByIdAsync(s).Result;

                        Korisnik k = unitOfWork.KorisnikRepository.Find(u => u.IDUser == s).FirstOrDefault();

                        if (!userManager.CheckPasswordAsync(apu, userToUpdate.OriginalPassword).Result)
                        {
                            returnMessage = "Uneli ste pogresnu lozinku....";
                            return Ok(returnMessage);
                        }

                        if (userToUpdate.Password != null && userToUpdate.Password == "" && !String.IsNullOrEmpty(userToUpdate.Password) && !String.IsNullOrWhiteSpace(userToUpdate.Password))
                        {
                            apu.PasswordHash = ApplicationUser.HashPassword(userToUpdate.Password);
                            userManager.Update(apu);
                        }

                        k.Adresa = userToUpdate.Adresa;
                        k.DatumRodjenja = userToUpdate.Birthday;
                        k.Prezime = userToUpdate.Lastname;
                        k.Ime = userToUpdate.Name;

                        if (String.Compare(userToUpdate.TipPutnika, "Regularni") == 0)
                            k.TipKorisnika = TipPutnika.Regularni;
                        else if (String.Compare(userToUpdate.TipPutnika, "Djak") == 0)
                            k.TipKorisnika = TipPutnika.Djak;
                        else
                            k.TipKorisnika = TipPutnika.Penzioner;

                        unitOfWork.KorisnikRepository.Update(k);
                        unitOfWork.Complete();
                        returnMessage = "Profil je uspesno azuriran....";

                        return Ok(returnMessage);
                    }
                    return Ok("Niste autentifikovani....");
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }
            else
            {
                ModelState.AddModelError("", "Not found!");
                return BadRequest(ModelState);
            }
        }

        [Route("getTipKorisnika")]
        [Authorize(Roles = "AppUser")]
        public IHttpActionResult GetTipKorisnika()
        {
            if (User.Identity.IsAuthenticated)
            {
                string s = User.Identity.GetUserId();
                Korisnik passenger = unitOfWork.KorisnikRepository.GetAll().Where(x => x.IDUser == s).FirstOrDefault();
                string message = "{\"TypeOfUser\" : \"" + passenger.TipKorisnika.ToString() + "\",";
                message += "\"IsValid\" : \"" + passenger.Stanje + "\"}";
                return Ok(message);
            }
            return Ok("Niste autentifikovani....");
        }

        [Route("getPutnike")]
        [Authorize(Roles = "Controller")]
        public IHttpActionResult GetPutnike()
        {

            List<KorisnikHelp> ret = new List<KorisnikHelp>();

            List<Korisnik> korisnici = unitOfWork.KorisnikRepository.GetAll().Where(x => x.Stanje == ProcesVerifikacije.Procesira).ToList();

            foreach (Korisnik k in korisnici)
            {
                ret.Add(new KorisnikHelp() { ID = k.ID, Adresa = k.Adresa, DatumRodjenja = k.DatumRodjenja.ToString("dd/MMMM/yyyy"), Email = k.Email, Ime = k.Ime, Prezime = k.Prezime, Stanje = k.Stanje.ToString(), TipKorisnika = k.TipKorisnika.ToString() });
            }

            return Ok(ret);
        }

        [HttpGet, Route("prihvatiKorisnika")]
        [Authorize(Roles = "Controller")]
        public IHttpActionResult PrihvatiKorisnika(int id)
        {
            try
            {
                Korisnik k = unitOfWork.KorisnikRepository.Find(u => u.ID == id).FirstOrDefault();
                k.Stanje = ProcesVerifikacije.Prihvacen;

                unitOfWork.KorisnikRepository.Update(k);
                unitOfWork.Complete();

                PosaljiMail(k.Email, ProcesVerifikacije.Prihvacen);

                return Ok($"Korisnik [email: {k.Email}] je PRIHVAĆEN....");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet, Route("odbijKorisnika")]
        [Authorize(Roles = "Controller")]
        public IHttpActionResult OdbijKorisnika(int id)
        {
            try
            {
                Korisnik k = unitOfWork.KorisnikRepository.Find(u => u.ID == id).FirstOrDefault();
                k.Stanje = ProcesVerifikacije.Odbijen;

                unitOfWork.KorisnikRepository.Update(k);
                unitOfWork.Complete();

                PosaljiMail(k.Email, ProcesVerifikacije.Odbijen);

                return Ok($"Korisnik [email: {k.Email}] je ODBIJEN....");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet, Route("getSlika")]
        public IHttpActionResult GetSlika(int id)
        {
            Korisnik k = unitOfWork.KorisnikRepository.Get(id);

            if (k.Slika == null)
            {
                return Ok("Nema slike");
            }  

            var filePath =  k.Slika;

            FileInfo fileInfo = new FileInfo(filePath);
            string type = fileInfo.Extension.Split('.')[1];
            byte[] data = new byte[fileInfo.Length];

            HttpResponseMessage response = new HttpResponseMessage();
            using (FileStream fs = fileInfo.OpenRead())
            {
                fs.Read(data, 0, data.Length);
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new ByteArrayContent(data);
                response.Content.Headers.ContentLength = data.Length;

            }

            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/png");

            return Ok(data);
        }

        [HttpPost, Route("dodajKontrolera")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult DodajKontrolera(RegistracijaModel userToRegister)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (userToRegister != null)
            {
                try
                {
                    var userStore = new UserStore<ApplicationUser>(context);
                    var userManager = new UserManager<ApplicationUser>(userStore);
                    string returnMessage = "";

                    if (context.Users.Any(u => u.UserName == userToRegister.Username))
                    {
                        returnMessage = "Nalog sa ovim email-om vec postoji....";
                        return Ok(returnMessage);
                    }

                    var user = new ApplicationUser() { Id = userToRegister.Username, UserName = userToRegister.Username, Email = userToRegister.Username, PasswordHash = ApplicationUser.HashPassword(userToRegister.Password) };
                    userManager.Create(user);
                    userManager.AddToRole(user.Id, "Controller");

                    returnMessage = "Uspesno ste registrovali kotrolera...";
                    return Ok(returnMessage);
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }
            else
            {
                ModelState.AddModelError("", "Not found!");
                return BadRequest(ModelState);
            }
        }

        private void PosaljiMail(string emailTo, ProcesVerifikacije stanje)
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("busns021@gmail.com", "Test123!");

            string body = "";
            string subject = "BusNs obaveštenje";
            if (stanje == ProcesVerifikacije.Prihvacen)
            {
                body = "Poštovani,\n\n Ovim putem Vas obaveštavamo da je Vaš status promenjen na PRIHVAĆEN.\n Sada možete da kupujete karte.\n\n Pozdrav,\nBus Ns";
            }
            else if (stanje == ProcesVerifikacije.Odbijen)
            {
                body = "Poštovani,\n\n Ovim putem Vas obaveštavamo da je Vaš status na žalost promenjen na ODBIJEN.\nZa više informacija kontaktirajte našu službu za podršku.\nHvala na razumevanju.\n\n Pozdrav,\nBus Ns";
            }

            MailMessage mm = new MailMessage("busns021@gmail.com", emailTo, subject, body);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);
        }
    }
}
