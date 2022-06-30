import axios, { AxiosRequestConfig, AxiosResponse } from "axios";
//import { NotificationManager } from "react-notifications";


class HttpRequest {
    constructor() {
        // if (environment.enModoDesarrollo === false) {
        //     let myWindow: any = window;
        //     if (myWindow.parent.getToken) {
        //         let token = myWindow.parent.getToken();
        //         axios.defaults.headers.common["Authorization"] = token;
        //     }
        // }
    }

    private IdDivScreenLoading: string = "DivScreenLoadingHttpRequest";

    private getLoaderHtml(): HTMLDivElement {
        const div = document.createElement("div");
        div.id = this.IdDivScreenLoading;
        div.style.position = "fixed";
        div.style.height = "100%";
        div.style.width = "100%";
        div.style.opacity = "0.5";
        div.style.background = "black";
        div.style.zIndex = "5000";
        div.style.top = "0";
        const divload = document.createElement("div");
        divload.style.height = "120px";
        divload.style.width = "120px";
        divload.style.marginTop = "-60px";
        divload.style.marginRight = "-60px";
        divload.style.zIndex = "5001";
        divload.style.border = "16px solid #c7c5c5";
        divload.style.borderTop = "16px solid #005cbe";
        divload.style.borderRadius = "50%";
        divload.style.animation = "spin 2s linear infinite";
        divload.style.textAlign = "center";
        divload.style.position = "absolute";
        divload.style.top = "50%";
        divload.style.right = "50%";
        div.appendChild(divload);
        return div;
    }
    public get(url: string, config?: AxiosRequestConfig, mostrarModalCarga: boolean = true) {
        let divLoader: any;
        if (mostrarModalCarga === true) {
            divLoader = this.blockScreenAndShowLoader();
        }

        const p = axios.get(url, config);
        return this.chainPromises(p, divLoader, mostrarModalCarga);
    }
    public delete(url: string, config?: AxiosRequestConfig, mostrarModalCarga: boolean = true) {
        let divLoader: any;
        if (mostrarModalCarga === true) {
            divLoader = this.blockScreenAndShowLoader();
        }

        const p = axios.delete(url, config);
        return this.chainPromises(p, divLoader, mostrarModalCarga);
    }
    public post(url: string, data?: any, config?: AxiosRequestConfig, mostrarModalCarga: boolean = true) {
        let divLoader: any;
        if (mostrarModalCarga === true) {
            divLoader = this.blockScreenAndShowLoader();
        }

        const p = axios.post(url, data, config);
        return this.chainPromises(p, divLoader, mostrarModalCarga);
    }

    private chainPromises(promise: Promise<AxiosResponse>, divLoader: any, mostrarModalCarga: boolean) {
        return promise
            .then((response) => {
                return response.data;
            })
            .catch((error) => {
                let mensaje = "Ha ocurrido un error.";
                if (!error.response) {
                    mensaje += " Error de conexión al servidor.";
                } else {
                    console.error(error.response);
                    if (error.response?.data.ExceptionMessage) {
                        mensaje = error.response?.data.ExceptionMessage;
                    }
                }
                //NotificationManager.error(mensaje, null, 10000);
                return Promise.reject(error.response?.data);
            })
            .finally(() => {
                if (mostrarModalCarga) {
                    this.unBlockScreenAndHideLoader(divLoader);
                }
            });
    }
    private blockScreenAndShowLoader(): any {
        const divLoader = this.getLoaderHtml();
        document.body.appendChild(divLoader);
        return divLoader;
    }
    private unBlockScreenAndHideLoader(divLoader: any) {
        document.body.removeChild(divLoader);
    }
}

export const request = new HttpRequest();
