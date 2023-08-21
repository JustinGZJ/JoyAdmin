import { axios } from '@/libs/api.request'

export const FilterProcessListList = (params) => {
  return axios.post('api/ProcessList/FilterList', params)
}

// 获取ProcessList列表
export function getProcessListList () {
  return axios.get('/api/ProcessList/GetList')
}

// 获取单个ProcessList
export function getProcessList (id) {
  return axios.get(`/api/ProcessList/GetOne/${id}`)
}

// 更新ProcessList
export function updateProcessList (data) {
  return axios.post('/api/ProcessList/Update', data)
}

// 删除ProcessList
export function deleteProcessList (id) {
  return axios.delete(`/api/ProcessList/Delete/${id}`)
}

// 新增ProcessList
export function addProcessList (data) {
  return axios.post('/api/ProcessList/Add', data)
}
export default {
  FilterProcessListList,
  getProcessListList,
  getProcessList,
  updateProcessList,
  deleteProcessList,
  addProcessList
}
