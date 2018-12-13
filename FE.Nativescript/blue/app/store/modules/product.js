import {HTTP} from '@/plugin/http'
import * as types from './type'
import products from '@/mockdata/productdata'

const PULL_HOTTREND="pullHotTrend"
const PULL_RELATED="pullRelated"
const LOAD_MORE="loadMore"
const PULL_SEARCH="pullSearch"
const CLEAR_SEARCH="clearSearch"

const product = {
    namespaced:true,
    state: { 
        all:[],
        categories:[],
        hottrends:[],
        related:[],
        search:[]
     },
    mutations: { 
      [types.PULL_ALL](state,data){
        state.all=data
      },
      [PULL_HOTTREND](state,data){
        state.hotTrends=data
      },
      [PULL_RELATED](state,data){
        state.related=data
      },
      [LOAD_MORE](state,data){
        state.all = state.all.concat(data)
      },
      [PULL_SEARCH](state,data){
        state.search = state.search.concat(data)
      },
      [CLEAR_SEARCH](state){
        state.search = []
      },
     },
    actions: {
      pullAll({commit}){
        return new Promise((resolve, reject) => {
            commit(types.PULL_ALL,products)

        //   HTTP.get('product')
        //   .then(response =>{         
        //     commit(types.PULL_ALL,response.data)
        //     resolve()
        //   })
        })
      },
      pullHotTrend({commit}){
        return new Promise((resolve, reject) => {
            commit(PULL_HOTTREND,products.filter(item=>item.id < 5))
        })
      },
      pullRelated({commit}, id){
        return new Promise((resolve, reject) => {
            //console.log(id)
            commit(PULL_RELATED,products.filter(item=>item.id != id))
        })
      },
      loadMore({commit}, id){
        return new Promise((resolve, reject) => {
            commit(LOAD_MORE,products.filter(item=>item.id < 5 ))
            resolve()
        })
      },
      search({commit}, text){
        commit(CLEAR_SEARCH) //Reset data

        return new Promise((resolve, reject) => {
            commit(PULL_SEARCH,products.filter(item=>item.name.indexOf(text) > -1))
            resolve()
        })
      },
      searchLoadMore({commit}, text){
        return new Promise((resolve, reject) => {
            commit(PULL_SEARCH,products.filter(item=>item.name.indexOf(text) > -1))
            resolve()
        })
      },
     },
    getters: { 
        all: state =>state.all,
        hottrends: state => state.hotTrends,
        related: state => state.related,
        search: state =>state.search
     }
  }
export default product