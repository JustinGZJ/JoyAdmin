import { axios } from '@/libs/api.request'

export const FilterSysUnitList = (params) => {
  return axios.post('api/SysUnit/FilterList', params)
}

// 获取SysUnit列表
export function getSysUnitList () {
  return axios.get('/api/SysUnit/GetList')
}

// 获取单个SysUnit
export function getSysUnit (id) {
  return axios.get(`/api/SysUnit/GetOne/${id}`)
}

// 更新SysUnit
export function updateSysUnit (data) {
  return axios.post('/api/SysUnit/Update', data)
}

// 删除SysUnit
export function deleteSysUnit (id) {
  return axios.delete(`/api/SysUnit/Delete/${id}`)
}

// 新增SysUnit
export function addSysUnit (data) {
  return axios.post('/api/SysUnit/Add', data)
}
