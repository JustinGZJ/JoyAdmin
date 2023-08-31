import { axios } from '@/libs/api.request'

export const FilterDefectItemList = (params) => {
  return axios.post('api/DefectItem/FilterList', params)
}

// 获取DefectItem列表
export function getDefectItemList () {
  return axios.get('/api/DefectItem/GetList')
}

// 获取单个DefectItem
export function getDefectItem (id) {
  return axios.get(`/api/DefectItem/GetOne/${id}`)
}

// 更新DefectItem
export function updateDefectItem (data) {
  return axios.post('/api/DefectItem/Update', data)
}

// 删除DefectItem
export function deleteDefectItem (id) {
  return axios.delete(`/api/DefectItem/Delete/${id}`)
}

// 新增DefectItem
export function addDefectItem (data) {
  return axios.post('/api/DefectItem/Add', data)
}
