import {HTTP} from '@/plugin/http'
import * as types from './type'
import categories from '@/mockdata/categorydata'

const category = {
    namespaced:true,
    state: { 
        all:[],
        info:{}
     },
    getters: { 
      all: state =>state.all,
      info: state =>state.info,
    },
    mutations: { 
      [types.PULL_ALL](state,data){
        state.all=data
      },
      [types.PULL_INFO](state,data){
        state.info=data
      }
     },
    actions: {
      pullAll({commit}){
        return new Promise((resolve, reject) => {
          // commit(types.PULL_ALL,categories)
          // resolve(categories)

          //console.log('categories')

          HTTP.get('categories')
          .then(response =>{      
           // console.log('categories: '+ response.data)
           // console.log('categories: '+ categories)
            commit(types.PULL_ALL,response.data)
            resolve(response.data)
          })
        })
      },
      pullInfo({commit},id){
        return new Promise((resolve, reject) => {
          HTTP.get('category/'+id)
          .then(response =>{         
            commit(types.PULL_INFO,response.data)
            resolve()
          })
        })
          
      },
      update({commit},{id,data}){
        return new Promise((resolve,reject)=>{
          HTTP.put('category/'+id,data)
          .then(() =>{         
            resolve()
          })
        })
      },
     },
  }
export default category