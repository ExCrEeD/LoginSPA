import React from "react";
import { Link } from "react-router-dom";
import styled from "styled-components";
import IconoErrorPopUp from "../Multimedia/IconoUsuarioLoginV1.png";
import LogoRe from "../Multimedia/LogoRe.png";
const ContenedorAutenticacionEnCurso = styled.div`
  font-family: "Ubuntu", sans-serif;
  margin-top: 0%;
  color: #163149;
`;
const SIconoAutenticacionEnCurso = styled.img`
  margin-top: 3%;
  height: 11%;
  width: 11%;
`;

const SLogoRe = styled.img`
  margin-top: 3%;
  height: 8%;
  width: 8%;
`;

const SMensaje = styled.h1`
  font-size: 45px;
`;
const SSubMensaje = styled.h2`
  font-size: 35px;
`;

const SLink = styled(Link)`
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
  transform: translateX(-50%);
  &:hover {
    background-color: #0a5890;
  }
`;

const AutenticacionEnCurso = () => {
  return (
    <ContenedorAutenticacionEnCurso>
      <SIconoAutenticacionEnCurso
        src={IconoErrorPopUp}></SIconoAutenticacionEnCurso>
      <SMensaje>
        Continúe el proceso de autenticación en la ventana emergente.
      </SMensaje>
      <SSubMensaje></SSubMensaje>
      <SLogoRe src={LogoRe}></SLogoRe>
    </ContenedorAutenticacionEnCurso>
  );
};

export default AutenticacionEnCurso;
