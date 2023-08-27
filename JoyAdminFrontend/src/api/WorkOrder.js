import { axios } from '@/libs/api.request'
export const FilterWorkOrderList = (params) => {
  return axios.post('api/WorkOrder/FilterList', params)
}

// 获取WorkOrder列表
export function getWorkOrderList () {
  return axios.get('/api/WorkOrder/GetList')
}

// 获取单个WorkOrder
export function getWorkOrder (id) {
  return axios.get(`/api/WorkOrder/GetOne/${id}`)
}

// 更新WorkOrder
export function updateWorkOrder (data) {
  return axios.post('/api/WorkOrder/Update', data)
}

// 删除WorkOrder
export function deleteWorkOrder (id) {
  return axios.delete(`/api/WorkOrder/Delete/${id}`)
}

// 新增WorkOrder
export function addWorkOrder (data) {
  return axios.post('/api/WorkOrder/Add', data)
}
