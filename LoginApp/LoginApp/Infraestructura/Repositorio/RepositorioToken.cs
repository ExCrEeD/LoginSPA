using LoginApp.DTOS;
using LoginApp.Infraestructura.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoginApp.Infraestructura.Repositorio
{
    public interface IRepositorioToken
    {
        void AlmacenarToken(DTOLoginXoauth2 loginXoauth2);
        DTOLoginXoauth2 ConsultarToken(string email);
        IEnumerable<string> ObtenerCuentasDeCorreo();
        void ActualizarExepcionAutenticacionEmail(string email, string excepcion);
    }
    public class RepositorioToken : IRepositorioToken
    {
        private readonly ContextoLogin dbContext;
        public RepositorioToken(ContextoLogin dbContext)
        {
            this.dbContext = dbContext;
        }

        public void ActualizarExepcionAutenticacionEmail(string email, string excepcion)
        {
            var cuenta = dbContext.CuentasCorreo.Where(x => x.Email == email).FirstOrDefault();
            if (cuenta != null)
            {
                cuenta.ExcepcionAutenticacion = excepcion;
                dbContext.SaveChanges();
            }
        }

        public void AlmacenarToken(DTOLoginXoauth2 loginXoauth2)
        {
            var inicioDeSesion = dbContext.CuentasCorreo.FirstOrDefault(f=>f.Email == loginXoauth2.Email);

            if (inicioDeSesion is null)
                dbContext.CuentasCorreo.Add(DeDTOAPersistencia(loginXoauth2));
            else
                dbContext.Entry(inicioDeSesion).CurrentValues.SetValues(DeDTOAPersistencia(loginXoauth2));

            dbContext.SaveChanges();

            static CuentasCorreo DeDTOAPersistencia(DTOLoginXoauth2 login) 
             => new CuentasCorreo
                {
                    AccesToken = login.AccesToken,
                    Email = login.Email,
                    ExpiracionAccesToken = login.ExpiracionAccesToken,
                    RefreshToken = login.RefreshToken,
                    Scope = login.Scopes,
                    ExpiracionRefreshToken = DateTime.Now.AddDays(1),
                    ExcepcionAutenticacion = null
                };
        }

        public DTOLoginXoauth2 ConsultarToken(string email)
        {
            var cuenta = dbContext.CuentasCorreo.Where(w => w.Email == email).FirstOrDefault();
            if (cuenta != null)
                if (!string.IsNullOrEmpty(cuenta.ExcepcionAutenticacion))
                    throw new Exception(cuenta.ExcepcionAutenticacion);
            
            return cuenta is null? null : DePersistenciaADominio(cuenta);

            static DTOLoginXoauth2 DePersistenciaADominio(CuentasCorreo s) => new DTOLoginXoauth2
            {
                AccesToken = s.AccesToken,
                Email = s.Email,
                RefreshToken = s.RefreshToken,
                Scopes = s.Scope,
                ExpiracionAccesToken = s.ExpiracionAccesToken
            };

        }

        public IEnumerable<string> ObtenerCuentasDeCorreo() => dbContext.CuentasCorreo.Select(s => s.Email).ToList();
       


    }
}
