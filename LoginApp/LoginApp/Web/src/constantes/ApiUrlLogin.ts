const apiUrlBase =  window.location.origin.includes("http://localhost:22500")? "../api/":"../aplicacion/api/";
export const apiAuhtorizationHeader = "BA7P?j5}&X";

export const UrlApiLogin = {
    ObtenerConfiguracionAPP : apiUrlBase + "login/ObtenerConfiguracionAPP",
    ConsultarToken : apiUrlBase + "login/ConsultarToken",
    AlmacenarToken : apiUrlBase + "login/AlmacenarToken",
    RefreshAccesToken : apiUrlBase + "login/RefreshAccesToken"
}