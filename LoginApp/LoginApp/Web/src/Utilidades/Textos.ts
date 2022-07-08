export const validarRutaPeticion = (rutaCompletaDePrueba: string) => {
  if (
    validarQueryStringNecesarios(rutaCompletaDePrueba) &&
    valiadarOrdenQueryString(rutaCompletaDePrueba)
  ) {
    return true;
  } else {
    return false;
  }
};

const validarQueryStringNecesarios = (rutaCompletaDePrueba: string) => {
  if (!rutaCompletaDePrueba.includes("?token=")) {
    return false;
  }
  if (!rutaCompletaDePrueba.includes("&scopes=")) {
    return false;
  }
  return true;
};

const valiadarOrdenQueryString = (rutaCompletaDePrueba: string) => {
  if (
    rutaCompletaDePrueba.indexOf("?token=") >
    rutaCompletaDePrueba.indexOf("&scopes=")
  ) {
    return false;
  }
  return true;
};
