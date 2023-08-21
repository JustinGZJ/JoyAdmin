import { axios } from '@/libs/api.request'
export const FilterProcessLineListList = (params) => {
  return axios.post('api/ProcessLineList/FilterList', params)
}

export function getProcessLineListList () {
  return axios.get('/api/ProcessLineList/GetList')
}

export function getProcessLineList (id) {
  return axios.get(`/api/ProcessLineList/GetOne/${id}`)
}

export function updateProcessLineList (data) {
  return axios.post('/api/ProcessLineList/Update', data)
}

export function deleteProcessLineList (id) {
  return axios.delete(`/api/ProcessLineList/Delete/${id}`)
}

export function addProcessLineList (data) {
  return axios.post('/api/ProcessLineList/Add', data)
}
export default {
  FilterProcessLineListList,
  getProcessLineListList,
  getProcessLineList,
  updateProcessLineList,
  deleteProcessLineList,
  addProcessLineList
}
