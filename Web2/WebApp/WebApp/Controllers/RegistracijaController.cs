using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
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
            unitOfWork.KorisnikRepository.Add(passenger);
            unitOfWork.Complete();
            returnMessage = "Uspesno registrovani...";
            return Ok(returnMessage);
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
                registerUser.UserType = p.TipKorisnika;

                return Ok(registerUser);
            }
            return Ok();
        }


        [Route("izmeniProfil")]
        [Authorize(Roles = "AppUser")]
        public IHttpActionResult IzmeniProfil(RegistracijaModel userToUpdate)
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

                if (userToUpdate.Password != null)
                {
                    apu.PasswordHash = ApplicationUser.HashPassword(userToUpdate.Password);
                    userManager.Update(apu);
                }

                k.Adresa = userToUpdate.Adresa;
                k.DatumRodjenja = userToUpdate.Birthday;
                k.Prezime = userToUpdate.Lastname;
                k.Ime = userToUpdate.Name;
                k.TipKorisnika = userToUpdate.UserType;
                unitOfWork.KorisnikRepository.Update(k);
                unitOfWork.Complete();
                returnMessage = "Profil je uspesno azuriran....";

                return Ok(returnMessage);
            }
            return Ok();
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
            return Ok();
        }
    }
}
