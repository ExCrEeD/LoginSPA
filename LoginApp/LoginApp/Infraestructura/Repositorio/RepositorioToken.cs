using LoginApp.DTOS;
using LoginApp.Infraestructura.Modelos;
using System;
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
            var inicioDeSesion = dbContext.IniciosDeSesion.FirstOrDefault(f=>f.Email == loginXoauth2.Email);

            if (inicioDeSesion is null)
                dbContext.IniciosDeSesion.Add(DeDTOAPersistencia(loginXoauth2));
            else
                dbContext.Entry(inicioDeSesion).CurrentValues.SetValues(DeDTOAPersistencia(loginXoauth2));

            dbContext.SaveChanges();

            static IniciosDeSesion DeDTOAPersistencia(DTOLoginXoauth2 login) 
             => new IniciosDeSesion
                {
                    AccesToken = login.AccesToken,
                    Email = login.Email,
                    ExpiracionAccesToken = login.ExpiracionAccesToken,
                    RefreshToken = login.RefreshToken,
                    Scope = login.Scopes,
                    ExpiracionRefreshToken = DateTime.Now.AddDays(1)
                };
        }

        public DTOLoginXoauth2 ConsultarToken(string email)
        {
            return dbContext.IniciosDeSesion.Where(w => w.Email == email)
            .Select(s =>
                new DTOLoginXoauth2
                {
                    AccesToken = s.AccesToken,
                    Email = s.Email,
                    RefreshToken = s.RefreshToken,
                    Scopes = s.Scope,
                    ExpiracionAccesToken = s.ExpiracionAccesToken
                }).FirstOrDefault();
        }
    }
}
