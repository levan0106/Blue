import {HTTP} from '@/plugin/http'
import * as types from './type'

const login = {
    namespaced:true,
    state: { 
    //   token:'',
    //   refreshToken:'',
    //   status:false
     },
    mutations: { 
    //   [types.DO_LOGIN](state,{status,token,refreshToken}){
    //         state.status = status
    //         state.token = token
    //         state.refreshToken=refreshToken
    //   },
      
     },
    actions: {
      doLogin({commit},{user,password}){
        return new Promise((resolve, reject) => {
            
            HTTP.get('auth?name='+user+'&pwd='+password+'&client=88')
            //HTTP.get('authorization/'+user+'/'+password)
            .then(response=>{
                var result = JSON.stringify(response);
                var status = response.status;           

                
                // console.log("response: "+result)

                var token = response.data.access_token;
                var refreshToken = response.data.refresh_token;
                
                // console.log('status:'+ status)
                // console.log('token:'+ token)

                // if(status === 'OK' && token != '')
                // {
                //     commit(types.DO_LOGIN,{
                //         status:true,
                //         token:token,
                //         refreshToken:refreshToken
                //     })
                // }else{
                //     commit(types.DO_LOGIN,{status:false,token:'',refreshToken:''})
                // }
            
                resolve({
                    status:status,
                    token:token,
                    refreshToken:refreshToken
                })
            })
            .catch(e=>{
                reject(e)
            })
        })
      },
     },
    getters: { 
    //   token: state => state.token,      
    //   refreshToken: state => state.refreshToken,
    //   status: state => state.status,
     }
  }
export default login