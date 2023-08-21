import { axios } from '@/libs/api.request'

// 获取Notice列表
export function getNoticeList () {
  return axios.get('/api/Notice/GetList')
}

// 获取单个Notice
export function getNotice (id) {
  return axios.get(`/api/Notice/GetOne/${id}`)
}

// 更新Notice
export function updateNotice (data) {
  return axios.post('/api/Notice/Update', data)
}

// 删除Notice
export function deleteNotice (id) {
  return axios.delete(`/api/Notice/Delete/${id}`)
}

// 新增Notice
export function addNotice (data) {
  return axios.post('/api/Notice/Add', data)
}
