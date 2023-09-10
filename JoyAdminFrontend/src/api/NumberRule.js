import { axios } from '@/libs/api.request'

export const GetNextSn = (formCode) => {
  return axios.get('api/NumberRule/GetNextSn/' + formCode)
}

export const FilterNumberRuleList = (params) => {
  return axios.post('api/NumberRule/FilterList', params)
}

// 获取NumberRule列表
export function getNumberRuleList () {
  return axios.get('/api/NumberRule/GetList')
}

// 获取单个NumberRule
export function getNumberRule (id) {
  return axios.get(`/api/NumberRule/GetOne/${id}`)
}

// 更新NumberRule
export function updateNumberRule (data) {
  return axios.post('/api/NumberRule/Update', data)
}

// 删除NumberRule
export function deleteNumberRule (id) {
  return axios.delete(`/api/NumberRule/Delete/${id}`)
}

// 新增NumberRule
export function addNumberRule (data) {
  return axios.post('/api/NumberRule/Add', data)
}

export default {
  FilterNumberRuleList,
  getNumberRuleList,
  getNumberRule,
  updateNumberRule,
  deleteNumberRule,
  addNumberRule
}
