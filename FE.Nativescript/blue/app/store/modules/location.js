import {HTTP} from '@/plugin/http'
import * as types from './type'
import locationdata from '@/mockdata/locationdata'

const LOCATION_CURRENT ="locationCurrent"
export default {
    namespaced:true,
    state: { 
        all:[],
        info:{},
        current:{}
     },
    mutations: { 
      [types.PULL_ALL](state,data){
        state.all=data
      },
      [types.PULL_INFO](state,data){
        state.info=data
      },
      [LOCATION_CURRENT](state,data){        
        var location =  state.all.filter(item=>item.Name == data)[0]
        state.current=location
      }
     },
    actions: {
      setLocation({commit}, data){
        commit(LOCATION_CURRENT,data)
      },
      pullCurrentLocation({commit},id){
          commit(LOCATION_CURRENT,'Hồ Chí Minh')
      },
      pullAll({commit}){
        return new Promise((resolve, reject) => {
          commit(types.PULL_ALL,locationdata)
          resolve(locations)
        })
      },
      pullInfo({commit},id){
        return new Promise((resolve, reject) => {
          HTTP.get('location/'+id)
          .then(response =>{         
            commit(types.PULL_INFO,response.data)
            resolve()
          })
        })
          
      },
      update({commit},{id,data}){
        return new Promise((resolve,reject)=>{
          HTTP.put('location/'+id,data)
          .then(() =>{         
            resolve()
          })
        })
      },
     },
    getters: { 
        all: state =>state.all,
        info: state =>state.info,
        current:state =>state.current
     }
  }