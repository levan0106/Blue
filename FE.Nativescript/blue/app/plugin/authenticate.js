import Vue from 'vue'
import * as ApplicationSettings from "application-settings"

let googleAuth = null
var JWT_TOKEN = "jwt_token"
var REFRESH_TOKEN="refresh_token"
var USER_ID = "user_id"
var USER_NAME = "User_Name"

const data={
    get:{},    
    load(){
        if(ApplicationSettings.getString("store")) {
            Object.assign(data.get, JSON.parse(ApplicationSettings.getString("store")))
        }
    }
};
const authentication = {
    system:{
        saveAuthenticationInfo (auth) {
            // localStorage.setItem(JWT_TOKEN, auth.token);
            // localStorage.setItem(REFRESH_TOKEN, auth.refreshToken);
            // localStorage.setItem(USER_ID, auth.userId);
            // localStorage.setItem(USER_NAME, auth.userName);
        },
        isAuthenticated () {
            // return localStorage.getItem(JWT_TOKEN) != '' && localStorage.getItem(JWT_TOKEN) != null;
        },
        currentUser () {
            // return localStorage.getItem(USER_ID);
            data.load();
            return data.get.currentUser;
        },
        currentUserProfile () {
            // return localStorage.getItem(USER_NAME);
        },
        getJwtToken () {
            data.load();
            console.log('getJwtToken: ' + data.get.token)
            return data.get.token;
        },
        getRefreshToken () {
            data.load();
            return data.get.refreshToken;
        },
    },
    google:{
        saveAuthenticationInfo (auth) {
            googleAuth = auth
        },
        isAuthenticated () {
            return googleAuth !== null && googleAuth.isSignedIn.get()
        },
        currentUser () {
            return googleAuth.currentUser.get()
        },
        currentUserProfile () {
            return this.currentUser().getBasicProfile()
        },
        getIdToken () {
            return this.currentUser().getAuthResponse().id_token
        }
    },
}
export default authentication 