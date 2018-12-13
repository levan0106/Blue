import Vue from 'nativescript-vue'
import App from './components/HomePage'
import Filter from './plugin/filter'
import Store from './store/store'
import Router from './plugin/router'
//import {TNSFontIcon, fonticon} from 'nativescript-fonticon'
// import SlideContainer from 'nativescript-slides'
import VueDevtools from 'nativescript-vue-devtools'
Vue.use(VueDevtools)

// Prints Vue logs when --env.production is *NOT* set while building
Vue.config.silent = (TNS_ENV === 'production')

// TNSFontIcon.debug = true; //-- Optional. Will output the css mapping to console.
// TNSFontIcon.paths = {
//   'fa': './font-awesome.css'
// };
// TNSFontIcon.loadCss();
// Vue.filter('fonticon', fonticon);

//Filter.load();

// Vue.use(SlideContainer)

new Vue({
  Filter,
  Store,
  Router,
  render: h => h('frame', [h(App)])
}).$start()
