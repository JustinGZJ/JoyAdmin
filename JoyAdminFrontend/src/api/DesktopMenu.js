import { axios } from '@/libs/api.request'

// 获取DesktopMenu列表
export function getDesktopMenuList () {
  return axios.get('/api/DesktopMenu/GetList')
}

// 获取单个DesktopMenu
export function getDesktopMenu (id) {
  return axios.get(`/api/DesktopMenu/GetOne/${id}`)
}

// 更新DesktopMenu
export function updateDesktopMenu (data) {
  return axios.post('/api/DesktopMenu/Update', data)
}

// 删除DesktopMenu
export function deleteDesktopMenu (id) {
  return axios.delete(`/api/DesktopMenu/Delete/${id}`)
}

// 新增DesktopMenu
export function addDesktopMenu (data) {
  return axios.post('/api/DesktopMenu/Add', data)
}
