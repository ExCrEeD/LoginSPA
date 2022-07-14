import React, { useEffect,useRef } from "react";
import logo from "./logo.svg";
import "./App.css";
import { MsalProvider } from "@azure/msal-react";
import { useState } from "react";
import {
  AuthenticationResult,
  IPublicClientApplication,
  PublicClientApplication,
} from "@azure/msal-browser";
import styled from "styled-components";
import { Link, useHistory,useLocation,useParams } from "react-router-dom";
import { validarRutaPeticion } from "./Utilidades/Textos";
import queryString from "query-string";
import {
  useMsal,
  AuthenticatedTemplate,
  UnauthenticatedTemplate,
} from "@azure/msal-react";
import { TokenAuth } from "modelos/TokenAuth";
import { AlmacenarToken } from "services/TokenServices";
import moment from "moment";
import { copyFile } from "fs";
import { verificarBloqueoPopUp } from "Utilidades/Navegador";
import BloqueoPopUp from "Paginas/BloqueoPopUp";
import AutenticacionEnCurso from "Paginas/AutenticacionEnCurso";
import CancelacionFlujoLogin from "Paginas/CancelacionFlujoLogin";
import {ObtenerConfiguracionAPP} from "services/TokenServices"
import { msalConfig } from "./authConfig";
import { NotificationManager } from "react-notifications";

const extractQueryStringParams = (query: string,locationHash : string) => {
  const ifArraySelectFirst = (obj: any) => {
    if (Array.isArray(obj)) {
      return obj[0];
    }
    return obj;
  };
  let { scope, redirect }: any = queryString.parse(query);
  scope = ifArraySelectFirst(scope);
  redirect = ifArraySelectFirst(redirect)+locationHash;
  return { scope,redirect };
};


export const App = () => {
  const location = useLocation();
  const { scope,redirect }: any = extractQueryStringParams(location.search,location.hash);
  const [bloqueoPopUp, setbloqueoPopUp] = useState<boolean>(false);
  const [popUpActivo, setpopUpActivo] = useState(true);
  const [msalInstance, setMsalInstance]  = useState<IPublicClientApplication>();
  


  
  useEffect(() => {
    ObtenerConfiguracionAPP().then(x=>{
      msalConfig.auth.clientId = x.clientID
      let instance =new PublicClientApplication(msalConfig);
      const accounts = instance.getAllAccounts();    
      if (accounts.length > 0) {
        instance.setActiveAccount(accounts[0]);
      }
      setbloqueoPopUp(verificarBloqueoPopUp());
      if (scope !== undefined) {
        loginOffice365(instance);
      }
      setMsalInstance(instance);
    });
  }, []);

  //const [providerOffice, setProviderOffice] = useState<any>();

  const loginOffice365 = (instance: IPublicClientApplication) => {
    const scopeParams = {
      scopes: scope.split(","),
    };
    //instance.loginRedirect(scopeParams).then(x=>console.log(instance));
    instance
      .loginPopup(scopeParams)
      .then((x) => {
        const authToken = getTokenFromStorage();
        AlmacenarToken(authToken);
        instance.logoutPopup().then(() => redirectOrigin(x.account!.username));
      })
      .catch((x) => {
        console.log(x);
        setpopUpActivo(false)});
  };

  function WelcomeUser() {
    const { accounts } = useMsal();
    const username = accounts[0].username;
    // const authToken = getTokenFromStorage();
    // AlmacenarToken(authToken);

    return <p>Welcome, {username}</p>;
  }

const redirectOrigin =(username:string) => {
  window.location.href = redirect+`?email=${username}`;
}

  const getTokenFromStorage = () => {
    const authToken: TokenAuth = {
      AccesToken: "",
      Email: "",
      ExpiracionAccesToken: "",
      RefreshToken: "",
      Scopes: "",
    };

    let keys = Object.keys(sessionStorage),
      i = keys.length;
    while (i--) {
      let store = sessionStorage.getItem(keys[i]);
      var isValidJSON = true;
      try {
        JSON.parse(store!);
      } catch {
        isValidJSON = false;
      }
      if (isValidJSON) {
        let storeObject = JSON.parse(store!);

        if (storeObject.credentialType == "RefreshToken")
          authToken.RefreshToken = storeObject.secret;

        if (storeObject.credentialType == "AccessToken") {
          console.log(storeObject.expiresOn);
          authToken.ExpiracionAccesToken = moment(
            storeObject.expiresOn * 1000
          ).format();
          authToken.AccesToken = storeObject.secret;
          authToken.Scopes = storeObject.target;
        }

        if (storeObject.authorityType == "MSSTS")
          authToken.Email = storeObject.username;
      }
    }
    return authToken;
  };

  return (
    
      msalInstance &&( <MsalProvider instance={msalInstance}>
        <div className='App'>
          {NotificationManager.error("xd", null, 10000)}
          {bloqueoPopUp ? (
            <BloqueoPopUp redirect={redirect} />
          ) : popUpActivo ? (
            <AutenticacionEnCurso />
          ) : (
            <CancelacionFlujoLogin redirect={redirect} />
          )}
        </div>
      </MsalProvider >)
  );
};

export default App;
