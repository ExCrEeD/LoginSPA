export function verificarBloqueoPopUp() {
  var nuevaVentana = window.open();
  try {
    nuevaVentana.close();
    return false;
  } catch (e) {
    return true;
  }
}
