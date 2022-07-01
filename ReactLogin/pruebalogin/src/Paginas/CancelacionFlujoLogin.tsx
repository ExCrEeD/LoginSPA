import React, { FC } from "react";
import { Link } from "react-router-dom";
import styled from "styled-components";
import IconoError from "../Multimedia/IconoErrorV2.png";
import LogoRe from "../Multimedia/LogoRe.png";
const ContenedorAutenticacionEnCurso = styled.div`
  font-family: "Ubuntu", sans-serif;
  margin-top: 0%;
  color: #163149;
`;
const SIconoAutenticacionEnCurso = styled.img`
  margin-top: 4%;
  height: 11%;
  width: 11%;
`;

const SMensaje = styled.h1`
  font-size: 40px;
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
  margin-top: 1%;
  &:hover {
    background-color: #0a5890;
  }
`;
interface IBloqueoPopUp {
  redirect: string;
}

const CancelacionFlujoLogin: FC<IBloqueoPopUp> = ({ redirect }) => {
  const redirecionar = () => {
    window.location.replace(redirect.replace("hashtag", "#"));
  };
  return (
    <ContenedorAutenticacionEnCurso>
      <SIconoAutenticacionEnCurso src={IconoError}></SIconoAutenticacionEnCurso>
      <SMensaje>El proceso ha sido cancelado por el usuario</SMensaje>
      <SBoton onClick={redirecionar}>Regresar</SBoton>
    </ContenedorAutenticacionEnCurso>
  );
};

export default CancelacionFlujoLogin;
