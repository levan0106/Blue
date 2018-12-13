//import Vue from 'vue'
import axios from 'axios'
//import Auth from '@/plugin/authenticate'
import store from '@/store/store'

export const HTTP = axios.create({
    //baseURL:'gateway.blue4rent.com:3000/api',
    baseURL:'http://192.168.0.106:3000/api',
    //baseURL:'http://210.211.119.25/gateway/api',
    headers:{
       // Authorization:'Bearer ' , //Auth.system.getJwtToken(),
        'Content-Type': 'application/json; charset=utf-8',        
    },
    dataType: 'json',
    async:true,
    crossDomain : true,
})
// Add a request interceptor
HTTP.interceptors.request.use(config => {
    // Do something before request is sent
   // console.log('Add a request interceptor: ' +store.getters.token)
    config.headers.authorization = 'Bearer ' + store.getters.token;
    return config;
  }, function (error) {
    // Do something with request error
    return Promise.reject(error);
  });

// Add a response interceptor
HTTP.interceptors.response.use((response) => { // intercept the global error
    return response
  }, function (error) {
    let originalRequest = error.config
    
    if (responseError(error.response) && !originalRequest._retry) { 
        originalRequest._retry = true
        return
    }
    
    if (error.response.status === 401 && !originalRequest._retry) { 
        
    console.log("expired: " +error.response.headers["Token-Expired"] )
        //if the error is 401 and header has tokenexpired
        if(error.response.headers["Token-Expired"] == true){
            // request refresh token
            doRefreshToken(function(response){
                HTTP.get(originalRequest.baseURL) //repeat the original request
            })
            return
        }else{
            // if the error is 401 and hasent already been retried
            originalRequest._retry = true
            //window.location.href = window.bwc.rootUrl + '/error'
            return
        }
    }
    // Do something with response error
    return Promise.reject(error)
  })

function responseError(response){
    console.log('Response Error status: ' + response.status)
    const isError = response === undefined || response.status === 404 || response.status === 403|| response.status === 500
    console.log('Response Error: ' + isError)
    return isError
}

function doRefreshToken(callback) {

    var jwtToken = Auth.system.getJwtToken();
    var refreshToken = Auth.system.getRefreshToken();
    HTTP.get('/account/RefreshToken?token=' + jwtToken + '&refreshToken=' + refreshToken)
    .then(response=>{
        if (typeof callback === "function") {
            callback(response)
        }
    })
    .catch(response=>{
        reject(response)
    })
}
//Vue.prototype.$http = HTTP;
export default HTTP