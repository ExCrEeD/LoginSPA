import React, { FC } from "react";
import { Link } from "react-router-dom";
import styled from "styled-components";
import IconoErrorPopUp from "../Multimedia/IconoErrorPopUp.png";
const ContenedorBloqueoPopUp = styled.div`
  font-family: "Ubuntu", sans-serif;
  margin-top: 0%;
  color: #163149;
`;
const SIconoErrorPopUp = styled.img`
  margin-top: 3%;
  height: 10%;
  width: 10%;
`;

const SMensajeError = styled.h1`
  font-size: 45px;
`;
const SSubMensajeError = styled.h2`
  font-size: 35px;
`;

const SBoton = styled.button`
  border: 1px solid #3280b8;
  padding: 8px 28px;
  font-family: Cairo, sans-serif;
  font-style: normal;
  font-weight: normal;
  text-decoration: none;
  font-size: 18px;
  background-color: rgba(50, 128, 184, 1);
  border-radius: 50px;
  color: #fff;
  margin-top: 8%;
  &:hover {
    background-color: #0a5890;
  }
`;

interface IBloqueoPopUp {
  redirect: string;
}
const BloqueoPopUp: FC<IBloqueoPopUp> = ({ redirect }) => {
  const redirecionar = () => {
    window.location.replace(redirect.replace("hashtag", "#"));
  };
  return (
    <ContenedorBloqueoPopUp>
      <SIconoErrorPopUp src={IconoErrorPopUp}></SIconoErrorPopUp>
      <SMensajeError>
        Las ventanas emergentes se encuentran bloqueadas
      </SMensajeError>
      <SSubMensajeError>
        Para continuar habilite este permiso en el navegador
        <br />
      </SSubMensajeError>
      <SBoton onClick={redirecionar}>Continuar</SBoton>
    </ContenedorBloqueoPopUp>
  );
};

export default BloqueoPopUp;
