import { axios } from '@/libs/api.request'

export const errorReq = () => {
  return axios.get('error_url')
}
export const saveErrorLogger = info => {
  return axios.post('api/System/SaveErrorLogger', info)
}
