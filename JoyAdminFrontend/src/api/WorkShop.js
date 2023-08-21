import { axios } from '@/libs/api.request'

// 获取WorkShop列表
export function getWorkShopList () {
  return axios.get('/api/WorkShop/GetList')
}

// 获取单个WorkShop
export function getWorkShop (id) {
  return axios.get(`/api/WorkShop/GetOne/${id}`)
}

// 更新WorkShop
export function updateWorkShop (data) {
  return axios.post('/api/WorkShop/Update', data)
}

// 删除WorkShop
export function deleteWorkShop (id) {
  return axios.delete(`/api/WorkShop/Delete/${id}`)
}

// 新增WorkShop
export function addWorkShop (data) {
  return axios.post('/api/WorkShop/Add', data)
}
