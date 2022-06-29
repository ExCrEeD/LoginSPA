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
import { Link } from "react-router-dom";

interface IApp {
  instance: PublicClientApplication;
}

export const App: React.FC<IApp> = ({ instance }) => {
  const [providerOffice, setProviderOffice] = useState<any>();
  const loginOffice365 = () => {
    instance.loginPopup(loginRequest).then((c) => setProviderOffice(c));
  };

  const obtenerToken = async () => {
    //  console.log(instance.acquireTokenSilent({
    //   account:providerOffice!.account,
    //   scopes:providerOffice.scopes
    // }) );
    console.log(providerOffice);
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
          <Link to='/PaginaNoEncontrada'>PaginaNoEncontrada</Link>
          <Link to='/sss'>sss</Link>
        </header>
      </div>
    </MsalProvider>
  );
};

export default App;
