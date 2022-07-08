
/**
 * Configuration object to be passed to MSAL instance on creation. 
 * For a full list of MSAL.js configuration parameters, visit:
 * https://github.com/AzureAD/microsoft-authentication-library-for-js/blob/dev/lib/msal-browser/docs/configuration.md 
 */
export const msalConfig = {
    auth: {
        clientId: "bfd32000-ad11-485b-a81f-dc7929e59b95", // This is the ONLY mandatory field that you need to supply.
        //authority: "https://login.microsoftonline.com/OrganizacionPruebaDominio.onmicrosoft.com", // Defaults to "https://login.microsoftonline.com/common"
        authority: "https://login.microsoftonline.com/common", // Defaults to "https://login.microsoftonline.com/common"
        redirectUri: "http://localhost:3001/", // Points to window.location.origin. You must register this URI on Azure Portal/App Registration.
        postLogoutRedirectUri: "http://localhost:3001/", // Indicates the page to navigate after logout.~
        navigateToLoginRequestUrl: true, // If "true", will navigate back to the original request location before processing the auth code response.
    },
    cache: {
        cacheLocation: "sessionStorage", // Configures cache location. "sessionStorage" is more secure, but "localStorage" gives you SSO between tabs.
        storeAuthStateInCookie: true, // Set this to "true" if you are having issues on IE11 or Edge
    },
    telemetry: {
        application: {
            appName: "Sinco ERP",
            appVersion: "1.0.0"
        }
    }
};


/**
 * Add here the endpoints and scopes when obtaining an access token for protected web APIs. For more information, see:
 * https://github.com/AzureAD/microsoft-authentication-library-for-js/blob/dev/lib/msal-browser/docs/resources-and-scopes.md
 */
export const protectedResources = {
    graphMe: {
        endpoint: "https://graph.microsoft.com/v1.0/me",
        scopes: ["User.Read"],
    },
    graphMessages: {
        endpoint: "https://graph.microsoft.com/v1.0/me/messages",
        scopes: ["Mail.Read"],
    },
    armTenants: {
        endpoint: "https://management.azure.com/tenants?api-version=2020-01-01",
        scopes: ["https://management.azure.com/user_impersonation"],
    }
}
