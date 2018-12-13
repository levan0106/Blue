import Vue from 'vue'
import Vuex from 'vuex'
import * as ApplicationSettings from "application-settings";
import login from '@/store/modules/login'
import category from '@/store/modules/category'
import product from '@/store/modules/product'
import location from '@/store/modules/location'
import search from '@/store/modules/search'


Vue.use(Vuex)

const store =  new Vuex.Store({
  
  modules:{
    login,
    category,
    product,
    location,
    search
  },
  state :{
    isAuthenticated:true,
    currentUser:{},
    token:'',
    refreshToken:''
  },
  getters:{
    isAuthenticated:state =>state.isAuthenticated,
    currentUser:state=>state.currentUser,
    token:state=>state.token
  },
  mutations :{
    load(state) {
      if(ApplicationSettings.getString("store")) {
          this.replaceState(
              Object.assign(state, JSON.parse(ApplicationSettings.getString("store")))
          );
      }
    },
    save(state, data) {
        state.isAuthenticated = data.isAuthenticated;
        state.currentUser = data.currentUser;
        state.token = data.token;
        state.refreshToken = data.refreshToken;

        ApplicationSettings.setString("store", JSON.stringify(state));
    },
    setCurrentUser(state,user){
      state.currentUser = user
    },
    setAuthenticate(state,auth){
      state.isAuthenticated = auth
    }
  },
  actions:{
    setCurrentUser({commit},user){
      commit('setCurrentUser',user)
    },
    setAuthenticate({commit},auth){
      commit('setAuthenticate',auth)
    }
  },
})
Vue.prototype.$store = store;
export default store