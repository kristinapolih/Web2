using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
        public IHttpActionResult RegisterUser(RegistracijaModel userToRegister)
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
    }
}
