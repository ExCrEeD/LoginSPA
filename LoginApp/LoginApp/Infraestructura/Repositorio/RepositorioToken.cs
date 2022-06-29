using LoginApp.DTOS;
using LoginApp.Infraestructura.Modelos;
using System.Linq;

namespace LoginApp.Infraestructura.Repositorio
{
    public interface IRepositorioToken
    {
        void AlmacenarToken(DTOLoginXoauth2 loginXoauth2);
        DTOLoginXoauth2 ConsultarToken(string email);
    }
    public class RepositorioToken : IRepositorioToken
    {
        private readonly ContextoLogin dbContext;
        public RepositorioToken(ContextoLogin dbContext)
        {
            this.dbContext = dbContext;
        }
        public void AlmacenarToken(DTOLoginXoauth2 loginXoauth2)
        {
            dbContext.IniciosDeSesion.Add(
                  new IniciosDeSesion
                  {
                      AccesToken = loginXoauth2.AccesToken,
                      Email = loginXoauth2.Email
                  }
             );
            dbContext.SaveChanges();
        }

        public DTOLoginXoauth2 ConsultarToken(string email)
        {
            return dbContext.IniciosDeSesion.Where(w => w.Email == email)
            .Select(s =>
                new DTOLoginXoauth2
                {
                    AccesToken = s.AccesToken,
                    Email = s.Email,
                    RefreshToken = null

                }).FirstOrDefault();
        }
    }
}
