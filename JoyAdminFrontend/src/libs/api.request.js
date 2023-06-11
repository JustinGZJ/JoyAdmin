// import HttpRequest from '@/libs/axios'
// import config from '@/config'
// const baseUrl = process.env.NODE_ENV === 'development' ? config.baseUrl.dev : config.baseUrl.pro

// const axios = new HttpRequest(baseUrl)
// export default axios

import Axios from 'axios'
import config from '@/config'
import { getToken } from '@/libs/util'
import { Notice } from 'view-design'
const baseUrl = process.env.NODE_ENV === 'development' ? config.baseUrl.dev : config.baseUrl.pro

export const setAuthorization = () => {
  axios.defaults.headers.Authorization = getToken() ? ('Bearer ' + getToken()) : ''
}

export const axios = Axios.create({
  baseURL: baseUrl,
  timeout: 120000,
  headers: {
    Authorization: 'Bearer ' + getToken()
  }
})

axios.interceptors.response.use((res) => {
  if (res.status != 200) {
    return Promise.reject(res)
  }
  return res
}, (error) => {
  if (error.response) {
    if (error.response.status == '401') {
      Notice.error({
        title: '登录信息失效，请重新登录。'
      })
    } else {
      Notice.error({
        title: '服务器错误：' + error.response.status,
        desc: error,
        duration: 5
      })
    }
  } else {
    Notice.error({
      title: error.message,
      duration: 5
    })
  }
  return Promise.reject(error)
})
