import { PublicClientApplication } from "@azure/msal-browser";
import React from "react";
import {
  BrowserRouter,
  Switch,
  Route,
 
} from "react-router-dom";
import App from "./App";
import { msalConfig } from "./authConfig";
import PaginaNoEncontrada from "./Paginas/PaginaNoEncontrada";

export const msalInstance = new PublicClientApplication(msalConfig);

const accounts = msalInstance.getAllAccounts();

if (accounts.length > 0) {
  msalInstance.setActiveAccount(accounts[0]);
}
//Todo:Pendiente ver por que no redireciona
export default function CustomLinkExample() {
  return (
    <BrowserRouter>
      <Switch>
        <Route exact path="/" >
          <App instance={msalInstance} />
        </Route>
        <Route  path="/PaginaNoEncontrada" >   
          <PaginaNoEncontrada>PaginaNoEncontrada</PaginaNoEncontrada>      
        </Route>              
        <Route  path="*" component={PaginaNoEncontrada}/>      
        
      </Switch>
    </BrowserRouter>
  );
}
