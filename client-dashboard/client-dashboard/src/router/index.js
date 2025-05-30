import Vue from 'vue'
import VueRouter from 'vue-router'
import ClientDashboard from '../views/ClientDashboard.vue'
import ClientViewer from '../views/ClientViewer.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Dashboard',
    component: ClientDashboard
  },
  {
    path: '/client/:id',
    name: 'ClientViewer',
    component: ClientViewer
  },
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
