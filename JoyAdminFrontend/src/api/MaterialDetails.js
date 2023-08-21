import { axios } from '@/libs/api.request'

export const FilterMaterialDetailList = (params) => {
  return axios.post('api/MaterialDetail/FilterList', params)
}

// 获取MaterialDetail列表
export function getMaterialDetailList () {
  return axios.get('/api/MaterialDetail/GetList')
}

// 获取单个MaterialDetail
export function getMaterialDetail (id) {
  return axios.get(`/api/MaterialDetail/GetOne/${id}`)
}

// 更新MaterialDetail
export function updateMaterialDetail (data) {
  return axios.post('/api/MaterialDetail/Update', data)
}

// 删除MaterialDetail
export function deleteMaterialDetail (id) {
  return axios.delete(`/api/MaterialDetail/Delete/${id}`)
}

// 新增MaterialDetail
export function addMaterialDetail (data) {
  return axios.post('/api/MaterialDetail/Add', data)
}

export default {
  FilterMaterialDetailList,
  getMaterialDetailList,
  getMaterialDetail,
  updateMaterialDetail,
  deleteMaterialDetail,
  addMaterialDetail
}
