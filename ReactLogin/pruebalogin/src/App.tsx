import React from "react";
import logo from "./logo.svg";
import "./App.css";
import { MsalProvider } from "@azure/msal-react";
import { loginRequest } from "./authConfig";
import { useState } from "react";
import {
  AuthenticationResult,
  PublicClientApplication,
} from "@azure/msal-browser";
import styled from "styled-components";
import { Link, useHistory } from "react-router-dom";
import { validarRutaPeticion } from "./Utilidades/Textos";

interface IApp {
  instance: PublicClientApplication;
}

export const App: React.FC<IApp> = ({ instance }) => {
  const [providerOffice, setProviderOffice] = useState<any>();
  const loginOffice365 = () => {
    instance.loginPopup(loginRequest).then((c) => setProviderOffice(c));
  };
  let history = useHistory();

  const obtenerToken = async () => {
    //  console.log(instance.acquireTokenSilent({
    //   account:providerOffice!.account,
    //   scopes:providerOffice.scopes
    // }) );
    console.log(providerOffice);
  };

  const validarURl = () => {
    const token: string =
      "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6IjJaUXBKM1VwYmpBWVhZR2FYRUpsOGxWMFRPSSJ9.eyJhdWQiOiJiZmQzMjAwMC1hZDExLTQ4NWItYTgxZi1kYzc5MjllNTliOTUiLCJpc3MiOiJodHRwczovL2xvZ2luLm1pY3Jvc29mdG9ubGluZS5jb20vYmNiNGJjNDEtZTg1NC00ZjEwLWI5MDYtZDE2NDNkNDVlMzQ5L3YyLjAiLCJpYXQiOjE2NTY1MjQwMDUsIm5iZiI6MTY1NjUyNDAwNSwiZXhwIjoxNjU2NTI3OTA1LCJhaW8iOiJBVFFBeS84VEFBQUFQVXM5SGE5Y2pZSUlKWUVBT3A2UVZZTlV6b2ppQUMxdkRVY2xDcHU0UGQxZVVJRDlzYk1uNWd3dXNxbmFDWFFCIiwiZW1haWwiOiJqZWlzb24uZmVybmFuZGV6QHNpbmNvLmNvbS5jbyIsIm5hbWUiOiJKZWlzb24gRmVybmFuZGV6Iiwibm9uY2UiOiJlZmMyMjZjMi1lZmI4LTRmNzktYjY5ZC00ZDg1NjM2MTk0ODciLCJvaWQiOiJiMGE5ZjBmZC03MTYxLTQxMTItYWJiNi0zNmU5M2Q1M2M0NWQiLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJqZWlzb24uZmVybmFuZGV6QHNpbmNvLmNvbS5jbyIsInJoIjoiMC5BVFFBUWJ5MHZGVG9FRS01QnRGa1BVWGpTUUFnMDc4UnJWdElxQl9jZVNubG01VTBBS3cuIiwic3ViIjoiMG9MZ1FoLVBXc1VnUi1DcXJnZzJSczNDelNNb3B5TEtDM3RuWnJvSTQ3ZyIsInRpZCI6ImJjYjRiYzQxLWU4NTQtNGYxMC1iOTA2LWQxNjQzZDQ1ZTM0OSIsInV0aSI6IlN0QUlSMDY3ZDAyMUhLVGVSSkNhQVEiLCJ2ZXIiOiIyLjAifQ.ZH8Vdlai6jPW7Vz148AyQYIZaP2YIAeTaFOCA4ujTJvlUXEeA7Tb5I3FMdv102tUdWDpLPENiQyzFIzgERJ2KBy0cj3wHAK9T4o3SZmq-5CBhvQTbXPw80t0FaWZUJaBSFCXiNxXzEM-4gMPvFjUAsbqy6KZ3i1NglL2yicKLVmAw3t2Dbl600SR9CqIqiUT0DzWSFD_7Zb5w_JURQ_vMbR4lnsk5SMiVj5KHaPoxRUc5cv4BN37Fvh-x3_4g5xo6J5IrXUcHPeVM3DH33fWQaARbFlRYSgr16CNXNbRbjqtDnXhEmdYM25nmhEgglAA-b11HUeAztUFjC8o2iG2Ow";
    const scopes: string[] = [
      "https://outlook.office.com/IMAP.AccessAsUser.All",
      "offline_access",
      "email",
      "openid",
    ];
    const rutaDePrueba: string = "https://www.sinco.com.co/";

    const rutaCompletaDePrueba: string =
      rutaDePrueba + "?token=" + token + "&scopes=" + scopes;

    validarRutaPeticion(rutaCompletaDePrueba)
      ? console.log("se genera solcitud")
      : history.push("/PaginaNoEncontrada");
  };

  return (
    <MsalProvider instance={instance}>
      <div className='App'>
        <header className='App-header'>
          <img src={logo} className='App-logo' alt='logo' />
          <p>
            Edit <code>src/App.js</code> and save to reload.
          </p>
          <a
            className='App-link'
            href='https://reactjs.org'
            target='_blank'
            rel='noopener noreferrer'>
            Learn React
          </a>
          <button onClick={() => loginOffice365()}>Office 365 Login</button>
          <button onClick={() => obtenerToken()}>ObtenerToken</button>
          <button onClick={() => validarURl()}>validarURl</button>

          <Link to='/PaginaNoEncontrada'>PaginaNoEncontrada</Link>
          <Link to='/sss'>sss</Link>
        </header>
      </div>
    </MsalProvider>
  );
};

export default App;
