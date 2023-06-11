import {
  Login,
  GetUserInfo
} from '@/api/user'
import { setToken, getToken } from '@/libs/util'
import { setAuthorization } from '@/libs/api.request'
export default {
  state: {
    userName: '',
    userId: '',
    avatarImgPath: '',
    token: getToken(),
    access: '',
    hasGetInfo: false,

  },
  mutations: {
    setAvatar(state, avatarPath) {
      state.avatarImgPath = avatarPath
    },
    setUserId(state, id) {
      state.userId = id
    },
    setUserName(state, name) {
      state.userName = name
    },
    setAccess(state, access) {
      state.access = access
    },
    setToken(state, token) {
      state.token = token
      setToken(token)
    },
    setHasGetInfo(state, status) {
      state.hasGetInfo = status
    },

  },

  actions: {
    // 登录
    handleLogin({ commit }, { userName, password }) {
      return new Promise((resolve, reject) => {
        var reqData = {
          Account: userName,
          Password: password
        }
        Login(reqData).then(res => {
          const data = res.data
          if (data.StatusCode == 200) { 
            commit('setToken', data.Data.Token)
            setAuthorization()
          }
          resolve(data)
        }).catch(err => {
          reject(err)
        })
      })
    },
    // 退出登录
    handleLogOut({ state, commit }) {
      return new Promise((resolve, reject) => {
        // logout(state.token).then(() => {
        //   commit('setToken', '')
        //   commit('setAccess', [])
        //   resolve()
        // }).catch(err => {
        //   reject(err)
        // })
        // 如果你的退出登录无需请求接口，则可以直接使用下面三行代码而无需使用logout调用接口
        commit('setToken', '')
        commit('setAccess', [])
        resolve()
      })
    },
    // 获取用户相关信息
    GetUserInfo({ commit }) {
      return new Promise((resolve, reject) => {
        try {
          GetUserInfo().then(res => {
            const data = res.data
            if (data.StatusCode == 200) {
              commit('setAvatar', '')
              commit('setUserName', data.Data.Name)
              commit('setUserId', data.Data.Id)
              commit('setAccess', data.Data.Access)
              commit('setHasGetInfo', true)
            }
            resolve(data)
          }).catch(err => {
            reject(err)
          })
        } catch (error) {
          reject(error)
        }
      })
    },

  }
}
