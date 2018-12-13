import {HTTP} from '@/plugin/http'
import * as types from './type'
import searchData from '@/mockdata/searchData'

const PULL_HISTORIES="pullHistories"
const PULL_SUGGESTIONS="pullSuggestions"
const SHOW_PANEL="showPanel"

const search = {
    namespaced:true,
    state: { 
        suggestions:[],
        histories:[],
        showPanel:false
     },
    mutations: { 
      [PULL_HISTORIES](state,data){
        state.suggestions=data
      },
      [PULL_SUGGESTIONS](state,data){
        state.histories=data
      },
      [SHOW_PANEL](state,data){
        state.showPanel=data
      },
     },
    actions: {
      pullHistories({commit}){
        return new Promise((resolve, reject) => {
            commit(PULL_HISTORIES,searchData.histories)
        })
      },
      pullSuggestions({commit}){
        return new Promise((resolve, reject) => {
          commit(PULL_SUGGESTIONS,searchData.suggestions)
        })
      },
      showPanel({commit},data){
          commit(SHOW_PANEL,data)
      },
    },
    getters: { 
      histories: state =>state.histories,
      suggestions: state => state.suggestions,
      showPanel: state => state.showPanel
    }
  }
export default search