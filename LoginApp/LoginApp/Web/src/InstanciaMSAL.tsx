import { PublicClientApplication } from "@azure/msal-browser";
import { msalConfig } from "./authConfig";

import {ObtenerConfiguracionAPP} from "services/TokenServices"

ObtenerConfiguracionAPP().then(x=>
  msalConfig.auth.clientId = x.clientID
);

export const msalInstance = new PublicClientApplication(msalConfig);

const accounts = msalInstance.getAllAccounts();

if (accounts.length > 0) {
  msalInstance.setActiveAccount(accounts[0]);
}
