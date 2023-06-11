import { axios } from '@/libs/api.request'

export const Login = formData => {
  return axios.post('api/RBAC/Login', formData)
}

export const GetUserInfo = () => {
  return axios.get('api/RBAC/GetUserInfo')
}
