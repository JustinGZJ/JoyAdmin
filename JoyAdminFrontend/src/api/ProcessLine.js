import { axios } from '@/libs/api.request'

export const FilterProcessLineList = (params) => {
  return axios.post('api/ProcessLine/FilterList', params)
}

// 获取Process列表
export function getProcessLineList () {
  return axios.get('/api/ProcessLine/GetList')
}

// 获取单个Process
export function getProcessLine (id) {
  return axios.get(`/api/ProcessLine/GetOne/${id}`)
}

// 更新Process
export function updateProcessLine (data) {
  return axios.post('/api/ProcessLine/Update', data)
}

// 删除Process
export function deleteProcessLine (id) {
  return axios.delete(`/api/ProcessLine/Delete/${id}`)
}

// 新增Process
export function addProcessLine (data) {
  return axios.post('/api/ProcessLine/Add', data)
}

export default {
  FilterProcessLineList,
  getProcessLineList,
  getProcessLine,
  updateProcessLine,
  deleteProcessLine,
  addProcessLine
}
