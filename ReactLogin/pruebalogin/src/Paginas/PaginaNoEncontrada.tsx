import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import styled from "styled-components";

const SContenedorPaginaNoEncontrada = styled.div`
  user-select: none;
`;
const SFondo = styled.div`
  text-align: center;
  font-family: Cairo, sans-serif;
  color: #f3f6f9;
  content: attr(data-error);
  font-size: 40vw;
  font-weight: 700;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  width: 100%;
  z-index: -1;
`;
const SMensaje = styled.h1`
  font-weight: 700;
  text-decoration: none;
  font-family: Montserrat, sans-serif;
  font-style: normal;
  color: #00335e;
  font-size: 50px;
  position: absolute;
  top: 40%;
  left: 50%;
  transform: translate(-50%, -50%);
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
  display: inline-flex;
  align-items: center;
  justify-content: center;
  position: absolute;
  top: 60%;
  left: 50%;
  transform: translate(-50%, -50%);
  &:hover {
    background-color: #0a5890;
  }
`;

const PaginaNoEncontrada = () => {
  return (
    <SContenedorPaginaNoEncontrada>
      <SFondo>
        404
        <SMensaje>Página no encontrada.</SMensaje>
        <SLink to='/'>Ir al Inicio</SLink>
      </SFondo>
    </SContenedorPaginaNoEncontrada>
  );
};

export default PaginaNoEncontrada;
