import { request } from "../core/HttpRequest";
import { ConfiguracionAPP } from "../modelos/ConfiguracionAPP";
import { UrlApiLogin } from "../constantes/ApiUrlLogin";
import { TokenAuth } from "modelos/TokenAuth";

export const ObtenerConfiguracionAPP = async (): Promise<ConfiguracionAPP> => {
  return request.get(UrlApiLogin.ObtenerConfiguracionAPP, undefined, false);
};

export const AlmacenarToken = async (tokenAuth: TokenAuth): Promise<string> => {
  return request.post(UrlApiLogin.AlmacenarToken, tokenAuth, undefined, false);
};
