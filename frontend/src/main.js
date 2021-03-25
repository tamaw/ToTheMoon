import Vue from 'vue'
import App from './App.vue'
import VueRouter from 'vue-router'
import axios from 'axios'
import VueAxios from 'vue-axios'

Vue.config.productionTip = false

Vue.use(VueRouter)
Vue.use(VueAxios, axios)
Vue.axios.defaults.baseURL = process.env.VUE_APP_BASE_URL;

const router = new VueRouter({
  mode: 'history',
  routes: [
      { path: '/', name: 'Home', component: () => import('./views/Home') },
      { path: '/404', name: '404', component: require('./views/_404').default },
      { path: '*', redirect: '404'}
    ]
})

new Vue({
  router,
  render: function (h) { return h(App) },
}).$mount('#app')
