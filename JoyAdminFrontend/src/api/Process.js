import { axios } from '@/libs/api.request'

// 获取Process列表
export function getProcessList () {
  return axios.get('/api/Process/GetList')
}

// 获取单个Process
export function getProcess (id) {
  return axios.get(`/api/Process/GetOne/${id}`)
}

// 更新Process
export function updateProcess (data) {
  return axios.post('/api/Process/Update', data)
}

// 删除Process
export function deleteProcess (id) {
  return axios.delete(`/api/Process/Delete/${id}`)
}

// 新增Process
export function addProcess (data) {
  return axios.post('/api/Process/Add', data)
}
