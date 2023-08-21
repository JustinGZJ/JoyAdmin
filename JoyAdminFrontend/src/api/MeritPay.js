import { axios } from '@/libs/api.request'

// 获取MeritPay列表
export function getMeritPayList () {
  return axios.get('/api/MeritPay/GetList')
}

// 获取单个MeritPay
export function getMeritPay (id) {
  return axios.get(`/api/MeritPay/GetOne/${id}`)
}

// 更新MeritPay
export function updateMeritPay (data) {
  return axios.post('/api/MeritPay/Update', data)
}

// 删除MeritPay
export function deleteMeritPay (id) {
  return axios.delete(`/api/MeritPay/Delete/${id}`)
}

// 新增MeritPay
export function addMeritPay (data) {
  return axios.post('/api/MeritPay/Add', data)
}
